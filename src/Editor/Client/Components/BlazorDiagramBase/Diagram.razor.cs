// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using Microsoft.AspNetCore.Components.Web;

namespace Medulla.Editor.Client.Components.BlazorDiagramBase;

using BlazorDiagram = Blazor.Diagrams.Core.Diagram;

public partial class Diagram
{

    public BlazorDiagram? NodeDiagram { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NodeDiagram = new BlazorDiagram();
        NodeDiagram.RegisterModelComponent<DatabaseTableModel, DatabaseTable>();
        NodeDiagram.MouseClick += ClickedDiagram;
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
