// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.IdentityModel.Tokens;

namespace Operator;

internal abstract class ServiceUris
{

    private static string GetEnvNotEmpty(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var value = Environment.GetEnvironmentVariable(key);
        if (value.IsNullOrEmpty())
            throw new Exception($"{key} is null or empty");
        return value!;
    }

    public static string GetDatabaseServiceUri()
    {
        return $"{GetEnvNotEmpty("SERVICE__DATABASE-SERVICE__HTTP__PROTOCOL")}://{GetEnvNotEmpty("SERVICE__DATABASE-SERVICE__HTTP__HOST")}:{GetEnvNotEmpty("SERVICE__DATABASE-SERVICE__HTTP__PORT")}";
    }

    public static string GetDatabaseSyncUri()
    {
        return $"http://{GetEnvNotEmpty("SERVICE__DATABASE-SYNC__HTTP__HOST")}:{GetEnvNotEmpty("SERVICE__DATABASE-SYNC__HTTP__PORT")}";        
    }
    
    
    
    
    
}
