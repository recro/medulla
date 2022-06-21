// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator.Model
{
    /// <summary>
    /// Spec of Column within Model as array
    /// </summary>
    public struct ColumnSpec
    {
        /// <summary>
        /// ColumnName is an element of ColumnSpec which is the column name
        /// </summary>
        public string? ColumnName { get; set; }
        /// <summary>
        /// Type is an element of ColumnSpec which is the column type
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// AllowNull is an element of ColumnSpec which indicates whether or not null is allowed
        /// </summary>
        public bool AllowNull { get; set; }
        /// <summary>
        /// DefaultValue is an element of ColumnSpec which should be set to what the default value of the column should
        /// be if it exists
        /// </summary>
        public string? DefaultValue { get; set; }
        /// <summary>
        /// AutoIncrement is an element of ColumnSpec which indicates whether auto increment is allowed or not
        /// </summary>
        public bool AutoIncrement { get; set; }
        /// <summary>
        /// PrimaryKey is an element of ColumnSpec which indicates whether this column is the primary key
        /// </summary>
        public bool PrimaryKey { get; set; }
        /// <summary>
        /// Field is an element of ColumnSpec which is the field name
        /// </summary>
        public string? Field { get; set; }
        /// <summary>
        /// Unique is an element of ColumnSpec which is indicates whether or not this column is unique
        /// </summary>
        public string? Unique { get; set; }
        /// <summary>
        /// Comment is an element of ColumnSpec which is a comment of this column
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// ValidateSpec? is an element of ColumnSpec which is a type ValidateSpec which validates fields
        /// </summary>
        public ValidateSpec? Validate { get; set; }

    }
}
