using k8s.Models;

namespace StorageService.Kubernetes.Crds.Data;

public class Utils
{
    // creats a CRD definition
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
