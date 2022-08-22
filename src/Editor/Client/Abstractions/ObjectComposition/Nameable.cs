// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Abstractions.ObjectComposition;

/// <summary>
/// Provides a implementation for setting title, description.
/// A class that wants to have a title or description can include this class in its properties.
/// </summary>
public class Nameable
{
    /// <summary>
    /// Title of this object.
    /// </summary>
    public string Title { get; set; } = "No Title";

    /// <summary>
    /// Description of this object.
    /// </summary>
    public string Description { get; set; } = "No Description";


    public static Nameable NewNameable(string? title, string? description)
    {
        var nameable = new Nameable();
        if (title != null)
        {
            nameable.Title = title;
        }

        if (description != null)
        {
            nameable.Description = description;
        }

        return nameable;
    }

}
