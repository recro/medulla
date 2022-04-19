using Microsoft.AspNetCore.Components;
using Blazor.Diagrams.Core;
using DatabaseDesigner.Wasm.Components.Database;
using DatabaseDesigner.Wasm.Components.Database.TableNode;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;
using System.Threading;
using System;

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

        private bool SettingsOn { get; set; } = false;

        [Parameter]
        public Diagram Diagram { get; set; }

        [Parameter]
        public Action<Diagram> AddToScene { get; set; }

        private void Clicked() {
            if (InTray) {
                AddToScene(Diagram);
            }
        }

    }
}
