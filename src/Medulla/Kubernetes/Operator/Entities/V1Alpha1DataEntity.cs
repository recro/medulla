// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;

namespace DatabaseControllerKubeOps.Controller.Entities;


/// <summary>
/// DatabaseSpec is spec of CRD for database
/// </summary>
public struct DatabaseSpec
{
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? Name { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? Host { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? Dialect { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? UsernameSecretName { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? PasswordSecretName { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public List<ModelSpec> Models { get; set; }
}

/// <summary>
/// Spec of CRD for model within Database
/// </summary>
public struct ModelSpec
{
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public string? Name { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    [Required]
    public List<ColumnSpec>? Columns { get; set; }
}



/// <summary>
/// Spec in Column for validation
/// </summary>
public struct ValidateSpec
{
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Is { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Not { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsEmail { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsUrl { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsIp { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsIpV4 { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsIpV6 { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsAlpha { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsAlphaNumeric { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsNumeric { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsInt { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsFloat { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsDecimal { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsLowercase { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsUppercase { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? IsNull { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? NotNull { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool? NotEmpty { get; set; }
}

/// <summary>
/// Spec of Column within Model as array
/// </summary>
public struct ColumnSpec
{
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? ColumnName { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool AllowNull { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? DefaultValue { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool AutoIncrement { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public bool PrimaryKey { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Field { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Unique { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// code will be deleted
    /// </summary>
    public ValidateSpec? Validate { get; set; }


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
    /// <summary>
    /// code will be deleted
    /// </summary>
    public List<DatabaseSpec>? Spec { get; set; }
}
