using DatabaseService.Utils;
using Google.Protobuf.Collections;
using GrpcDatabaseService;

namespace DatabaseService.Kubernetes.Crds.Data;

public class Load
{


    private static List<Kubernetes.Crds.Data.Databases> LoadFrom(RepeatedField<GrpcDatabaseService.Database> requestDatabases)
    {
        var databases = new List<Kubernetes.Crds.Data.Databases>();
        for (int i = 0; i < requestDatabases.Count; i++)
        {
            var models = new List<Kubernetes.Crds.Data.Model>();

            for (int modelsI = 0; modelsI < requestDatabases[i].Models.Count; modelsI++)
            {
                var columns = new List<Kubernetes.Crds.Data.Column>();

                for (int columnsI = 0; columnsI < requestDatabases[i].Models[modelsI].Column.Count; columnsI++)
                {
                    columns.Add(new Column()
                    {
                        AllowNull = requestDatabases[i].Models[modelsI].Column[columnsI].AllowNull,
                        ColumnName = requestDatabases[i].Models[modelsI].Column[columnsI].ColumnName,
                        Comment = requestDatabases[i].Models[modelsI].Column[columnsI].Comment,
                        DefaultValue = requestDatabases[i].Models[modelsI].Column[columnsI].DefaultValue,
                        Field = requestDatabases[i].Models[modelsI].Column[columnsI].FieldName,
                        PrimaryKey = requestDatabases[i].Models[modelsI].Column[columnsI].PrimaryKey,
                        Type = requestDatabases[i].Models[modelsI].Column[columnsI].Type,
                        Unique = requestDatabases[i].Models[modelsI].Column[columnsI].Unique,
                    });
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

    public static List<Kubernetes.Crds.Data.Databases> GetDatabasesFromDatabaseRequest(CreateDatabasesRequest request)
        => LoadFrom(request.Database);

    public static List<Kubernetes.Crds.Data.Databases> GetDatabasesFromDatabaseRequest(UpdateDatabasesRequest request)
        => LoadFrom(request.Database);


    public static GetDatabasesResponse GetDatabasesFromCrd(CustomResourceList<CResource> crs)
    {

        RepeatedField<GrpcDatabaseService.Data> data = new RepeatedField<GrpcDatabaseService.Data>();
        for (int i = 0; i < crs?.Items?.Count; i++)
        {
            RepeatedField<GrpcDatabaseService.Database> databases = new RepeatedField<GrpcDatabaseService.Database>();
            for (int dbI = 0; dbI < crs?.Items?[i]?.Databases?.Count; dbI++)
            {
                var db = new GrpcDatabaseService.Database();
                db.Name = crs?.Items?[i]?.Databases?[dbI].Name;
                db.Dialect = crs?.Items?[i]?.Databases?[dbI].Dialect;

                for (int modelI = 0; modelI < crs?.Items?[i]?.Databases?[dbI]?.Models?.Count; modelI++)
                {
                    GrpcDatabaseService.Model model = new GrpcDatabaseService.Model();
                    for (int columnI = 0; columnI < crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?.Count; columnI++)
                    {
                        GrpcDatabaseService.Column column = new GrpcDatabaseService.Column();

                        column.FieldName = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Field;
                        column.Type = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Type;
                        column.Unique = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].Unique;
                        if (crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].PrimaryKey == null)
                            throw new Exception("Primary Key may not be null");
                        column.PrimaryKey = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI].PrimaryKey ?? default(bool);
                        column.AllowNull = crs?.Items?[i]?.Databases?[dbI]?.Models?[modelI]?.Columns?[columnI]?.AllowNull ?? default(bool);
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
