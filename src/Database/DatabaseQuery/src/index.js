
let PROTO_PATH = __dirname + '/query.proto';
let grpc = require('@grpc/grpc-js');
let protoLoader = require('@grpc/proto-loader');
const { Sequelize, Model, DataTypes } = require('sequelize');

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

async function dbConnect(database, user, password, host, dialect) {
    const sequelize = new Sequelize(database, user, password, {
        host,
        dialect
    });

    try {
        await sequelize.authenticate();
        console.log('Connection has been established successfully.');
    } catch (error) {
        console.error('Unable to connect to the database:', error);
        throw error
    }
    
    return sequelize
}



function Create(call, callback) {
    console.log(call.request)

    const { Database, User, Password, Host, Dialect } = call.request
    let sequelize = dbConnect(Database, User, Password, Host, Dialect)
    
    const data = call.request
    
    
    let modelObject = {}
    let keys = 

    const User = sequelize.define(data.Model.ModelName, {
        name: DataTypes.TEXT,
        favoriteColor: {
            type: DataTypes.TEXT,
            defaultValue: 'green'
        },
        age: DataTypes.INTEGER,
        cash: DataTypes.INTEGER
    });
    
    callback(null, { Model: "test", Database: "test database" })
}

function Get(call, callback) {
    call.on('data', function(getRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    });
}

function Delete(call, callback) {
    call.on('data', function(deleteRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    });
}

function Update(call, callback) {
    call.on('data', function(updateRequest) {
        console.log("received data")
        console.log(createRequest)
    });
    call.on('end', function() {
        console.log("ended data received")
        callback(null, {});
    });
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



