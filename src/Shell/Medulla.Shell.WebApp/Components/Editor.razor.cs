// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Medulla.AppRenderer.Core;
using Medulla.AppRenderer.Core.Abstractions;

namespace Medulla.Shell.WebApp.Components;

public partial class Editor
{
    /// <summary>
    /// Component Structure
    /// </summary>
    public ComponentStructure ComponentStructure { get; set; } = new()
    {
        Children = new List<ComponentStructure>()
        {
          new ComponentStructure()
          {
              ComponentType = ComponentType.Component1,
              Children = new List<ComponentStructure>()
              {
                  new ComponentStructure()
                  {
                      ComponentType = ComponentType.Component1
                  }
              },
          }
        },
        ComponentType = ComponentType.Component1
    };

}
