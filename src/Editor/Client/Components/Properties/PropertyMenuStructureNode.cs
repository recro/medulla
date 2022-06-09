// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;
using YamlDotNet.Core.Tokens;

namespace Medulla.Editor.Client.Components.Properties;

public enum InputType
{
    TextInput,
    TextArea,
    Select,
    AddMultiple,
}

public class AnyTypeValue
{
    public List<string>? listOfStrings { get; set; }

}

public class PropertyMenuStructureNode
{

    public Nameable Nameable { get; set; } = new();
    public PropertyMenuStructure PropertyMenuStructure { get; set; } = new();

    public bool IsOpen { get; set; } = false;

    public InputType InputType { get; set; } = InputType.TextInput;

    public Action<AnyTypeValue>? OnValueChange { get; set; }

    public bool HasNoChildren()
    {
        if (PropertyMenuStructure.PropertyMenuStructureNodes == null)
            return true;
        if (PropertyMenuStructure.PropertyMenuStructureNodes.Count > 0)
            return false;
        return true;
    }

}
