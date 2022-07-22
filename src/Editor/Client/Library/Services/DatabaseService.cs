// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcDatabaseService;

namespace Medulla.WorkflowDesigner.Client.Library.Services;

public class DatabaseService
{


    public static void ConsoleLogListOfDatabases()
    {

    }


    public static async Task<Database> LoadDatabase()
    {

        var client = GetClient();

        var medullaDatabases = new GetDatabasesRequest() {Name = "medulla"};

        var dbs = await client.GetDatabasesAsync(medullaDatabases);

        Database database = Database.GetDatabase();

        dbs.Data[0].Databases[0].



    }

    public static void SaveDatabaseTablesToBackend(Database database)
    {
        var client = DatabaseService.GetClient();
        var models = database.GetModelsFromTables();
        client.CreateDatabasesAsync(new CreateDatabasesRequest()
        {
            Database = { new[]
            {
                new GrpcDatabaseService.Database()
                {
                    Dialect = "mysql",
                    Name = "medulla",
                    Models = { models }
                }
            } }
        });
    }

    public static GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient GetClient()
    {
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        //var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
        var client = new GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient(channel);
        return client;
    }


}
