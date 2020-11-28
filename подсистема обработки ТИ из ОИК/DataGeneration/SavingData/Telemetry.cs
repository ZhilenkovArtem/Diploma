using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingData
{
    [Serializable]
    public class Telemetry
    {
        public int Address { get; set; }

        public double Value { get; set; }

        public Telemetry(int address, double value)
        {
            this.Address = address;
            this.Value = value;
        }
    }
}
