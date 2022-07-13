// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace Medulla.WorkflowDesigner.Client.Library;

/// <summary>
/// Data field
/// </summary>
public class DataField
{
    public int Id = 0;
    public string Name = "";
    public string Type = "";
}

/// <summary>
/// WorkflowNodeInstance represents one instance of a workflow in the designer
/// </summary>
public class WorkflowNodeInstance : NodeModel
{
    /// <summary>
    /// Name of workflow node instance
    /// </summary>
    public string Name { get; set; } = "Workflow Action";

    public List<DataField> InputDataFields = new();
    public List<DataField> OutputDataFields = new();

    /// <summary>
    /// Constructor for WorkflowNodeInstance
    /// </summary>
    /// <param name="position"></param>
    public WorkflowNodeInstance(Point? position = null) : base(position, RenderLayer.HTML)
    {
    }

    public void UpdateInputField(int id, string name, string type)
    {
        bool found = false;
        foreach (var inputDataField in InputDataFields)
        {
            if (inputDataField.Id == id)
            {
                found = true;
                inputDataField.Name = name;
                inputDataField.Type = type;
            }
        }

        if (!found)
        {
            AddInputField(id, name, type);
        }
        Console.WriteLine($"input field name {name} type {type}");
    }

    public void UpdateOutputField(string id, string name, string type)
    {
        Console.WriteLine($"output field name {name} type {type}");
    }

    public DataField AddInputField(int index, string name, string type)
    {
        var field = new DataField() {Id = index, Name = "", Type = ""};
        InputDataFields.Add(field);
        return field;
    }

    public static string NewId()
    {
        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();
        return myuuidAsString;
    }

}
