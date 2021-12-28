using Newtonsoft.Json;
using System.Text;

namespace Ginlong
{
    /// <summary>
    ///     Provides the data model for a Ginlong plant data point.
    /// </summary>
    public sealed class GinlongPlantDataPoint
    {
        /// <summary>
        ///     Gets or sets the date in Unix milliseconds.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        ///     Gets or sets the power output of the plant in kW.
        /// </summary>
        public double Power { get; set; }

        public static string GetHeaderList()
        {
            return new StringBuilder()
                .Append(@"Date")
                .Append(@",Power")
                .ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return new StringBuilder()
                .AppendFormat("{0}", Date)
                .AppendFormat(",{0}", Power)
                .ToString();
        }
    }
}