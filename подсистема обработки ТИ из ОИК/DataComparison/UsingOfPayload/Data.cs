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
    public class Data
    {
        public int ID { get; set; }

        public double Value { get; set; }

        public double Weight { get; set; }

        public Data(int id, double value,
            double controlParametrCoefficient, double districtCoefficient)
        {
            this.ID = id;
            this.Value = value;
            this.Weight = controlParametrCoefficient * districtCoefficient;
        }

        public static void SaveHashSet(HashSet<Data> dataSet, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, dataSet);
            }
        }

        public static HashSet<Data> LoadHashSet(string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (HashSet<Data>)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}
