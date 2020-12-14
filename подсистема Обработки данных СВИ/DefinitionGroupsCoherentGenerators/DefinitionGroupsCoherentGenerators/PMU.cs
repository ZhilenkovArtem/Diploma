namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Данные устройства синхронизированных векторных измерений
    /// </summary>
    public class PMU
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID PMU
        /// </summary>
        public int IDCode { get; set; }

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
        /// <param name="name">Наименование</param>
        /// <param name="idCode">ID</param>
        /// <param name="voltage">Напряжение</param>
        public PMU(string name, int idCode, Complex voltage)
        {
            this.Name = name;
            this.IDCode = idCode;
            this.Voltage = voltage;
            this.Power = float.NaN;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="idCode">ID</param>
        /// <param name="voltage">Напряжение</param>
        /// <param name="power">Мощность</param>
        public PMU(string name, int idCode, Complex voltage, float power)
        {
            this.Name = name;
            this.IDCode = idCode;
            this.Voltage = voltage;
            this.Power = power;
        }
    }
}
