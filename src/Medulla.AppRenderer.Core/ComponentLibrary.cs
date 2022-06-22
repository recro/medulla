// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Medulla.AppRenderer.Core.Abstractions;

namespace Medulla.AppRenderer.Core;

/// <summary>
/// FoundComponent implements the interface IComponent and is what ComponentLibrary returns.
/// </summary>
public class FoundComponent : IComponent
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
    /// Reference to Component to be used in DynamicComponent as Type.
    /// </summary>
    public Type? ComponentTypeReference { get; set; }
}

/// <summary>
/// Gets all components that exist in Namespace Medulla.AppRenderer.Core with Reflection.
/// </summary>
public class ComponentLibrary
{

    /// <summary>
    /// Gets a list of FoundComponents with Reflection. Any component that has the attribute Component.
    /// </summary>
    /// <returns>List of IComponent</returns>
    public static List<IComponent> GetComponents()
    {
        const string Nspace = "Medulla.AppRenderer.Components";

        var q = from t in Assembly.GetExecutingAssembly().GetTypes()
            where t.IsClass && t.Namespace == Nspace
            select t;
        q.ToList().ForEach(t => Console.WriteLine(t.Name));

        List<IComponent?> comps = new();
        q.ToList().ForEach(t => comps.Add(t.FullName == null ? null : new FoundComponent()
        {
            Name = t.Name,
            ComponentTypeReference = Type.GetType(t.FullName),
            Properties = new Dictionary<string, object>(),
            Children = new List<IComponent>(){},
        }));

        comps.RemoveAll((t) => t == null);
        List<IComponent> compsNotNull = new();
        

        return comps;
    }
}
