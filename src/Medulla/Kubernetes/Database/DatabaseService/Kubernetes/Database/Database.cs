using DatabaseService.Utils;
using GrpcDatabaseService;
using k8s.Autorest;
using k8s.Models;

namespace DatabaseService.Kubernetes.Database;

public class Database
{

    public static async Task<bool> Create(CreateDatabaseResourcesRequest request)
    {
        string uuid = Guid.NewGuid().ToString();
        Console.WriteLine(uuid);
        Console.WriteLine(uuid);
        Console.WriteLine(uuid);
        Console.WriteLine(uuid);
        Console.WriteLine(uuid);
        Console.WriteLine(uuid);
        string dbName = "db-" + request.Name + uuid;
        Console.WriteLine("new db name" + dbName);
        var client = Kubernetes.Load.Client.Load.GetClient();
        var pod = new V1Pod("v1", "Pod", 
            new V1ObjectMeta()
            {
                Name = dbName,
                Labels = new Dictionary<string, string>()
                {
                    {"database", request.Name}
                }
            },
            new V1PodSpec()
            {
                Containers = new List<V1Container>()
                {
                    new V1Container()
                    {
                        Name = "database",
                        Image = "mysql:latest",
                        Ports = new List<V1ContainerPort>()
                        {
                            new V1ContainerPort()
                            {
                                Protocol = "TCP",
                                ContainerPort = 3306,
                                HostPort = 3306
                            }
                        },
                        Env = new List<V1EnvVar>()
                        {
                            new V1EnvVar()
                            {
                                Name = "MYSQL_DATABASE",
                                Value = request.DatabaseName
                            },
                            new V1EnvVar()
                            {
                                Name = "MYSQL_USER",
                                Value = request.User
                            },
                            new V1EnvVar()
                            {
                                Name = "MYSQL_PASSWORD",
                                Value = request.Password
                            },
                            new V1EnvVar()
                            {
                                Name = "MYSQL_ROOT_PASSWORD",
                                Value = request.Password
                            }
                        }
                    }
                }
            }
        );

        try
        {
            var res = await client.CreateNamespacedPodWithHttpMessagesAsync(pod, "default");
            Console.WriteLine(res.Response.Content);
            Console.WriteLine(res.Response.Headers);
            Console.WriteLine(res.Response.Version);
            Console.WriteLine(res.Response.StatusCode);
            Console.WriteLine(res.Response.ReasonPhrase);
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
            Console.WriteLine(e.Message);
            return false;
        }


        var service = new V1Service()
        {
            Kind = "Service",
            Metadata = new V1ObjectMeta()
            {
                Name = dbName
            },
            Spec = new V1ServiceSpec()
            {
                Selector = new Dictionary<string, string>()
                {
                    { "database", request.Name }
                },
                Type = "ClusterIP",
                Ports = new List<V1ServicePort>()
                {
                    new V1ServicePort()
                    {
                        Name = "db",
                        Port = 3306,
                        TargetPort = 3306
                    }
                }
                
            }
        };
        
        
        try
        {
            var res = await client.CreateNamespacedServiceWithHttpMessagesAsync(service, "default");
            Console.WriteLine(res.Body);
            Console.WriteLine(res.Response.Content);
            Console.WriteLine(res.Response.Headers);
            Console.WriteLine(res.Response.Version);
            Console.WriteLine(res.Response.ReasonPhrase);
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
            Console.WriteLine(e.Message);
            return false;
        }
        
        return true;
    }
    
    
    
    
}


