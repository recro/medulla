// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Components.Editor.EditorView;

public class EditorViewNode
{
    public string Type { get; set; } = "RenderFragment";

    public readonly List<EditorViewNode> Children = new();
    public Dictionary<string, object> Parameters { get; set; } = new();
}
