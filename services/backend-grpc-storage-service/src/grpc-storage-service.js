
import grpc from '@grpc/grpc-js';
import protoLoader from '@grpc/proto-loader';
import { saveObject } from "./grpc-storage-service/save-object";

function getServer(routeguide) {
    const server = new grpc.Server();
    server.addService(routeguide.RouteGuide.service, {
        saveObject
    });
    return server;
}

function createPackageDefinition() {
    const PROTO_PATH = __dirname + './storage.proto';

    const packageDefinition = protoLoader.loadSync(
        PROTO_PATH,
        {keepCase: true,
            longs: String,
            enums: String,
            defaults: true,
            oneofs: true
        });
    const protoDescriptor = grpc.loadPackageDefinition(packageDefinition);
    const routeguide = protoDescriptor.routeguide;
    return routeguide;
}


export const run = async () => {
    const server = getServer(createPackageDefinition())
    server.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
        server.start();
    });
};
