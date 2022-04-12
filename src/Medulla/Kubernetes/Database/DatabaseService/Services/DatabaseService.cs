using DatabaseService.Kubernetes.Crds.Data;
using Grpc.Core;
using GrpcDatabaseService;

namespace DatabaseService.Services;

public class DatabaseService : DatabaseSvc.DatabaseSvcBase
{
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(ILogger<DatabaseService> logger)
    {
        _logger = logger;
    }

    public override async Task<CreateDatabaseResourcesResponse> CreateDatabaseResources(CreateDatabaseResourcesRequest request, ServerCallContext context)
    {
        var isCreated = await Kubernetes.Database.Database.Create(request);
        return new CreateDatabaseResourcesResponse()
        {
            IsCreated = isCreated
        };
    }

    public override async Task<GetDatabasesResponse> GetDatabases(GetDatabasesRequest request, ServerCallContext context)
    {
        Console.WriteLine("Running Get Databases");
        var crs = await Kubernetes.Crds.Data.Actions.Get();
        return Load.GetDatabasesFromCrd(crs);
    }
    
    

    public override Task<CreateDatabasesResponse> CreateDatabases(CreateDatabasesRequest request,
        ServerCallContext context)
    {
        Console.WriteLine("Running Create Databases");
        if (request.Database.Count == 0)
            throw new Exception("Databases are empty");

        var databases = Load.GetDatabasesFromDatabaseRequest(request);
        
        // Create Custom Crd
        Kubernetes.Crds.Data.Actions.Create(databases[0].Name, databases);
        
        return Task.FromResult(new CreateDatabasesResponse()
        {
            IsCreated = true
        });
    }

    public override async Task<DeleteDatabasesResponse> DeleteDatabases(DeleteDatabasesRequest request, ServerCallContext context)
    {
        Console.WriteLine("Running Delete Databases");

        var isDeleted = await Kubernetes.Crds.Data.Actions.Delete(request.Name);
        
        return new DeleteDatabasesResponse
        {
            IsDeleted = isDeleted
        };
    }
    
    
    
    
}