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
        public /*DateTime*/ float Time { get; set; }

        public DeviationsList() { }

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

        //public DeviationsList(ConfigurationFrame configFrame, ConfigurationFrame previousConfigFrame)
        //{
        //    this.Time = configFrame.Time;
        //    this.CenterOfInertia = GetCenterOfInertia(configFrame.Data, previousConfigFrame.Data);
        //    this.DevList = GetDevList(configFrame.Data, previousConfigFrame.Data, CenterOfInertia);
        //}

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

        /*private float GetCenterOfInertia(List<PMU> data, List<PMU> previuosData)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var pMU = data[i];
                var previousPMU = previuosData[i];
                var angle = GetActualAngle(pMU.Voltage.Phase, previousPMU.Voltage.Phase);
                float power = pMU.Power;
                numerator += power * angle;
                denominator += power;
            }
            return (numerator / denominator);
        }*/

        /// <summary>
        /// Получить список отклонений для данного среза данных
        /// </summary>
        /// <param name="data">Данные по всем PMU в кадре</param>
        /// <param name="coi">Центр инерции</param>
        /// <returns>Список отклонений</returns>
        private List<PMUsDeviation> GetDevList(List<RedonePmuData> data, float coi)
        {
            var devList = new List<PMUsDeviation>();
            foreach (var pmu in data)
            {
                devList.Add(new PMUsDeviation(pmu.IDCode, pmu.VoltagePhase - coi));
            }
            return devList;
        }

        /*private List<PMUsDeviation> GetDevList(List<PMU> data, List<PMU> previuosData, float coi)
        {
            var devList = new List<PMUsDeviation>();
            for (int i = 0; i < data.Count; i++)
            {
                var pMU = data[i];
                var previousPMU = previuosData[i];
                var angle = GetActualAngle(pMU.Voltage.Phase, previousPMU.Voltage.Phase);
                devList.Add(new PMUsDeviation(pMU.IDCode, angle - coi));
            }
            return devList;
        }*/

        /*private float GetActualAngle(double phase, double previousPhase)
        {
            var angle = (float)(phase * 180 / Math.PI);
            var prevAngle = (float)(previousPhase * 180 / Math.PI);
            if ((angle - prevAngle) < -250)
            {
                angle = angle + 360;
            }
            else if ((angle - prevAngle) > 250)
            {
                angle = angle - 360;
            }
            return angle;
        }*/
    }
}
