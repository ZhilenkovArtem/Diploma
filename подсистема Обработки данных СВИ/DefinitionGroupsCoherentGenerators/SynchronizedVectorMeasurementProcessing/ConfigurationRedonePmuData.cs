using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizedVectorMeasurementProcessing
{
    public class ConfigurationRedonePmuData
    {
        public List<RedonePmuData> Data { get; set; }

        public float Time { get; set; }

        public ConfigurationRedonePmuData() { }

        public ConfigurationRedonePmuData(List<RedonePmuData> data, float time)
        {
            this.Data = data;
            this.Time = time;
        }
    }
}
