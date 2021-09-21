// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Xml;
using Medulla.Frontend.Client.Components.Editor;
using Medulla.Frontend.Client.Components.RegisteredComponents.Buttons;

namespace Medulla.Frontend.Client.Code
{
    abstract public class EnvironmentAbstractionHandler
    {

        abstract protected void HandleInEditor(Editor editor, UniqueId uniqueId);

        abstract protected void HandleInProduction();

        abstract protected void HandleInDevelopment();

        public enum Environment
        {
            Editor,
            Production,
            Development
        }

        public void Handle(Editor editor, UniqueId uniqueId)
        {
            Environment env = GetCurrentEnvironment();
            if (env == Environment.Development)
            {
                HandleInDevelopment();
            }
            else if (env == Environment.Editor)
            {
                HandleInEditor(editor, uniqueId);
            }
            else if (env == Environment.Production)
            {
                HandleInProduction();
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
