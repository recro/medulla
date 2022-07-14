// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Medulla.Editor.Client.Components.Properties.Types;

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
        if (_database == null)
        {
            _database = new Database();
        }
        return _database;
    }


    public string? Id { get; set; }
    private DatabaseTable? ActiveEditingTable { get; set; }
    private List<DatabaseTable> Tables { get; set; } = new();

    public void SaveTable(DatabaseTable table)
    {
        var t = Tables.Find(t => t.Id == table.Id);
        if (t != null)
        {
            t.Overwrite(table);
        }
        else
        {
            Tables.Add(table);
        }
    }

    public void SyncTablesWithBackend()
    {
        Print();
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"" +
                             $"                " +
                             $"                " +
                             $"                ");

        foreach (var table in Tables)
        {
            stringBuilder.Append($"" +
                                 $"                " +
                                 table.ToString()    +
                                 $"                ");
        }

        stringBuilder.Append($"" +
                             $"                " +
                             $"                " +
                             $"                ");

        return stringBuilder.ToString();
    }
}
