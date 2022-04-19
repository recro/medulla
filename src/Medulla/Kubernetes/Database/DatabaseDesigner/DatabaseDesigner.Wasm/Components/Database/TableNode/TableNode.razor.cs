using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;

namespace DatabaseDesigner.Wasm.Components.Database.TableNode
{
    public partial class TableNode
    {
        [Parameter]
        public Table Node { get; set; }
        [Parameter]
        public bool InTray { get; set; } = false;
    }
}
