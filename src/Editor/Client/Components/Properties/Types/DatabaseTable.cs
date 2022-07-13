// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.Properties.Types;

public class Column
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsUnique { get; set; } = false;
}

public class DatabaseTable
{
    public string Name { get; set; } = "";
    public List<Column> Columns { get; set; } = new();
}

