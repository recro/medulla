using App.Utils;
using k8s;
using System;
using k8s.Models;

namespace App.Controller;

public sealed class Controller {

    public static void Start() {

        AppOptionsSingleton appOptions = AppOptionsSingleton.GetInstance();

        KubernetesClientConfiguration config;

        if (appOptions.IsKubernetesConfigInCluster()) {
            Logger.Message("Kube config is in-cluster");
            config = KubernetesClientConfiguration.InClusterConfig();

        } else if (appOptions.IsKubernetesConfigLocal()) {
            Logger.Message("Kube config is local");
            config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
        } else {
            throw new System.Exception("Failed not appOptions.IsKubernetesConfigInCluster or appOptions.IsKubernetesConfigLocal");
        }

        Controller.StartWithConfig(config);

    }


    private async static void StartWithConfig(KubernetesClientConfiguration config) {

        IKubernetes client = new Kubernetes(config);

        // var podlistResp = client.ListNamespacedService("default", watch: true);
        // C# 8 required https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/november/csharp-iterating-with-async-enumerables-in-csharp-8
        // await foreach (var (type, item) in podlistResp.)
        // {
        //     Console.WriteLine("==on watch event==");
        //     Console.WriteLine(type);
        //     Console.WriteLine(item.Metadata.Name);
        //     Console.WriteLine("==on watch event==");
        // }

    }

}
