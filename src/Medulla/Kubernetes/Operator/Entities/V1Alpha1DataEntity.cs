// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;
using Operator.model;

namespace Medulla.Kubernetes.Operator.Entities;


/// <summary>
/// DatabaseSpec is spec of CRD for database
/// </summary>
public struct DatabaseSpec
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Host { get; set; }
    [Required]
    public string? Dialect { get; set; }
    [Required]
    public string? UsernameSecretName { get; set; }
    [Required]
    public string? PasswordSecretName { get; set; }
    [Required]
    public List<ModelSpec> Models { get; set; }
}




/// <summary>
/// V1Alpha1DataEntity is CRD for data
/// </summary>
[Description("A CustomResourceDefinition which allows building with data in Medulla.")]
[KubernetesEntity(
    ApiVersion = "v1alpha1",
    Kind = "Data",
    Group = "medulla.recro.com",
    PluralName = "data")]
public class V1Alpha1DataEntity : CustomKubernetesEntity
{
    public List<DatabaseSpec>? Spec { get; set; }
}
