// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.Properties;


namespace Medulla.Editor.Client.Components.DatabaseDesigner;

public partial class DatabaseDesigner : IDisposable
{

    public void SetDatabaseProperties(List<string>? databaseTables)
    {

        var tables = new PropertyMenuStructureNode();
        tables.Nameable = new() {Title = "Table Columns", Description = "tables"};
        tables.PropertyMenuStructure = new() {Nameable = new() {Title = "Tables", Description = "tables"}, PropertyMenuStructureNodes = new() {}};
        tables.PropertyMenuStructure.Nameable = new() {Title = "Table Columns", Description = "tables"};
        tables.IsOpen = true;
        if (databaseTables != null)
        {
            foreach (var table in databaseTables)
            {
                tables.PropertyMenuStructure.PropertyMenuStructureNodes.Add(new()
                {
                    Nameable = new()
                    {
                        Title = table,
                        Description = table + " Columns"
                    },
                    InputType = InputType.AddMultiple,
                    IsOpen = true
                });
            }
        }


        EditorPage!.SetPropertyMenuStructure(new PropertyMenuStructure()
        {
            Nameable = new Nameable()
            {
                Title = "Database Tables",
                Description = "Database Tables"
            },
            PropertyMenuStructureNodes = new()
            {
                new PropertyMenuStructureNode()
                {
                    InputType = InputType.AddMultiple,
                    IsOpen = true,
                    Nameable = new ()
                    {
                        Title = "Database Tables",
                        Description = "Database Tables"
                    },
                    OnValueChange = (AnyTypeValue value) =>
                    {
                        SetDatabaseProperties(value.listOfStrings);
                    }
                },
                tables
            }

        });

    }

    void OnOpen(bool isOpen)
    {
            SetDatabaseProperties(null);
    }


}
