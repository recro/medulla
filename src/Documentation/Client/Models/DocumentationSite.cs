// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Documentation.Client.Models;

/// <summary>
/// Represents a documentation site
/// </summary>
public class DocumentationSite
{
    /// <summary>
    /// The site title
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The site description
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The site author
    /// </summary>
    public string Author { get; init; } = string.Empty;

    /// <summary>
    /// The site product
    /// </summary>
    public string Product { get; init; } = string.Empty;

    /// <summary>
    /// The github url
    /// </summary>
    public string Github { get; init; } = string.Empty;

    /// <summary>
    /// The repo documentation path
    /// </summary>
    public string RepoDocumentationPath { get; init; } = string.Empty;

    /// <summary>
    /// The site categories
    /// </summary>
    public List<DocumentationCategory> Categories { get; init; } = new();

    /// <summary>
    /// Represents a documentation site yaml file
    /// </summary>
    public class Yaml
    {
        /// <summary>
        /// The site title
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// The site description
        /// </summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// The site author
        /// </summary>
        public string Author { get; init; } = string.Empty;

        /// <summary>
        /// The site product
        /// </summary>
        public string Product { get; init; } = string.Empty;

        /// <summary>
        /// The github url
        /// </summary>
        public string Github { get; init; } = string.Empty;

        /// <summary>
        /// The repo documentation path
        /// </summary>
        public string RepoDocumentationPath { get; init; } = string.Empty;

        /// <summary>
        /// The site categories
        /// </summary>
        public List<string> Categories { get; init; } = new();
    }
}
