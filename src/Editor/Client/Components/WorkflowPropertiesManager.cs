// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Models;
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

    private WorkflowEventsIn? _workflowEventsIn;

    private WorkflowNodeInstance? activeNode;

    private List<WorkflowNodeInstance>? nodes = new();

    public WorkflowPropertiesManager()
    {
        _workflowEventsIn = new WorkflowEventsIn()
        {
            SetWorkflowEventsOut = SetDesigner,
        };
    }

    /// <summary>
    /// Get Workflow Events In
    /// </summary>
    /// <returns></returns>
    public WorkflowEventsIn? GetWorkflowDesignerEventsIn()
    {
        return _workflowEventsIn;
    }

    private void SetDesigner(WD workflowDesigner)
    {
        this._workflowDesigner = workflowDesigner;
        this._workflowDesigner.SetWorkflowActive += SetWorkflowActive;
    }

    private void SetWorkflowActive(Model model, MouseEventArgs events)
    {
        if (model != null)
        {
            WorkflowNodeInstance node = (WorkflowNodeInstance)model;
            activeNode = node;
            _propertyMenuStructure.Nameable = new Nameable() {Title = "Node " + node.Name, Description = "Node " + node.Name};
            _propertyMenuStructure.PropertyMenuStructureNodes = new List<PropertyMenuStructureNode>()
            {
                new PropertyMenuStructureNode()
                {
                    Nameable = new Nameable() {Title = "Field", Description = "Field"},
                    InputType = InputType.WorkflowDataInput,
                    OnValueChange = WorkflowFieldUpdated,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        ListOfStrings = new List<string>()
                        {
                            "string",
                            "integer",
                        },
                        Number = 0
                    }
                },
                new PropertyMenuStructureNode()
                {
                    Nameable = new Nameable() {Title = "Add new Field to workflow", Description = "Add new Field to workflow"},
                    InputType = InputType.Button,
                    OnValueChange = AddNewFieldToWorkflow,
                    AnyTypeInput = new AnyTypeInput()
                    {
                        ListOfStrings = new List<string>()
                        {
                            "string",
                            "integer",
                        }
                    }
                },
            };
            _editorPage!.StateChanged();
        }
        else
        {
            _propertyMenuStructure.Nameable = new Nameable() {Title = "Add an action to the Workflow designer", Description = ""};
            _propertyMenuStructure.PropertyMenuStructureNodes = new List<PropertyMenuStructureNode>()
            {
                new PropertyMenuStructureNode()
                {
                    Nameable = new Nameable() {Title = "Add Workflow", Description = "Add an action to the Workflow designer"},
                    InputType = InputType.Button,
                    OnValueChange = AddWorkflow
                }
            };
            _editorPage!.StateChanged();
        }
    }

    private void AddWorkflow(AnyTypeValue anyTypeValue)
    {
        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();
        Console.WriteLine("The number of nodes are " + nodes?.Count);
        var workflowNode = new WorkflowNodeInstance() {Name = myuuidAsString};
        workflowNode.AddPort(PortAlignment.Left);
        workflowNode.AddPort(PortAlignment.Right);
        nodes?.Add(workflowNode);
        if (nodes == null) throw new ArgumentNullException("nodes is null");
        _workflowDesigner?.SetNodes(nodes);
        _editorPage?.StateChanged();
        //_workflowEventsIn?.AddWorkflow?.Invoke();
    }

    private void AddNewFieldToWorkflow(AnyTypeValue value)
    {
        Console.WriteLine("number of property menu structure nodes ");
        Console.WriteLine(_propertyMenuStructure?.PropertyMenuStructureNodes?.Count-1);
        _propertyMenuStructure?.PropertyMenuStructureNodes?.Insert(_propertyMenuStructure.PropertyMenuStructureNodes.Count-1, new PropertyMenuStructureNode()
        {
            Nameable = new Nameable() {Title = "Field", Description = "Field"},
            InputType = InputType.WorkflowDataInput,
            OnValueChange = WorkflowFieldUpdated,
            AnyTypeInput = new AnyTypeInput()
            {
                ListOfStrings = new List<string>()
                {
                    "string",
                    "integer",
                },
                Number = _propertyMenuStructure.PropertyMenuStructureNodes.Count-1
            }
        });
        _editorPage!.StateChanged();
    }

    private void UpdateActiveNodeField(int id, string? name, string? type)
    {
        if (name == null) throw new ArgumentNullException("name is null");
        if (type == null) throw new ArgumentNullException("type is null");
        activeNode?.UpdateInputField(id, name, type);
        if (nodes == null) throw new ArgumentNullException("nodes is null");
        _editorPage?.StateChanged();
        _workflowDesigner?.SetNodes(nodes);
        _editorPage?.StateChanged();
        //_workflowDesigner.UpdateNodeWithField(activeNode);
    }

    private void WorkflowFieldUpdated(AnyTypeValue value)
    {
        UpdateActiveNodeField(value.Number, value?.ListOfStrings?[0], value?.ListOfStrings?[1]);
        //_workflowDesigner?.UpdateNodeWithField(activeNode, , );
    }

}
