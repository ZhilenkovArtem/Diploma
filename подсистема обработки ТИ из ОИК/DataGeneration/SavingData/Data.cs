using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingData
{
    [Serializable]
    public class Data
    {
        public int ID { get; set; }

        public double Value { get; set; }

        public bool IsGenerator { get; set; }

        public double ControlParametr { get; set; }

        public Data(int id, double value, bool isGenerator, double controlParametr)
        {
            this.ID = id;
            this.Value = value;
            this.IsGenerator = isGenerator;
            this.ControlParametr = controlParametr;
        }
    }
}
