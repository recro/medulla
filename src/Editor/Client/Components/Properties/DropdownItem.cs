// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.Properties;

/// <summary>
/// Dropdown Items
/// </summary>
public struct DropdownItem
{
    public string Key;
    public string Value;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public DropdownItem(string key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Create Dropdown Items from list
    /// </summary>
    /// <param name="itemsList"></param>
    /// <returns></returns>
    public static List<DropdownItem> CreateDropdownItemsFromList(List<string> itemsList)
    {
        Console.WriteLine(itemsList);
        List<DropdownItem> items = new();
        foreach (var item in itemsList)
        {
            Console.WriteLine(item);
            items.Add(new DropdownItem() { Key = item, Value = item });
        }

        return items;
    }

}
