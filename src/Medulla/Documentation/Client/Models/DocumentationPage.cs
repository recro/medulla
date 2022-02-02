// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Markdig;
using Microsoft.AspNetCore.Components;

namespace Medulla.Documentation.Client.Models;

/// <summary>
/// Represents a documentation page
/// </summary>
public class DocumentationPage
{
    /// <summary>
    /// The page name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The page category
    /// </summary>
    public DocumentationCategory Category { get; init; } = new();

    /// <summary>
    /// The page title
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The page short title
    /// </summary>
    public string ShortTitle { get; init; } = string.Empty;

    /// <summary>
    /// The page intro
    /// </summary>
    public string Intro { get; init; } = string.Empty;

    /// <summary>
    /// The page headers
    /// </summary>
    public List<(string Id, string Value)> Headers { get; init; } = new();

    /// <summary>
    /// The page content
    /// </summary>
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// Gets the Html for the page
    /// </summary>
    public MarkupString Html => (MarkupString)Markdown.ToHtml(Content, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

    /// <summary>
    /// Represents a documentation page yaml
    /// </summary>
    public class Yaml
    {
        /// <summary>
        /// The page title
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// The page short title
        /// </summary>
        public string ShortTitle { get; init; } = string.Empty;

        /// <summary>
        /// The page intro
        /// </summary>
        public string Intro { get; init; } = string.Empty;
    }
}
