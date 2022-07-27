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
var routeguide = protoDescriptor.query;


const Get = ({ request }, call) => {
    console.log(request)
    console.log("Get");
    call("test")
}

const List = () => {
    console.log(request)
    console.log("List");
    call("test")
}

const Create = () => {
    console.log(request)
    console.log("Create");
    call("test")
}

const Update = () => {
    console.log(request)
    console.log("Update");
    call("test")
}

const Delete = () => {
    console.log(request)
    console.log("Delete");
    call("test")
}



function getServer() {
    var server = new grpc.Server();
    console.log(routeguide)
    server.addService(routeguide.QuerySvc.service, {
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



