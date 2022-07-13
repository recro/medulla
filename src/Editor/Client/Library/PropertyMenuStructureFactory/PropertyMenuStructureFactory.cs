// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Components.Properties;

namespace Medulla.WorkflowDesigner.Client.Library.PropertyMenuStructureFactory;

public abstract class PropertyMenuStructureFactory
{
    public abstract PropertyMenuStructure GetStructure(Action<AnyTypeValue> action);

    public static PropertyMenuStructure GetStructure(PropertyMenuStructureFactory structure, Action<AnyTypeValue> action)
    {
        return structure.GetStructure(action);
    }
}
