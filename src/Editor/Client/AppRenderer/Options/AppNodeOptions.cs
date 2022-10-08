// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.AppRenderer.Options;

/// <summary>
/// Stores list of Key value pairs.
/// </summary>
public class AppNodeOptions
{

    /// <summary>
    /// List of Key value pairs
    /// </summary>
    /// <value></value>
    public List<Pair>? Options { get; set; }

    /// <summary>
    /// Get the value of the key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetOption(string key)
    {
        Console.WriteLine("GetOption key: " + key);
        if (Options == null) {
            return "Options is null not set";
        }
        if (Options.Count == 0) {
            return "Options is empty";
        }
        Console.WriteLine("Options count: " + Options.Count);
        var contentItem = Options?.Find((i) => i?.Key?.KeyName == key);
        if (contentItem == null) {
            return "Key Not Found";
        }
        string? content = contentItem?.Value?.ValueOfKey.ToString();
        if (content == null) {
            return "No Content";
        }

        return content;
    }


}

