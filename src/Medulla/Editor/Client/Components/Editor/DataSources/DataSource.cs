// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Components.Editor.DataSources
{
    /// <summary>
    /// DataSource
    /// </summary>
    public class DataSource
    {

        /// <summary>
        /// uuid
        /// </summary>
        public string? uuid { get; set; }

        /// <summary>
        /// data source name
        /// </summary>
        public string? dataSourceName { get; set; }

        /// <summary>
        /// is static data
        /// </summary>
        public bool isStaticData { get; set; }

        /// <summary>
        /// is grpc data source
        /// </summary>
        public bool isGrpcDataSource { get; set; }

        /// <summary>
        /// is external api rest source
        /// </summary>
        public bool isExternApiRestSource { get; set; }

        /// <summary>
        /// data source type
        /// </summary>
        public string? dataSourceType { get; set; }

        /// <summary>
        /// static data
        /// </summary>
        public string? staticData { get; set; }

        /// <summary>
        /// cast to data type
        /// </summary>
        public string? castDataToType { get; set; }

        /// <summary>
        /// grpc source
        /// </summary>
        public string? grpcSource { get; set; }

        /// <summary>
        /// external rest api source
        /// </summary>
        public string? externalRestApiSource { get; set; }



    }
}
