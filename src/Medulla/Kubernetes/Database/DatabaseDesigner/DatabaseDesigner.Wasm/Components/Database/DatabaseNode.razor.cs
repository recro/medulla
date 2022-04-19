using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;
using DatabaseDesigner.Wasm.Components.Database.TableNode.TableColumnNode;

namespace DatabaseDesigner.Wasm.Components.Database
{
    public partial class DatabaseNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.Database Node { get; set; }
    }
}
