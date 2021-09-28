using System;
using Medulla.Frontend.Client.Components.Editor.PropertiesWindow;
using Medulla.Frontend.Client.Library.Utilities.Unique;
using Microsoft.AspNetCore.Components;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    public abstract class BaseComponent :ComponentBase
    {
        public BaseComponent()
        {
        }


        /*
         * These parameters are system required
         */
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter]
        public EditorViewNode EditorViewNode { get; set; } = default!;

        [Parameter]
        public Medulla.Frontend.Client.Components.Editor.Editor Editor { get; set; } = default!;
        /*
         * These parameters are system required
         */

        [Parameter]
        public Medulla.Frontend.Client.Library.Utilities.Unique.UniqueId UniqueId { get; set; } = default!;


        abstract protected Properties GetProperties();
        abstract protected bool IsClickable();


        class ClickHandler : Medulla.Frontend.Client.Code.EnvironmentAbstractionHandler
        {
            protected override void HandleInEditor(Editor.Editor editor, UniqueId uniqueId, Medulla.Frontend.Client.Components.RegisteredComponents.BaseComponent clickableBaseComponent)
            {
                Properties properties = clickableBaseComponent.GetProperties();
                //properties.PropertyList.Add(new() { Name = "Content", InputDescription = "Content", DefaultValue = currentValue, InputType = "input" });
                //properties.PropertyList.Add(new() { Name = "Size", InputDescription = "Size", DefaultValue = currentValueSize, InputType = "input" });
                //properties.PropertyList.Add(new() { Name = "Type", InputDescription = "Type", DefaultValue = currentValueType, InputType = "input" });
                //Opens Properties Window
                editor.SetCurrentComponentToEditorViewNodeWithUniqueId(uniqueId);
                properties.SetPropertyValuesFromEditorViewNode(editor, uniqueId);
                editor.SetProperties(properties, uniqueId);
            }

            protected override void HandleInProduction()
            {
                //Production Code
                throw new NotImplementedException();
            }

            protected override void HandleInDevelopment()
            {
                //Development Code
                throw new NotImplementedException();
            }

        }

        public void Clicked()
        {
            var click = new ClickHandler();
            click.Handle(Editor, this.UniqueId, this, IsClickable());
        }


    }
}

