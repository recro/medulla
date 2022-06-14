// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Components;
using Medulla.Editor.Client.Library.Utilities.Unique;

namespace Medulla.Editor.Client.Library.Abstractions.Environment.EnvironmentAbstractionHandler
{
    /// <summary>
    /// Environment Abstraction Handler
    /// </summary>
    public abstract class EnvironmentAbstractionHandler
    {

        /// <summary>
        /// Handle In Editor
        /// </summary>
        protected abstract void HandleInEditor(EditorPage editor, UniqueId? uniqueId, Components.RegisteredComponents.BaseComponent clickableBaseComponent);

        /// <summary>
        /// Handle In Production
        /// </summary>
        protected abstract void HandleInProduction();

        /// <summary>
        /// Handle In Development
        /// </summary>
        protected abstract void HandleInDevelopment();

        private enum Environment
        {
            Editor,
            Production,
            Development
        }

        /// <summary>
        /// Handle
        /// </summary>
        public void Handle(EditorPage editor, UniqueId? uniqueId, Components.RegisteredComponents.BaseComponent clickableBaseComponent, bool isClickable)
        {
            var env = GetCurrentEnvironment();
            switch (env)
            {
                case Environment.Development:
                    {
                        if (isClickable)
                        {
                            HandleInDevelopment();
                        }

                        break;
                    }
                case Environment.Editor:
                    HandleInEditor(editor, uniqueId, clickableBaseComponent);
                    break;
                case Environment.Production:
                    {
                        if (isClickable)
                        {
                            HandleInProduction();
                        }

                        break;
                    }
            }
        }

        private static Environment GetCurrentEnvironment()
        {
            //code that figures out the environment
            //TODO implement this function
            return Environment.Editor;
        }

    }
}
