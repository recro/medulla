﻿// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.GrpcServices.ObjectStorageApi;

using Medulla.MedullaObjects;

public class ObjectManager
{
    public static async Task<SaveResponse> SaveObject(SaveRequest request)
    {
        switch (request.Type)
        {
            case "MedullaAction":

                break;
            case "MedullaPage":

                break;
        }
        return new SaveResponse();
    }

}