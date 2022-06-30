// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Medulla.AppRenderer.Components;
using Microsoft.AspNetCore.Components;

namespace Medulla.AppRenderer.Core;

/// <summary>
/// Gets all components that exist in Namespace Medulla.AppRenderer.Core with Reflection.
/// </summary>
public class ComponentLibrary
{
    /// <summary>
    /// Gets a list of FoundComponents with Reflection. Any component that has the attribute Component.
    /// </summary>
    /// <returns>List of ComponentBase</returns>
    public static List<ComponentBase> GetComponents()
    {
        return new List<ComponentBase>()
        {
            new Component1()
        };
    }

}
