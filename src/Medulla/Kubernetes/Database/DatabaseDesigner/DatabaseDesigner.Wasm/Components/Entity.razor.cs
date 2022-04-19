using Microsoft.AspNetCore.Components;

namespace DatabaseDesigner.Wasm.Components
{
    public partial class Entity
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        [Parameter]
        public bool InTray { get; set; } = false;

        [Parameter]
        public string Name { get; set; } = "[Name]";

        [Parameter]
        public string Image { get; set; } = "#";
    }
}
