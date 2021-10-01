// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Net.Http;
using System.Threading.Tasks;
using Medulla.Actions.SamGov.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medulla.Test
{
    [TestClass]
    public class OpportunityTest
    {
        [TestMethod]
        public async Task TestOpportunity()
        {
            // Act
            var response = await OpportunityServices.GetOpportunities();

            //Assert
            Assert.IsTrue(response);
        }
    }
}
