// Licensed to the Medulla Contributors under one or more agreements.
// The Medulla Contributors licenses this file to you under the Apache 2.0 license.
// See the LICENSE file in the project root for more information.

using System;

namespace Medulla.Frontend.Client.Library.Services.Models
{

    /// <summary>
    /// Weather Forecast
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Summary
        /// </summary>
        public string? Summary { get; set; }
    }

}

