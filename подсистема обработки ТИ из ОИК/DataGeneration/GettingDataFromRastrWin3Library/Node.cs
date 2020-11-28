using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class Node
    {
        public int State { get; set; }

        public int Number { get; set; }

        public double NominalVoltage { get; set; }

        public double CalculatedVoltage { get; set; }

        public double ActiveLoad { get; set; }

        public double ReactiveLoad { get; set; }

        public int District { get; set; }

        public Node(int state, int number, double nominalVoltage,
            double calculatedVoltage, double activeLoad, double reactiveLoad, int district)
        {
            this.State = state;
            this.Number = number;
            this.NominalVoltage = nominalVoltage;
            this.CalculatedVoltage = calculatedVoltage;
            this.ActiveLoad = activeLoad;
            this.ReactiveLoad = reactiveLoad;
            this.District = district;
        }

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
    }
}
