// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;

namespace ApplicationControllerKubeOps.Controller.Entities;


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

/// <summary>
/// V1Alpha1DataEntity is CRD for data
/// </summary>
[Description("A CustomResourceDefinition which allows building with data in Medulla.")]
[KubernetesEntity(
    ApiVersion = "v1alpha1",
    Kind = "Application",
    Group = "medulla.io",
    PluralName = "application")]
public class V1Alpha1ApplicationEntity : CustomKubernetesEntity
{
    /// <summary>
    /// Spec is is an element of the V1Alpha1ApplicationEntity  which is a list of type ApplicationSpec
    /// </summary>
    public List<ApplicationSpec>? Spec { get; set; }
}

