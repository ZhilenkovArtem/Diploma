using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Конфигурация кадра данных
    /// </summary>
    [Obsolete]
    public class ConfigurationFrame
    {
        /// <summary>
        /// Данных по всем PMU в кадре данных
        /// </summary>
        public List<PMU> Data { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public float Time { get; set; }

        public ConfigurationFrame() { }

        public ConfigurationFrame(List<PMU> data, float time)
        {
            this.Data = data;
            this.Time = time;
        }
    }
}
