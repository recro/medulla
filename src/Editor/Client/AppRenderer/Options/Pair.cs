// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.AppRenderer.Options;

/// <summary>
/// Stores Pair of Key and Value.
/// </summary>
public class Pair
{
    /// <summary>
    /// Key of Pair
    /// </summary>
    /// <value></value>
    public Key? Key { get; set; }

    /// <summary>
    /// Value of pair
    /// </summary>
    /// <value></value>
    public Value? Value { get; set; }

}
