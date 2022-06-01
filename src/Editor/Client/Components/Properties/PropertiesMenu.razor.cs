// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions;
using Medulla.Editor.Client.Abstractions.ObjectComposition;

namespace Medulla.Editor.Client.Components.Properties;

/// <summary>
/// Properties Menu
/// </summary>
public partial class PropertiesMenu
{
    /// <summary>
    /// Nameable class to give Title and description
    /// </summary>
    public Nameable Nameable { get; set; } = new();



}
