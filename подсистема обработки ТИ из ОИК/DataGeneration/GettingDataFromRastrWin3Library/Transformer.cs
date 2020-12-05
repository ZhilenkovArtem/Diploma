using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Трансформатор
    /// </summary>
    public class Transformer
    {
        /// <summary>
        /// Состояние (вкл/откл)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Начальный узел, к которому привязана ветвь-трансформатор
        /// </summary>
        public Node FirstNode { get; set; }

        /// <summary>
        /// Конечный узел, к которому привязана ветвь-трансформатор
        /// </summary>
        public Node LastNode { get; set; }

        /// <summary>
        /// Коэффициент контрольного параметра
        /// </summary>
        public double ControlParametrCoefficient { get; set; }

        /// <summary>
        /// Активная мощность через трансформатор
        /// </summary>
        public double ActivePower { get; set; }

        /// <summary>
        /// Реактивная мощность через трансформатор
        /// </summary>
        public double ReactivePower { get; set; }

        /// <summary>
        /// Ток через трансформатор
        /// </summary>
        public double Current { get; set; }

        /// <summary>
        /// Коэффициент района
        /// </summary>
        public double DistrictCoefficient { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="state"></param>
        /// <param name="firstNode"></param>
        /// <param name="lastNode"></param>
        /// <param name="controlParametrCoefficient"></param>
        /// <param name="activePower"></param>
        /// <param name="reactivePower"></param>
        /// <param name="current"></param>
        /// <param name="districtCoefficient"></param>
        public Transformer(int state, Node firstNode, Node lastNode, 
            double controlParametrCoefficient, double activePower, double reactivePower, 
            double current, double districtCoefficient)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.ControlParametrCoefficient = controlParametrCoefficient;
            this.ActivePower = activePower;
            this.ReactivePower = reactivePower;
            this.Current = current;
            this.DistrictCoefficient = districtCoefficient;
        }

        /// <summary>
        /// Получить имена всех свойств класса
        /// </summary>
        /// <returns>Массив имен свойств класса</returns>
        public string[] GetNamesOfProperties()
        {
            return new string[]
            {
                "State",
                "ActivePower",
                "ReactivePower",
                "Current"
            };
        }
    }
}
