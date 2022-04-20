using Microsoft.AspNetCore.Components;
using Blazor.Diagrams.Core;
using DatabaseDesigner.Wasm.Components.Database;
using DatabaseDesigner.Wasm.Components.Database.TableNode;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace DatabaseDesigner.Wasm.Components
{
    public partial class Entity
    {

        [Parameter]
        public RenderFragment SettingsForm { get; set; }

        [Parameter]
        public RenderFragment NodePorts { get; set; }
        
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

        [Parameter]
        public string SettingsTitle { get; set; } = "[Settings Title]";

        private bool IsLoading { get; set; } = false;

        public async void QuickLoading()
        {
            IsLoading = true;
            await Task.Delay(1000);
            IsLoading = false;
        }

        private void Clicked() {
            if (InTray) {
                AddToScene(Diagram);
            }
        }

    }
}
