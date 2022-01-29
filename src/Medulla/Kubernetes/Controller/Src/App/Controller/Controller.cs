using App.Utils;
using k8s;

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
            config = KubernetesClientConfiguration.BuildDefaultConfig();
        } else {
            throw new System.Exception("Failed not appOptions.IsKubernetesConfigInCluster or appOptions.IsKubernetesConfigLocal");
        }

        Controller.StartWithConfig(config);

    }


    public static void StartWithConfig(KubernetesClientConfiguration config) {

    }

}
