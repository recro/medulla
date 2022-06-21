// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator.model
{
    /// <summary>
    /// Spec of Column within Model as array
    /// </summary>
    public struct ColumnSpec
    {
        public string? ColumnName { get; set; }
        public string? Type { get; set; }
        public bool AllowNull { get; set; }
        public string? DefaultValue { get; set; }
        public bool AutoIncrement { get; set; }
        public bool PrimaryKey { get; set; }
        public string? Field { get; set; }
        public string? Unique { get; set; }
        public string? Comment { get; set; }
        public ValidateSpec? Validate { get; set; }

    }
}
