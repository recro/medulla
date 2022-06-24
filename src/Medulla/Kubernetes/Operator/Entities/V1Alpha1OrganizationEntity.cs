// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;
using KubeOps.Operator.Entities.Extensions;
using Medulla.Kubernetes.Operator.Model.Organization;

namespace Medulla.Kubernetes.Operator.Entities;



/// <summary>
/// V1Alpha1OrganizationEntity is CRD for organization
/// </summary>
[Description("A CustomResourceDefinition which allows building with organization in Medulla.")]
[KubernetesEntity(
    ApiVersion = "v1alpha1",
    Kind = "Organization",
    Group = "medulla.io",
    PluralName = "organization")]
public class V1Alpha1OrganizationEntity : CustomKubernetesEntity
{
    public V1Alpha1OrganizationEntity() { }

    /// <summary>
    /// Spec is a list elements of type Organization
    /// </summary>
    public List<OrganizationSpec> Spec { get; set; } = new List<OrganizationSpec>();
}
