using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Список отклонений для среза данных от центра инерции системы
    /// </summary>
    public class DeviationsList
    {
        /// <summary>
        /// Список отклонений по всем PMU
        /// </summary>
        public List<PMUsDeviation> DevList { get; set; }

        /// <summary>
        /// Центр инерции системы
        /// </summary>
        public float CenterOfInertia { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configFrame">Данные из кадра</param>
        public DeviationsList(ConfigurationFrame configFrame)
        {
            this.Time = configFrame.Time;
            this.CenterOfInertia = GetCenterOfInertia(configFrame.Data);
            this.DevList = GetDevList(configFrame.Data, CenterOfInertia);
        }

        /// <summary>
        /// Получить центр инерции системы
        /// </summary>
        /// <param name="data">Данные по всем PMU в кадре</param>
        /// <returns>Значение центра инерции</returns>
        private float GetCenterOfInertia(HashSet<PMU> data)
        {
            float numerator = 0;
            float denominator = 0;
            foreach (var pmu in data)
            {
                var values = Complex.ConvertToTrigonometric(pmu.Voltage);
                var angle = values[1];
                var power = pmu.Power;
                numerator += power * angle;
                denominator += power;
            }
            return numerator / denominator;
        }

        /// <summary>
        /// Получить список отклонений для данного среза данных
        /// </summary>
        /// <param name="data">Данные по всем PMU в кадре</param>
        /// <param name="coi">Центр инерции</param>
        /// <returns>Список отклонений</returns>
        private List<PMUsDeviation> GetDevList(HashSet<PMU> data, float coi)
        {
            var devList = new List<PMUsDeviation>();
            foreach (var pmu in data)
            {
                var angle = Complex.ConvertToTrigonometric(pmu.Voltage)[1];
                devList.Add(new PMUsDeviation(pmu.IDCode, angle - coi));
            }
            return devList;
        }
    }
}
