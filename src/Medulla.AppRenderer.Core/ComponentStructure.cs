// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.AppRenderer.Components;
using Microsoft.AspNetCore.Components;

namespace Medulla.AppRenderer.Core.Abstractions;

/// <summary>
/// A Component is any visible Blazor component that has properties, children,
/// </summary>
public class ComponentStructure
{
    /// <summary>
    /// Children of IComponentStructure are IComponents which are rendered into a dom tree.
    /// </summary>
    public List<ComponentStructure>? Children { get; set; }

    /// <summary>
    /// Each component has properties which are rendered to the properties menu for low code.
    /// </summary>
    public Dictionary<string, string>? Properties { get; set; }

    /// <summary>
    /// The name of the component which will appear in Component menu.
    /// </summary>
    public string? Name { get; set; }

    public ComponentType ComponentType = ComponentType.Component1;

}
