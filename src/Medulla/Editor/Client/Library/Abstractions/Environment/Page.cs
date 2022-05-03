// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Library.Abstractions.Environment
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
            if (Page.s_instance == null)
            {
                Page.s_instance = new Page();
            }
            return Page.s_instance;
        }

        /// <summary>
        /// Update Instance
        /// </summary>
        public static void UpdateInstance(Page page)
        {
            Page.s_instance = page;
        }

    }
}
