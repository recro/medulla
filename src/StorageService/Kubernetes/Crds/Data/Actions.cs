using System.Text.Json;
using k8s;
using Microsoft.Rest;

namespace StorageService.Kubernetes.Crds.Data;

public class Actions
{
    public static async void Create(string name, List<Databases> databases)
    {
        var client = SharedClasses.Kubernetes.Load.Client.Load.GetClient();

        // creating a K8s client for the CRD
        var myCRD = Utils.MakeCRD();
        Console.WriteLine("working with CRD: {0}.{1}", myCRD.PluralName, myCRD.Group);
        //var generic = new GenericClient(client, myCRD.Group, myCRD.Version, myCRD.PluralName);

        // creating a sample custom resource content
        var myCr = Utils.MakeCResource(name, "default");
        try
        {
            Console.WriteLine("creating{1} CR {0}", myCr?.Metadata?.Name, "ARG1");
            Console.WriteLine(String.Join(",", myCr));
            Console.WriteLine("________________________________________________");
            Console.WriteLine(JsonSerializer.Serialize(myCr));
            Console.WriteLine("________________________________________________");
            var response = await client.CreateNamespacedCustomObjectWithHttpMessagesAsync(
                myCr,
                myCRD.Group, myCRD.Version,
                myCr?.Metadata?.NamespaceProperty ?? "default",
                myCRD.PluralName).ConfigureAwait(false);
            Console.WriteLine(response);
            Console.WriteLine(response.Response.StatusCode);
            Console.WriteLine(response.Response.Headers);
            Console.WriteLine(response.Response.Content);
            Console.WriteLine(response.Response.Version);
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


    }

    public static async Task<CustomResourceList<CResource>> Get()
    {
        var myCRD = Utils.MakeCRD();
        var client = SharedClasses.Kubernetes.Load.Client.Load.GetClient();
        var generic = new GenericClient(client, myCRD.Group, myCRD.Version, myCRD.PluralName);
        Console.WriteLine("CR list:");
        var crs = await generic.ListNamespacedAsync<CustomResourceList<CResource>>("default").ConfigureAwait(false);
        // foreach (var cr in crs.Items)
        // {
        //     Console.WriteLine("- CR Item {0} = {1}", crs.Items.IndexOf(cr), cr.Metadata.Name);
        // }
        return crs;
    }

    public static async Task<bool> Delete(string name)
    {
        // deleting the custom resource
        try
        {
            var myCRD = Utils.MakeCRD();
            var client = SharedClasses.Kubernetes.Load.Client.Load.GetClient();
            var generic = new GenericClient(client, myCRD.Group, myCRD.Version, myCRD.PluralName);
            await generic.DeleteNamespacedAsync<CResource>(
                "default",
                name).ConfigureAwait(false);
            Console.WriteLine("Deleted the CR");
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine("Exception type {0}", exception);
            return false;
        }
    }



}
