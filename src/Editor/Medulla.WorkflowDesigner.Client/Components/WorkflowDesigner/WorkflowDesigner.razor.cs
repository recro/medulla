// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models;
using Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner.Node;
using Medulla.WorkflowDesigner.Client.Library;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner;

public partial class WorkflowDesigner : IDisposable
{

    [Inject]
    public IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// Diagram of Workflow designer
    /// </summary>
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


    public void Dispose()
    {
        throw new NotImplementedException();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Diagram.RegisterModelComponent<WorkflowNodeInstance, NodeComponent>();
        /*Diagram.Nodes.Add(new Table());

        Diagram.Links.Added += OnLinkAdded;
        Diagram.Links.Removed += Diagram_LinkRemoved;*/
    }

    #region WorkwithWorkflows
    private void NewWorkflowInstance()
    {
        Console.WriteLine("new workflow instnace");
        Diagram.Nodes.Add(new WorkflowNodeInstance());
    }
    #endregion




}
