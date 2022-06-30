// See https://aka.ms/new-console-template for more information

using Medulla.AppRenderer.Core.Abstractions;

Console.WriteLine("test 1");
string xml = System.IO.File.ReadAllText(@".\test.xml");
Console.WriteLine("test 3");
var componentStructure = ComponentStructure.CreateFromXml(xml);
Console.WriteLine(componentStructure.ComponentType);
Console.WriteLine(componentStructure.SerializeToXml());
