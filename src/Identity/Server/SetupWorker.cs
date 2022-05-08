// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Identity.Server.Data;
using Medulla.Identity.Server.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Medulla.Identity.Server;

/// <summary>
/// A worker that configures the identity server.
/// </summary>
public class SetupWorker : IHostedService
{
    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Creates a <see cref="SetupWorker"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public SetupWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Starts the <see cref="SetupWorker"/> asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/> representing the starting of the <see cref="SetupWorker"/>.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedClients(applicationManager);
        await SeedUsers(userManager);
    }

    /// <summary>
    /// Seeds clients.
    /// </summary>
    /// <param name="applicationManager">The application manager.</param>
    /// <returns>A <see cref="Task"/> representing the seeding of clients.</returns>
    private static async Task SeedClients(IOpenIddictApplicationManager applicationManager)
    {
        if (await applicationManager.FindByClientIdAsync("medulla-shell") is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor()
            {
                ClientId = "medulla-shell",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Medulla",
                Type = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost/authentication/logout-callback")
                },
                RedirectUris =
                {
                    new Uri("https://localhost/authentication/login-callback")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            });
        }
    }

    /// <summary>
    /// Seeds user accounts.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <returns>A <see cref="Task"/> representing the seeding of user accounts.</returns>
    private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.FindByEmailAsync("admin@medulla.io") is null)
        {
            await userManager.CreateAsync(new ApplicationUser()
            {
                Email = "admin@medulla.io",
                UserName = "admin@medulla.io"
            }, "P@ssword1");
        }
    }

    /// <summary>
    /// Stops the worker asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the stopping of the worker.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
