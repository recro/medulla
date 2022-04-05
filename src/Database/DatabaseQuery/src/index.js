
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


function Create(call, callback) {
    call.on('data', function(createRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    }
}

function Get(call, callback) {
    call.on('data', function(getRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    }
}

function Delete(call, callback) {
    call.on('data', function(deleteRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    }
}

function Update(call, callback) {
    call.on('data', function(updateRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    }
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



