using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;
using Blazor.Diagrams.Core;
using System;

namespace DatabaseDesigner.Wasm.Components.Database.TableNode
{
    public partial class TableNode
    {
        [Parameter]
        public Table Node { get; set; }
        [Parameter]
        public bool InTray { get; set; } = false;

        [Parameter]
        public Diagram Diagram { get; set; }

        
        [Parameter]
        public Action<Diagram> AddToScene { get; set; }
    }
}
