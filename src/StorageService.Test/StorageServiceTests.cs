// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.


using StorageService.Kubernetes.Crds.Data;

namespace StorageService.Test;
public class StorageServiceTests
{
    [Fact]
    public void TestToStringOfCrd()
    {
        var resource = Utils.MakeCResource("test", "default", "test", "test", "test");
        var json = resource.ToString();
        Assert.Equal("test (Labels: {medulla-resource-type : database-definition})", json);
    }
}
