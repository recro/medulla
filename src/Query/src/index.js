const PROTO_PATH = __dirname + '/../query.proto';
let grpc = require('@grpc/grpc-js');
let protoLoader = require('@grpc/proto-loader');

const { Sequelize, QueryTypes } = require('sequelize');


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

const { DATABASE, USERNAME, PASSWORD, HOST } = process.env


function generateSQLSelect(column, comparison, modelName, value) {

    return "SELECT * FROM `users`"
}


const Get = async ({ request }, call) => {

    const sequelize = new Sequelize(DATABASE, USERNAME, PASSWORD, {
        host: HOST,
        dialect: 'mysql'
    });


    try {
        await sequelize.authenticate();
        console.log('Connection has been established successfully.');
    } catch (error) {
        console.error('Unable to connect to the database:', error);
    }

    const { Column, Comparison, ModelName, Value } = request;

    const users = await sequelize.query(generateSQLSelect(Column, Comparison, ModelName, Value), { type: QueryTypes.SELECT });

    console.log(users);

    let record = {
        Record: {
            Columns: [
                {
                    Name: "Test",
                    Value: "Test",
                    ValueType: "String"
                }
            ]
        }
    }




    sequelize.close();



    call(null, record)
}

const List = ({ request }, call) => {
    call(null, {
        Record: {
            Columns: [
                {
                    Name: "Test",
                    Value: "Test",
                    ValueType: "String"
                }
            ]
        }
    })
}

const Create = ({ request }, call) => {
    call(null, {
        Record: {
            Columns: [
                {
                    Name: "Test",
                    Value: "Test",
                    ValueType: "String"
                }
            ]
        }
    })
}

const Update = ({ request }, call) => {
    call(null, {
        Record: {
            Columns: [
                {
                    Name: "Test",
                    Value: "Test",
                    ValueType: "String"
                }
            ]
        }
    })
}

const Delete = ({ request }, call) => {
    call(null, {
        Record: {
            Columns: [
                {
                    Name: "Test",
                    Value: "Test",
                    ValueType: "String"
                }
            ]
        }
    })
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



