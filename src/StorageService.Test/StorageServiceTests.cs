namespace StorageService.Test;

using StorageService.Kubernetes.Crds.Data;

public class StorageServiceTests
{
    [Fact]
    public void  TestToStringOfCrd()
    {
        CResource resource = Utils.MakeCResource("test", "default", "test", "test", "test");
        string json = resource.ToString();
        Assert.Equal("test (Labels: {medulla-resource-type : database-definition})", json);
    }
}
