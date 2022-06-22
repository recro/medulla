// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using KubeOps.Operator.Entities.Annotations;

namespace Medulla.Kubernetes.Operator.Model.Database
{
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
}
