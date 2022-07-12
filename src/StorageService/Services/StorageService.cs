using Grpc.Core;

namespace StorageService.Services;

public class StorageService : Storage.StorageBase
{
    private readonly ILogger<StorageService> _logger;

    public StorageService(ILogger<StorageService> logger)
    {
        _logger = logger;
    }

    public override Task<Response> saveObject(Object request, ServerCallContext context)
    {
        return base.saveObject(request, context);
    }

    public override Task<ObjectList> listObjects(SearchObject request, ServerCallContext context)
    {
        return base.listObjects(request, context);
    }

    public override Task<Response> deleteObject(ObjectId request, ServerCallContext context)
    {
        return base.deleteObject(request, context);
    }
}
