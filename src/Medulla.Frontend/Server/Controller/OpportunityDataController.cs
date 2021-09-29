// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace Medulla.Frontend.Server.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class OpportunityDataController : ControllerBase
    {
        [HttpGet]
        public async Task<bool> Get()
        {
            // Returning oppounity json records
            return await DataAccess.OpportunityDA.GetOpportunities();
        }
    }
}
