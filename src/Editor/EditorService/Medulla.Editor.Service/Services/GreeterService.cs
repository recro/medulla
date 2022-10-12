using Grpc.Core;
using Greeter;

namespace EditorService.Services;

/// <summary>
/// Grpc service for the editor
/// </summary>
public class GreeterService : Greeter.Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    /// <summary>
    /// Constructor for Grpc service for editor
    /// </summary>
    /// <param name="logger"></param>
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Say hey to the editor
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
