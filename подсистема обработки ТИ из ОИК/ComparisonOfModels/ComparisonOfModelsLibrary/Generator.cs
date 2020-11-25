using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparisonOfModelsLibrary
{
    public class Generator
    {
        public bool State { get; set; }

        public float Voltage { get; set; }

        public float ActivePower { get; set; }

        public float ReactivePower { get; set; }

        public float NominalActivePower { get; set; }

        public int Address { get; set; }
    }
}
