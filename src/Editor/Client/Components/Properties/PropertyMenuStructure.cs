// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;

namespace Medulla.Editor.Client.Components.Properties;

/// <summary>
/// Defines the structure of the PropertyMenu
/// </summary>
public class PropertyMenuStructure
{

    public Nameable Nameable { get; set; } = new();

    public List<PropertyMenuStructureNode>? PropertyMenuStructureNodes { get; set; } = new();




}
