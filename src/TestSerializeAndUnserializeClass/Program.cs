// See https://aka.ms/new-console-template for more information

using System.Runtime.Serialization.Formatters.Binary;

var test = new Test();

using (var stream = new MemoryStream())
{
    var formatter = new BinaryFormatter();
    formatter.Ser
}





[Serializable]
class Test
{
    public string TestField = "Test";
}



















