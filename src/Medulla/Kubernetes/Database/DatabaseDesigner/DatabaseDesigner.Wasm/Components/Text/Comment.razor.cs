using Microsoft.AspNetCore.Components;
using System;

namespace DatabaseDesigner.Wasm.Components.Text
{
    public partial class Comment
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

    }
}
