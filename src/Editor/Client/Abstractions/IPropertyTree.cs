// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Abstractions;

/// <summary>
/// Implements a list of properties as a tree view
/// </summary>
public interface IPropertyTree
{


    /// <summary>
    /// Opens tree of properties to see all properties in sub trees.
    /// </summary>
    public void OpenAllProperties();

    /// <summary>
    /// Closes every tree of properties to hide all properties.
    /// </summary>
    public void CloseAllProperties();

    /// <summary>
    /// This method manages adding a property to a property tree.
    /// </summary>
    public bool AddProperty<T>(IProperty<T> property);

    /// <summary>
    /// This method manages removing a property from a properties tree.
    /// </summary>
    public bool RemoveProperty<T>(IProperty<T> property);

    /// <summary>
    /// Remove Property at index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool RemovePropertyAt(int index);

    /// <summary>
    /// Get List of properties in properties tree.
    /// </summary>
    /// <returns>List of properties</returns>
    public List<IProperty<T>> GetProperties<T>();

    /// <summary>
    /// Gets a property at an index
    /// </summary>
    /// <param name="at"></param>
    /// <returns></returns>
    public IProperty<T> GetPropertyAt<T>(int at);


}
