using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UsingOfPayload
{
    [Serializable]
    public class Telemetry
    {
        public int Address { get; set; }

        public double Value { get; set; }

        public bool IsTelesignal { get; set; }

        public Telemetry(int address, double value, bool flag)
        {
            this.Address = address;
            this.Value = value;
            this.IsTelesignal = flag;
        }

        public static void SaveList(List<Telemetry> telemetries, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, telemetries);
            }
        }

        public static List<Telemetry> LoadList(string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (List<Telemetry>)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}
