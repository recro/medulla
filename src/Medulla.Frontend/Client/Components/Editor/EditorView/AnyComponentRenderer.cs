// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Medulla.Frontend.Client.Components.Editor.EditorView
{
    public class AnyComponentRenderer: IComponent
    {

        private readonly RenderFragment _renderFragment;
        private RenderHandle _renderHandle;

        public AnyComponentRenderer(string _componentName)
        {
            
        }
        
        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            throw new NotImplementedException();
        }
        
    }
}
