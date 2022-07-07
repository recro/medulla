import {loadLocal} from "../kubernetes/load-cluster";
import {createStorageObject} from "../kubernetes/create-storage-object";


const apiObjects = loadLocal();


export const saveObject = async ({ request }, callback) => {
    console.log(request);

    await createStorageObject(apiObjects)


    callback(null, { message: "test"})
};
