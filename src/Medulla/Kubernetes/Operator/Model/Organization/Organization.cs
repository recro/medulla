// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using KubeOps.Operator.Entities.Annotations;

namespace Medulla.Kubernetes.Operator.Model.Organization;

/// <summary>
/// Spec of CRD for Organization
/// </summary>
public struct Organization
{
    /// <summary>
    /// ModelSpec constructor
    /// </summary>
    public Organization(){}
    /// <summary>
    /// Name is an element of ModelSpec which is the name of the model/table
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

}
