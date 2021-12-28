using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ginlong
{
    /// <summary>
    ///     Provides the reader for the a Ginlong plant.
    /// </summary>
    public sealed class GinlongPlantReader
    {
        private readonly string _plantId;
        private HttpClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GinlongPlantReader"/> class.
        /// </summary>
        /// <param name="plantId">The id of the plant.</param>
        /// <param name="authCookie">The value of the authentication cookie named 'rememberMe'.</param>
        public GinlongPlantReader(string plantId, string authCookie)
        {
            _plantId = plantId;
            InitializeClient(authCookie);
        }

        /// <summary>
        ///     Get the data points for the specified period.
        /// </summary>
        /// <param name="start">The starting date of the period.</param>
        /// <param name="end">The ending date (inclusive) of the period.</param>
        /// <returns></returns>
        public async Task<IEnumerable<GinlongPlantDataPoint>> GetPeriod(DateTime start, DateTime end)
        {
            var result = new List<GinlongPlantDataPoint>();

            DateTime current = start;
            while (current <= end)
            {
                var response = await Request(current);
                var data = Parse(response);

                result.AddRange(data);

                current = current.AddDays(1);
            }

            return result;
        }

        private void InitializeClient(string authCookie)
        {
            var baseAddress = new Uri("https://m.ginlong.com");

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("rememberMe", authCookie));

            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            _client = new HttpClient(handler);
        }

        private async Task<string> Request(DateTime dateTime)
        {
            // A GET request is performed with three parameters
            //   plantId: the id of the plant
            //   type: fixed value of 1
            //   day: the day for which to get the data in format yyyy/MM/dd
            var url = string.Format(
                "https://m.ginlong.com/cpro/epc/plantDetail/showCharts.json?plantId={0}&type=1&date={1}",
                _plantId,
                dateTime.ToString("yyyy/MM/dd"));

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new HttpRequestException("The request was unsuccessfull.");
        }

        private IEnumerable<GinlongPlantDataPoint> Parse(string response)
        {

            var json = JObject.Parse(response);
            var data = (JArray)json["result"]["chartsDataAll"];

            foreach (var item in data)
            {
                yield return new GinlongPlantDataPoint()
                {
                    Date = (string)item["date"],
                    Power = (double)item["power"],
                };
            }
        }
    }
}