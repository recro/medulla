// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Medulla.Frontend.Client.Library.Services.Models;

namespace Medulla.Frontend.Client.Library.Services.TestWeatherService
{
    /// <summary>
    /// Weather Service
    /// </summary>
    public class WeatherService
    {

        /// <summary>
        /// Http Client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Weather Service
        /// </summary>
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get Weather
        /// </summary>
        public async Task<WeatherForecast[]> GetWeather()
        {
            var result = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            if (result != null)
            {
                return result;
            }
            return new WeatherForecast[] { };
        }


    }
}
