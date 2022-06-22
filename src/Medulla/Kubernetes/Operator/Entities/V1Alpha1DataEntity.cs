// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;
using Medulla.Kubernetes.Operator.Model.Database;

namespace Medulla.Kubernetes.Operator.Entities;


/// <summary>
/// V1Alpha1DataEntity is CRD for data
/// </summary>
[Description("A CustomResourceDefinition which allows building with data in Medulla.")]
[KubernetesEntity(
    ApiVersion = "v1alpha1",
    Kind = "Data",
    Group = "medulla.io",
    PluralName = "data")]
public class V1Alpha1DataEntity : CustomKubernetesEntity
{
    public V1Alpha1DataEntity(){}

    /// <summary>
    /// Spec is a list elements of type DatabaseSpec
    /// </summary>
    public List<DatabaseSpec> Spec { get; set; } = new List<DatabaseSpec>();
}
