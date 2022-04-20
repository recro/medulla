using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;
using Blazor.Diagrams.Core;
using System;

namespace DatabaseDesigner.Wasm.Components.Database
{
    public partial class DatabaseNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.Database Node { get; set; }

        [Parameter]
        public bool InTray { get; set; } = false;

        [Parameter]
        public Diagram Diagram { get; set; }

        
        [Parameter]
        public Action<Diagram> AddToScene { get; set; }

        private Entity Entity { get; set; }

        public void QuickLoadingOnEntity() => Entity.QuickLoading();

        [Parameter]
        public RenderFragment Preview { get; set; }

    }
}
