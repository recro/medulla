// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Models;
using System.Text.Json.Serialization;

namespace Medulla.Editor.Client.Components.DatabaseDesigner;

public class ColumnPort : PortModel
{
    public ColumnPort(NodeModel parent, Column column, PortAlignment alignment = PortAlignment.Bottom)
        : base(parent, alignment, null, null)
    {
        Column = column;
    }

    [JsonIgnore]
    public Column Column { get; }

    public override bool CanAttachTo(PortModel port)
    {
        // Avoid attaching to self port/node
        if (!base.CanAttachTo(port))
            return false;

        var targetPort = port as ColumnPort;
        var targetColumn = targetPort!.Column;

        if (Column.Type != targetColumn.Type)
            return false;

        if (Column.Primary && targetColumn.Primary)
            return false;

        if (Column.Primary && targetPort.Links.Count > 0 ||
            targetColumn.Primary && Links.Count > 1) // Ongoing link
            return false;

        return true;
    }
}
