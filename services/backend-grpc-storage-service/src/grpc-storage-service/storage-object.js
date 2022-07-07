import {loadLocal} from "../kubernetes/load-cluster";
import {storageObject} from "../kubernetes/storage-object";




export const storageObject = async ({ request }, callback) => {
    console.log(request);
    await storageObject(request)

    callback(null, { message: "test"})
};
