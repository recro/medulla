// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

namespace Medulla.Frontend.Client.Components.Editor.DataSources
{
    public class DataSource
    {

        public string uuid { get; set; }

        public string dataSourceName { get; set; }

        public bool isStaticData { get; set; }

        public bool isGrpcDataSource { get; set; }

        public bool isExternApiRestSource { get; set; }

        public string dataSourceType { get; set; }

        public string staticData { get; set; }

        public string castDataToType { get; set; }

        public string grpcSource { get; set; }

        public string externalRestApiSource { get; set; }



    }
}
