using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Класс для описания конфигурации фрейма переделанных данных
    /// </summary>
    public class ConfigurationRedonePmuData
    {
        public List<RedonePmuData> Data { get; set; }

        public DateTime Time { get; set; }

        public ConfigurationRedonePmuData() { }

        public ConfigurationRedonePmuData(
            List<RedonePmuData> data, DateTime time)
        {
            this.Data = data;
            this.Time = time;
        }
    }
}
