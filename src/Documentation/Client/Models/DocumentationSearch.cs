// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Documentation.Client.Models;

/// <summary>
/// Represents a documentation search
/// </summary>
public class DocumentationSearch
{
    /// <summary>
    /// The search query
    /// </summary>
    public string Query { get; set; } = string.Empty;
}
