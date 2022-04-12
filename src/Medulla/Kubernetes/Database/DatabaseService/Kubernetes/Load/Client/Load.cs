using k8s;

namespace DatabaseService.Kubernetes.Load.Client;

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