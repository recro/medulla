// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Google.Protobuf.Collections;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcDatabaseService;
using Medulla.Editor.Client.Components.BlazorDiagramBase;

namespace Medulla.WorkflowDesigner.Client.Library.Services;

public class DatabaseService
{

    public static void UpdateDiagramActiveDatabaseTableWithSwitchTable(Diagram diagram, string tableName)
    {
        var found = false;
        var models = ConvertListDatabaseTableModelFromDatabase(true);
        foreach (var model in models)
        {
            if (model.Name == tableName)
            {
                diagram.UpdateActiveModel(model);
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new Exception($"Expected to find model with name {tableName}");
        }
    }


    public static void ConsoleLogListOfDatabases()
    {

    }

    public static List<string> GetListOfDatabaseTables(bool loadFromBackend = false)
    {
        List<string> tables = new();

        if (loadFromBackend)
        {
            LoadDatabaseFromBackend();
        }

        var db = Database.GetDatabase();
        foreach (var table in db.Tables)
        {
            tables.Add(table.Name);
        }

        return tables;
    }


    public static async void LoadDatabaseFromBackend()
    {
        var client = GetClient();
        var medullaDatabases = new GetDatabasesRequest() { Name = "medulla" };
        var dbs = await client.GetDatabasesAsync(medullaDatabases);
        var database = Database.GetDatabase();
        Console.WriteLine($"Database name {dbs.Data[0].Databases[0].Models[0].Name}");
        UpdateDatabaseWithTables(database, dbs.Data[0].Databases[0].Models);
    }

    public static List<DatabaseTableModel> ConvertListDatabaseTableModelFromDatabase(bool loadFromBackend = false)
    {
        List<DatabaseTableModel> models = new();

        if (loadFromBackend)
        {
            LoadDatabaseFromBackend();
        }

        var db = Database.GetDatabase();

        Console.WriteLine($"Found db with {db.Tables.Count} tables");
        foreach (var table in db.Tables)
        {
            models.Add(ConvertDatabaseTableModelFromDatabaseTable(table));
        }

        return models;
    }

    private static DatabaseTableModel ConvertDatabaseTableModelFromDatabaseTable(
        Editor.Client.Components.Properties.Types.DatabaseTable databaseTable)
    {
        DatabaseTableModel model = new()
        {
            Name = databaseTable.Name
        };

        foreach (var tableColumn in databaseTable.Columns)
        {
            model.InputDataFields.Add(new Editor.Client.Components.BlazorDiagramBase.DataField()
            {
                Name = tableColumn.Name,
                Type = tableColumn.Type
            });
        }

        return model;
    }

    private static void UpdateDatabaseWithTables(Database database, RepeatedField<Model> models)
    {
        var tables =
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
            Console.WriteLine($"Found model with name {model.Name}");
            tables.Add(ConvertDatabaseTableFromGrpcModel(model));
            Console.WriteLine($"Found table with name {tables[^1].Name}");
        }
        return tables;
    }

    private static Editor.Client.Components.Properties.Types.DatabaseTable ConvertDatabaseTableFromGrpcModel(Model model)
    {
        Editor.Client.Components.Properties.Types.DatabaseTable table = new()
        {
            Name = model.Name
        };
        foreach (var column in model.Column)
        {
            table.Columns.Add(ConvertDatabaseColumnFromGrpcColumn(column));
        }
        return table;
    }

    private static Editor.Client.Components.Properties.Types.Column ConvertDatabaseColumnFromGrpcColumn(Column column)
    {
        Editor.Client.Components.Properties.Types.Column
            tableColumn = new()
            {
                Name = column.FieldName,
                Type = column.Type,
                Required = !column.AllowNull,
                IsUnique = column.Unique.Length > 0
            };

        return tableColumn;
    }

    public static void SaveDatabaseTablesToBackend(Database database)
    {
        var client = GetClient();
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

    private static DatabaseSvc.DatabaseSvcClient GetClient()
    {
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        //var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
        var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        return client;
    }


}
