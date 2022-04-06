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
}
