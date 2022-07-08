// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.WorkflowDesigner.Client.Library;

namespace Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner;

/// <summary>
/// Workflow Events
/// </summary>
public class WorkflowEventsIn
{

    /// <summary>
    /// Set workflow events out
    /// </summary>
    public Action<WorkflowDesigner>? SetWorkflowEventsOut;

    /// <summary>
    /// Add Workflow
    /// </summary>
    public Action? AddWorkflow;

    public Action<List<WorkflowNodeInstance>>? UpdateDatabase { get; set; }

}
