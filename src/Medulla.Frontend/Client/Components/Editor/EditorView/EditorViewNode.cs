// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Medulla.Frontend.Client.Components.Editor.EditorView
{
    public class EditorViewNode
    {
        public List<EditorViewNode>? Children { get; set; } = new();

        public string RenderComponentType { get; set; } = "RenderFragment";

        public Dictionary<string, object>? Parameters { get; set; } = new();

        public void BuildChildrenRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent(0, Type.GetType(RenderComponentType));

            foreach (var param in Parameters)
            {
                builder.AddAttribute(1, param.Key, param.Value);
            }
            
            foreach (var node in Children)
            {
                node.BuildChildrenRenderFragment(builder);
            }
            
            builder.CloseComponent();
        }
        

    }
}
