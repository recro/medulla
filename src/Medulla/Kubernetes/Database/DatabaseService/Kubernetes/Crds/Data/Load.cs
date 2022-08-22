// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Google.Protobuf.Collections;
using GrpcDatabaseService;

namespace DatabaseService.Kubernetes.Crds.Data;

public class Load
{


    private static List<Databases> LoadFrom(RepeatedField<GrpcDatabaseService.Database> requestDatabases)
    {
        var databases = new List<Databases>();
        for (var i = 0; i < requestDatabases.Count; i++)
        {
            var models = new List<Model>();

            for (var modelsI = 0; modelsI < requestDatabases[i].Models.Count; modelsI++)
            {
                var columns = new List<Column>();

                for (var columnsI = 0; columnsI < requestDatabases[i].Models[modelsI].Column.Count; columnsI++)
                {
                    var column = new Column()
                    {
                        AllowNull = requestDatabases[i].Models[modelsI].Column[columnsI].AllowNull,
                        ColumnName = requestDatabases[i].Models[modelsI].Column[columnsI].ColumnName,
                        Comment = requestDatabases[i].Models[modelsI].Column[columnsI].Comment,
                        DefaultValue = requestDatabases[i].Models[modelsI].Column[columnsI].DefaultValue,
                        Field = requestDatabases[i].Models[modelsI].Column[columnsI].FieldName,
                        PrimaryKey = requestDatabases[i].Models[modelsI].Column[columnsI].PrimaryKey,
                        Type = requestDatabases[i].Models[modelsI].Column[columnsI].Type,
                        Unique = requestDatabases[i].Models[modelsI].Column[columnsI].Unique,
                    };
                    if (column.Unique == "test")
                    {
                        column.Unique = null;
                    }
                    columns.Add(column);
                }

                models.Add(new Model()
                {
                    Name = requestDatabases[i].Models[modelsI].Name,
                    Columns = columns
                });
            }


            databases.Add(new Databases()
            {
                Name = requestDatabases[i].Name,
                Dialect = requestDatabases[i].Dialect,
                UsernameSecretName = "test",
                PasswordSecretName = "test",
                Host = "db-" + requestDatabases[i].Name,
                Models = models
            });
        }

        return databases;

    }

    public static List<Databases> GetDatabasesFromDatabaseRequest(CreateDatabasesRequest request)
    {
        return LoadFrom(request.Database);
    }

    public static List<Databases> GetDatabasesFromDatabaseRequest(UpdateDatabasesRequest request)
    {
        return LoadFrom(request.Database);
    }

    public static GetDatabasesResponse GetDatabasesFromCrd(CustomResourceList<CResource> crs)
    {

        var data = new RepeatedField<GrpcDatabaseService.Data>();
        for (var i = 0; i < crs?.Items?.Count; i++)
        {
            var databases = new RepeatedField<GrpcDatabaseService.Database>();
            for (var dbI = 0; dbI < crs?.Items?[i]?.Databases?.Count; dbI++)
            {
                var db = new GrpcDatabaseService.Database
                {
                    Name = crs?.Items?[i]?.Databases?[dbI].Name,
                    Dialect = crs?.Items?[i]?.Databases?[dbI].Dialect
                };

                for (var modelI = 0; modelI < crs?.Items?[i]?.Databases?[dbI]?.Models?.Count; modelI++)
                {
                    var model = new GrpcDatabaseService.Model
                    {
                        Name = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI].Name
                    };
                    for (var columnI = 0; columnI < crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?.Count; columnI++)
                    {
                        var column = new GrpcDatabaseService.Column
                        {
                            FieldName = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Field,
                            Type = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Type,
                            Unique = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Unique ?? "test"
                        };
                        if (crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].PrimaryKey == null)
                        {
                            throw new Exception("Primary Key may not be null");
                        }

                        column.PrimaryKey = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].PrimaryKey ?? default;
                        column.AllowNull = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI]?.AllowNull ?? default;
                        column.Comment = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Comment;
                        column.ColumnName = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].ColumnName;

                        model.Column.Add(column);
                    }
                    db.Models.Add(model);
                }
                databases.Add(db);
            }

            data.Add(new GrpcDatabaseService.Data()
            {
                Name = crs?.Items?[i]?.Metadata?.Name,
                Databases = { databases }
            });

        }


        return new GetDatabasesResponse()
        {
            Data = { data }
        };
    }


}
