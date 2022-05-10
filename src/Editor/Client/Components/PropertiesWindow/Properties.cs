// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.PropertiesWindow
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


        // <summary>
        // Set Property
        // </summary>
        // <param name="editor"></param>
        // <param name="uniqueId"></param>
        //public void SetPropertyValuesFromEditorViewNode(EditorPage editor, UniqueId? uniqueId)
        //{
        //    foreach (var prop in PropertyList)
        //    {
        //        var currentValue = editor!.GetComponentPropertyValue(uniqueId, prop.Name);
        //        // prop.DefaultValue = currentValue;
        //    }
        //}

    }
}
