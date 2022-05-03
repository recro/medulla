// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using YamlDotNet.Core.Tokens;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    /// <summary>
    /// Base Component Container
    /// </summary>
    public abstract class BaseComponentContainer : BaseComponent
    {


        /// <summary>
        /// hover class
        /// </summary>
        protected string hoverClass = "";
        
        /// <summary>
        /// is Hovering
        /// </summary>
        protected bool isHovering = false;

        /// <summary>
        /// Handle Drag Enter
        /// </summary>
        protected void HandleDragEnter()
        {
            hoverClass = "hovering";
            isHovering = true;
        }

        /// <summary>
        /// Handle Drag Leave
        /// </summary>
        protected void HandleDragLeave()
        {
            hoverClass = "";
            isHovering = false;
        }


        /// <summary>
        /// Handle Drop
        /// </summary>
        protected void HandleDrop()
        {
            hoverClass = "";
            Console.WriteLine("handling drop");
            Editor.PlaceInUniqueId = this.UniqueId;
            Editor.AddComponentToEditorViewNode(Editor.EditorViewNode);
            foreach (var child in GetChildrenToAddOnAdd())
            {
                Editor.PlaceInUniqueId = this.UniqueId;
                Editor.CurrentComponent = child;
                Editor.AddComponentToEditorViewNode(Editor.EditorViewNode);
            }
            Editor.StateChanged();
        }

        /// <summary>
        /// Get Children To Add On Add
        /// </summary>
        protected abstract List<EditorViewNode?> GetChildrenToAddOnAdd();

    }



}
