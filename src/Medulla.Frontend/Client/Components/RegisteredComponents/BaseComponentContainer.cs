// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using YamlDotNet.Core.Tokens;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    public abstract class BaseComponentContainer : BaseComponent
    {


        protected string hoverClass = "";
        protected bool isHovering = false;

        protected void HandleDragEnter()
        {
            hoverClass = "hovering";
            isHovering = true;
        }

        protected void HandleDragLeave()
        {
            hoverClass = "";
            isHovering = false;
        }


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

        protected abstract List<EditorViewNode?> GetChildrenToAddOnAdd();

    }



}
