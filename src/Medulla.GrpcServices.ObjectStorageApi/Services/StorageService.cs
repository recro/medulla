// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Grpc.Core;

namespace Medulla.GrpcServices.ObjectStorageApi.Services;

using ObjectStorageApi;

public class StorageService : Storage.StorageBase
{
    public override Task<SaveResponse> SaveObject(SaveRequest request, ServerCallContext context)
    {
        return ObjectManager.SaveObject(request);
    }

    public override Task<GetObjectResponse> GetObject(GetObjectRequest request, ServerCallContext context)
    {
        return base.GetObject(request, context);
    }
}
