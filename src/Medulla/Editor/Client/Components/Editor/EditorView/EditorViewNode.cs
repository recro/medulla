// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Medulla.Frontend.Client.Library.Utilities.Unique;


public class EditorViewNode
{
    /// <summary>
    ///  This class performs an important function.
    /// </summary>
    public string Type { get; set; } = "RenderFragment";

    public readonly List<EditorViewNode?> Children = new();
    public Dictionary<string, object> Parameters { get; set; } = new();

    public UniqueId? UniqueId { get; set; }

    public bool IsContainer { get; set; } = false;


    public string GetJson()
    {
        List<string> childrenJson = new List<string>();
        for (var i = 0; i < Children.Count; i++)
        {
            string childJson = Children[i].GetJson();
            childrenJson.Add(childJson);
        }

        string childrenJsonObjects = String.Join(',', childrenJson);
        string childrenJsonArray = $"[ {childrenJsonObjects} ]";
        string json = JsonSerializer.Serialize(Parameters);

        return $"{{  " +
               $"\"Type\": \"{Type}\", " +
               $"\"Children\": {childrenJsonArray}, " +
               $"\"Parameters\": {json}, " +
               $"\"UniqueId\": \"{UniqueId.Id}\", " +
               $"\"IsContainer\": {IsContainer.ToString().ToLower()} " +
               $" }}";

    }

}
