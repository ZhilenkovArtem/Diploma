using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Сегмент линии электропередачи
    /// </summary>
    public class LineSegment
    {
        /// <summary>
        /// Состояние (вкл/откл)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Начальный узел, к которому привязана ветвь-линия
        /// </summary>
        public Node FirstNode { get; set; }

        /// <summary>
        /// Конечный узел, к которому привязана ветвь-линия
        /// </summary>
        public Node LastNode { get; set; }

        /// <summary>
        /// Коэффициент контрольного параметра
        /// </summary>
        public double ControlParametrCoefficient { get; set; }

        /// <summary>
        /// Поток активной мощности в начале ветви
        /// </summary>
        public double ActivePowerAtFirst { get; set; }

        /// <summary>
        /// Поток активной мощности в конце ветви
        /// </summary>
        public double ActivePowerInTheEnd { get; set; }

        /// <summary>
        /// Поток реактивной мощности в начале ветви
        /// </summary>
        public double ReactivePowerAtFirst { get; set; }

        /// <summary>
        /// Поток реактивной мощности в конце ветви
        /// </summary>
        public double ReactivePowerInTheEnd { get; set; }

        /// <summary>
        /// Ток в начале ветви
        /// </summary>
        public double CurrentAtFirst { get; set; }

        /// <summary>
        /// Ток в конце ветви
        /// </summary>
        public double CurrentInTheEnd { get; set; }

        /// <summary>
        /// Коэффициент района
        /// </summary>
        public double DistrictCoefficient { get; set; }

        /// <summary>
        /// Коструктор
        /// </summary>
        /// <param name="state"></param>
        /// <param name="firstNode"></param>
        /// <param name="lastNode"></param>
        /// <param name="controlParametrCoefficient"></param>
        /// <param name="activePowerAtFirst"></param>
        /// <param name="activePowerInTheEnd"></param>
        /// <param name="reactivePowerAtFirst"></param>
        /// <param name="reactivePowerInTheEnd"></param>
        /// <param name="currentAtFirst"></param>
        /// <param name="currentInTheEnd"></param>
        /// <param name="districtCoefficient"></param>
        public LineSegment(int state, Node firstNode, Node lastNode, 
            double controlParametrCoefficient, double activePowerAtFirst, 
            double activePowerInTheEnd, double reactivePowerAtFirst, 
            double reactivePowerInTheEnd, double currentAtFirst, 
            double currentInTheEnd, double districtCoefficient)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.ControlParametrCoefficient = controlParametrCoefficient;
            this.ActivePowerAtFirst = activePowerAtFirst;
            this.ActivePowerInTheEnd = activePowerInTheEnd;
            this.ReactivePowerAtFirst = reactivePowerAtFirst;
            this.ReactivePowerInTheEnd = reactivePowerInTheEnd;
            this.CurrentAtFirst = currentAtFirst;
            this.CurrentInTheEnd = currentInTheEnd;
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
                "ActivePowerAtFirst",
                "ActivePowerInTheEnd",
                "ReactivePowerAtFirst",
                "ReactivePowerInTheEnd",
                "CurrentAtFirst",
                "CurrentInTheEnd"
            };
        }
    }
}
