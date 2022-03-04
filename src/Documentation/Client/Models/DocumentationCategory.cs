// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Documentation.Client.Models;

/// <summary>
/// Represents a documentation category
/// </summary>
public class DocumentationCategory
{
    /// <summary>
    /// The category name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The category site
    /// </summary>
    public DocumentationSite Site { get; init; } = new();

    /// <summary>
    /// The category title
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The category short title
    /// </summary>
    public string ShortTitle { get; init; } = string.Empty;

    /// <summary>
    /// The category intro
    /// </summary>
    public string Intro { get; init; } = string.Empty;

    /// <summary>
    /// The category pages
    /// </summary>
    public List<DocumentationPage> Pages { get; init; } = new();

    /// <summary>
    /// Represents a documentation category yaml file
    /// </summary>
    public class Yaml
    {
        /// <summary>
        /// The category title
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// The category short title
        /// </summary>
        public string ShortTitle { get; init; } = string.Empty;

        /// <summary>
        /// The category intro
        /// </summary>
        public string Intro { get; init; } = string.Empty;

        /// <summary>
        /// The category pages
        /// </summary>
        public List<string> Pages { get; init; } = new();
    }
}
