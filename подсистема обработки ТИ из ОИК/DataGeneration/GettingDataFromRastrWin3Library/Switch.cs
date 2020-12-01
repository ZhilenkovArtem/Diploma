using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class Switch
    {
        public int State { get; set; }

        public Node FirstNode { get; set; }

        public Node LastNode { get; set; }

        public double ControlParametrCoefficient { get; set; }

        public double DistrictCoefficient { get; set; }

        public Switch(int state, Node firstNode, Node lastNode,
            double controlParametrCoefficient, double districtCoefficient)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.ControlParametrCoefficient = controlParametrCoefficient;
            this.DistrictCoefficient = districtCoefficient;
        }

        public string[] GetNamesOfProperties()
        {
            return new string[]
            {
                "State"
            };
        }
    }
}
