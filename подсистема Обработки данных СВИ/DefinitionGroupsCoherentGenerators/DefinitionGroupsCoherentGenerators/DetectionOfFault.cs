using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Определение было ли серьезное возмущение
    /// </summary>
    public class DetectionOfFault
    {
        /// <summary>
        /// Максимальное накопленное отклонение
        /// </summary>
        private const int MAXACCUMULATEDDEVIATION = 30;

        /// <summary>
        /// Определить было ли возмущение
        /// </summary>
        /// <param name="devList">Список отклонений по всем PMU</param>
        /// <returns>Было или не было возмущение</returns>
        public static bool DetectFault(List<PMUsDeviation> devList)
        {
            foreach (var pmu in devList)
            {
                if (pmu.AccumulatedDeviation >= MAXACCUMULATEDDEVIATION)
                    return true;
            }
            return false;
        }
    }
}
