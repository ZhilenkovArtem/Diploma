using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class Generator
    {
        public int State { get; set; }

        public Node ConnectionNode { get; set; }

        public double ActivePower { get; set; }

        public double ReactivePower { get; set; }

        public double NominalActivePower { get; set; }

        public Generator(int state, Node connectionNode, double activePower, 
            double reactivePower, double nominalActivePower)
        {
            this.State = state;
            this.ConnectionNode = connectionNode;
            this.ActivePower = activePower;
            this.ReactivePower = reactivePower;
            this.NominalActivePower = nominalActivePower;
        }

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
