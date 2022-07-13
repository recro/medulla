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

    public override async Task<Response> saveObject(Object request, ServerCallContext context)
    {
        await Task.Delay(1000);
        Actions.Create(request.Name, "default", request.Uuid, request.StorageData, request.Type);
        return new Response() {Message = "Created"};
    }

    public override async Task<ObjectList> listObjects(SearchObject request, ServerCallContext context)
    {
        var crds = await Actions.Get();
        //return base.listObjects(request, context);
        RepeatedField<Object> objects = new RepeatedField<Object>();
        foreach (var crd in crds.Items!)
        {
            objects.Add(new Object()
            {
                Name = crd?.Metadata?.Name,
                Type = crd?.Type,
                Uuid = crd?.Uuid,
                StorageData = crd?.StorageData
            });
        }

        return new ObjectList() { Objects = { objects }};
    }

    public override Task<Response> deleteObject(ObjectId request, ServerCallContext context)
    {
        return base.deleteObject(request, context);
    }
}
