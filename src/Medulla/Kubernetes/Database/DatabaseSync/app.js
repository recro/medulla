const express = require('express')
var bodyParser = require('body-parser')

const app = express()

app.use(bodyParser.json());       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
    extended: true
}));

const { Sequelize, Model, DataTypes } = require('sequelize');

async function syncDatabase(data) {
    // log all env variables

    console.log("___________________LOGGING ALL ENV VARS_____________________________")
    let keys = Object.keys(process.env);
    for (let i = 0; i < keys.length; i++) {
        console.log({ [keys[i]]: process.env[keys[i]] });
    }
    console.log("___________________AFTER LOGGING ALL ENV VARS_____________________________")



    const { DATABASE_USERNAME, DATABASE_PASSWORD, SERVICE__MYSQL__HOST,
        MYSQL_MYSQL_SERVICE_PORT } = process.env;


    data = JSON.parse(data.data)
    console.log(data);

    const databases = data.Spec;

    for (let i = 0; i < databases.length; i++) {

        const { Name, Host: host, Dialect: dialect, UsernameSecretName, PasswordSecretName, Models } = databases[i];

        console.log("CONNECTING ON HOST " + SERVICE__MYSQL__HOST)
        await resetDatabase(Name, DATABASE_USERNAME, DATABASE_PASSWORD,
            SERVICE__MYSQL__HOST, 3306)

        console.log(Name, host, dialect, UsernameSecretName, PasswordSecretName)
        console.log("CONNECTING ON HOST " + SERVICE__MYSQL__HOST)
        const sequelize = new Sequelize(Name, DATABASE_USERNAME, DATABASE_PASSWORD, {
            host: SERVICE__MYSQL__HOST,
            dialect,
            port: 3306
        });

        try {
            await sequelize.authenticate();
            console.log('Connection has been established successfully.');
        } catch (error) {
            console.error('Unable to connect to the database:', error);
        }


        for (let modelIndex = 0; modelIndex < Models.length; modelIndex++) {
            console.log(Models[modelIndex]);

            let initObject = { };
            for (let columnIndex = 0; columnIndex < Models[modelIndex].Columns.length; columnIndex++) {
                initObject[Models[modelIndex].Columns[columnIndex].ColumnName]
                    = Models[modelIndex].Columns[columnIndex];
            }



            let keys = Object.keys(initObject);
            for (let keysIndex = 0; keysIndex < keys.length; keysIndex++) {

                let subKeys = Object.keys(initObject[keys[keysIndex]])

                for (let subKeysIndex = 0; subKeysIndex < subKeys.length; subKeysIndex++) {


                    const toLowerSubProperty = subKeys[subKeysIndex][0].toLowerCase() + subKeys[subKeysIndex].slice(1)
                    const deleteSubProperty = subKeys[subKeysIndex][0].toUpperCase() + subKeys[subKeysIndex].slice(1)

                    initObject[keys[keysIndex]][(toLowerSubProperty)]
                        = initObject[keys[keysIndex]][subKeys[subKeysIndex]]

                    delete initObject[keys[keysIndex]][(deleteSubProperty)]

                }

            }


            keys = Object.keys(initObject);
            for (let keysIndex = 0; keysIndex < keys.length; keysIndex++) {
                if (typeof initObject[keys[keysIndex]] !== "object") {
                    delete initObject[keys[keysIndex]]
                }
            }

            console.log("initializing table", Models[modelIndex].Name)
            console.log({ initObject })

            const Model = sequelize.define(Models[modelIndex].Name, initObject, {
                sequelize, // We need to pass the connection instance
                modelName: Models[modelIndex].Name // We need to choose the model name
                // Other model options go here
            });

            await Model.sync({ force: true })
        }
    }

}

function resetDatabase(dbName, user, password, host, port) {
    return new Promise((resolve, reject) => {
        try {
            const mysql = require('mysql2/promise');

            console.log({ dbName, user, password, host, port })
    
            mysql.createConnection({
                user,
                password,
                host,
                port,
            }).then((connection) => {
                console.log("successfully connected to database")
                console.log("droping database if exists " + dbName)
                // reset database
                connection.query(`DROP DATABASE IF EXISTS ${dbName};`).then(() => {
                    console.log("database dropped");
                    console.log("creating database " + dbName)
                    // create database
                    connection.query(`CREATE DATABASE IF NOT EXISTS ${dbName};`).then(() => {
                        console.log("creating database complete");
                        resolve(true);
                    })
                })
                
            })
        } catch (err) {
            reject(err)
        }
    })
}


app.post('/listen-for-database-schema', async function (req, res) {
    try {
        await syncDatabase(req.body)
    } catch (err) {
        console.error(err);
        return res.send(err);
    }
    res.send('Synced database')
})

app.post('/create-database', async function (req, res) {
    try {
        const { dbName } = req.body;
        if (!dbName) {
            res.send("Must specify name of database to create");
        }
        await createDatabase(dbName);
    } catch (err) {
        console.error(err);
        return res.send(err);
    }
});

app.listen(3000, () => console.log("listening on port 3000"))


