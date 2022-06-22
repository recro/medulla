// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Medulla.AppRenderer;
using Medulla.AppRenderer.Components;

namespace Medulla.AppRenderer.Blazor.Components;

/// <summary>
/// Renders Components from a base IComponent.
/// </summary>
public partial class ComponentRenderer
{

    /// <summary>
    /// Base component which will render all components.
    /// </summary>
    [Parameter]
    public Core.Abstractions.IComponent? BaseComponent { get; set; }


}
