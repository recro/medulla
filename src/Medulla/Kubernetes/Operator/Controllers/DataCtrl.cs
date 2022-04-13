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
using System.IO;

namespace DatabaseControllerKubeOps.Controller.Controllers;


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

            var dbSyncServiceProtocol = Environment.GetEnvironmentVariable("DATABASE_SYNC_SERVICE_PROTOCOL");
            var dbSyncServiceHost = Environment.GetEnvironmentVariable("DATABASE_SYNC_SERVICE_HOST");
            var dbSyncServicePort = Environment.GetEnvironmentVariable("DATABASE_SYNC_SERVICE_PORT");
            var dbSyncServiceAddress =
                $"{dbSyncServiceProtocol}://{dbSyncServiceHost}:{dbSyncServicePort}/listen-for-database-schema";
            Console.WriteLine("db sync service address: " + dbSyncServiceAddress);
            var response = await httpClient.PostAsync(dbSyncServiceAddress, content);
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
            Console.WriteLine(e.ToString());
            Console.WriteLine(e.Body.ToString());
            Console.WriteLine(e.Response.ReasonPhrase);
            Console.WriteLine(e.Response.StatusCode.ToString());
            Console.WriteLine(e.Response.Content.ToString());
            Console.WriteLine(e.Response.Headers.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}


/// <summary>
/// DataCtrl class for KubeOps Data CRD
/// </summary>
[EntityRbac(typeof(V1Alpha1DataEntity), Verbs = RbacVerb.All)]
public class DataCtrl : IResourceController<V1Alpha1DataEntity>
{

    /// <summary>
    /// CreatedAsync called by KubeOps when CRD created
    /// </summary>
    /// <param name="resource">crd resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> CreatedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("Created");

        Console.WriteLine("creating channel");
        var dbServiceHost = Environment.GetEnvironmentVariable("DATABASE_SERVICE_HOST");
        var dbServicePort = Environment.GetEnvironmentVariable("DATABASE_SERVICE_PORT");
        var dbServiceProtocol = Environment.GetEnvironmentVariable("DATABASE_SERVICE_PROTOCOL");
        var dbServiceAddress = $"{dbServiceProtocol}://{dbServiceHost}:{dbServicePort}";
        Console.WriteLine("DatabaseServiceAddress: " + dbServiceAddress);
        var channel = GrpcChannel.ForAddress(dbServiceAddress);
        Console.WriteLine("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        Console.WriteLine("Client created");

        for (var i = 0; i < resource.Spec!.Count; i++)
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

        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// ReconcileAsync called by KubeOps when resource updated
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult?> ReconcileAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("Created");

        Console.WriteLine("creating channel");
        var dbServiceHost = Environment.GetEnvironmentVariable("DATABASE_SERVICE_HOST");
        var dbServicePort = Environment.GetEnvironmentVariable("DATABASE_SERVICE_PORT");
        var dbServiceProtocol = Environment.GetEnvironmentVariable("DATABASE_SERVICE_PROTOCOL");
        var dbServiceAddress = $"{dbServiceProtocol}://{dbServiceHost}:{dbServicePort}";
        Console.WriteLine("DatabaseServiceAddress: " + dbServiceAddress);
        var channel = GrpcChannel.ForAddress(dbServiceAddress);
        Console.WriteLine("Creating client");
        var client = new DatabaseSvc.DatabaseSvcClient(channel);
        Console.WriteLine("Client created");

        for (var i = 0; i < resource.Spec!.Count; i++)
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

        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// StatusModifiedAsync called by KubeOps when status modified
    /// </summary>
    /// <param name="resource">KubeOps CRD resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> StatusModifiedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("StatusModifiedAsync");
        OnChange.UpdateDatabase(resource);
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

    /// <summary>
    /// DeletedAsync Called by KubeOps when resource deleted
    /// </summary>
    /// <param name="resource">KubeOps CRD Resource</param>
    /// <returns></returns>
    public Task<ResourceControllerResult> DeletedAsync(V1Alpha1DataEntity resource)
    {
        Console.WriteLine("DeletedAsync");
        return Task.FromResult<ResourceControllerResult>(null!)!;
    }

}
