// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using KubeOps.Operator;

namespace Controller;


/// <summary>
/// delete code
/// </summary>
public class Startup
{
    /// <summary>
    /// delete code
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddKubernetesOperator();
    }

    /// <summary>
    /// delete code
    /// </summary>
    public void Configure(IApplicationBuilder app)
    {
        app.UseKubernetesOperator();
    }
}
