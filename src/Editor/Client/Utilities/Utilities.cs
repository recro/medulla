// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Components.Properties;

namespace Medulla.Editor.Client.Utilities;

public class Utilities
{

    public static List<DropdownItem> CreateListOfDropdownItems(params string[] items)
    {
        var list = new List<DropdownItem>();

        foreach (var item in items)
        {
            list.Add(new DropdownItem() { Key = item, Value = item });
        }

        return list;
    }


}
