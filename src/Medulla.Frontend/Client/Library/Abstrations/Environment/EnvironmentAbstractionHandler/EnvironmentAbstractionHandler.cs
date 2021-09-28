// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Frontend.Client.Components.Editor;
using Medulla.Frontend.Client.Library.Utilities.Unique;

namespace Medulla.Frontend.Client.Code
{
    public abstract class EnvironmentAbstractionHandler
    {

        protected abstract void HandleInEditor(Editor editor, UniqueId uniqueId, Medulla.Frontend.Client.Components.RegisteredComponents.BaseComponent clickableBaseComponent);

        protected abstract void HandleInProduction();

        protected abstract void HandleInDevelopment();

        public enum Environment
        {
            Editor,
            Production,
            Development
        }

        public void Handle(Editor editor, UniqueId uniqueId, Medulla.Frontend.Client.Components.RegisteredComponents.BaseComponent clickableBaseComponent, bool isClickable)
        {
            Environment env = GetCurrentEnvironment();
            if (env == Environment.Development)
            {
                if (isClickable)
                {
                    HandleInDevelopment();
                }
            }
            else if (env == Environment.Editor)
            {
                HandleInEditor(editor, uniqueId, clickableBaseComponent);
            }
            else if (env == Environment.Production)
            {
                if (isClickable)
                {
                    HandleInProduction();
                }
            }
        }

        public static Environment GetCurrentEnvironment()
        {
            //code that figures out the environment
            //TODO implement this function
            return Environment.Editor;
        }

    }
}
