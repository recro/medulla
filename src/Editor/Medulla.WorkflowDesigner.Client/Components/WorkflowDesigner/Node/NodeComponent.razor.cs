// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.WorkflowDesigner.Client.Library;
using Microsoft.AspNetCore.Components;

namespace Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner.Node;

/// <summary>
/// NodeComponent represents the WorkflowNodeInstance in the Designer.
/// </summary>
public partial class NodeComponent
{

    /// <summary>
    /// The WorkflowNodeInstance
    /// </summary>
    [Parameter]
    public WorkflowNodeInstance? Node { get; set; }

}
