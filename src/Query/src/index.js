const PROTO_PATH = __dirname + '/../query.proto';
let grpc = require('@grpc/grpc-js');
let protoLoader = require('@grpc/proto-loader');

// Suggested options for similarity to existing grpc.load behavior
var packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    });
var protoDescriptor = grpc.loadPackageDefinition(packageDefinition);
// The protoDescriptor object has the full package hierarchy
var routeguide = protoDescriptor.routeguide;


const Get = ({ request }, call) => {
    console.log("Get");
    call("test")
}

const List = () => {
    console.log("List");
    call("test")
}

const Create = () => {
    console.log("Create");
    call("test")
}

const Update = () => {
    console.log("Update");
    call("test")
}

const Delete = () => {
    console.log("Delete");
    call("test")
}



function getServer() {
    var server = new grpc.Server();
    server.addService(routeguide.RouteGuide.service, {
        Get,
        List,
        Create,
        Update,
        Delete
    });
    return server;
}

let routeServer = getServer();
routeServer.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
    console.log("Starting server");
    routeServer.start();
});



