// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.BlazorDiagramBase;
using Medulla.Editor.Client.Components.Properties;

namespace Medulla.WorkflowDesigner.Client.Library.PropertyMenuStructureFactory;

public class AddDatabaseTableMenuStructureFactory : PropertyMenuStructureFactory
{
    public override PropertyMenuStructure GetStructure(Action<AnyTypeValue> action)
    {
        return new PropertyMenuStructure()
        {
            Nameable = Nameable.NewNameable("Database Designer", null),
            PropertyMenuStructureNodes = new ()
            {
                new()
                {
                    Nameable = Nameable.NewNameable("Add Table", "Add database table"),
                    InputType = InputType.Button,
                    OnValueChange = (action)
                }
            }
        };
    }
}
