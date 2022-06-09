// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.Properties.NodeValueTypes;

public class Database
{
    public string? Name { get; set; }

}

public class DatabaseList
{
    public List<Database>? Databases { get; set; }
}



public class DatabaseValue : AnyTypeValue
{
    public void SetValue<T>(T Value)
    {
        throw new NotImplementedException();
    }

    public T GetValue<T>()
    {
        throw new NotImplementedException();
    }
}
