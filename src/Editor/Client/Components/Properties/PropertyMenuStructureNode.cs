// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;

namespace Medulla.Editor.Client.Components.Properties;

/// <summary>
/// Enumeration for what Input type should be rendered for a property.
/// </summary>
public enum InputType
{
    /// <summary>
    /// Renders a text input field.
    /// </summary>
    TextInput,

    /// <summary>
    /// Renders a text area field.
    /// </summary>
    TextArea,

    /// <summary>
    /// Renders an add multiple input field.
    /// </summary>
    AddMultiple,
}

/// <summary>
/// Type which is for any property that needs list of strings
/// </summary>
public class AnyTypeValue
{
    /// <summary>
    /// Type which is for any property that needs list of strings
    /// </summary>
    public List<string>? listOfStrings { get; set; }

}

/// <summary>
/// This class represents a single Property in a property menu. It can either be a input field,
/// or it can be a parent of a list of input fields.
/// </summary>
public class PropertyMenuStructureNode
{

    /// <summary>
    /// Title and description of input field. This will be rendered in html as label.
    /// </summary>
    public Nameable Nameable { get; set; } = new();

    /// <summary>
    /// This is a resursive implementation of Properties. If this field has properties, then this node will be
    /// rendered as a parent of properties.
    /// </summary>
    public PropertyMenuStructure PropertyMenuStructure { get; set; } = new();

    /// <summary>
    /// IsOpen controls whether the dropdown is open or closed for this Property if it is a parent of properties.
    /// </summary>
    public bool IsOpen { get; set; } = false;

    /// <summary>
    /// Input field controls what type of input is rendered in the field.
    /// </summary>
    public InputType InputType { get; set; } = InputType.TextInput;

    /// <summary>
    /// OnValueChange is an action which is an event. When the value is changed then it will be run.
    /// </summary>
    public Action<AnyTypeValue>? OnValueChange { get; set; }

    /// <summary>
    /// Checks if node has sub nodes.
    /// </summary>
    /// <returns>bool</returns>
    public bool HasNoChildren()
    {
        if (PropertyMenuStructure.PropertyMenuStructureNodes == null)
        {
            return true;
        }

        if (PropertyMenuStructure.PropertyMenuStructureNodes.Count > 0)
        {
            return false;
        }

        return true;
    }
}
