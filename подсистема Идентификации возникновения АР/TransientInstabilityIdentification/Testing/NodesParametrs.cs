namespace Testing
{
    /// <summary>
    /// Параметры узла
    /// </summary>
    public class NodesParametrs
    {
        /// <summary>
        /// Напряжение
        /// </summary>
        public double Voltage { get; set; }

        /// <summary>
        /// Угол напряжения
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="voltage">Напряжение</param>
        /// <param name="angle">Угол напряжения</param>
        public NodesParametrs(double voltage, double angle)
        {
            this.Voltage = voltage;
            this.Angle = angle;
        }
    }
}
