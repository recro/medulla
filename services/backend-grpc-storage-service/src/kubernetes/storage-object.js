
import {CONFIG} from "../config";
import {loadLocal} from "./load-cluster";


function errorWithKubernetes(err) {
    console.log("ERROR CREATING KUBERNETES OBJECT\n\n\n".repeat(10))
    console.log(err)
}

export const createStorageObject = async (object) => {
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
    }).catch(errorWithKubernetes)

};

export const getListOfStorageObjects = async (object) => {
    const apiObjects = await loadLocal();
    return new Promise((resolve) => {
        apiObjects.customObjectsApi.listNamespacedCustomObject('medulla.io', 'v1alpha1',
            CONFIG.namespaceForCustomObjects, 'storageobjects')
            .then((objects) => {
                console.log('-'.repeat(20))
                console.log(objects.body.items)
                resolve(objects.body.items);
        })
            .catch(errorWithKubernetes)
    })
};
