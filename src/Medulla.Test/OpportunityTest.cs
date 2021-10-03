// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Medulla.Actions.SamGov.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medulla.Test
{
    [TestClass]
    public class OpportunityTest
    {
        [TestMethod]
        public async Task TestOpportunity()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"API", ""},
                {"API:Key", "Pass API Key here"}
            };

            // Initialize the configuation
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            // Act
            var response = await OpportunityServices.GetOpportunities(configuration);

            //Assert
            Assert.IsTrue(response);
        }
    }
}
