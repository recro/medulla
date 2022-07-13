// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;

namespace Medulla.Editor.Client.Components.BlazorDiagramBase;

public class DataField
{
    public string Type { get; set; } = "string";
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}

public class DatabaseTableModel : NodeModel
{
    public bool Selected { get; set; }
    public string Name { get; set; } = "";
    public List<DataField> InputDataFields { get; set; } = new();
}
