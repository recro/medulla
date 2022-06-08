// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.DatabaseDesigner;

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using System.Collections.Generic;
using System.Linq;

public class Table : NodeModel
{
    public Table(Point? position = null) : base(position, RenderLayer.HTML)
    {
        Columns = new List<Column>
        {
            new Column
            {
                Name = "Id",
                Type = ColumnType.Integer,
                Primary = true
            },
            new Column
            {
                Name = "Test",
                Type = ColumnType.Integer
            }
        };

        AddPort(Columns[0], PortAlignment.Right);
        AddPort(Columns[1], PortAlignment.Left);
    }

    public string Name { get; set; } = "Table";
    public List<Column> Columns { get; }
    public bool HasPrimaryColumn => Columns.Any(c => c.Primary);

    public ColumnPort? GetPort(Column column) => Ports!.Cast<ColumnPort>()!.FirstOrDefault(p => p!.Column == column);

    public void AddPort(Column column, PortAlignment alignment) => AddPort(new ColumnPort(this, column, alignment));


}
