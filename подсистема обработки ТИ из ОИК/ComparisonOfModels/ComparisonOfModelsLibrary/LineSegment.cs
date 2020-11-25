using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfModelsLibrary
{
    public class LineSegment
    {
        public bool State { get; set; }

        public float ActivePowerAtFirst { get; set; }

        public float ActivePowerInTheEnd { get; set; }

        public float ReactivePowerAtFirst { get; set; }

        public float ReactivePowerInTheEnd { get; set; }

        public float CurrentAtFirst { get; set; }

        public float CurrentInTheEnd { get; set; }

        public float BaseVoltage { get; set; }

        public int Address { get; set; }
    }
}
