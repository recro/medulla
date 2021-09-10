// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Rendering;

namespace Medulla.Frontend.Client.Components.Editor.EditorView
{
    public class EditorViewNode
    {
        public readonly List<EditorViewNode> Children = new();

        public string RenderComponentType { get; set; } = "RenderFragment";

        public Dictionary<string, object> Parameters { get; set; } = new();

        public void BuildChildrenRenderFragment(RenderTreeBuilder builder)
        {
            var type = Type.GetType(RenderComponentType);
            if (type == null)
            {
                throw new NullReferenceException(
                    "Type is not expected to be null. The RenderComponentType was not found.");
            }
            builder.OpenComponent(0, type);

            foreach (var (key, value) in Parameters)
            {
                builder.AddAttribute(1, key, value);
            }
            
            foreach (var node in Children)
            {
                node.BuildChildrenRenderFragment(builder);
            }
            
            builder.CloseComponent();
        }
        

    }
}
