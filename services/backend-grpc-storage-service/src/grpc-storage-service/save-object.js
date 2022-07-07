import {loadLocal} from "../kubernetes/load-cluster";
import {createStorageObject} from "../kubernetes/create-storage-object";




export const saveObject = async ({ request }, callback) => {
    console.log(request);
    await createStorageObject(request)

    callback(null, { message: "test"})
};
