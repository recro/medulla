


# How to deploy

### Make sure you change the host ip in the operator kustomization.yaml file

### Deploy Operator
`kubectl apply -k C:\Users\keith\RiderProjects\medulla\src\Medulla\Kubernetes\Operator\config\install\`

### Deploy Medulla
`helm install medulla C:\Users\keith\RiderProjects\medulla\src\LocalDeploy\deploy\helm\medulla\`

### Delete Medulla
`helm delete medulla`

### Upgrade Medulla

`helm upgrade medulla C:\Users\keith\RiderProjects\medulla\src\LocalDeploy\deploy\helm\medulla\`

#### Connecting to DatabaseService

##### Connecting is best done through Postman

`url: 35.188.20.183:5188`
`Proto file: C:\Users\keith\RiderProjects\medulla\src\Medulla\Kubernetes\Database\DatabaseService\Protos`

#### GetDatabases
`{}`


#### CreateDatabases
```
{
    "database": [
        {
            "models": [
                {
                    "name": "Account",
                    "column": [
                        {
                            "fieldName": "firstName",
                            "primaryKey": false,
                            "unique": "firstName",
                            "type": "VARCHAR(100)",
                            "columnName": "firstName",
                            "defaultValue": "",
                            "comment": "First Name",
                            "allowNull": false
                        }
                    ]
                }
            ],
            "name": "medulla",
            "dialect": "mysql"
        }
    ]
}
```


#### Edit env vars for Operator 
`C:\Users\keith\RiderProjects\medulla\src\Medulla\Kubernetes\Operator\config\operator\kustomization.yaml`


#### Build and Push Operator Image
`C:\Users\keith\RiderProjects\medulla\src\Medulla\Kubernetes\Operator\build.sh`











