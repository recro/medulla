
using System;
using App.Utils;

namespace App;

public enum KubeConfigLoadType {
    LoadLocal,
    LoadInCluster
    
}

public sealed class AppOptions {

    private KubeConfigLoadType _loadType { get; set; }
    private static AppOptions _instance; 


    private AppOptions() {
        _loadType = GetKubeConfigLoadType();
    }

    public static AppOptions GetInstance() {
        if (_instance == null) {
            _instance = new AppOptions();
        }
        return _instance;
    }



    private KubeConfigLoadType GetKubeConfigLoadType() {
        var loadType = Environment.GetEnvironmentVariable("KUBE_CONFIG_LOAD_TYPE");
        if (loadType == null) {
            Logger.Message("Environment variable was not found KUBE_CONFIG_LOAD_TYPE default setting is local");
        } else {
            if (loadType == "in-cluster") {
                return KubeConfigLoadType.LoadInCluster;
            } else if (loadType == "local") {
                return KubeConfigLoadType.LoadLocal;
            } else throw new Exception("Environment variable KUBE_CONFIG_LOAD_TYPE must be in-cluster or local");
        }
        return KubeConfigLoadType.LoadInCluster;
    }

    public bool IsKubernetesConfigInCluster() {
        return _loadType == KubeConfigLoadType.LoadInCluster;
    }

    public bool IsKubernetesConfigLocal() {
        return _loadType == KubeConfigLoadType.LoadLocal;
    }

}