// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Medulla.AppRenderer;
using Medulla.AppRenderer.Components;
using Medulla.AppRenderer.Core.Abstractions;

namespace Medulla.AppRenderer.Blazor.Components;

/// <summary>
/// Renders Components from a base IComponentStructure.
/// </summary>
public partial class ComponentRenderer
{

    /// <summary>
    /// Base component which will render all components.
    /// </summary>
    [Parameter]
    public ComponentStructure? ComponentStructure { get; set; }

    private DynamicComponent Render()
    {
        var dynamicComponent = new DynamicComponent() {Type = typeof(Component1)};
        if (ComponentStructure != null)
        {
            dynamicComponent.Parameters = new Dictionary<string, object>();
            dynamicComponent.Parameters["ChildContent"] = ComponentStructure.GetDynamicComponent();
        }


        /*var dictionary = new Dictionary<string, object>();
        dictionary["ChildContent"] = new ComponentRenderer()
        {
            ComponentStructure =
        }
        return dictionary;*/
        return dynamicComponent;
    }


}
