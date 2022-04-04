using KubeOps.Operator;

namespace DatabaseControllerKubeOps;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) => services.AddKubernetesOperator();
    public void Configure(IApplicationBuilder app) => app.UseKubernetesOperator();
}
