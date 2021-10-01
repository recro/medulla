// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Net.Http.Headers;

namespace Medulla.Actions.SamGov.Services
{
    public class OpportunityServices
    {
        public static async Task<bool> GetOpportunities()
        {
            // Create API key from https://sam.gov/content/home after creating account
            // and change API Key in Environment Variable.
            var apiKey = Environment.GetEnvironmentVariable("Key");
            var postedFrom = Environment.GetEnvironmentVariable("PostedFrom");
            var postedTo = Environment.GetEnvironmentVariable("PostedTo");
            //string url = string.Format(Environment.GetEnvironmentVariable("Url"), apiKey, postedFrom, postedTo);
            string url = $"https://api.sam.gov/prod/opportunities/v1/search?limit=15&api_key={apiKey}&postedFrom={postedFrom}&postedTo={postedTo}";
            string response = String.Empty;
            bool isSucess = false;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(url);

                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                    isSucess = Res.IsSuccessStatusCode;
                }
                else
                {
                    response = Res.StatusCode.ToString();
                }
                await System.IO.File.WriteAllTextAsync($"opportunity_{DateTime.Now.ToString("MMddyyyyss")}.json", response);
                return isSucess;

            }
        }
    }
}
