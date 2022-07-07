import {createStorageObject, getListOfStorageObjects} from "../kubernetes/storage-object";


export const saveObject = async ({ request }, callback) => {
    console.log(request);
    await createStorageObject(request)

    callback(null, { message: "created storage object"})
};

export const listObjects = async ({ request }, callback) => {
    console.log(request);
    const list = await getListOfStorageObjects(request)
    callback(null, { list })
}
