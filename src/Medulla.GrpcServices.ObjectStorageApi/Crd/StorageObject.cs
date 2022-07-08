// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.


[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This is just an example.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "This is just an example.")]

namespace Medulla.GrpcServices.ObjectStorageApi.Crd;

using System.Text.Json.Serialization;
using Grpc.Core;
using k8s.Models;
using k8s;
using k8s.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;


public class CustomResourceDefinition
{
    public string? Version { get; set; }

    public string? Group { get; set; }

    public string? PluralName { get; set; }

    public string? Kind { get; set; }

    public string? Namespace { get; set; }
}

public abstract class CustomResource : KubernetesObject
{
    [JsonPropertyName("metadata")]
    public V1ObjectMeta? Metadata { get; set; }
}

public abstract class CustomResource<TSpec, TStatus> : CustomResource
{
    [JsonPropertyName("spec")]
    public TSpec? Spec { get; set; }

    [JsonPropertyName("CStatus")]
    public TStatus? CStatus { get; set; }
}

public class CustomResourceList<T> : KubernetesObject
    where T : CustomResource
{
    public V1ListMeta? Metadata { get; set; }
    public List<T>? Items { get; set; }
}

public class CResource : CustomResource<CResourceSpec, CResourceStatus>
{
    public override string ToString()
    {
        var labels = "{";
        foreach (var kvp in Metadata?.Labels!)
        {
            labels += kvp.Key + " : " + kvp.Value + ", ";
        }

        labels = labels.TrimEnd(',', ' ') + "}";

        return $"{Metadata.Name} (Labels: {labels}), Spec: {Spec?.CityName}";
    }
}

public class CResourceSpec
{
    [JsonPropertyName("cityName")]
    public string? CityName { get; set; }
}

public class CResourceStatus : V1Status
{
    [JsonPropertyName("temperature")]
    public string? Temperature { get; set; }
}
