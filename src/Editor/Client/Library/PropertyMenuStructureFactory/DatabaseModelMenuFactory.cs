// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.BlazorDiagramBase;
using Medulla.Editor.Client.Components.Properties;
using Medulla.WorkflowDesigner.Client.Library.Services;

namespace Medulla.WorkflowDesigner.Client.Library.PropertyMenuStructureFactory;

public class DatabaseModelMenuFactory : PropertyMenuStructureFactory
{
    private DatabaseTableModel _model;
    private Diagram _diagram;

    public DatabaseModelMenuFactory(DatabaseTableModel model, Diagram diagram)
    {
        _model = model;
        _diagram = diagram;
    }

    public override PropertyMenuStructure GetStructure(Action<AnyTypeValue> action)
    {
        var tables = DatabaseService.GetListOfDatabaseTables(true);
        return new PropertyMenuStructure()
        {

            Nameable = Nameable.NewNameable("Model", null),
            PropertyMenuStructureNodes = new ()
            {
                new()
                {
                    Nameable = Nameable.NewNameable("Switch Table", "Switch Active Database Table"),
                    InputType = InputType.Dropdown,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        ListOfStrings = tables,
                        DropdownActiveItem = _model.Name
                    },
                    OnValueChange = (value) =>
                    {
                        Console.WriteLine($"Changed dropdown {value.DropdownValue.Name} {value.DropdownValue.Description}");
                        DatabaseService.UpdateDiagramActiveDatabaseTableWithSwitchTable(_diagram, value.DropdownValue.Name);
                    }
                },
                new()
                {
                    Nameable = Nameable.NewNameable("Model", "Model"),
                    InputType = InputType.DatabaseTableModel,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        DatabaseTableModel = _model
                    },
                    OnValueChange = (action),
                    PropertyMenuStructure = new()
                    {
                        Nameable = Nameable.NewNameable("Test", "Test"),
                        PropertyMenuStructureNodes = new ()
                        {
                        }
                    }
                },
                new()
                {
                    Nameable = Nameable.NewNameable("Relation", "Relation"),
                    InputType = InputType.Button,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        DatabaseTableModel = _model
                    },
                    PropertyMenuStructure = new()
                    {
                        Nameable = Nameable.NewNameable("Test", "Test"),
                        PropertyMenuStructureNodes = new ()
                        {
                        }
                    }
                },
                new()
                {
                    Nameable = Nameable.NewNameable("Commit", "Commit your work to the database."),
                    InputType = InputType.Button,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        ListOfStrings = new ()
                        {
                            "Test",
                            "test"
                        }
                    },
                    OnValueChange = (AnyTypeValue value) =>
                    {
                        Database data = Database.GetDatabase();
                        DatabaseService.SaveDatabaseTablesToBackend(data);
                    }
                },
                new()
                {
                    Nameable = Nameable.NewNameable("Load", "Load changes from backend."),
                    InputType = InputType.Button,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        ListOfStrings = new ()
                        {
                            "Test",
                            "test"
                        }
                    },
                    OnValueChange = (AnyTypeValue value) =>
                    {
                       DatabaseService.LoadDatabaseFromBackend();
                    }
                }
            }
        };
    }
}
