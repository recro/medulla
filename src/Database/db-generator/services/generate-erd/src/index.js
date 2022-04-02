
const express = require('express')
let bodyParser = require('body-parser')
let cors = require('cors')
const fs = require('fs')
const { exec } = require("child_process");
const app = express()

app.use(cors())

app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

app.post('/generate-erd', async (req, res) => {

    const { db } = req.body
    console.log(db)

    const data = db
    // const data = [
    //     {
    //         "name": "medulla",
    //         "host": "mysql-service",
    //         "dialect": "mysql",
    //         "usernameSecretName": "user",
    //         "passwordSecretName": "test",
    //         "models": [
    //             {
    //                 "name": "User",
    //                 "columns": [
    //                     {
    //                         "columnName": "email",
    //                         "type": "VARCHAR(250)",
    //                         "allowNull": true,
    //                         "defaultValue": "test@test.com",
    //                         "primaryKey": true,
    //                         "field": "email",
    //                         "unique": "email",
    //                         "comment": "This is the email field"
    //                     }
    //                 ]
    //             },
    //             {
    //                 "name": "Account",
    //                 "columns": [
    //                     {
    //                         "columnName": "phoneNumber",
    //                         "type": "VARCHAR(50)",
    //                         "allowNull": true,
    //                         "defaultValue": "",
    //                         "primaryKey": false,
    //                         "field": "phone_number",
    //                         "unique": "uniqueField",
    //                         "comment": "Stores the phone number for the user"
    //                     }
    //                 ]
    //             },
    //             {
    //                 "name": "Products",
    //                 "columns": [
    //                     {
    //                         "columnName": "productId",
    //                         "type": "INT(11)",
    //                         "allowNull": false,
    //                         "defaultValue": "1",
    //                         "primaryKey": true,
    //                         "field": "product_id",
    //                         "unique": "uniqueField",
    //                         "comment": "Product id for Product"
    //                     }
    //                 ]
    //             },
    //             {
    //                 "name": "Files",
    //                 "columns": [
    //                     {
    //                         "columnName": "fileName",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": false,
    //                         "defaultValue": "",
    //                         "primaryKey": true,
    //                         "field": "file_name",
    //                         "unique": "uniqueField",
    //                         "comment": "File"
    //                     }
    //                 ]
    //             },
    //             {
    //                 "name": "TestTable",
    //                 "columns": [
    //                     {
    //                         "columnName": "testColumn",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": false,
    //                         "defaultValue": "",
    //                         "primaryKey": true,
    //                         "field": "test_column",
    //                         "unique": "test",
    //                         "comment": "This is a test"
    //                     },
    //                     {
    //                         "columnName": "phoneNumber",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": true,
    //                         "defaultValue": "555-555-5555",
    //                         "primaryKey": false,
    //                         "field": "phone_number",
    //                         "unique": "phoneNumber",
    //                         "comment": "This is a phone number field"
    //                     },
    //                     {
    //                         "columnName": "phoneNumber0",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": true,
    //                         "defaultValue": "555-555-5555",
    //                         "primaryKey": false,
    //                         "field": "phone_number0",
    //                         "unique": "phoneNumber",
    //                         "comment": "This is a phone number field"
    //                     },
    //                     {
    //                         "columnName": "phoneNumber1",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": true,
    //                         "defaultValue": "555-555-5555",
    //                         "primaryKey": false,
    //                         "field": "phone_number1",
    //                         "unique": "phoneNumber",
    //                         "comment": "This is a phone number field"
    //                     },
    //                     {
    //                         "columnName": "phoneNumber2",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": true,
    //                         "defaultValue": "555-555-5555",
    //                         "primaryKey": false,
    //                         "field": "phone_number2",
    //                         "unique": "phoneNumber",
    //                         "comment": "This is a phone number field"
    //                     },
    //                     {
    //                         "columnName": "phoneNumber3",
    //                         "type": "VARCHAR(100)",
    //                         "allowNull": true,
    //                         "defaultValue": "555-555-5555",
    //                         "primaryKey": false,
    //                         "field": "phone_number3",
    //                         "unique": "phoneNumber",
    //                         "comment": "This is a phone number field"
    //                     }
    //                 ]
    //             }
    //         ]
    //     }
    // ]

    let prisma = `
    
    generator erd {
      provider = "prisma-erd-generator"
    }
    
    datasource db {
      provider = "postgres"
      url      = env("DATABASE_URL")
    }
    
    model Database {
      id Int @id
      database Int
    }
    
    
    `

    for (let i = 0; i < data.length; i++)
    {
        const db = data[i]
        const models = db.models

        for (let modelI = 0; modelI < models.length; modelI++) {
            const model = models[modelI]

            prisma += `
            
            
            
             model ${model.name} {
             id Int @id 
             database Database @relation(fields: [id], references: [id])
             
            `

            const columns = model.columns

            for (let columnI = 0; columnI < columns.length; columnI++) {

                const column = columns[columnI]
                prisma += `
                 ${column.field} String
                `


            }

            prisma += `
             }
             
            `

        }

    }

    fs.writeFileSync("schema.prisma", prisma);

    exec("npx prisma format", (error, stdout, stderr) => {
        if (error) {
            console.log(`error: ${error.message}`);
            return;
        }
        if (stderr) {
            console.log(`stderr: ${stderr}`);
            return;
        }

        exec("npx prisma generate", (error, stdout, stderr) => {
            if (error) {
                console.log(`error: ${error.message}`);
                return;
            }
            if (stderr) {
                console.log(`stderr: ${stderr}`);
                return;
            }

            res.send(
                fs.readFileSync('ERD.svg',
                    {encoding:'utf8', flag:'r'})  )

        });

    });



})

app.listen(3001)








