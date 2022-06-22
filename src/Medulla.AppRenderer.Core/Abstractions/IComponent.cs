// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.AppRenderer.Core.Abstractions;

/// <summary>
/// A Component is any visible Blazor component that has properties, children,
/// </summary>
public interface IComponent
{
    /// <summary>
    /// Children of IComponent are IComponents which are rendered into a dom tree.
    /// </summary>
    public List<IComponent>? Children { get; set; }

    /// <summary>
    /// Each component has properties which are rendered to the properties menu for low code.
    /// </summary>
    public Dictionary<string, object>? Properties { get; set; }

    /// <summary>
    /// The name of the component which will appear in Component menu.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Reference to Component to be used in DynamicComponent as Type
    /// </summary>
    public Type? ComponentTypeReference { get; set; }
}
