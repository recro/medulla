// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.Properties;

namespace Medulla.WorkflowDesigner.Client.Library.PropertyMenuStructureFactory;

public class DatabaseModelMenuFactory : PropertyMenuStructureFactory
{
    public override PropertyMenuStructure GetStructure(Action<AnyTypeValue> action)
    {
        return new PropertyMenuStructure()
        {
            Nameable = Nameable.NewNameable("Model", null),
            PropertyMenuStructureNodes = new ()
            {
                new()
                {
                    Nameable = Nameable.NewNameable("Model", "Model"),
                    InputType = InputType.DatabaseTableModel,
                    OnValueChange = (action)
                }
            }
        };
    }
}
