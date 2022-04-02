
using DatabaseControllerKubeOps;
using KubeOps.Operator;

public static class Program
{
    public static Task<int> Main(string[] args) =>
        CreateHostBuilder(args)
            .Build()
            .RunOperatorAsync(args);

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
