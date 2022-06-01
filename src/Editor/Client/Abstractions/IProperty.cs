// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;

namespace Medulla.Editor.Client.Abstractions;

/// <summary>
/// Implementation of Property in Properties tree.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IProperty<T>
{
    /// <summary>
    /// Sets name of property.
    /// </summary>
    /// <param name="nameable">Gives title, and description</param>
    public void SetNameable(Nameable nameable);

    /// <summary>
    /// Locks property
    /// </summary>
    public void LockProperty();

    /// <summary>
    /// Unlock property.
    /// </summary>
    public void UnlockProperty();

    /// <summary>
    /// This method clears the value set to a property.
    /// </summary>
    public void ClearProperty();

    /// <summary>
    /// This method manages highlighting the selected property in the window.
    /// </summary>
    public void SelectProperty();

    /// <summary>
    /// This method manages opening the property to show its input field.
    /// </summary>
    public void OpenProperty();

    /// <summary>
    /// Closes this property to hide its input field.
    /// </summary>
    public void CloseProperty();

    /// <summary>
    /// Sets property value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SetProperty(T value);


}
