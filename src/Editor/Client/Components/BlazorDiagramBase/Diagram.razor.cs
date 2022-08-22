// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Models.Base;
using Medulla.WorkflowDesigner.Client.Library.Services;
using Microsoft.AspNetCore.Components.Web;

using BlazorDiagram = Blazor.Diagrams.Core.Diagram;

namespace Medulla.Editor.Client.Components.BlazorDiagramBase;
public partial class Diagram
{

    public BlazorDiagram? NodeDiagram { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NodeDiagram = new BlazorDiagram();
        NodeDiagram.RegisterModelComponent<DatabaseTableModel, DatabaseTable>();
        NodeDiagram.MouseClick += ClickedDiagram;

        Console.WriteLine("On Load: Check Switch option");
        if (DiagramSwitchOption == DiagramSwitch.DatabaseDesigner)
        {
            Console.WriteLine("On Load: Switch option db");
            DatabaseService.LoadDatabaseFromBackend();
            Task.Delay(1000).ContinueWith(t =>
            {
                LoadDatabaseTables();
            });
        }

        StateHasChanged();
    }

    private void LoadDatabaseTables()
    {
        var models = DatabaseService.ConvertListDatabaseTableModelFromDatabase(true);
        Console.WriteLine("On Load: Clear");
        NodeDiagram?.Nodes.Clear();
        foreach (var model in models)
        {
            Console.WriteLine("On Load: Adding new model to diagram");
            NodeDiagram?.Nodes.Add(model);
        }
    }

    private void ClickedDiagram(Model model, MouseEventArgs eventArgs)
    {
        try
        {
            Console.WriteLine("Checking if null");
            Console.WriteLine(model.Id);
            ActiveModel = (DatabaseTableModel)model;
        }
        catch (NullReferenceException e)
        {
            ActiveModel = null;
        }
        StateHasChanged();
    }

    private void AddTable(DatabaseTableModel table)
    {
        NodeDiagram?.Nodes.Add(table);
    }

}
