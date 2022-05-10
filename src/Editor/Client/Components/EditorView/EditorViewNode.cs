// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using Medulla.Editor.Client.Library.Utilities.Unique;

namespace Medulla.Editor.Client.Components.EditorView;

/// <summary>
/// Editor View Node
/// </summary>
public class EditorViewNode
{
    /// <summary>
    ///  This class performs an important function.
    /// </summary>
    public string Type { get; set; } = "RenderFragment";

    /// <summary>
    /// Children
    /// </summary>
    public readonly List<EditorViewNode?> Children = new();

    /// <summary>
    /// Parameters
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();

    /// <summary>
    /// Unique Id
    /// </summary>
    public UniqueId? UniqueId { get; set; }

    /// <summary>
    /// Is Container
    /// </summary>
    public bool IsContainer { get; set; } = false;


    /// <summary>
    /// Get Json
    /// </summary>
    public string GetJson()
    {
        var childrenJson = new List<string?>();
        for (var i = 0; i < Children?.Count; i++)
        {
            var childJson = Children?[i]?.GetJson();
            childrenJson.Add(childJson);
        }

        var childrenJsonObjects = string.Join(',', childrenJson);
        var childrenJsonArray = $"[ {childrenJsonObjects} ]";
        var json = JsonSerializer.Serialize(Parameters);

        return $"{{  " +
               $"\"Type\": \"{Type}\", " +
               $"\"Children\": {childrenJsonArray}, " +
               $"\"Parameters\": {json}, " +
               $"\"UniqueId\": \"{UniqueId?.Id}\", " +
               $"\"IsContainer\": {IsContainer.ToString().ToLower()} " +
               $" }}";

    }

}
