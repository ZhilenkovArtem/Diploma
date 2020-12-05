using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Выключатель
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Состояние (вкл/откл)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Начальный узел, к которому привязана ветвь-выключатель
        /// </summary>
        public Node FirstNode { get; set; }

        /// <summary>
        /// Конечный узел, к которому привязана ветвь-выключатель
        /// </summary>
        public Node LastNode { get; set; }

        /// <summary>
        /// Коэффициент контрольного параметра
        /// </summary>
        public double ControlParametrCoefficient { get; set; }

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
        /// <param name="districtCoefficient"></param>
        public Switch(int state, Node firstNode, Node lastNode,
            double controlParametrCoefficient, double districtCoefficient)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.ControlParametrCoefficient = controlParametrCoefficient;
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
                "State"
            };
        }
    }
}
