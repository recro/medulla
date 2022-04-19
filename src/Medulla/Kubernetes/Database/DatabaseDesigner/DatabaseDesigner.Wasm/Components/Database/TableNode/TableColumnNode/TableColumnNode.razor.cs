using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;
using Blazor.Diagrams.Core;
using System;

namespace DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode
{
    public partial class TableColumnNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.TableColumn Node { get; set; }
        
        [Parameter]
        public bool InTray { get; set; } = false;
        
        [Parameter]
        public Diagram Diagram { get; set; }

        [Parameter]
        public Action<Diagram> AddToScene { get; set; }

    }
}
