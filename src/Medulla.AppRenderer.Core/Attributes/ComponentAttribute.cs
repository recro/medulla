// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.AppRenderer.Core.Attributes;

/// <summary>
/// Tells reflection apis that the class is a component.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ComponentAttribute : Attribute
{
    /// <summary>
    /// Name of Component
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructor for Component Attribute
    /// </summary>
    public ComponentAttribute(string name)
    {
        Name = name;
    }

}
