using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Конфигурация кадра данных
    /// </summary>
    public class ConfigurationFrame
    {
        /// <summary>
        /// Данных по всем PMU в кадре данных
        /// </summary>
        public HashSet<PMU> Data { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public DateTime Time { get; set; }
    }
}
