// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Models.Base;
using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.Properties;
using Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner;
using Medulla.WorkflowDesigner.Client.Library;
using Microsoft.AspNetCore.Components.Web;

namespace Medulla.Editor.Client.Components;

using WD = Medulla.WorkflowDesigner.Client.Components.WorkflowDesigner.WorkflowDesigner;

/// <summary>
/// Workflow Properties Manager
/// </summary>
public class WorkflowPropertiesManager
{

    public EditorPage? _editorPage { private get; set; }
    public PropertyMenuStructure _propertyMenuStructure { get; set; } = new PropertyMenuStructure();
    public WD? _workflowDesigner { get; set; }


    /// <summary>
    /// Get Workflow Events In
    /// </summary>
    /// <returns></returns>
    public WorkflowEventsIn GetWorkflowDesignerEventsIn()
    {
        return new WorkflowEventsIn() {SetWorkflowEventsOut = SetDesigner};
    }

    private void SetDesigner(WD workflowDesigner)
    {
        this._workflowDesigner = workflowDesigner;
        this._workflowDesigner.SetWorkflowActive += SetWorkflowActive;
    }

    private void SetWorkflowActive(Model model, MouseEventArgs events)
    {
        WorkflowNodeInstance node = (WorkflowNodeInstance)model;
        _propertyMenuStructure.Nameable = new Nameable() {Title = "Node " + node.Name, Description = "Node " + node.Name};
        _propertyMenuStructure.PropertyMenuStructureNodes = new List<PropertyMenuStructureNode>()
        {
            new PropertyMenuStructureNode()
            {
                Nameable = new Nameable() {Title = "test", Description = "test"},
                InputType = InputType.AddMultiple
            }
        };
        _editorPage!.StateChanged();
    }

}
