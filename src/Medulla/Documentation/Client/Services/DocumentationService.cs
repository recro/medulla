// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using Medulla.Documentation.Client.Models;
using Microsoft.AspNetCore.Components;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Medulla.Documentation.Client.Services;

/// <summary>
/// Provides the ability to get markdown based documentation sites
/// </summary>
public class DocumentationService
{
    /// <summary>
    /// The documentation site
    /// </summary>
    public DocumentationSite Site { get; private set; } = new();

    /// <summary>
    /// True if the documentation service is ready
    /// </summary>
    public bool Ready { get; private set; } = false;

    /// <summary>
    /// The http client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// The http client
    /// </summary>
    private readonly NavigationManager _navigation;

    /// <summary>
    /// Creates an instance of the documentation service
    /// </summary>
    /// <param name="httpClient">The http client</param>
    /// <param name="navigation">The navigation manager</param>
    public DocumentationService(HttpClient httpClient, NavigationManager navigation)
    {
        _httpClient = httpClient;
        _navigation = navigation;
    }

    /// <summary>
    /// Initializes the documentation service
    /// </summary>
    /// <param name="basePath">The documentation base path</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException">Occurs when a referenced documentation file is not found</exception>
    /// <exception cref="FileLoadException">Occurs when front matter content is missing from a page</exception>
    public async Task InitializeAsync(string basePath)
    {
        Ready = false;

        var request = await _httpClient.GetAsync(Path.Combine(basePath, "site.yaml"));

        if (!request.IsSuccessStatusCode)
        {
            throw new FileNotFoundException("Could not find the site.yaml file.", Path.Combine(basePath, "site.yaml"));
        }

        var yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var markdownPipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseYamlFrontMatter()
            .Build();

        var siteCategories = new List<DocumentationCategory>();
        var siteYamlString = await request.Content.ReadAsStringAsync();
        var siteYaml = yamlDeserializer.Deserialize<DocumentationSite.Yaml>(siteYamlString);

        var documentationSite = new DocumentationSite()
        {
            Title = siteYaml.Title,
            Description = siteYaml.Description,
            Author = siteYaml.Author,
            Product = siteYaml.Product,
            Github = siteYaml.Github,
            Categories = siteCategories,
            RepoDocumentationPath = siteYaml.RepoDocumentationPath
        };

        foreach (var category in siteYaml.Categories)
        {
            request = await _httpClient.GetAsync(Path.Combine(basePath, category, "category.yaml"));

            if (!request.IsSuccessStatusCode)
            {
                throw new FileNotFoundException("Could not find the category.yaml file.", Path.Combine(basePath, category, "category.yaml"));
            }

            var categoryPages = new List<DocumentationPage>();
            var categoryYamlString = await request.Content.ReadAsStringAsync();
            var categoryYaml = yamlDeserializer.Deserialize<DocumentationCategory.Yaml>(categoryYamlString);

            var documentationCategory = new DocumentationCategory()
            {
                Name = category,
                Site = documentationSite,
                Title = categoryYaml.Title,
                ShortTitle = categoryYaml.ShortTitle,
                Intro = categoryYaml.Intro,
                Pages = categoryPages
            };

            foreach (var page in categoryYaml.Pages)
            {
                request = await _httpClient.GetAsync(Path.Combine(basePath, category, $"{page}.md"));

                if (!request.IsSuccessStatusCode)
                {
                    throw new FileNotFoundException("Could not find the .md file.", Path.Combine(basePath, category, $"{page}.md"));
                }

                var pageString = await request.Content.ReadAsStringAsync();
                var pageMarkdown = Markdown.Parse(pageString, markdownPipeline);
                var pageYamlFrontMatter = pageMarkdown.Descendants<YamlFrontMatterBlock>().FirstOrDefault();
                var pageHeaders = pageMarkdown.Descendants<HeadingBlock>();

                if (pageYamlFrontMatter == null)
                {
                    throw new FileLoadException("Could not find the front matter for the page.", Path.Combine(basePath, category, $"{page}.md"));
                }

                var pageYamlString = pageString.Substring(pageYamlFrontMatter.Span.Start, pageYamlFrontMatter.Span.Length - 3);
                var pageYaml = yamlDeserializer.Deserialize<DocumentationPage.Yaml>(pageYamlString);

                categoryPages.Add(new DocumentationPage()
                {
                    Name = page,
                    Category = documentationCategory,
                    Title = pageYaml.Title,
                    ShortTitle = pageYaml.ShortTitle,
                    Intro = pageYaml.Intro,
                    Headers = pageHeaders.Select(h =>
                    {
                        return (
                            Regex.Replace(pageString.Substring(h.Span.Start + h.Level + 1, h.Span.Length - h.Level - 1).Replace(' ', '-').ToLower(), "[^a-zA-Z0-9_.-]+", string.Empty).Replace("--", "-"),
                            pageString.Substring(h.Span.Start + h.Level + 1, h.Span.Length - h.Level - 1)
                        );
                    }).ToList(),
                    Content = pageString[(pageYamlFrontMatter.Span.End + 1)..].TrimStart(new char[] { '\r', '\n' })
                });
            }

            siteCategories.Add(documentationCategory);
        }

        Site = documentationSite;

        Ready = true;
    }

