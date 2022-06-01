// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Abstractions;

/// <summary>
/// This interface defines an implementation for how a Properties Menu will work, and how other components will interact
/// with it.
/// </summary>
public interface IPropertyMenu
{

    /// <summary>
    /// Title of Properties window
    /// </summary>
    /// <param name="title">Title of properties window</param>
    public void SetTitle(string title);

    /// <summary>
    /// Opens tree of properties to see all properties in sub trees.
    /// </summary>
    public void OpenAllProperties();

    /// <summary>
    /// Closes every tree of properties to hide all properties.
    /// </summary>
    public void CloseAllProperties();

    /// <summary>
    /// This method manages adding a properties tree to the root properties tree, or to another properties tree.
    /// </summary>
    /// <param name="propertyTree">Add a properties tree to menu</param>
    public void AddPropertiesTree(IPropertyTree propertyTree);

    /// <summary>
    /// This method manages removing a properties tree from the Properties menu.
    /// </summary>
    /// <param name="propertyTree">Remove a properties tree from menu</param>
    public void RemovePropertiesTree(IPropertyTree propertyTree);

    /// <summary>
    /// Removes all property trees
    /// </summary>
    public void RemoveAllPropertyTrees();

}
