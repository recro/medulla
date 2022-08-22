// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using k8s.Models;

namespace StorageService.Kubernetes.Crds.Data;

/// <summary>
/// delete code
/// </summary>
public class CResource : CustomResource<Databases, CResourceStatus>
{
    /// <summary>
    /// delete code
    /// </summary>
    public override string ToString()
    {
        var labels = "{";
        foreach (var kvp in Metadata?.Labels!)
        {
            labels += kvp.Key + " : " + kvp.Value + ", ";
        }

        labels = labels.TrimEnd(',', ' ') + "}";

        return $"{Metadata.Name} (Labels: {labels})";
    }


}


/// <summary>
/// delete code
/// </summary>
public class Column
{
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("columnName")]
    public string? ColumnName { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("allowNull")]
    public bool AllowNull { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("defaultValue")]
    public string? DefaultValue { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("primaryKey")]
    public bool PrimaryKey { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("field")]
    public string? Field { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("unique")]
    public string? Unique { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

}

/// <summary>
/// delete code
/// </summary>
public class Model
{
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }


    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("columns")]
    public List<Column>? Columns { get; set; }
}

/// <summary>
/// delete code
/// </summary>
public class Databases
{
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("host")]
    public string? Host { get; set; }
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("dialect")]
    public string? Dialect { get; set; }
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("usernameSecretName")]
    public string? UsernameSecretName { get; set; }
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("passwordSecretName")]
    public string? PasswordSecretName { get; set; }
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("models")]
    public List<Model>? Models { get; set; }

}

/// <summary>
/// delete code
/// </summary>
public class CResourceStatus : V1Status
{
    /// <summary>
    /// delete code
    /// </summary>
    [JsonPropertyName("temperature")]
    public string? Temperature { get; set; }
}
