using App.Utils;

namespace App;

public sealed class Controller {

    public static void Start() {

        AppOptions appOptions = AppOptions.GetInstance();

        if (appOptions.IsKubernetesConfigInCluster()) {
            Logger.Message("Kube config is in-cluster");

        } else if (appOptions.IsKubernetesConfigLocal()) {
            Logger.Message("Kube config is local");

        }

    }

}
