using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Класс для описания конфигурации фрейма переделанных данных
    /// </summary>
    public class ConfigurationRedonePmuData
    {
        public List<RedonePmuData> Data { get; set; }

        public float Time { get; set; }

        public ConfigurationRedonePmuData() { }

        public ConfigurationRedonePmuData(
            List<RedonePmuData> data, float time)
        {
            this.Data = data;
            this.Time = time;
        }
    }
}
