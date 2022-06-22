// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace Medulla.WorkflowDesigner.Client.Library;

/// <summary>
/// WorkflowNodeInstance represents one instance of a workflow in the designer
/// </summary>
public class WorkflowNodeInstance : NodeModel
{

    /// <summary>
    /// Constructor for WorkflowNodeInstance
    /// </summary>
    /// <param name="position"></param>
    public WorkflowNodeInstance(Point? position = null) : base(position, RenderLayer.HTML)
    {
    }

}
