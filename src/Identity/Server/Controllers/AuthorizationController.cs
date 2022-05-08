// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Security.Claims;
using Medulla.Identity.Server.Extensions;
using Medulla.Identity.Server.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Medulla.Identity.Server.Controllers;

/// <summary>
/// A controller that handles authorization.
/// </summary>
public class AuthorizationController : Controller
{
    /// <summary>
    /// The user manager.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// The application manager.
    /// </summary>
    private readonly IOpenIddictApplicationManager _applicationManager;

    /// <summary>
    /// The authorization manager.
    /// </summary>
    private readonly IOpenIddictAuthorizationManager _authorizationManager;

    /// <summary>
    /// The sign in manager.
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary>
    /// The scope manager.
    /// </summary>
    private readonly IOpenIddictScopeManager _scopeManager;

    /// <summary>
    /// Creates a <see cref="AuthorizationController"/> instance.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="applicationManager">The application manager.</param>
    /// <param name="authorizationManager">The authorization manager.</param>
    /// <param name="signInManager">The sign in manager.</param>
    /// <param name="scopeManager">The scope manager.</param>
    public AuthorizationController(
        UserManager<ApplicationUser> userManager,
        IOpenIddictApplicationManager applicationManager,
        IOpenIddictAuthorizationManager authorizationManager,
        SignInManager<ApplicationUser> signInManager,
        IOpenIddictScopeManager scopeManager)
    {
        _userManager = userManager;
        _applicationManager = applicationManager;
        _authorizationManager = authorizationManager;
        _signInManager = signInManager;
        _scopeManager = scopeManager;
    }

    /// <summary>
    /// The authorize endpoint.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> representing the login of the user.</returns>
    /// <exception cref="InvalidOperationException">Occurs when the request cant be processed.</exception>
    [HttpGet("~/identity/authorize")]
    [HttpPost("~/identity/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
            throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (request.HasPrompt(Prompts.Login))
        {
            var prompt = string.Join(" ", request.GetPrompts().Remove(Prompts.Login));

            var parameters = Request.HasFormContentType ?
                Request.Form.Where(parameter => parameter.Key != Parameters.Prompt).ToList() :
                Request.Query.Where(parameter => parameter.Key != Parameters.Prompt).ToList();

            parameters.Add(KeyValuePair.Create(Parameters.Prompt, new StringValues(prompt)));

            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties()
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(parameters)
                }
            );
        }

        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        if (!result.Succeeded || (request.MaxAge != null && result.Properties?.IssuedUtc != null &&
            DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value)))
        {
            if (request.HasPrompt(Prompts.None))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>()
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.LoginRequired,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is not logged in."
                    })
                );
            }

            return Challenge(
                authenticationSchemes: IdentityConstants.ApplicationScheme,
                properties: new AuthenticationProperties()
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList()
                    )
                }
            );
        }

        var user = await _userManager.GetUserAsync(result.Principal) ??
            throw new InvalidOperationException("The user details cannot be retrieved.");

        var application = await _applicationManager.FindByClientIdAsync(request.ClientId ?? string.Empty) ??
            throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

        var authorizations = await _authorizationManager.FindAsync(
            subject: await _userManager.GetUserIdAsync(user),
            client: await _applicationManager.GetIdAsync(application) ?? string.Empty,
            status: Statuses.Valid,
            type: AuthorizationTypes.Permanent,
            scopes: request.GetScopes()
        ).ToListAsync();

        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        principal.SetScopes(request.GetScopes());
        principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

        var authorization = authorizations.LastOrDefault();

        if (authorization == null)
        {
            authorization = await _authorizationManager.CreateAsync(
                principal: principal,
                subject: await _userManager.GetUserIdAsync(user),
                client: await _applicationManager.GetIdAsync(application) ?? string.Empty,
                type: AuthorizationTypes.Permanent,
                scopes: principal.GetScopes()
            );
        }

        principal.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));

        foreach (var claim in principal.Claims)
        {
            claim.SetDestinations(GetDestinations(claim, principal));
        }

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// The logout endpoint.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> representing the logout of the user.</returns>
    [HttpGet("~/identity/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return SignOut(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties()
            {
                RedirectUri = "/"
            }
        );
    }

    /// <summary>
    /// The token endpoint.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> representing the user token.</returns>
    /// <exception cref="InvalidOperationException">Occurs when the grant type is unsupported.</exception>
    [HttpPost("~/identity/token"), Produces("application/json")]
    public async Task<IActionResult> Token()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
            throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
        {
            var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            var user = await _userManager.GetUserAsync(principal);

            if (principal == null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The claims principal doesnt exist."
                    })
                );
            }

            if (user == null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                    })
                );
            }

            if (!await _signInManager.CanSignInAsync(user))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
                    })
                );
            }

            foreach (var claim in principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, principal));
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }

    /// <summary>
    /// Gets a destination for a claim and claims principal.
    /// </summary>
    /// <param name="claim">The <see cref="Claim"/>.</param>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/>.</param>
    /// <returns>The destination.</returns>
    private static IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
    {
        switch (claim.Type)
        {
            case Claims.Name:
                yield return Destinations.AccessToken;

                if (principal.HasScope(Scopes.Profile))
                {
                    yield return Destinations.IdentityToken;
                }

                yield break;

            case Claims.Email:
                yield return Destinations.AccessToken;

                if (principal.HasScope(Scopes.Email))
                {
                    yield return Destinations.IdentityToken;
                }

                yield break;

            case Claims.Role:
                yield return Destinations.AccessToken;

                if (principal.HasScope(Scopes.Roles))
                {
                    yield return Destinations.IdentityToken;
                }

                yield break;

            case "AspNet.Identity.SecurityStamp":
                yield break;

            default:
                yield return Destinations.AccessToken;
                yield break;
        }
    }
}
