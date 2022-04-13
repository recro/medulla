const express = require('express')
var bodyParser = require('body-parser')

const app = express()

app.use(bodyParser.json());       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
    extended: true
}));

const { Sequelize, Model, DataTypes } = require('sequelize');

async function syncDatabase(data) {
    data = JSON.parse(data.data)
    console.log(data);

    const databases = data.Spec;

    for (let i = 0; i < databases.length; i++) {
        const { Name, Host: host, Dialect: dialect, UsernameSecretName, PasswordSecretName, Models } = databases[i];
        console.log(Name, host, dialect, UsernameSecretName, PasswordSecretName)
        const sequelize = new Sequelize(Name, UsernameSecretName, PasswordSecretName, {
            host,
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


app.post('/listen-for-database-schema', async function (req, res) {
    try {
        await syncDatabase(req.body)
    } catch (err) {
        console.error(err);
        return res.send(err);
    }
    res.send('Synced database')
})

app.listen(3000)