// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace Medulla.WorkflowDesigner.Client.Library;

public abstract class GrpcWrapper
{

    private HttpClient _httpClient;
    private GrpcChannel _channel;
    protected GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient _client;

    public GrpcWrapper()
    {
        _httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        //var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
        _channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = _httpClient });
        _client = new GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient(_channel);
    }



}
