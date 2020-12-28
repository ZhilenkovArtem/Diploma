namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Отклонение угла PMU от центра инерции системы
    /// </summary>
    public class PMUsDeviation
    {
        /// <summary>
        /// ID PMU
        /// </summary>
        public float PMUsIDCode { get; set; }

        /// <summary>
        /// Отклонение угла PMU от центра инерции системы
        /// </summary>
        public float DeviationFromCOI { get; set; }

        /// <summary>
        /// Накопленное отклонение
        /// </summary>
        public float AccumulatedDeviation { get; set; }

        /// <summary>
        /// Разница в отклонении по сравнению с более ранним значением
        /// </summary>
        public float DeltaRelativeToPreviousValue { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">ID PMU</param>
        /// <param name="deviation">Отклонение</param>
        public PMUsDeviation(float idCode, float deviation)
        {
            this.PMUsIDCode = idCode;
            this.DeviationFromCOI = deviation;
            this.AccumulatedDeviation = float.NaN;
            this.DeltaRelativeToPreviousValue = float.NaN;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="idCode">ID PMU</param>
        /// <param name="deviation">Отклонение</param>
        /// <param name="accumDeviation">Накопленное отклонение</param>
        /// <param name="delta">Разница отклонений</param>
        public PMUsDeviation(float idCode, float deviation, 
            float accumDeviation, float delta)
        {
            this.PMUsIDCode = idCode;
            this.DeviationFromCOI = deviation;
            this.AccumulatedDeviation = accumDeviation;
            this.DeltaRelativeToPreviousValue = delta;
        }
    }
}
