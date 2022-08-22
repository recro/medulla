// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;

namespace StorageService.Kubernetes.Crds.Data;
/// <summary>
/// code will be deleted
/// </summary>
public class Utils
{
    // creats a CRD definition
    /// <summary>
    /// code will be deleted
    /// </summary>
    public static CustomResourceDefinition MakeCRD()
    {
        var myCRD = new CustomResourceDefinition()
        {
            Kind = "Data",
            Group = "medulla.recro.com",
            Version = "v1alpha1",
            PluralName = "data",
        };

        return myCRD;
    }

    // creats a CR instance
    /// <summary>
    /// code will be deleted
    /// </summary>
    public static CResource MakeCResource(string name, string _namespace, string uuid, string storageData, string type)
    {
        var myCResource = new CResource()
        {
            Kind = "GenericStorageObject",
            ApiVersion = "medulla.io/v1alpha1",
            Metadata = new V1ObjectMeta
            {
                Name = name,
                NamespaceProperty = _namespace,
                Labels = new Dictionary<string, string>
                {
                    {
                        "medulla-resource-type", "database-definition"
                    },
                },
            },
            Uuid = uuid,
            StorageData = storageData,
            Type = type,
        };
        return myCResource;
    }
}
