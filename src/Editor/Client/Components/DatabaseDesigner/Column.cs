// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.
using System;

namespace Medulla.Editor.Client.Components.DatabaseDesigner;

public class Column
{
    public event Action? Changed;

    public string? Name { get; set; }
    public ColumnType Type { get; set; }
    public bool Primary { get; set; }

    public void Refresh() => Changed?.Invoke();
}

public enum ColumnType
{
    Boolean,
    Char,
    String,
    SByte,
    Short,
    Integer,
    Long
}
