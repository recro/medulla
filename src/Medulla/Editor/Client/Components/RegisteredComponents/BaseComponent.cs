﻿// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Medulla.Frontend.Client.Components.Editor.PropertiesWindow;
using Medulla.Frontend.Client.Library.Abstractions.Environment.EnvironmentAbstractionHandler;
using Medulla.Frontend.Client.Library.Utilities.Unique;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Medulla.Frontend.Client.Components.RegisteredComponents
{
    /// <summary>
    /// Base Component
    /// </summary>
    public abstract class BaseComponent : ComponentBase
    {
        /*
         * These parameters are system required
         */
        
        /// <summary>
        /// Child Content
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        /// <summary>
        /// Editor View Node
        /// </summary>
        [Parameter]
        public EditorViewNode EditorViewNode { get; set; } = default!;

        /// <summary>
        /// Editor
        /// </summary>
        [Parameter]
        public Medulla.Frontend.Client.Components.Editor.Editor Editor { get; set; } = default!;
        /*
         * These parameters are system required
         */

        /// <summary>
        /// Margin Left
        /// </summary>
        [Parameter]
        public string ML { get; set; } = "";

        /// <summary>
        /// Margin Top
        /// </summary>
        [Parameter]
        public string MT { get; set; } = "";

        /// <summary>
        /// Margin Bottom
        /// </summary>
        [Parameter]
        public string MB { get; set; } = "";

        /// <summary>
        /// Margin Right
        /// </summary>
        [Parameter]
        public string MR { get; set; } = "";

        /// <summary>
        /// Padding Left
        /// </summary>
        [Parameter]
        public string PL { get; set; } = "";

        /// <summary>
        /// Padding Top
        /// </summary>
        [Parameter]
        public string PT { get; set; } = "";

        /// <summary>
        /// Padding Bottom
        /// </summary>
        [Parameter]
        public string PB { get; set; } = "";

        /// <summary>
        /// Padding Right
        /// </summary>
        [Parameter]
        public string PR { get; set; } = "";

        /// <summary>
        /// Is Bold
        /// </summary>
        [Parameter]
        public string IsBold { get; set; } = "";

        /// <summary>
        /// After Add Component
        /// </summary>
        protected abstract void AfterAddComponent(Medulla.Frontend.Client.Components.Editor.Editor editor, UniqueId uniqueId);

        /// <summary>
        /// Get Count
        /// </summary>
        public int GetCount()
        {
            return Editor.GetDepthOfEditorViewNodeWithUniqueId(Editor.EditorViewNode, UniqueId, 1);
        }

        /// <summary>
        /// Mouse Over
        /// </summary>
        protected void MouseOver(MouseEventArgs e)
        {

            Editor.StateChanged();
        }

        /// <summary>
        /// Mouse Leave
        /// </summary>
        protected void MouseLeave(MouseEventArgs e)
        {

        }

        /// <summary>
        /// Get Font Style
        /// </summary>
        protected string GetFontStyle()
        {
            string style = "";
            if (IsBold == "True")
            {
                style += "font-weight: bold";
            }

            return $" {style} ";
        }

        /// <summary>
        /// Get Number
        /// </summary>
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
        
        /// <summary>
        /// Get Margin
        /// </summary>
        protected string GetMargin()
        {
            string ml = GetNumber("ml-", ML);
            string mr = GetNumber("mr-", MR);
            string mb = GetNumber("mb-", MB);
            string mt = GetNumber("mt-", MT);


            return $" {ml} {mr} {mb} {mt} ";
        }


        /// <summary>
        /// Get Padding
        /// </summary>
        protected string GetPadding()
        {

            string pl = GetNumber("pl-", PL);
            string pr = GetNumber("pr-", PR);
            string pb = GetNumber("pb-", PB);
            string pt = GetNumber("pt-", PT);

            return $" {pl} {pr} {pb} {pt} ";
        }

        /// <summary>
        /// Unique Id
        /// </summary>
        [Parameter]
        public UniqueId? UniqueId { get; set; } = default!;


        /// <summary>
        /// Get Input Attributes
        /// </summary>
        protected Dictionary<string, object> GetInputAttributes()
        {
            if (Editor.CurrentComponent?.UniqueId == null)
            {
                Console.WriteLine("Editor.CurrentComponent.UniqueId was null");
                return new Dictionary<string, object>();
            }
            return Editor.CurrentComponent.UniqueId.Equals(this.UniqueId) ? new Dictionary<string, object> { { "checked", "true" } } : new Dictionary<string, object>();
        }

        /// <summary>
        /// Make Active
        /// </summary>
        protected void MakeActive()
        {
            UpdatePropertiesWindow(Editor, this.EditorViewNode.UniqueId);
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        protected abstract Properties GetProperties();
        
        /// <summary>
        /// Is Clickable
        /// </summary>
        protected abstract bool IsClickable();

        /// <summary>
        /// Does Implement Padding
        /// </summary>
        protected abstract bool DoesImplementPadding();

        /// <summary>
        /// Does Implement Margin
        /// </summary>
        protected abstract bool DoesImplementMargin();

        /// <summary>
        /// Does Implement Fonts
        /// </summary>
        protected abstract bool DoesImplementFonts();

        /// <summary>
        /// Is Hover Component Container
        /// </summary>
        protected abstract bool IsHoverComponentContainer();

        /// <summary>
        /// Update Properties
        /// </summary>
        public void UpdatePropertiesWindow(Editor.Editor editor, UniqueId? uniqueId)
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
            Editor.StateChanged();
        }

        /// <summary>
        /// Unselect
        /// </summary>
        public void Unselect()
        {
            Editor.CurrentComponent = null;
            Editor.StateChanged();
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

        /// <summary>
        /// Clicked
        /// </summary>
        protected void Clicked()
        {
            var click = new ClickHandler();
            click.Handle(Editor, this.UniqueId, this, IsClickable());
        }


    }
}
