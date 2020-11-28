using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class Transformer
    {
        public int State { get; set; }

        public Node FirstNode { get; set; }

        public Node LastNode { get; set; }

        public double NominalVoltage { get; set; }

        public double ActivePower { get; set; }

        public double ReactivePower { get; set; }

        public double Current { get; set; }

        public Transformer(int state, Node firstNode, Node lastNode, 
            double nominalVoltage, double activePower, double reactivePower, 
            double current)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.NominalVoltage = nominalVoltage;
            this.ActivePower = activePower;
            this.ReactivePower = reactivePower;
            this.Current = current;
        }

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
