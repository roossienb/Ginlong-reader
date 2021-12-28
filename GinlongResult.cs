using System.Collections.Generic;

namespace Ginlong
{
    /// <summary>
    ///     Provides the data model for the result of the Ginlong API.
    /// </summary>
    public sealed class GinlongResult
    {
        /// <summary>
        ///     Gets or sets the set of data points.
        /// </summary>
        public IEnumerable<GinlongInverterDataPoint> result { get; set; }
    }
}