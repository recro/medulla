// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Models;
using Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner.Node;
using Medulla.WorkflowDesigner.Client.Library;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner;

/// <summary>
/// Workflow Designer
/// </summary>
public partial class WorkflowDesigner : IDisposable
{

    /// <summary>
    /// Events
    /// </summary>
    [Parameter]
    public WorkflowEventsIn? WorkflowEventsIn { get; set; }

    /// <summary>
    /// Setworkflow active
    /// </summary>
    public event Action<Model, MouseEventArgs>? SetWorkflowActive;

    /// <summary>
    /// Created node
    /// </summary>
    public event Action<WorkflowNodeInstance>? CreatedNode;

    /// <summary>
    /// Js Runtime
    /// </summary>
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

        WorkflowEventsIn!.AddWorkflow += NewWorkflowInstance;
        WorkflowEventsIn?.SetWorkflowEventsOut?.Invoke(this);
        Diagram.MouseClick += SetWorkflowActive;

        /*Diagram.Nodes.Add(new Table());

        Diagram.Links.Added += OnLinkAdded;
        Diagram.Links.Removed += Diagram_LinkRemoved;*/
    }

    #region WorkwithWorkflows
    private void NewWorkflowInstance()
    {
        Console.WriteLine("new workflow instnace");
        var node = new WorkflowNodeInstance();
        Diagram.Nodes.Add(node);
        CreatedNode?.Invoke(node);
    }
    #endregion




}
