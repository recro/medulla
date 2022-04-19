using DatabaseDesigner.Core.Models;
using Microsoft.AspNetCore.Components;

namespace DatabaseDesigner.Wasm.Components
{
    public partial class DatabaseNode
    {
        [Parameter]
        public Database Node { get; set; }
    }
}
