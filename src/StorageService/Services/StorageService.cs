using Google.Protobuf.Collections;
using Grpc.Core;
using StorageService.Kubernetes.Crds.Data;

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
        Actions.Create(request.Name, "default", request.Uuid, request.StorageData, request.Type);
        return Task.FromResult(new Response() {Message = "Created"});
    }

    public override ObjectList listObjects(SearchObject request, ServerCallContext context)
    {
        Actions.Get();
        //return base.listObjects(request, context);
        var objectList = new ObjectList();
        RepeatedField<Object> objects = new RepeatedField<Object>();

        return new ObjectList() { Objects = { objects }};
    }

    public override Task<Response> deleteObject(ObjectId request, ServerCallContext context)
    {
        return base.deleteObject(request, context);
    }
}
