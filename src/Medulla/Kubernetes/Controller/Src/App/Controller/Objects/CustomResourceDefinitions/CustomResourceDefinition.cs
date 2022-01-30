
using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controller;
using Newtonsoft.Json;
using App.Controller.Objects.CustomResourceDefinitions;

namespace App.Controller.Objects.CustomResourceDefinitions;


// [module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "This is just an example.")]
// [module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "This is just an example.")]

public class CustomResourceDefinition
{
    public string Version { get; set; }

    public string Group { get; set; }

    public string PluralName { get; set; }

    public string Kind { get; set; }

    public string Namespace { get; set; }
}

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


public class CRD : App.Controller.Objects.CustomResourceDefinitions.CustomResourceDefinitionClient {

    protected string _crdNamespace { get; set;}

    public CRD(KubernetesClientConfiguration config, string group, string version, string pluralName, string _namespace) : base(config, group, version, pluralName) {
        _crdNamespace = _namespace;
    }

    public async Task GetResources() {
        var crs = await _crdClient.ListNamespacedAsync<CustomResourceList<CResource>>(_crdNamespace ?? "default").ConfigureAwait(false);
        foreach (var cr in crs.Items)
        {
            Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
        }
    }

}
