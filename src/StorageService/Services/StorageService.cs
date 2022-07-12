using Grpc.Core;
using StorageService;

namespace StorageService.Services;

public class StorageService : Storage.StorageBase
{
    private readonly ILogger<StorageService> _logger;

    public StorageService(ILogger<StorageService> logger)
    {
        _logger = logger;
    }
}
