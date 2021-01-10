using System;
using System.Collections.Generic;
using System.Numerics;

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
        public float Time { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config">Данные из кадра</param>
        public DeviationsList(ConfigurationRedonePmuData config)
        {
            this.Time = config.Time;
            this.CenterOfInertia = GetCenterOfInertia(config.Data);
            this.DevList = GetDevList(config.Data, CenterOfInertia);
        }

        /// <summary>
        /// Получить центр инерции системы
        /// </summary>
        /// <param name="data">Данные по всем PMU в кадре</param>
        /// <returns>Значение центра инерции</returns>
        private float GetCenterOfInertia(List<RedonePmuData> data)
        {
            float numerator = 0;
            float denominator = 0;
            foreach (var pmu in data)
            {
                var angle = pmu.VoltagePhase;
                float power = pmu.Power;
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
        private List<PMUsDeviation> GetDevList(
            List<RedonePmuData> data, float coi)
        {
            var devList = new List<PMUsDeviation>();
            foreach (var pmu in data)
            {
                devList.Add(new PMUsDeviation(
                    pmu.IDCode, pmu.VoltagePhase - coi));
            }
            return devList;
        }
    }
}