    /// <summary>
    /// Checks if a category is active
    /// </summary>
    /// <param name="category">The category</param>
    /// <returns>If the category is active</returns>
    public bool IsCategoryActive(DocumentationCategory category)
    {
        return _navigation.Uri.Replace(_navigation.BaseUri, string.Empty).StartsWith(category.Name);
    }

    /// <summary>
    /// Checks if a page is active
    /// </summary>
    /// <param name="page">The page</param>
    /// <returns>If the page is active</returns>
    public bool IsPageActive(DocumentationPage page)
    {
        return _navigation.Uri.Replace(_navigation.BaseUri, string.Empty).EndsWith(page.Name);
    }

    /// <summary>
    /// Gets the github edit link for the current page
    /// </summary>
    /// <returns></returns>
    public string GetGithubEditLink()
    {
        var uriParts = _navigation.Uri.Replace(_navigation.BaseUri, string.Empty).Split('/');
        var basePath = $"{Site.Github}/edit/main{Site.RepoDocumentationPath}";

        if (uriParts.Length == 1 && !string.IsNullOrWhiteSpace(uriParts[0]) && uriParts[0] != "search")
        {
            return $"{basePath}{uriParts[0]}/category.yaml";
        }
        else if (uriParts.Length == 2)
        {
            return $"{basePath}{uriParts[0]}/{uriParts[1]}.md";
        }
        else
        {
            return $"{basePath}site.yaml";
        }
    }

    /// <summary>
    /// Gets a documentation category
    /// </summary>
    /// <param name="category">The category</param>
    /// <returns>The category</returns>
    public DocumentationCategory? GetCategory(string category)
    {
        var result = Site.Categories.Where(c => c.Name == category).FirstOrDefault();

        if (result == null)
        {
            return null;
        }
        else
        {
            return result;
        }
    }

    /// <summary>
    /// Gets a documentation page
    /// </summary>
    /// <param name="category">The category</param>
    /// <param name="page">The page</param>
    /// <returns>The page</returns>
    public DocumentationPage? GetPage(string category, string page)
    {
        var categoryResult = GetCategory(category);

        if (categoryResult == null)
        {
            return null;
        }
        else
        {
            var result = categoryResult.Pages.Where(p => p.Name == page).FirstOrDefault();

            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
    }

    /// <summary>
    /// Searches for pages matching a search query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public IEnumerable<DocumentationPage> Search(string query)
    {
        var lowerQuery = query.ToLower();

        return Site.Categories.SelectMany(c => c.Pages)
            .Where(p => p.Title.ToLower().Contains(lowerQuery) || p.Intro.ToLower().Contains(lowerQuery) || p.Content.ToLower().Contains(lowerQuery));
    }
}
