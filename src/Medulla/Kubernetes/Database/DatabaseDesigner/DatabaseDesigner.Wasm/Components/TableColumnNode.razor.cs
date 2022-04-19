using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;

namespace DatabaseDesigner.Wasm.Components
{
    public partial class TableColumnNode
    {
        [Parameter]
        public DatabaseDesigner.Core.Models.TableColumn Node { get; set; }
    }
}
