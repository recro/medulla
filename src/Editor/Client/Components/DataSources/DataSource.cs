// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Editor.Client.Components.DataSources
{
    /// <summary>
    /// DataSource
    /// </summary>
    public class DataSource
    {

        /// <summary>
        /// Uuid
        /// </summary>
        public string? Uuid { get; set; }

        /// <summary>
        /// data source name
        /// </summary>
        public string? DataSourceName { get; set; }

        /// <summary>
        /// is static data
        /// </summary>
        public bool IsStaticData { get; set; }

        /// <summary>
        /// is grpc data source
        /// </summary>
        public bool IsGrpcDataSource { get; set; }

        /// <summary>
        /// is external api rest source
        /// </summary>
        public bool IsExternApiRestSource { get; set; }

        /// <summary>
        /// data source type
        /// </summary>
        public string? DataSourceType { get; set; }

        /// <summary>
        /// static data
        /// </summary>
        public string? StaticData { get; set; }

        /// <summary>
        /// cast to data type
        /// </summary>
        public string? CastDataToType { get; set; }

        /// <summary>
        /// grpc source
        /// </summary>
        public string? GrpcSource { get; set; }

        /// <summary>
        /// external rest api source
        /// </summary>
        public string? ExternalRestApiSource { get; set; }



    }
}
