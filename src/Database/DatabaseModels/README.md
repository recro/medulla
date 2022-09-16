

# Ran the below commands to initialize the local sqlite db
`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

`dotnet ef dbcontext scaffold "Filename=../medulla.db" Microsoft.EntityFrameworkCore.Sqlite --namespace Packt.Shared --data-annotations`
