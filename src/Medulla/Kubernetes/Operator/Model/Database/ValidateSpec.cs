// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Operator.Model
{

    /// <summary>
    /// Spec in Column for validation
    /// </summary>
    public struct ValidateSpec
    {
        /// <summary>
        /// matches this RegExp /^[a-z]+$/i
        /// </summary>
        public string? Is { get; set; }
        /// <summary>
        /// matches this RegExp /^[a-z]+$/i
        /// </summary>
        public string? Not { get; set; }
        /// <summary>
        /// checks for email format (foo@bar.com)
        /// </summary>
        public bool? IsEmail { get; set; }
        /// <summary>
        ///   checks for url format (https://foo.com)
        /// </summary>
        public bool? IsUrl { get; set; }
        /// <summary>
        ///   checks for IPv4 (129.89.23.1) or IPv6 format
        /// </summary>
        public bool? IsIp { get; set; }
        /// <summary>
        ///  checks for IPv4 (129.89.23.1)
        /// </summary>
        public bool? IsIpV4 { get; set; }
        /// <summary>
        ///  checks for IPv6 format
        /// </summary>
        public bool? IsIpV6 { get; set; }
        /// <summary>
        /// will only allow letters
        /// </summary>
        public bool? IsAlpha { get; set; }
        /// <summary>
        /// will only allow alphanumeric characters, so "_abc" will fail
        /// </summary>
        public bool? IsAlphaNumeric { get; set; }
        /// <summary>
        /// will only allow numbers
        /// </summary>
        public bool? IsNumeric { get; set; }
        /// <summary>
        ///  checks for valid integers
        /// </summary>
        public bool? IsInt { get; set; }
        /// <summary>
        ///  checks for valid floating point numbers
        /// </summary>
        public bool? IsFloat { get; set; }
        /// <summary>
        /// checks for any numbers
        /// </summary>
        public bool? IsDecimal { get; set; }
        /// <summary>
        ///  checks for lowercase
        /// </summary>
        public bool? IsLowercase { get; set; }
        /// <summary>
        ///  checks for uppercase
        /// </summary>
        public bool? IsUppercase { get; set; }
        /// <summary>
        /// won't allow null
        /// </summary>
        public bool? IsNull { get; set; }
        /// <summary>
        ///  only allows null
        /// </summary>
        public bool? NotNull { get; set; }
        /// <summary>
        /// don't allow empty strings
        /// </summary>
        public bool? NotEmpty { get; set; }
    }

}
