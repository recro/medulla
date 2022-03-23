
using KubeOps.Operator.Controller;
using KubeOps.Operator.Rbac;
using DatabaseControllerKubeOps.Controller.Entities;
using KubeOps.Operator.Controller.Results;
using k8s;
using k8s.Models;
using System.Text.Json;
using System.Threading;

namespace DatabaseControllerKubeOps.Controller.Controllers;


internal class OnChange
{

    public static async void UpdateDatabase(V1Alpha1DataEntity entity)
    {
        var values = new Dictionary<string, string>
        {
            { "data", JsonSerializer.Serialize(entity) },
        };
        var content = new FormUrlEncodedContent(values);

        HttpClient httpClient = new HttpClient();

        var response = await httpClient.PostAsync("http://database-sync-service:3000/listen-for-database-schema", content);
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);

    }
}


[EntityRbac(typeof(V1Alpha1DataEntity), Verbs = RbacVerb.All)]
public class DataCtrl : IResourceController<V1Alpha1DataEntity>
{
    
    public Task<ResourceControllerResult> CreatedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("Created");
        OnChange.UpdateDatabase(resource);
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> ReconcileAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("ReconcileAsync");
        OnChange.UpdateDatabase(resource);
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> StatusModifiedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("StatusModifiedAsync");
        OnChange.UpdateDatabase(resource);
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> DeletedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("DeletedAsync");
        return Task.FromResult<ResourceControllerResult>(null);
    }

}