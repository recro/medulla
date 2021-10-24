using System;
using System.Collections.Generic;
using Medulla.Frontend.Client.Components.Editor.PropertiesWindow;
using Medulla.Frontend.Client.Library.Abstractions.Environment.EnvironmentAbstractionHandler;
using Medulla.Frontend.Client.Library.Utilities.Unique;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    public abstract class BaseComponent : ComponentBase
    {
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
        public string ML { get; set; } = "";

        [Parameter]
        public string MT { get; set; } = "";

        [Parameter]
        public string MB { get; set; } = "";

        [Parameter]
        public string MR { get; set; } = "";


        [Parameter]
        public string PL { get; set; } = "";

        [Parameter]
        public string PT { get; set; } = "";

        [Parameter]
        public string PB { get; set; } = "";

        [Parameter]
        public string PR { get; set; } = "";

        [Parameter]
        public string IsBold { get; set; } = "";


        protected abstract void AfterAddComponent(Medulla.Frontend.Client.Components.Editor.Editor editor, UniqueId uniqueId);

        public int GetCount()
        {
            return Editor.GetDepthOfEditorViewNodeWithUniqueId(Editor.EditorViewNode, UniqueId, 1);
        }

        protected void MouseOver(MouseEventArgs e)
        {

            Editor.StateChanged();
        }

        protected void MouseLeave(MouseEventArgs e)
        {

        }

        protected string GetFontStyle()
        {
            string style = "";
            if (IsBold == "True")
            {
                style += "font-weight: bold";
            }

            return $" {style} ";
        }


        private static string GetNumber(string prepend, string input)
        {
            var number = 0;
            try
            {
                number = int.Parse(input);
            }
            catch
            {
                return "";
            }

            return prepend + number;
        }

        protected string GetMargin()
        {
            string ml = GetNumber("ml-", ML);
            string mr = GetNumber("mr-", MR);
            string mb = GetNumber("mb-", MB);
            string mt = GetNumber("mt-", MT);


            return $" {ml} {mr} {mb} {mt} ";
        }

        protected string GetPadding()
        {

            string pl = GetNumber("pl-", PL);
            string pr = GetNumber("pr-", PR);
            string pb = GetNumber("pb-", PB);
            string pt = GetNumber("pt-", PT);

            return $" {pl} {pr} {pb} {pt} ";
        }


        [Parameter]
        public UniqueId? UniqueId { get; set; } = default!;


        protected Dictionary<string, object> GetInputAttributes()
        {
            if (Editor.CurrentComponent?.UniqueId == null)
            {
                Console.WriteLine("Editor.CurrentComponent.UniqueId was null");
                return new Dictionary<string, object>();
            }
            return Editor.CurrentComponent.UniqueId.Equals(this.UniqueId) ? new Dictionary<string, object> { { "checked", "true" } } : new Dictionary<string, object>();
        }

        protected void MakeActive()
        {
            UpdatePropertiesWindow(Editor, this.EditorViewNode.UniqueId);
        }

        protected abstract Properties GetProperties();
        protected abstract bool IsClickable();

        protected abstract bool DoesImplementPadding();

        protected abstract bool DoesImplementMargin();

        protected abstract bool DoesImplementFonts();

        protected abstract bool IsHoverComponentContainer();

        protected void UpdatePropertiesWindow(Editor.Editor editor, UniqueId? uniqueId)
        {
            Properties properties = this.GetProperties();
            if (this.DoesImplementFonts())
            {
                properties.PropertyList.Add(new Property
                {
                    Name = "IsBold",
                    InputDescription = "Is Bold",
                    DefaultValue = new Dictionary<string, object>
                    {
                            { "PropertyName", "IsBold" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Checkbox"
                }
                );
            }
            if (this.DoesImplementMargin())
            {
                properties.PropertyList.Add(new()
                {
                    Name = "ML",
                    InputDescription = "Margin Left",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Margin Left"
                            },
                            { "PropertyName", "ML" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "MR",
                    InputDescription = "Margin Right",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Margin Right"
                            },
                            { "PropertyName", "MR" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "MT",
                    InputDescription = "Margin Top",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Margin Top"
                            },
                            { "PropertyName", "MT" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "MB",
                    InputDescription = "Margin Bottom",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Margin Bottom"
                            },
                            { "PropertyName", "MB" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
            }
            if (this.DoesImplementPadding())
            {
                properties.PropertyList.Add(new()
                {
                    Name = "PL",
                    InputDescription = "Padding Left",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Padding Left"
                            },
                            { "PropertyName", "PL" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "PR",
                    InputDescription = "Padding Right",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Padding Right"
                            },
                            { "PropertyName", "PR" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "PT",
                    InputDescription = "Padding Top",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Padding Top"
                            },
                            { "PropertyName", "PT" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
                properties.PropertyList.Add(new()
                {
                    Name = "PB",
                    InputDescription = "Padding Bottom",
                    DefaultValue = new Dictionary<string, object>()
                        {
                            {
                                "DropdownItems", new Dictionary<string, string>() {
                                    { "None", "" },
                                    { "1", "1" },
                                    { "2", "2" },
                                    { "3", "3" },
                                    { "4", "4" },
                                    { "5", "5" },
                                    { "6", "6" },
                                }
                            },
                            {
                                "Title", "Padding Bottom"
                            },
                            { "PropertyName", "PB" }
                        },
                    InputType = "Medulla.Frontend.Client.Components.Editor.PropertiesWindow.PropertyComponents.Dropdown"
                }
                );
            }
            editor.SetCurrentComponentToEditorViewNodeWithUniqueId(uniqueId);
            properties.SetPropertyValuesFromEditorViewNode(editor, uniqueId);
            editor.SetProperties(properties, uniqueId);
        }


        private class ClickHandler : EnvironmentAbstractionHandler
        {
            protected override void HandleInEditor(Editor.Editor editor, UniqueId? uniqueId, BaseComponent clickableBaseComponent)
            {
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

        protected void Clicked()
        {
            var click = new ClickHandler();
            click.Handle(Editor, this.UniqueId, this, IsClickable());
        }


    }
}

