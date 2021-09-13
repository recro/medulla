// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Medulla.Frontend.Client.Components.Editor.EditorView;

public class EditorRenderer : ComponentBase
{
    [Parameter]
    public EditorViewNode EditorViewNode { get; set; } = default!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var type = Type.GetType(EditorViewNode.Type);

        if (type == null)
            throw new NullReferenceException("Type is not expected to be null. The RenderComponentType was not found.");

        builder.OpenComponent(0, type);

        foreach (var (key, value) in EditorViewNode.Parameters)
            builder.AddAttribute(1, key, value);

        builder.AddAttribute(1, "EditorViewNode", EditorViewNode);

        if (EditorViewNode.Children.Count > 0)
        {
            builder.AddAttribute(2, "ChildContent", (RenderFragment)((childBuilder) =>
            {
                foreach (var child in EditorViewNode.Children)
                {
                    childBuilder.OpenComponent(0, typeof(EditorRenderer));
                    childBuilder.AddAttribute(1, "EditorViewNode", child);
                    childBuilder.CloseComponent();
                }
            }));
        }

        builder.CloseComponent();
    }
}
