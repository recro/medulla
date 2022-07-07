
import {CONFIG} from "../config";
import {loadLocal} from "./load-cluster";

export const storageObject = async (object) => {
    const apiObjects = await loadLocal();
    apiObjects.customObjectsApi.createNamespacedCustomObject('medulla.io', 'v1alpha1',
        CONFIG.namespaceForCustomObjects, 'storageobjects', {
            apiVersion: "medulla.io/v1alpha1",
            kind: "GenericStorageObject",
            metadata: {
                name: "generic-storage-object"
            },
            uuid: "test",
            storageData: "Test",
            type: "Test"
        }).then((e) => {
    }).catch((err) => {
        console.log("ERROR CREATING KUBERNETES OBJECT\n\n\n".repeat(10))
        console.log(err)
    })

};
