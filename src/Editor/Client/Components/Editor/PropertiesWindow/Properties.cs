// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Medulla.Frontend.Client.Library.Utilities.Unique;

namespace Medulla.Frontend.Client.Components.Editor.PropertiesWindow
{
    /// <summary>
    /// Properties in Property Window
    /// </summary>
    public class Properties
    {

        /// <summary>
        /// List of properties
        /// </summary>
        public List<Property> PropertyList { get; set; } = new List<Property>();


        /// <summary>
        /// Set Property
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="uniqueId"></param>
        public void SetPropertyValuesFromEditorViewNode(Editor editor, UniqueId? uniqueId)
        {
            foreach (var prop in PropertyList)
            {
                var currentValue = editor!.GetComponentPropertyValue(uniqueId, prop.Name);
                // prop.DefaultValue = currentValue;
            }
        }

    }
}
