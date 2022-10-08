namespace Medulla.Editor.Client.AppRenderer;

using AppRenderer.Options;

/// <summary>
/// AppNode is the basic building block for an app
/// </summary>
public class AppNode 
{
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


