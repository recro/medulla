﻿// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using Controller;
using KubeOps.Operator;
using Operator;

namespace DatabaseControllerKubeOps;

public static class Program
{
    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static Task<int> Main(string[] args) {
        Console.WriteLine("Listing Service Uris");
        Console.WriteLine(ServiceUris.GetDatabaseServiceUri());
        Console.WriteLine(ServiceUris.GetDatabaseSyncUri());

        return CreateHostBuilder(args)
            .Build()
            .RunOperatorAsync(args);
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
