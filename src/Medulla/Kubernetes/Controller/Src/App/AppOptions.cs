
using App;
using System;

namespace App;

public enum KubeConfigLoadType {
    LoadInCluster,
    LoadLocal
}

public sealed class AppOptions {

    public static KubeConfigLoadType GetKubeConfigLoadType() {
        var loadType = Environment.GetEnvironmentVariable("KUBE_CONFIG_LOAD_TYPE");
        if (loadType == "in-cluster") {
            KubeConfigLoadType type = KubeConfigLoadType.LoadInCluster;
            return type;
        } else if (loadType == "local") {
            KubeConfigLoadType type = KubeConfigLoadType.LoadLocal;
            return type;
        } else throw new Exception("KubeConfigLoadType Failed env KUBE_CONFIG_LOAD_TYPE was not local or in-cluster");
    }

}