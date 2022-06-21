// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using KubeOps.Operator.Entities.Annotations;

namespace Operator.model
{
    /// <summary>
    /// Spec of CRD for model within Database
    /// </summary>
    public struct ModelSpec
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public List<ColumnSpec>? Columns { get; set; }
    }

}
