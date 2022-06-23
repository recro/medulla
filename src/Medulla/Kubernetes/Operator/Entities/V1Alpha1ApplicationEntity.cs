// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;
using Medulla.Kubernetes.Operator.Model.Application;

namespace Medulla.Kubernetes.Operator.Entities;


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
    /// Empty Constructor for V1Alpha1ApplicationEntity
    /// </summary>
    public V1Alpha1ApplicationEntity() { }
    /// <summary>
    /// Spec is is an element of the V1Alpha1ApplicationEntity  which is a list of type ApplicationSpec
    /// </summary>
    public List<ApplicationSpec> Spec { get; set; } = new List<ApplicationSpec>();

    [Required]
    public V1Alpha1OrganizationEntity Organization { get; set; } = new V1Alpha1OrganizationEntity();
}

