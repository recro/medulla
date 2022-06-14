// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Components.EditorView;

namespace Medulla.Editor.Client.Library.Abstractions.Environment
{
    /// <summary>
    /// Page
    /// </summary>
    public class Page
    {

        /// <summary>
        /// Page
        /// </summary>
        private static Page? s_instance;

        /// <summary>
        /// Editor View Node
        /// </summary>
        public EditorViewNode EditorViewNode { get; set; } = new();

        /// <summary>
        /// GetInstance
        /// </summary>
        public static Page GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new Page();
            }
            return s_instance;
        }

        /// <summary>
        /// Update Instance
        /// </summary>
        public static void UpdateInstance(Page page)
        {
            s_instance = page;
        }

    }
}
