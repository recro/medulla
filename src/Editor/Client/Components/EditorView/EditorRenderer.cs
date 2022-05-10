// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Medulla.Editor.Client.Components.EditorView
{

    /// <summary>
    /// EditorRender renders editor
    /// </summary>
    public class EditorRenderer : ComponentBase
    {

        /// <summary>
        /// editor view node base node
        /// </summary>
        [Parameter]
        public EditorViewNode EditorViewNode { get; set; } = default!;

        /// <summary>
        /// Editor object
        /// </summary>
        [CascadingParameter]
        public EditorPage Editor { get; set; } = default!;

        /// <summary>
        /// Builds render tree
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var type = Type.GetType(EditorViewNode.Type);


            if (type == null)
            {
                Console.WriteLine("EditorViewNode type was null");
                return;
            }

            builder.OpenComponent(0, type);

            foreach (var (key, value) in EditorViewNode.Parameters)
            {
                builder.AddAttribute(1, key, value);
            }

            builder.AddAttribute(1, "EditorViewNode", EditorViewNode);
            builder.AddAttribute(2, "Editor", Editor);
            builder.AddAttribute(2, "UniqueId", EditorViewNode.UniqueId);

            if (EditorViewNode.Children.Count > 0)
            {
                builder.AddAttribute(3, "ChildContent", (RenderFragment)((childBuilder) =>
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

}
