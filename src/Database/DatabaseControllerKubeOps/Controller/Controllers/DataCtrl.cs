
using KubeOps.Operator.Controller;
using KubeOps.Operator.Rbac;
using DatabaseControllerKubeOps.Controller.Entities;
using KubeOps.Operator.Controller.Results;
using k8s;
using k8s.Models;
using System.Text.Json;
using System.Threading;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcDatabaseService;
using Microsoft.Rest;

namespace DatabaseControllerKubeOps.Controller.Controllers;


internal class OnChange
{

    public static async void UpdateDatabase(V1Alpha1DataEntity entity)
    {
        try
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
        catch (HttpOperationException httpOperationException) when (httpOperationException.Message.Contains("422"))
        {
            var phase = httpOperationException.Response.ReasonPhrase;
            var content = httpOperationException.Response.Content;
            Console.WriteLine("response content: {0}", content);
            Console.WriteLine("response phase: {0}", phase);
        }
        catch (HttpOperationException e)
        {
            Console.WriteLine("In this exception catch");
            Console.WriteLine(e);
            Console.WriteLine(e.Body);
            Console.WriteLine(e.Response.ReasonPhrase);
            Console.WriteLine(e.Response.StatusCode);
            Console.WriteLine(e.Response.Content);
            Console.WriteLine(e.Response.Headers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}


[EntityRbac(typeof(V1Alpha1DataEntity), Verbs = RbacVerb.All)]
public class DataCtrl : IResourceController<V1Alpha1DataEntity>
{
    
    public Task<ResourceControllerResult> CreatedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("Created");

        Console.WriteLine("creating channel");
        var channel = GrpcChannel.ForAddress("http://localhost:5188");
        Console.WriteLine("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        Console.WriteLine("Client created");

        for (var i = 0; i < resource.Spec.Count; i++)
        {
            Console.WriteLine("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }
        
        Console.WriteLine("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);
        
        OnChange.UpdateDatabase(resource);
        
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> ReconcileAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("Created");

        Console.WriteLine("creating channel");
        var channel = GrpcChannel.ForAddress("http://internal-database-service:5188");
        Console.WriteLine("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        Console.WriteLine("Client created");

        for (var i = 0; i < resource.Spec.Count; i++)
        {
            Console.WriteLine("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }
        
        Console.WriteLine("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);
        
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
