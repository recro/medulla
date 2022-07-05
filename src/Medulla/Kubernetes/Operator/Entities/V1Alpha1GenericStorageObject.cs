// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using k8s.Models;
using KubeOps.Operator.Entities;
using KubeOps.Operator.Entities.Annotations;

namespace Operator.Entities;

/// <summary>
/// V1Alpha1DataEntity is CRD for data
/// </summary>
[Description("A CustomResourceDefinition which allows building with data in Medulla.")]
[KubernetesEntity(
    ApiVersion = "v1alpha1",
    Kind = "GenericStorageObject",
    Group = "medulla.io",
    PluralName = "storageobjects")]
public class V1Alpha1GenericStorageObject : CustomKubernetesEntity
{
    /// <summary>
    /// Unique Id
    /// </summary>
    public string Uuid { get; set; } = "";

    /// <summary>
    /// StorageData
    /// </summary>
    public string StorageData { get; set; } = "";

    /// <summary>
    /// Type of data
    /// </summary>
    public string Type { get; set; } = "";

}
