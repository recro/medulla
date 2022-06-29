using Microsoft.AspNetCore.Components;
using System;

namespace DatabaseDesigner.Wasm.Components.Forms.Fields
{
    public partial class Input
    {

        [Parameter]
        public string Label { get; set; } = "[No Label]";

        private string Id { get; set; }

        public Input() 
        {
            Id = (Guid.NewGuid()).ToString();
        }

        [Parameter]
        public EventCallback WhenChanged { get; set; }


    }
}
