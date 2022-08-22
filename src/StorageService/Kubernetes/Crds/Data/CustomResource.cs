// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using k8s;
using k8s.Models;

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This is just an example.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "This is just an example.")]

namespace StorageService.Kubernetes.Crds.Data;


/// <summary>
/// delete code
/// </summary>
public class CustomResourceDefinition
{
    /// <summary>
    /// delete code
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    public string? Group { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    public string? PluralName { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    public string? Kind { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    public string? Namespace { get; set; }
}

/// <summary>
/// delete code
/// </summary>
public abstract class CustomResource : KubernetesObject
{
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("metadata")]
    public V1ObjectMeta? Metadata { get; set; }
}

/// <summary>
/// delete code
/// </summary>
public abstract class CustomResource<TSpec, TStatus> : CustomResource
{
    /*[JsonPropertyName("spec")]
    public List<Databases>? Databases { get; set; }*/

    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("CStatus")]
    public TStatus? CStatus { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("Uuid")]
    public string? Uuid { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("Type")]
    public string? Type { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("StorageData")]
    public string? StorageData { get; set; }
}

/// <summary>
/// delete code
/// </summary>
public class CustomResourceList<T> : KubernetesObject
    where T : CustomResource
{
    /// <summary>
    /// delete code
    /// </summary>
    public V1ListMeta? Metadata { get; set; }

    /// <summary>
    /// delete code
    /// </summary>
    public List<T>? Items { get; set; }
}
