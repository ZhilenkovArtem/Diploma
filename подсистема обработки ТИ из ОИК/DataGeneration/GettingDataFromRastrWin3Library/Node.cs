using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Узел (вершина)
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Состояние (вкл/откл)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Номер узла в таблице узлов в RastrWin3
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Коэффициент контрольного параметра
        /// </summary>
        public double ControlParametrCoefficient { get; set; }

        /// <summary>
        /// Расчетное напряжение
        /// </summary>
        public double CalculatedVoltage { get; set; }

        /// <summary>
        /// Активная мощность нагрузки в узле
        /// </summary>
        public double ActiveLoad { get; set; }

        /// <summary>
        /// Реактивная мощность нагрузки в узле
        /// </summary>
        public double ReactiveLoad { get; set; }

        /// <summary>
        /// Коэффициент района
        /// </summary>
        public double DistrictCoefficient { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="state"></param>
        /// <param name="number"></param>
        /// <param name="nominalVoltage"></param>
        /// <param name="calculatedVoltage"></param>
        /// <param name="activeLoad"></param>
        /// <param name="reactiveLoad"></param>
        /// <param name="district"></param>
        public Node(int state, int number, double nominalVoltage,
            double calculatedVoltage, double activeLoad, double reactiveLoad, int district)
        {
            this.State = state;
            this.Number = number;
            this.ControlParametrCoefficient = GetVoltageCoefficient(nominalVoltage);
            this.CalculatedVoltage = calculatedVoltage;
            this.ActiveLoad = activeLoad;
            this.ReactiveLoad = reactiveLoad;
            this.DistrictCoefficient = districtCoefficients[district];
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
                "CalculatedVoltage",
                "ActiveLoad",
                "ReactiveLoad"
            };
        }

        /// <summary>
        /// Получить коэффициент по напряжению (по контрольному параметру)
        /// </summary>
        /// <param name="nominalVoltage">Номинальное напряжение</param>
        /// <returns>Коэффициент по напряжению</returns>
        private double GetVoltageCoefficient(double nominalVoltage)
        {
            if (nominalVoltage == 110)
            {
                return 1;
            }
            else
            {
                return nominalVoltage * 3 / 500;
            }
        }
        
        /// <summary>
        /// Номер района и соответствующий ему районный коэффициент
        /// </summary>
        private Dictionary<int, double> districtCoefficients = new Dictionary<int, double>()
        {
            {60208, 0.5},
            {60401, 1},
            {60402, 1},
            {60403, 0.5},
            {60404, 0.75},
            {60405, 0.5},
            {60504, 0.5},
            {60511, 0.75},
            {60512, 1},
            {60513, 0.5},
            {60515, 1},
            {60516, 1},
            {60517, 0.5},
            {60521, 0.5},
            {60522, 0.5},
            {60523, 0.5},
            {60524, 0.5},
            {60525, 1},
            {60526, 1},
            {60527, 0.5},
            {60528, 0.5},
            {60531, 0.75},
            {60533, 1},
            {60534, 1},
            {70400, 0.5}
        };
    }
}
