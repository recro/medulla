// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Google.Protobuf.Collections;
using Medulla.Editor.Client.Components.Properties.Types;
using Column = Medulla.Editor.Client.Components.Properties.Types.Column;

namespace Medulla.WorkflowDesigner.Client.Library;

/*public class Column
{
    public string? Id { get; set; }
    public string Field { get; set; } = "";
    public string Type { get; set; } = "string";
    public bool Unique { get; set; } = false;
    public bool Required { get; set; } = false;
}*/

/*public class DatabaseTable
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<Column> Columns { get; set; } = new();

}*/

public class Database
{
    private static Database? _database;

    private Database()
    {
    }

    public static Database GetDatabase()
    {
        _database ??= new Database();
        return _database;
    }


    public string? Id { get; set; }
    public DatabaseTable? ActiveEditingTable { get; set; }
    public List<DatabaseTable> Tables { get; set; } = new();


    public void Clear()
    {
        Tables = new();
    }

    private static RepeatedField<GrpcDatabaseService.Column> GetColumnsFromTableColumns(List<Column> tableColumns)
    {
        var columns = new RepeatedField<GrpcDatabaseService.Column>();
        foreach (var tableColumn in tableColumns)
        {
            columns.Add(new GrpcDatabaseService.Column()
            {
                AllowNull = false,
                Comment = "test",
                Type = tableColumn.Type,
                Unique = "test",
                ColumnName = tableColumn.Name,
                DefaultValue = "",
                FieldName = tableColumn.Name,
                PrimaryKey = false,
            });
        }
        return columns;
    }

    public RepeatedField<GrpcDatabaseService.Model> GetModelsFromTables()
    {
        var models = new RepeatedField<GrpcDatabaseService.Model>();
        foreach (var table in Tables)
        {
            var model = new GrpcDatabaseService.Model()
            {
                Column = { GetColumnsFromTableColumns(table.Columns) },
                Name = table.Name
            };
            models.Add(model);
        }
        return models;
    }

    public void SaveTable(DatabaseTable table)
    {
        var t = Tables.Find(t => t.Name == table.Name);
        var tid = Tables.Find(t => t.Id == table.Id);
        if (tid != null && t == null)
        {
            t = tid;
        }
        if (t != null)
        {
            t.Overwrite(table);
        }
        else
        {
            Tables.Add(table);
        }
    }



    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"" +
                             $"                " +
                             $"                " +
                             $"                ");

        var item = 0;
        foreach (var table in Tables)
        {
            stringBuilder.Append($"TABLE NUMBER {item}" +
                                 $"                " +
                                 table.ToString() +
                                 $"                ");
            item++;
        }

        stringBuilder.Append($"" +
                             $"                " +
                             $"                " +
                             $"                ");

        return stringBuilder.ToString();
    }
}
