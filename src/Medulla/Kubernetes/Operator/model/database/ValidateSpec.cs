// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator.model
{

    /// <summary>
    /// Spec in Column for validation
    /// </summary>
    public struct ValidateSpec
    {
        public string? Is { get; set; }
        public string? Not { get; set; }
        public bool? IsEmail { get; set; }
        public bool? IsUrl { get; set; }
        public bool? IsIp { get; set; }
        public bool? IsIpV4 { get; set; }
        public bool? IsIpV6 { get; set; }
        public bool? IsAlpha { get; set; }
        public bool? IsAlphaNumeric { get; set; }
        public bool? IsNumeric { get; set; }
        public bool? IsInt { get; set; }
        public bool? IsFloat { get; set; }
        public bool? IsDecimal { get; set; }
        public bool? IsLowercase { get; set; }
        public bool? IsUppercase { get; set; }
        public bool? IsNull { get; set; }
        public bool? NotNull { get; set; }
        public bool? NotEmpty { get; set; }
    }

}
