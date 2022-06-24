// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.Viewer;

/// <summary>
/// Enum for which view is active
/// </summary>
public enum WhichOption
{

    /// <summary>
    /// Editor view
    /// </summary>
    Editor,
    /// <summary>
    /// Workflow view
    /// </summary>
    WorkflowDesigner
}

/// <summary>
/// Controls which view is active
/// </summary>
public class OptionalViews
{
    /// <summary>
    /// Which enum is active
    /// </summary>
    public WhichOption WhichOption { get; set; } = WhichOption.Editor;
}
