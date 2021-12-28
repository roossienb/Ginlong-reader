using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ginlong
{
    /// <summary>
    ///     Provides the reader for the Ginlong API.
    /// </summary>
    public sealed class GinlongInverterReader
    {
        private readonly string _deviceId;
        private HttpClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GinlongInverterReader"/> class.
        /// </summary>
        /// <param name="deviceId">The id of the inverter device.</param>
        /// <param name="authCookie">The value of the authentication cookie named 'rememberMe'.</param>
        public GinlongInverterReader(string deviceId, string authCookie)
        {
            _deviceId = deviceId;
            InitializeClient(authCookie);
        }

        /// <summary>
        ///     Get the data points for the specified period.
        /// </summary>
        /// <param name="start">The starting date of the period.</param>
        /// <param name="end">The ending date (inclusive) of the period.</param>
        /// <returns></returns>
        public async Task<IEnumerable<GinlongInverterDataPoint>> GetPeriod(DateTime start, DateTime end)
        {
            var result = new List<GinlongInverterDataPoint>();

            DateTime current = start;
            while (current <= end)
            {
                var response = await Request(current);

                // The response is not valid JSON and requires fixing before it is parsed.
                var fixedResponse = FixResponse(response);

                var json = JsonConvert.DeserializeObject<GinlongResult>(fixedResponse);
                result.AddRange(json.result);

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
            //   deviceId: the id of the inverter
            //   type: fixed value of 1
            //   day: the day for which to get the data in format yyyy/MM/dd
            var url = string.Format(
                "https://m.ginlong.com/cpro/device/doInvGraph.json?deviceId={0}&type=1&day={1}",
                _deviceId,
                dateTime.ToString("yyyy/MM/dd"));

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new HttpRequestException("The request was unsuccessfull.");
        }

        private string FixResponse(string response)
        {
            // To create valid JSON:
            //   replace \" by "
            //   replace "{ by {
            //   replace }" by }
            //   replace ' by
            return response
                .Replace("\\\"", "\"")
                .Replace("\"{", "{")
                .Replace("}\"", "}")
                .Replace("'", "\"");
        }
    }
}