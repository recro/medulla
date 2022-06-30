// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace Medulla.AppRenderer.Components;

/// <summary>
/// Component
/// </summary>
public partial class Component1
{

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

}
