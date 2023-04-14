// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Services.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medulla.Services.Identity.Data;

/// <summary>
/// The application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Creates a <see cref="ApplicationDbContext"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions"/>.</param>
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}
