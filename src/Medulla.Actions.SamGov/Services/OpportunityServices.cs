// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Medulla.Actions.SamGov.Services
{
    public class OpportunityServices
    {
        public static async Task<bool> GetOpportunities(IConfiguration configuration)
        {
            // Create API key from https://sam.gov/content/home after creating account
            // and change API Key in Environment Variable.

            var apiKey = configuration["API:Key"]; //Environment.GetEnvironmentVariable("Key");
            var postedFrom = Environment.GetEnvironmentVariable("PostedFrom");
            var postedTo = Environment.GetEnvironmentVariable("PostedTo");
            string url = $"https://api.sam.gov/prod/opportunities/v1/search?limit=15&api_key={apiKey}&postedFrom={postedFrom}&postedTo={postedTo}";
            string response;
            var isSucess = false;
            using var client = new HttpClient();
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var res = await client.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    response = res.Content.ReadAsStringAsync().Result;
                    isSucess = res.IsSuccessStatusCode;
                }
                else
                {
                    response = res.StatusCode.ToString();
                }
                await File.WriteAllTextAsync($"opportunity_{DateTime.Now.ToString("MMddyyyyss")}.json", response);
                return isSucess;
            }
        }
    }
}
