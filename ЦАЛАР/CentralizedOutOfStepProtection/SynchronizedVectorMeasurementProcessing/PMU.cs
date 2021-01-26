﻿using System;
using System.Numerics;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Данные устройства синхронизированных векторных измерений
    /// </summary>
    [Obsolete]
    public class PMU
    {
        /// <summary>
        /// ID PMU
        /// </summary>
        public float IDCode { get; set; }

        /// <summary>
        /// Комплексное значение напряжения
        /// </summary>
        public Complex Voltage { get; set; }

        /// <summary>
        /// Мощность
        /// </summary>
        public float Power { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">ID</param>
        /// <param name="voltage">Напряжение</param>
        public PMU(float idCode, Complex voltage)
        {
            this.IDCode = idCode;
            this.Voltage = voltage;
            this.Power = float.NaN;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">ID</param>
        /// <param name="voltage">Напряжение</param>
        /// <param name="power">Мощность</param>
        public PMU(float idCode, Complex voltage, float power)
        {
            this.IDCode = idCode;
            this.Voltage = voltage;
            this.Power = power;
        }
    }
}
