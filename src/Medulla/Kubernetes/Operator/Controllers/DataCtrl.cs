// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.


using System.Text.Json;
using System.Threading;
using DatabaseControllerKubeOps.Controller.Entities;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcDatabaseService;
using k8s;
using k8s.Models;
using KubeOps.Operator.Controller;
using KubeOps.Operator.Controller.Results;
using KubeOps.Operator.Rbac;
using Microsoft.Rest;

namespace Medulla.Kubernetes.Operator.Controllers;


/// <summary>
/// On Change exposes interface for updating DataSync service with the database structure.
/// The DataSync Service will apply changes to the database structure.
/// </summary>
internal class OnChange
{


    /// <summary>
    /// UpdateDatabase will send a request to the data sync service with the updated database entity.
    /// </summary>
    /// <param name="entity">V1Alpha1DataEntity entity received from KubeOps for crd</param>
    /// <param name="_iLogger">ILogger interface for logging</param>
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


/// <summary>
/// DataCtrl class for KubeOps Data CRD
/// </summary>
[EntityRbac(typeof(V1Alpha1DataEntity), Verbs = RbacVerb.All)]
public class DataCtrl : IResourceController<V1Alpha1DataEntity>
{

    private ILogger<DataCtrl>? _iLogger { get; set; }

    /// <summary>
    /// CreatedAsync called by KubeOps when CRD created
    /// </summary>
    /// <param name="resource">crd resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> CreatedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger!.LogInformation("Created");

        _iLogger!.LogInformation("creating channel");
        var channel = GrpcChannel.ForAddress("http://localhost:5188");
        _iLogger!.LogInformation("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        _iLogger!.LogInformation("Client created");

        for (var i = 0; i < resource.Spec!.Count; i++)
        {
            _iLogger!.LogInformation("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }

        _iLogger!.LogInformation("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);

        OnChange.UpdateDatabase(resource, _iLogger!);

        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// ReconcileAsync called by KubeOps when resource updated
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> ReconcileAsync(V1Alpha1DataEntity resource)
    {
        _iLogger!.LogInformation("Created");

        _iLogger!.LogInformation("creating channel");
        var channel = GrpcChannel.ForAddress("http://internal-database-service:5188");
        _iLogger!.LogInformation("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        _iLogger!.LogInformation("Client created");

        for (var i = 0; i < resource.Spec!.Count; i++)
        {
            _iLogger!.LogInformation("Sending request to Create Database Resources");
            client.CreateDatabaseResources(new CreateDatabaseResourcesRequest()
            {
                Name = resource.Metadata.Name,
                DatabaseName = resource.Spec[i].Name,
                Password = resource.Spec[i].PasswordSecretName,
                ServiceName = resource.Spec[i].Host,
                User = resource.Spec[i].UsernameSecretName,
            }, new CallOptions() { });
        }

        _iLogger!.LogInformation("Waiting for 5 seconds for resources to be created");
        System.Threading.Thread.Sleep(5000);

        OnChange.UpdateDatabase(resource, _iLogger!);

        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// StatusModifiedAsync called by KubeOps when status modified
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> StatusModifiedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger!.LogInformation("StatusModifiedAsync");
        OnChange.UpdateDatabase(resource, _iLogger!);
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// DeletedAsync Called by KubeOps when resource deleted
    /// </summary>
    /// <param name="resource">KubeOps CRD Resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> DeletedAsync(V1Alpha1DataEntity resource)
    {
        _iLogger!.LogInformation("DeletedAsync");
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

}
