// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Medulla.Editor.Client.Abstractions.ObjectComposition;
using Medulla.Editor.Client.Components.Properties;


namespace Medulla.Editor.Client.Components.DatabaseDesigner;

public partial class DatabaseDesigner : IDisposable
{

    public void SetDatabaseProperties()
    {
        EditorPage!.SetPropertyMenuStructure(new PropertyMenuStructure()
        {
            Nameable = new Nameable()
            {
                Title = "Databases",
                Description = "Databases"
            },
            PropertyMenuStructureNodes = new()
            {
                new ()
                {
                    InputType = InputType.AddMultiple,
                    IsOpen = true,
                    Nameable = new ()
                    {
                        Title = "Databases",
                        Description = "Databases"
                    }
                }
            }

        });

    }

    void OnOpen(bool isOpen)
    {
            SetDatabaseProperties();
    }


}
