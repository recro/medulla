

using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Controller;

namespace App.Controller;


public class KubernetesClient {

    protected IKubernetes _client { get; set; }

    public KubernetesClient(KubernetesClientConfiguration config) {

        // creating the k8s client
        IKubernetes client = new Kubernetes(config);
        _client = client;

        
    }

    
}
