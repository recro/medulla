// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

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
    /// <summary>
    /// Name is an element of DatabaseSpec which is the name of the database
    /// </summary>
    [Required]
    public string? Name { get; set; }
    /// <summary>
    /// Host is an element of DatabaseSpec which is the connection path to the database
    /// </summary>
    [Required]
    public string? Host { get; set; }
    /// <summary>
    /// Dialect is an element of DatabaseSpec which is the the dialect of the database
    /// </summary>
    [Required]
    public string? Dialect { get; set; }
    /// <summary>
    /// UsernameSecretName is an element of DatabaseSpec which is the username to connect to the database
    /// </summary>
    [Required]
    public string? UsernameSecretName { get; set; }
    /// <summary>
    /// PasswordSecretName is an element of DatabaseSpec which is the password to connect to the database
    /// </summary>
    [Required]
    public string? PasswordSecretName { get; set; }
    /// <summary>
    /// Models is a list elements of type ModelSpec
    /// </summary>
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
    Group = "medulla.io",
    PluralName = "data")]
public class V1Alpha1DataEntity : CustomKubernetesEntity
{
    /// <summary>
    /// Spec is a list elements of type DatabaseSpec
    /// </summary>
    public List<DatabaseSpec>? Spec { get; set; }
}
