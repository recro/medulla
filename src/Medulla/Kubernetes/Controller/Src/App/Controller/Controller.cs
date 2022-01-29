using App.Utils;

namespace App.Controller;

public sealed class Controller {

    public static void Start() {

        AppOptionsSingleton appOptions = AppOptionsSingleton.GetInstance();

        if (appOptions.IsKubernetesConfigInCluster()) {
            Logger.Message("Kube config is in-cluster");

        } else if (appOptions.IsKubernetesConfigLocal()) {
            Logger.Message("Kube config is local");

        }

    }

}
