using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ginlong
{
    /// <summary>
    ///     Provides a writer to CSV for Ginlong data points.
    /// </summary>
    public sealed class GinlongInverterCsvWriter
    {
        private IEnumerable<GinlongInverterDataPoint> _data = new List<GinlongInverterDataPoint>();
        private bool _hasHeader = false;
        private string _filename = "plant.csv";

        /// <summary>
        ///     Enables the header printed to the file.
        /// </summary>
        /// <returns>A reference to this <see cref="GinlongInverterCsvWriter"/> instance.</returns>
        public GinlongInverterCsvWriter WithHeader()
        {
            _hasHeader = true;
            return this;
        }

        /// <summary>
        ///     Sets the name of the file.
        /// </summary>
        /// <param name="filename">The name of the export file.</param>
        /// <returns>A reference to this <see cref="GinlongInverterCsvWriter"/> instance.</returns>
        public GinlongInverterCsvWriter SetFileName(string filename)
        {
            _filename = filename;
            return this;
        }

        /// <summary>
        ///     Sets the data to be exported.
        /// </summary>
        /// <param name="data">The dataset to be exported.</param>
        /// <returns>A reference to this <see cref="GinlongInverterCsvWriter"/> instance.</returns>
        public GinlongInverterCsvWriter SetData(IEnumerable<GinlongInverterDataPoint> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            return this;
        }

        /// <summary>
        ///     Executes the export.
        /// </summary>
        public void Execute()
        {
            var strBuilder = new StringBuilder();

            if (_hasHeader)
            {
                strBuilder.AppendLine(GinlongInverterDataPoint.GetHeaderList());
            }

            foreach(var point in _data)
            {
                strBuilder.AppendLine(point.ToString());
            }

            File.WriteAllText(_filename, strBuilder.ToString());
        }
    }
}