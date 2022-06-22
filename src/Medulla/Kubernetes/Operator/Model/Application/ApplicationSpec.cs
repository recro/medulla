// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using KubeOps.Operator.Entities.Annotations;

namespace Medulla.Kubernetes.Operator.Model.Application;


/// <summary>
/// ApplicationSpec is spec of CRD for application
/// </summary>
public struct ApplicationSpec
{
    /// <summary>
    /// Name is is an element of the ApplicationSpec which is the name of the Application created by Medulla
    /// </summary>
    [Required]
    public string? Name { get; set; }
    /// <summary>
    /// Description is is an element of the ApplicationSpec which is the description of the Application created by Medulla
    /// </summary>
    [Required]
    public string? Description { get; set; }
}
