const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader')
import { saveObject, listObjects } from "./grpc-storage-service/storage-object";


function getServer(routeguide) {
    const server = new grpc.Server();
    server.addService(routeguide.Storage.service, {
        saveObject,
        listObjects
    });
    return server;
}

function createPackageDefinition() {
    const PROTO_PATH = __dirname + '/storage.proto';
    const packageDefinition = protoLoader.loadSync(
        PROTO_PATH,
        {keepCase: true,
            longs: String,
            enums: String,
            defaults: true,
            oneofs: true
        });
    return grpc.loadPackageDefinition(packageDefinition);
}

export const SERVER_ADDRESS = '0.0.0.0:50051';

export const run = async () => {
    const server = getServer(createPackageDefinition())
    server.bindAsync(SERVER_ADDRESS, grpc.ServerCredentials.createInsecure(), () => {
        server.start();
    });
};
