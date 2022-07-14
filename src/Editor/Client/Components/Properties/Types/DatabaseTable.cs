// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.Properties.Types;

public class Column
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsUnique { get; set; } = false;

    public void Overwrite(Column column)
    {
        Id = column.Id;
        Name = column.Name;
        Type = column.Type;
        IsUnique = column.IsUnique;
    }
}

public class DatabaseTable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Column> Columns { get; set; } = new();

    public DatabaseTable()
    {
        Id = Guid.NewGuid().ToString();
        Name = "New Table";
    }

    public void Overwrite(DatabaseTable table)
    {
        Id = table.Id;
        Name = table.Name;
        foreach (var tableColumn in table.Columns)
        {
            if (Columns.Exists(t => t.Id == tableColumn.Id))
            {
                foreach (var column in Columns)
                {
                    if (column.Id == tableColumn.Id)
                    {
                        column.Overwrite(tableColumn);
                        break;
                    }
                }
            }
            else
            {
                Columns.Add(tableColumn);
            }
        }
    }
}

