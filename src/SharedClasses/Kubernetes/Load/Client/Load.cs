// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s;

namespace SharedClasses.Kubernetes.Load.Client;

public class Load
{

    public static IKubernetes GetClient()
    {

        var loadInCluster = Environment.GetEnvironmentVariable("LOAD_KUBERNETES_IN_CLUSTER");
        if (loadInCluster == "yes")
        {
            var k8SClientConfig = KubernetesClientConfiguration.InClusterConfig();
            IKubernetes client = new k8s.Kubernetes(k8SClientConfig);
            return client;
        }
        else
        {
            var k8SClientConfig = KubernetesClientConfiguration.BuildDefaultConfig();
            IKubernetes client = new k8s.Kubernetes(k8SClientConfig);
            return client;
        }
    }
}
