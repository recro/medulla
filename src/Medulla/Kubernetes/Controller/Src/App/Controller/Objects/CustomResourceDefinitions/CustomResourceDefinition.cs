
using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controller;

namespace App.Controller.Objects.CustomResourceDefinitions;


public class CustomResourceDefinition : App.Controller.KubernetesClient {

    protected GenericClient _crdClient { get; set; }

    public CustomResourceDefinition(KubernetesClientConfiguration config, string group, string version, string pluralName) : base(config) {
        var crdClient = new GenericClient(config, group, version, pluralName);
        _crdClient = crdClient;
    }

    public async void GetResources() {
        var crs = await generic.ListNamespacedAsync<CustomResourceList<CResource>>(myCr.Metadata.NamespaceProperty ?? "default").ConfigureAwait(false);
        foreach (var cr in crs.Items)
        {
            Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
        }
    }


}
