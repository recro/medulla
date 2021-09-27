using System;
using Medulla.Frontend.Client.Components.Editor.PropertiesWindow;
using Medulla.Frontend.Client.Library.Utilities.Unique;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    public abstract class ClickableBaseComponent: BaseComponent
    {
        public ClickableBaseComponent()
        {
        }



        class ClickHandler : Medulla.Frontend.Client.Code.EnvironmentAbstractionHandler
        {
            protected override void HandleInEditor(Editor.Editor editor, UniqueId uniqueId, Medulla.Frontend.Client.Components.RegisteredComponents.ClickableBaseComponent clickableBaseComponent)
            {
                //Opens Properties Window
                editor.SetCurrentComponentToEditorViewNodeWithUniqueId(uniqueId);
                Console.WriteLine("Button was clicked");
                string currentValue = editor.GetComponentPropertyValue(uniqueId, "Content");
                string currentValueSize = editor.GetComponentPropertyValue(uniqueId, "Size");
                string currentValueType = editor.GetComponentPropertyValue(uniqueId, "Type");
                Properties properties = new();
                properties.PropertyList.Add(new() { Name = "Content", InputDescription = "Content", DefaultValue = currentValue, InputType = "input" });
                properties.PropertyList.Add(new() { Name = "Size", InputDescription = "Size", DefaultValue = currentValueSize, InputType = "input" });
                properties.PropertyList.Add(new() { Name = "Type", InputDescription = "Type", DefaultValue = currentValueType, InputType = "input" });
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
                Console.WriteLine("Button was clicked");
            }
        }

        public void Clicked()
        {
            var click = new ClickHandler();
            click.Handle(Editor, this.UniqueId, this);
        }


    }
}

