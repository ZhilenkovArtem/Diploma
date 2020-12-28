using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizedVectorMeasurementProcessing
{
    public class RedonePmuData
    {
        /// <summary>
        /// ID PMU
        /// </summary>
        public float IDCode { get; set; }

        /// <summary>
        /// Амплитуда напряжения
        /// </summary>
        public float VoltageMagnitude { get; set; }

        /// <summary>
        /// Фаза напряжения
        /// </summary>
        public float VoltagePhase { get; set; }

        /// <summary>
        /// Мощность
        /// </summary>
        public float Power { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">id</param>
        /// <param name="voltageMagnitude">амплитуда напряжения</param>
        /// <param name="voltagePhase">фаза напряжения</param>
        public RedonePmuData(float idCode, float voltageMagnitude, float voltagePhase)
        {
            this.IDCode = idCode;
            this.VoltageMagnitude = voltageMagnitude;
            this.VoltagePhase = voltagePhase;
            this.Power = float.NaN;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">id</param>
        /// <param name="voltageMagnitude">амплитуда напряжения</param>
        /// <param name="voltagePhase">фаза напряжения</param>
        /// <param name="power">мощность</param>
        public RedonePmuData(float idCode, float voltageMagnitude, float voltagePhase, float power)
        {
            this.IDCode = idCode;
            this.VoltageMagnitude = voltageMagnitude;
            this.VoltagePhase = voltagePhase;
            this.Power = power;
        }
    }
}
