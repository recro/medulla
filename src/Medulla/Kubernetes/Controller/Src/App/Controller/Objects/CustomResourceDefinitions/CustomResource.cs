using k8s.Models;
using Newtonsoft.Json;
using App.Controller.Objects.CustomResourceDefinitions;

namespace App.Controller.Objects.CustomResourceDefinitions;

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
