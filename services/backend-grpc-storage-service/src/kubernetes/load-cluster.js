const k8s = require('@kubernetes/client-node');
import {CustomObjectsApi} from "@kubernetes/client-node";

function load(load) {
    const kc = new k8s.KubeConfig();

    load(kc)

    const k8sApi = kc.makeApiClient(k8s.CoreV1Api);
    const customObjectsApi = kc.makeApiClient(CustomObjectsApi)

    return { k8sApi, customObjectsApi }
}

export const loadCluster = async () => {
    return load((kc) => {
        kc.loadFromCluster();
    })
};

export const loadLocal = async () => {
    return load((kc) => {
        kc.loadFromDefault();
    })
};

