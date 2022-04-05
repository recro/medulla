
let PROTO_PATH = __dirname + '/query.proto';
let grpc = require('@grpc/grpc-js');
let protoLoader = require('@grpc/proto-loader');

// implement grpc api and sequelize
let packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    });

let protoDescriptor = grpc.loadPackageDefinition(packageDefinition);
// The protoDescriptor object has the full package hierarchy
console.log(protoDescriptor)
let routeguide = protoDescriptor.routeguide;

console.log(packageDefinition);


function Create() {
    
}

function Get() {
    
}

function Delete() {
    
}

function Update() {
    
}


function getServer() {
    var server = new grpc.Server();
    server.addService(protoDescriptor.DbQuery.service, {
        Create,
        Get,
        Delete,
        Update,
    });
    return server;
}



var routeServer = getServer();
routeServer.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
    console.log("starting server")
    routeServer.start();
});



