using System;
using System.Threading.Tasks;

namespace Ginlong
{
    /// <summary>
    ///     Example program for Ginlong export.
    /// </summary>
    /// <remarks>
    ///     See bartroossien.com for explanation.
    /// </remarks>
    class Program
    {
        /// <summary>
        ///     Enter the ID of the inverter here.
        /// </summary>
        private static readonly string _deviceId = "000000";

        /// <summary>
        ///     Enter the ID of the planet here.
        /// </summary>
        private static readonly string _plantId = "0000000";

        /// <summary>
        ///     Enter the value of the 'rememberMe' cookie here.
        /// </summary>
        private static readonly string _authCookie = @"00000000";

        public static void Main(string[] args)
        {
            PlantExample().Wait();
            InverterExample().Wait();
        }

        private async static Task PlantExample()
        {
            var startDate = new DateTime(2021, 1, 1);
            var endDate = new DateTime(2021, 1, 2);

            var reader = new GinlongPlantReader(_plantId, _authCookie);
            var dataPoints = await reader.GetPeriod(startDate, endDate);

            new GinlongPlantCsvWriter()
                .WithHeader()
                .SetFileName($"plant_{_plantId}.csv")
                .SetData(dataPoints)
                .Execute();
        }

        private async static Task InverterExample()
        {
            var startDate = new DateTime(2021, 12, 1);
            var endDate = new DateTime(2021, 12, 2);

            var reader = new GinlongInverterReader(_deviceId, _authCookie);
            var dataPoints = await reader.GetPeriod(startDate, endDate);

            new GinlongInverterCsvWriter()
                .WithHeader()
                .SetFileName($"device_{_deviceId}.csv")
                .SetData(dataPoints)
                .Execute();
        }
    }
}