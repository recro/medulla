
# Test Create Database
`grpc_cli call localhost:5188 CreateDatabases --json_input '{"database":[{"name":"medulla","dialect":"mysql","models":[{"name":"User","column":[{"columnName":"test","type":"VARCHAR(100)","allowNull":true,"defaultValue":"test","primaryKey":false,"fieldName":"test","unique":"test","comment":"test"}]}]}]}' --protofiles=Protos/database.proto`

# Test Get Databases
`grpc_cli call localhost:5188 GetDatabases --json_input "{}" --protofiles=Protos/database.proto`

# Test Delete Database
`grpc_cli call localhost:5188 DeleteDatabases --json_input "{'name': 'database-wownzwsxok'}" --protofiles=Protos/database.proto`

# Test Delete Database
`grpc_cli call localhost:5188 CreateDatabaseResources --json_input "{'name': 'test', 'user': 'test', 'password': 'test', 'serviceName':'test', 'databaseName':'test' }" --protofiles=Protos/database.proto`


-----

# Run database locally 
`kubectl run some-mysql --env MYSQL_ROOT_PASSWORD=my-secret-pw --image=mysql:latest --labels="medulla-resource-type=database"`



# Search for databases by label
`kubectl get pods -l medulla-resource-type=database -o json`