using Microsoft.AspNetCore.Components;

namespace Medulla.Editor.Client.Components.DatabaseDesigner;

public partial class TableNode
{
    [Parameter]
    public Table? Node { get; set; }
}
