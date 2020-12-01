using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class LineSegment
    {
        public int State { get; set; }

        public Node FirstNode { get; set; }

        public Node LastNode { get; set; }

        public double ControlParametrCoefficient { get; set; }

        public double ActivePowerAtFirst { get; set; }

        public double ActivePowerInTheEnd { get; set; }

        public double ReactivePowerAtFirst { get; set; }

        public double ReactivePowerInTheEnd { get; set; }

        public double CurrentAtFirst { get; set; }

        public double CurrentInTheEnd { get; set; }

        public double DistrictCoefficient { get; set; }

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
