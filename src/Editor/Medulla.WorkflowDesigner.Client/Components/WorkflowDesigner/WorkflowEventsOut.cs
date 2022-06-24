// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.WorkflowDesigner.Client.Library;

namespace Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner;

/// <summary>
/// Workflow events out
/// </summary>
public class WorkflowEventsOut
{

    /// <summary>
    /// Set active
    /// </summary>
    public event Action? SetInstanceActive;

    /// <summary>
    /// Created instance
    /// </summary>
    public event Action<WorkflowNodeInstance>? CreatedInstance;

}
