using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;

namespace DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode
{
    public partial class TableColumnNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.TableColumn Node { get; set; }
        
        [Parameter]
        public bool InTray { get; set; } = false;
    }
}
