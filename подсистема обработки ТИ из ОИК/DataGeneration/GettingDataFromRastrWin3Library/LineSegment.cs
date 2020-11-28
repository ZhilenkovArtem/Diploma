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

        public double NominalVoltage { get; set; }

        public double ActivePowerAtFirst { get; set; }

        public double ActivePowerInTheEnd { get; set; }

        public double ReactivePowerAtFirst { get; set; }

        public double ReactivePowerInTheEnd { get; set; }

        public double CurrentAtFirst { get; set; }

        public double CurrentInTheEnd { get; set; }

        public LineSegment(int state, Node firstNode, Node lastNode, 
            double nominalVoltage, double activePowerAtFirst, 
            double activePowerInTheEnd, double reactivePowerAtFirst, 
            double reactivePowerInTheEnd, double currentAtFirst, 
            double currentInTheEnd)
        {
            this.State = state;
            this.FirstNode = firstNode;
            this.LastNode = lastNode;
            this.NominalVoltage = nominalVoltage;
            this.ActivePowerAtFirst = activePowerAtFirst;
            this.ActivePowerInTheEnd = activePowerInTheEnd;
            this.ReactivePowerAtFirst = reactivePowerAtFirst;
            this.ReactivePowerInTheEnd = reactivePowerInTheEnd;
            this.CurrentAtFirst = currentAtFirst;
            this.CurrentInTheEnd = currentInTheEnd;
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
