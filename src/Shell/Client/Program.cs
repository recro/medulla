// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Shell.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5188", new GrpcChannelOptions { HttpClient = httpClient });
var client = new GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient(channel);

builder.Services.AddSingleton(client);

await builder.Build().RunAsync();
