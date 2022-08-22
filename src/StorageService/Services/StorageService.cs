// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Google.Protobuf.Collections;
using Grpc.Core;
using StorageService.Kubernetes.Crds.Data;

namespace StorageService.Services;

/// <summary>
/// code will be deleted
/// </summary>
public class StorageService : Storage.StorageBase
{
    private readonly ILogger<StorageService> _logger;

    /// <summary>
    /// code will be deleted
    /// </summary>
    public StorageService(ILogger<StorageService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// code will be deleted
    /// </summary>
    public override async Task<Response> saveObject(Object request, ServerCallContext context)
    {
        await Task.Delay(1000);
        Actions.Create(request.Name, "default", request.Uuid, request.StorageData, request.Type);
        return new Response() { Message = "Created" };
    }

    /// <summary>
    /// code will be deleted
    /// </summary>
    public override async Task<ObjectList> listObjects(SearchObject request, ServerCallContext context)
    {
        var crds = await Actions.Get();
        //return base.listObjects(request, context);
        var objects = new RepeatedField<Object>();
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

        return new ObjectList() { Objects = { objects } };
    }

    /// <summary>
    /// code will be deleted
    /// </summary>
    public override Task<Response> deleteObject(ObjectId request, ServerCallContext context)
    {
        return base.deleteObject(request, context);
    }
}
