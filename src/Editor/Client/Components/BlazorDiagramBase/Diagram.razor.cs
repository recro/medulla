// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace Medulla.Editor.Client.Components.BlazorDiagramBase;

using BlazorDiagram = Blazor.Diagrams.Core.Diagram;

public partial class Diagram
{

    public BlazorDiagram? NodeDiagram { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();


        NodeDiagram = new BlazorDiagram();
    }

    private void Setup()
    {
        var node1 = NewNode(50, 50);
        var node2 = NewNode(300, 300);
        var node3 = NewNode(300, 50);
        NodeDiagram?.Nodes.Add(node1);
        NodeDiagram?.Nodes.Add(node2);
        NodeDiagram?.Nodes.Add(node3);
    }

    private NodeModel NewNode(double x, double y)
    {
        var node = new NodeModel(new Point(x, y));
        node.AddPort(PortAlignment.Bottom);
        node.AddPort(PortAlignment.Top);
        node.AddPort(PortAlignment.Left);
        node.AddPort(PortAlignment.Right);
        return node;
    }

}
