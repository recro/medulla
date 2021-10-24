// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Library.Abstractions.Environment
{
    public class Page
    {

        private static Page? s_instance;

        public EditorViewNode EditorViewNode { get; set; } = new();

        public static Page GetInstance()
        {
            if (Page.s_instance == null)
            {
                Page.s_instance = new Page();
            }
            return Page.s_instance;
        }

        public static void UpdateInstance(Page page)
        {
            Page.s_instance = page;
        }

    }
}
