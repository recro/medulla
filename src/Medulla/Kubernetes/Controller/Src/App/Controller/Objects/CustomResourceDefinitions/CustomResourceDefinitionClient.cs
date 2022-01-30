
using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controller;

namespace App.Controller.Objects.CustomResourceDefinitions;


public class CustomResourceDefinitionClient : App.Controller.KubernetesClient {

    protected GenericClient _crdClient { get; set; }

    public CustomResourceDefinitionClient(KubernetesClientConfiguration config, string group, string version, string pluralName) : base(config) {
        var crdClient = new GenericClient(config, group, version, pluralName);
        _crdClient = crdClient;
    }

}
