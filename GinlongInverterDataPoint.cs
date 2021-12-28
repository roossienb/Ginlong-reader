using Newtonsoft.Json;
using System.Text;

namespace Ginlong
{
    /// <summary>
    ///     Provides the data model for a Ginlong data point.
    /// </summary>
    public sealed class GinlongInverterDataPoint
    {
        /// <summary>
        ///     Gets or sets the DC voltage on the first string in Volt.
        /// </summary>
        [JsonProperty("1a")]
        public double DcVoltageString1 { get; set; }

        /// <summary>
        ///     Gets or sets the DC voltage on the second string in Volt.
        /// </summary>
        [JsonProperty("1b")]
        public double DcVoltageString2 { get; set; }

        /// <summary>
        ///     Gets or sets the DC current in the first string in Ampere.
        /// </summary>
        [JsonProperty("1j")]
        public double DcCurrentString1 { get; set; }

        /// <summary>
        ///     Gets or sets the DC current in the second string in Ampere.
        /// </summary>
        [JsonProperty("1k")]
        public double DcCurrentString2 { get; set; }

        /// <summary>
        ///     Gets or sets the DC power of the two strings combined in Watt.
        /// </summary>
        [JsonProperty("1ab")]
        public double DcPower { get; set; }

        /// <summary>
        ///     Gets or sets the AC voltage on the first phase in Volt.
        /// </summary>
        [JsonProperty("1ah")]
        public double AcVoltagePhase1 { get; set; }

        /// <summary>
        ///     Gets or sets the AC voltage on the second phase in Volt.
        /// </summary>
        [JsonProperty("1ai")]
        public double AcVoltagePhase2 { get; set; }

        /// <summary>
        ///     Gets or sets the AC voltage on the third phase in Volt.
        /// </summary>
        [JsonProperty("1aj")]
        public double AcVoltagePhase3 { get; set; }

        /// <summary>
        ///     Gets or sets the AC current in the first phase in Ampere.
        /// </summary>
        [JsonProperty("1ak")]
        public double AcCurrentPhase1 { get; set; }

        /// <summary>
        ///     Gets or sets the AC current in the second phase in Ampere.
        /// </summary>
        [JsonProperty("1al")]
        public double AcCurrentPhase2 { get; set; }

        /// <summary>
        ///     Gets or sets the AC current in the third phase in Ampere
        /// </summary>
        [JsonProperty("1am")]
        public double AcCurrentPhase3 { get; set; }

        /// <summary>
        ///     Gets or sets the AC total active power in Watt.
        /// </summary>
        [JsonProperty("1ao")]
        public double AcActivePower { get; set; }

        /// <summary>
        ///     Gets or sets the grid frequency in Hertz.
        /// </summary>
        [JsonProperty("1ar")]
        public double Frequency { get; set; }

        /// <summary>
        ///     Gets or sets the AC apparent power in Volt-Ampere.
        /// </summary>
        [JsonProperty("1bs")]
        public double AcApparentPower { get; set; }

        /// <summary>
        ///     Gets or sets the power factor of the electricity put on the grid.
        /// </summary>
        [JsonProperty("1bt")]
        public double PowerFactor { get; set; }

        /// <summary>
        ///     Gets or sets the temperature of the inverter in degrees Celsius.
        /// </summary>
        [JsonProperty("1df")]
        public double InverterTemperature { get; set; }

        public static string GetHeaderList()
        {
            return new StringBuilder()
                .Append(@"DC V1")
                .Append(@",DC V2")
                .Append(@",DC I1")
                .Append(@",DC I2")
                .Append(@",DC P")
                .Append(@",AC V1")
                .Append(@",AC V2")
                .Append(@",AC V3")
                .Append(@",AC I1")
                .Append(@",AC I2")
                .Append(@",AC I3")
                .Append(@",AC P")
                .Append(@",AC S")
                .Append(@",AC cosphi")
                .Append(@",AC f")
                .Append(@",T_inverter")
                .ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return new StringBuilder()
                .AppendFormat("{0}", DcVoltageString1)
                .AppendFormat(",{0}", DcVoltageString1)
                .AppendFormat(",{0}", DcCurrentString1)
                .AppendFormat(",{0}", DcCurrentString1)
                .AppendFormat(",{0}", DcPower)
                .AppendFormat(",{0}", AcVoltagePhase1)
                .AppendFormat(",{0}", AcVoltagePhase2)
                .AppendFormat(",{0}", AcVoltagePhase3)
                .AppendFormat(",{0}", AcCurrentPhase1)
                .AppendFormat(",{0}", AcCurrentPhase2)
                .AppendFormat(",{0}", AcCurrentPhase3)
                .AppendFormat(",{0}", AcActivePower)
                .AppendFormat(",{0}", AcApparentPower)
                .AppendFormat(",{0}", PowerFactor)
                .AppendFormat(",{0}", Frequency)
                .AppendFormat(",{0}", InverterTemperature)
                .ToString();
        }
    }
}