using GrpcDatabaseService;
using k8s.Models;

namespace DatabaseService.Kubernetes.Crds.Data;

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
    public static CResource MakeCResource(string name, string _namespace, List<Kubernetes.Crds.Data.Databases> databases)
    {
        var myCResource = new CResource()
        {
            Kind = "Data",
            ApiVersion = "medulla.recro.com/v1alpha1",
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
            Databases = databases
        };
        return myCResource;
    }
}