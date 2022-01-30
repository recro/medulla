
using k8s;
using System;
using k8s.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

public abstract class CustomResource : KubernetesObject
{
    [JsonProperty(PropertyName = "metadata")]
    public V1ObjectMeta Metadata { get; set; }
}

public abstract class CustomResource<TSpec, TStatus> : CustomResource
{
    [JsonProperty(PropertyName = "spec")]
    public TSpec Spec { get; set; }

    [JsonProperty(PropertyName = "CStatus")]
    public TStatus CStatus { get; set; }
}

public class CustomResourceList<T> : KubernetesObject
where T : CustomResource
{
    public V1ListMeta Metadata { get; set; }
    public List<T> Items { get; set; }
}

public class CustomResourceDefinition
{
    public string Version { get; set; }

    public string Group { get; set; }

    public string PluralName { get; set; }

    public string Kind { get; set; }

    public string Namespace { get; set; }
}

public class CResource : CustomResource<CResourceSpec, CResourceStatus>
{
    public override string ToString()
    {
        var labels = "{";
        foreach (var kvp in Metadata.Labels)
        {
            labels += kvp.Key + " : " + kvp.Value + ", ";
        }

        labels = labels.TrimEnd(',', ' ') + "}";

        return $"{Metadata.Name} (Labels: {labels}), Spec: {Spec.CityName}";
    }
}


public class CResourceSpec
{
    [JsonProperty(PropertyName = "cityName")]
    public string CityName { get; set; }
}

public class CResourceStatus : V1Status
{
    [JsonProperty(PropertyName = "temperature", NullValueHandling = NullValueHandling.Ignore)]
    public string Temperature { get; set; }
}


class Controller {

    public static async Task Main() {
        var k8SClientConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile();
        IKubernetes client = new Kubernetes(k8SClientConfig);

        var myCRD = new CustomResourceDefinition()
            {
                Kind = "CResource",
                Group = "csharp.com",
                Version = "v1alpha1",
                PluralName = "customresources",
            };

        var myCr = new CResource()
            {
                Kind = "CResource",
                ApiVersion = "csharp.com/v1alpha1",
                Metadata = new V1ObjectMeta
                {
                    Name = "cr-instance-london",
                    NamespaceProperty = "default",
                    Labels = new Dictionary<string, string>
                    {
                        {
                            "identifier", "city"
                        },
                    },
                },
                // spec
                Spec = new CResourceSpec
                {
                    CityName = "London",
                },
            };

        Console.WriteLine("working with CRD: {0}.{1}", myCRD.PluralName, myCRD.Group);
        var generic = new GenericClient(client, myCRD.Group, myCRD.Version, myCRD.PluralName);
        Console.WriteLine("CR list:");
        var crs = await generic.ListNamespacedAsync<CustomResourceList<CResource>>(myCr.Metadata.NamespaceProperty ?? "default").ConfigureAwait(false);
        foreach (var cr in crs.Items)
        {
            Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
        }
    }

}