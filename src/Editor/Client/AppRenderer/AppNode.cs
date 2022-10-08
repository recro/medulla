// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.


using Medulla.Editor.Client.AppRenderer.Options;

namespace Medulla.Editor.Client.AppRenderer;
/// <summary>
/// AppNode is the basic building block for an app
/// </summary>
public class AppNode
{

    /// <summary>
    /// Id of the node
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Component to render
    /// </summary>
    /// <value></value>
    public string Component { get; set; } = "";


    /// <summary>
    /// Options for this AppNode.
    /// </summary>
    /// <value></value>
    public AppNodeOptions? Options { get; set; }

    /// <summary>
    /// List of Children
    /// </summary>
    /// <returns></returns>
    public List<AppNode> Children { get; set; } = new List<AppNode>();

}


