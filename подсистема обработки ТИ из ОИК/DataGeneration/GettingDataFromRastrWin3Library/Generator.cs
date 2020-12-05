using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Генераторы
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Состояние (вкл/откл)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Узел, к которому привязан генератор
        /// </summary>
        public Node ConnectionNode { get; set; }

        /// <summary>
        /// Активная мощность генератора
        /// </summary>
        public double ActivePower { get; set; }

        /// <summary>
        /// Реактивная мощность генератора
        /// </summary>
        public double ReactivePower { get; set; }

        /// <summary>
        /// Коэффициент контрольного параметра
        /// </summary>
        public double ControlParametrCoefficient { get; set; }

        /// <summary>
        /// Коэффициент района
        /// </summary>
        public double DistrictCoefficient { get; set; }

        /// <summary>
        /// Конструктор генратора
        /// </summary>
        /// <param name="state">Состояние</param>
        /// <param name="connectionNode">Узел присоединения</param>
        /// <param name="activePower">Активная мощность</param>
        /// <param name="reactivePower">Реактивная мощность</param>
        /// <param name="nominalActivePower">Номинальная активная мощность</param>
        /// <param name="districtCoefficient">Коэффициент района</param>
        public Generator(int state, Node connectionNode, double activePower, 
            double reactivePower, double nominalActivePower, 
            double districtCoefficient)
        {
            this.State = state;
            this.ConnectionNode = connectionNode;
            this.ActivePower = activePower;
            this.ReactivePower = reactivePower;
            this.ControlParametrCoefficient = nominalActivePower / 250;
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
                "ReactivePower"
            };
        }
    }
}
