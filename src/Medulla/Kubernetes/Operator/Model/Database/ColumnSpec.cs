// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Kubernetes.Operator.Model.Database;

/// <summary>
/// Spec of Column within Model as array
/// </summary>
public struct ColumnSpec
{

    /// <summary>
    /// ColumnSpec Constructor
    /// </summary>
    public ColumnSpec(ValidateSpec validateSpec)
    {
        Validate = validateSpec;
    }

    /// <summary>
    /// ColumnName is an element of ColumnSpec which is the column name
    /// </summary>
    public string ColumnName { get; set; } = "defaultColumnName";

    /// <summary>
    /// Type is an element of ColumnSpec which is the column type
    /// </summary>
    public string Type { get; set; } = "TEXT";

    /// <summary>
    /// AllowNull is an element of ColumnSpec which indicates whether or not null is allowed
    /// </summary>
    public bool AllowNull { get; set; } = true;

    /// <summary>
    /// DefaultValue is an element of ColumnSpec which should be set to what the default value of the column should
    /// be if it exists
    /// </summary>
    public string? DefaultValue { get; set; } = "";

    /// <summary>
    /// AutoIncrement is an element of ColumnSpec which indicates whether auto increment is allowed or not
    /// </summary>
    public bool AutoIncrement { get; set; } = false;

    /// <summary>
    /// PrimaryKey is an element of ColumnSpec which indicates whether this column is the primary key
    /// </summary>
    public bool PrimaryKey { get; set; } = false;

    /// <summary>
    /// Field is an element of ColumnSpec which is the field name
    /// </summary>
    public string Field { get; set; } = "defaultFieldName";

    /// <summary>
    /// Unique is an element of ColumnSpec which is indicates whether or not this column is unique
    /// </summary>
    public string Unique { get; set; } = "uniqueFieldIndexName";

    /// <summary>
    /// Comment is an element of ColumnSpec which is a comment of this column
    /// </summary>
    public string Comment { get; set; } = "Comment for Column";
    /// <summary>
    /// ValidateSpec? is an element of ColumnSpec which is a type ValidateSpec which validates fields
    /// </summary>
    public ValidateSpec? Validate { get; set; }

}
