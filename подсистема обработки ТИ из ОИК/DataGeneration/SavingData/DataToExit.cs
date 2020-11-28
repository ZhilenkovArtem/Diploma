using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingData
{
    public class DataToExit
    {
        public HashSet<Data> Datas { get; set; }

        public List<Telemetry> Telemetries { get; set; }

        public int Index { get; set; }

        public DataToExit(HashSet<Data> datas, List<Telemetry> telemetries, int index)
        {
            this.Datas = datas;
            this.Telemetries = telemetries;
            this.Index = index;
        }
    }
}
