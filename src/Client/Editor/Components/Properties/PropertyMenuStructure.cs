// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Client.Editor.Abstractions.ObjectComposition;

namespace Medulla.Client.Editor.Components.Properties;

/// <summary>
/// Defines the structure of the PropertyMenu
/// </summary>
public class PropertyMenuStructure
{
    /// <summary>
    /// Title and description of menu structure which is placed in html in the Property Menu.
    /// </summary>
    public Nameable Nameable { get; set; } = new();

    /// <summary>
    /// The menu structure to be rendered. List of PropertyMenuStructureNode.
    /// </summary>
    public List<PropertyMenuStructureNode>? PropertyMenuStructureNodes { get; set; } = new();

}
