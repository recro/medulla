// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Google.Protobuf.Collections;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcDatabaseService;

namespace Medulla.WorkflowDesigner.Client.Library.Services;

public class DatabaseService
{


    public static void ConsoleLogListOfDatabases()
    {

    }


    public static async void LoadDatabase()
    {
        var client = GetClient();
        var medullaDatabases = new GetDatabasesRequest() {Name = "medulla"};
        var dbs = await client.GetDatabasesAsync(medullaDatabases);
        Database database = Database.GetDatabase();

        UpdateDatabaseWithTables(database, dbs.Data[0].Databases[0].Models);

    }

    private static void UpdateDatabaseWithTables(Database database, RepeatedField<Model> models)
    {
        List<Editor.Client.Components.Properties.Types.DatabaseTable> tables =
            ConvertDatabaseTablesFromGrpcModels(models);

        database.Clear();

        foreach (var table in tables)
        {
            database.Tables.Add(table);
        }
    }

    private static List<Editor.Client.Components.Properties.Types.DatabaseTable> ConvertDatabaseTablesFromGrpcModels(RepeatedField<Model> models)
    {
        List<Editor.Client.Components.Properties.Types.DatabaseTable> tables = new();
        foreach (var model in models)
        {
            tables.Add(ConvertDatabaseTableFromGrpcModel(model));
        }
        return tables;
    }

    private static Editor.Client.Components.Properties.Types.DatabaseTable ConvertDatabaseTableFromGrpcModel(Model model)
    {
        Editor.Client.Components.Properties.Types.DatabaseTable table = new();

        table.Name = model.Name;
        foreach (var column in model.Column)
        {
            table.Columns.Add(ConvertDatabaseColumnFromGrpcColumn(column));
        }
        return table;
    }

    private static Editor.Client.Components.Properties.Types.Column ConvertDatabaseColumnFromGrpcColumn(Column column)
    {
        Editor.Client.Components.Properties.Types.Column
            tableColumn = new();

        tableColumn.Name = column.FieldName;
        tableColumn.Type = column.Type;
        tableColumn.Required = !column.AllowNull;
        tableColumn.IsUnique = column.Unique.Length > 0;

        return tableColumn;
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

    private static GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient GetClient()
    {
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        //var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
        var client = new GrpcDatabaseService.DatabaseSvc.DatabaseSvcClient(channel);
        return client;
    }


}
