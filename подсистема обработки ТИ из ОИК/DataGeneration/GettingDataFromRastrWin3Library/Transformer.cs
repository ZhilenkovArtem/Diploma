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

        public double ControlParametrCoefficient { get; set; }

        public double ActivePower { get; set; }

        public double ReactivePower { get; set; }

        public double Current { get; set; }

        public double DistrictCoefficient { get; set; }

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
