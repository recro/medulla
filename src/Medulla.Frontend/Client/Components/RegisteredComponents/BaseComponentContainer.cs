// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    public abstract class BaseComponentContainer : BaseComponent
    {

        protected void HandleDrop()
        {
            Editor.PlaceInUniqueId = this.UniqueId;
            /*if (Editor.PlaceAfterComponentWithUniqueId != null && !Editor.PlaceAfterComponentWithUniqueId.Equals(this.UniqueId))
            {
                return;
            }
            // Properties properties = baseComponent.GetProperties();
            isHovering = false;
            if (Editor.RemoveComponentOnHoverLeaveWithUniqueId != null && 
                Editor.CurrentComponent.UniqueId.Equals(Editor.RemoveComponentOnHoverLeaveWithUniqueId))
            {
                Editor.RemoveComponentWithUniqueId(Editor.EditorViewNode, Editor.RemoveComponentOnHoverLeaveWithUniqueId);    
            }
            // EditorViewNode.Children.Add(Editor.CurrentComponent);
            
            Editor.AddComponentToEditorViewNode(Editor.EditorViewNode);
            BaseComponent baseComponent = (BaseComponent)Activator.CreateInstance(Type.GetType(Editor.CurrentComponent.Type));
            baseComponent.UpdatePropertiesWindow(Editor, Editor.CurrentComponent.UniqueId);
            Editor.StateChanged();*/
        }
        
    }
    
    
    
}
