using App.Utils;
using k8s;
using System;
using k8s.Models;
using App.Controller.Objects.CustomResourceDefinitions;

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
        CRD crd = new CRD(config, "pages", "v1", "pages", "default");
        await crd.GetResources();

    }

}
