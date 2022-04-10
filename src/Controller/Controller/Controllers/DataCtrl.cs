
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

    public static async void UpdateDatabase(V1Alpha1DataEntity entity, ILogger<DataCtrl> _iLogger)
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
            _iLogger.LogInformation(responseString);
        }
        catch (HttpOperationException httpOperationException) when (httpOperationException.Message.Contains("422"))
        {
            var phase = httpOperationException.Response.ReasonPhrase;
            var content = httpOperationException.Response.Content;
            _iLogger.LogInformation("response content: {0}", content);
            _iLogger.LogInformation("response phase: {0}", phase);
        }
        catch (HttpOperationException e)
        {
            _iLogger.LogInformation("In this exception catch");
            _iLogger.LogInformation(e.ToString());
            _iLogger.LogInformation(e.Body.ToString());
            _iLogger.LogInformation(e.Response.ReasonPhrase);
            _iLogger.LogInformation(e.Response.StatusCode.ToString());
            _iLogger.LogInformation(e.Response.Content.ToString());
            _iLogger.LogInformation(e.Response.Headers.ToString());
        }
        catch (Exception e)
        {
            _iLogger.LogInformation(e.ToString());
        }
    }
}


[EntityRbac(typeof(V1Alpha1DataEntity), Verbs = RbacVerb.All)]
public class DataCtrl : IResourceController<V1Alpha1DataEntity>
{

    private ILogger<DataCtrl> _iLogger { get; set; }

    public Task<ResourceControllerResult> CreatedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger.LogInformation("Created");

        _iLogger.LogInformation("creating channel");
        var channel = GrpcChannel.ForAddress("http://localhost:5188");
        _iLogger.LogInformation("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        _iLogger.LogInformation("Client created");

        for (var i = 0; i < resource.Spec.Count; i++)
        {
            _iLogger.LogInformation("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }
        
        _iLogger.LogInformation("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);
        
        OnChange.UpdateDatabase(resource, _iLogger);
        
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> ReconcileAsync(V1Alpha1DataEntity resource)
    {
        _iLogger.LogInformation("Created");

        _iLogger.LogInformation("creating channel");
        var channel = GrpcChannel.ForAddress("http://internal-database-service:5188");
        _iLogger.LogInformation("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        _iLogger.LogInformation("Client created");

        for (var i = 0; i < resource.Spec.Count; i++)
        {
            _iLogger.LogInformation("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }
        
        _iLogger.LogInformation("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);
        
        OnChange.UpdateDatabase(resource, _iLogger);
        
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> StatusModifiedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger.LogInformation("StatusModifiedAsync");
        OnChange.UpdateDatabase(resource, _iLogger);
        return Task.FromResult<ResourceControllerResult>(null);
    }

    public Task<ResourceControllerResult> DeletedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger.LogInformation("DeletedAsync");
        return Task.FromResult<ResourceControllerResult>(null);
    }

}
