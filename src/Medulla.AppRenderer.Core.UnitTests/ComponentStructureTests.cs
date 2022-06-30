// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.AppRenderer.Core.Abstractions;

namespace Medulla.AppRenderer.Core.UnitTests;

public class ComponentStructureTests
{

    [Fact]
    public void MustSerializeXmlAndDeserializeXml()
    {
        string xml = System.IO.File.ReadAllText(@".\test.xml");
        xml = xml.ReplaceLineEndings("");
        var componentStructure = ComponentStructure.CreateFromXml(xml);
        Assert.Equal(ComponentType.Component1, componentStructure.ComponentType);
        Assert.Equal(xml, componentStructure.SerializeToXml());
    }

}
