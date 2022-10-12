// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace Medulla.Editor.Client.AppRenderer.RendererComponents;

/// <summary>
/// Finds components to render.
/// </summary>
public class RenderComponentsFinder
{

    /// <summary>
    /// Get components to render.
    /// </summary>
    /// <returns></returns>
    public static List<ComponentBase> GetComponents()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(a => a.GetTypes());
        List<ComponentBase> renderComponents = (List<ComponentBase>)types.Where(t => t.GetInterfaces().Contains(typeof(ComponentBase)));
        /*foreach (var renderComponent in renderComponents)
        {
            var instance = Activator.CreateInstance((ComponentBase)renderComponent)!;
            renderComponents.Add((ComponentBase)instance);
        }*/

        return renderComponents;
    }

}
