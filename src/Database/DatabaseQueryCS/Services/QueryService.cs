using Grpc.Core;
using DatabaseQueryCS;

namespace DatabaseQueryCS.Services;

public class DatabaseQuery : DbQuery.DbQueryBase
{
    private readonly ILogger<DatabaseQuery> _logger;

    public DatabaseQuery(ILogger<DatabaseQuery> logger)
    {
        _logger = logger;
    }

    public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        Console.WriteLine("Create request");
        return Task.FromResult(new CreateResponse() {});
    }

    public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
    {
        return base.Get(request, context);
    }


    public override Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        return base.Delete(request, context);
    }

    public override Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        return base.Update(request, context);
    }
    
}
