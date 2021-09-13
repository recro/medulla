// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Medulla.Frontend.Client.Components.Editor.EditorView
{
    public class EditorRenderer: ComponentBase
    {
        
        [Parameter]
        public EditorStructure EditorStructure { get; set; } = default!;
        
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            EditorStructure.EditorViewNode.Render(builder, EditorStructure);
        }
    }
}
