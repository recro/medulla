using System.Net.Http;
using System.Threading.Tasks;
using Medulla.Frontend.Server.Controller;
using Medulla.Frontend.Server.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medulla.Test
{
    [TestClass]
    public class OpportunityTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            // Act
            var response = await OpportunityDA.GetOpportunities();

            //Assert
            Assert.IsTrue(response);
        }
    }
}
