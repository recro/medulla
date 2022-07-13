// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models;
using Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner.Node;
using Medulla.WorkflowDesigner.Client.Library;

namespace Medulla.WorkflowDesigner.Client.Components.DatabaseDesigner;

public partial class DatabaseDesigner : IDisposable
{

    public Diagram Diagram { get; } = new Diagram(new DiagramOptions
    {
        GridSize = 40,
        AllowMultiSelection = false,
        Links = new DiagramLinkOptions
        {
            Factory = (diagram, sourcePort) =>
            {
                return new LinkModel(sourcePort, null)
                {
                    Router = Routers.Orthogonal,
                    PathGenerator = PathGenerators.Straight,
                };
            }
        }
    });

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Diagram.RegisterModelComponent<WorkflowNodeInstance, NodeComponent>();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }


}
