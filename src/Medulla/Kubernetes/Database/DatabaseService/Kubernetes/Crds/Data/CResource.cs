using System.Text.Json.Serialization;
using k8s.Models;

namespace DatabaseService.Kubernetes.Crds.Data;

public class CResource : CustomResource<Databases, CResourceStatus>
{
    public override string ToString()
    {
        var labels = "{";
        foreach (var kvp in Metadata.Labels)
        {
            labels += kvp.Key + " : " + kvp.Value + ", ";
        }

        labels = labels.TrimEnd(',', ' ') + "}";

        return $"{Metadata.Name} (Labels: {labels}), Database Name: {Databases[0].Name}";
    }
    
    
}


public class Column
{
    [JsonPropertyName("columnName")]
    public string ColumnName { get; set; }
    
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    
    [JsonPropertyName("allowNull")]
    public bool AllowNull { get; set; }
    
    
    [JsonPropertyName("defaultValue")]
    public string DefaultValue { get; set; }
    
    
    [JsonPropertyName("primaryKey")]
    public bool PrimaryKey { get; set; }
    
    
    [JsonPropertyName("field")]
    public string Field { get; set; }
    
    
    [JsonPropertyName("unique")]
    public string Unique { get; set; }
    
    
    [JsonPropertyName("comment")]
    public string Comment { get; set; }
    
}

public class Model
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    
    [JsonPropertyName("columns")]
    public List<Column> Columns { get; set; }
}

public class Databases
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("host")]
    public string Host { get; set; }
    [JsonPropertyName("dialect")]
    public string Dialect { get; set; }
    [JsonPropertyName("usernameSecretName")]
    public string UsernameSecretName { get; set; }
    [JsonPropertyName("passwordSecretName")]
    public string PasswordSecretName { get; set; }
    [JsonPropertyName("models")]
    public List<Model> Models { get; set; }

}

public class CResourceStatus : V1Status
{
    [JsonPropertyName("temperature")]
    public string Temperature { get; set; }
}