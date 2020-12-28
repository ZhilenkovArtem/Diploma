using System;
using System.Collections.Generic;
using System.Linq;
using SynchronizedVectorMeasurementProcessing;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Numerics;

namespace ClientUDP
{
    public class TestData
    {
        public static List<ConfigurationFrame> GetTestData()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            var folder = Environment.CurrentDirectory;
            var pathToTestData = Path.Combine(folder, "TestData");
            var pathToStable = Path.Combine(pathToTestData, "Stable");
            var pathToUnstableSlow = Path.Combine(pathToTestData, "UnstableSlow");
            var pathToUnstableFast = Path.Combine(pathToTestData, "UnstableFast");
            var slice = Path.Combine(pathToUnstableSlow, "7");

            // Лист множества данных каждого из узлов всех объектов электроэнергетики во времени
            var multiplicityNodesData = new List<Node>();
            // Лист множества данных каждого из генераторов всех объектов электроэнергетики во времени
            var multiplicityGeneratorsData = new List<Generator>();

            var directory = new DirectoryInfo(slice);
            foreach (var subDirectory in directory.GetDirectories())
            {
                if (subDirectory.GetDirectories().Count() != 0)
                {
                    foreach (var subSubDir in subDirectory.GetDirectories())
                    {
                        if (subSubDir.Name == "node")
                        {
                            foreach (var enumNodeData in GetNodesParameters(subSubDir))
                            {
                                multiplicityNodesData.Add(enumNodeData);
                            }
                        }
                        else if (subSubDir.Name == "machine")
                        {
                            foreach (var enumGeneratorData in GetGeneratorsParameters(subSubDir))
                            {
                                multiplicityGeneratorsData.Add(enumGeneratorData);
                            }
                        }

                    }
                }
                else
                {
                    foreach (var enumNodeData in GetNodesParameters(subDirectory))
                    {
                        multiplicityNodesData.Add(enumNodeData);
                    }
                }
            }

            var multiplicityData = new List<CommonData>();
            foreach (var node in multiplicityNodesData)
            {
                var cData = new List<CommonDataFormat>();
                var generator = multiplicityGeneratorsData.Where(gen => gen.CommonAddress == node.CommonAddress).FirstOrDefault();

                if (generator != null)
                {
                    var genPowerList = generator.PowerValues.ToList();
                    float time = 0.50F;
                    int i = 0;
                    foreach (var nodeVoltage in node.VoltageValues)
                    {
                        cData.Add(new CommonDataFormat(time, nodeVoltage.Voltage, genPowerList[i].Power));
                        i++;
                        time += 0.02F;
                    }
                }
                else
                {
                    float time = 0.50F;
                    int i = 0;
                    foreach (var nodeVoltage in node.VoltageValues)
                    {
                        cData.Add(new CommonDataFormat(time, nodeVoltage.Voltage, nodeVoltage.Angle));
                        i++;
                        time += 0.02F;
                    }
                }
                multiplicityData.Add(new CommonData(node.CommonAddress, cData));
            }

            var frameList = new List<ConfigurationFrame>();
            var dataCount = multiplicityData[0].Data.Count();
            for (int i = 0; i < dataCount; i++)
            {                
                var pMUs = new List<PMU>();
                foreach (var cd in multiplicityData)
                {                    
                    pMUs.Add(new PMU(cd.CommonAddress, cd.Data[i].Voltage, cd.Data[i].Power));
                }
                frameList.Add(new ConfigurationFrame(pMUs, multiplicityData[0].Data[i].Time));
            }

            return frameList;
        }

        private static List<Node> GetNodesParameters(DirectoryInfo directory)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var eesObjectsData = new List<Node>();

            foreach (var file in directory.GetFiles())
            {
                IEnumerable<NodeDataFormat> data;
                if (Path.GetExtension(file.FullName) == ".csv")
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                        {
                            csvReader.Configuration.Delimiter = ";";
                            data = csvReader.GetRecords<NodeDataFormat>();
                            var ca = float.Parse(file.Name.Substring(3, file.Name.Length - 7));

                            eesObjectsData.Add(new Node(ca, data.ToList()));
                        }
                    }
                }
            }
            return eesObjectsData;
        }

        private static List<Generator> GetGeneratorsParameters(DirectoryInfo directory)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var eesObjectsData = new List<Generator>();

            foreach (var file in directory.GetFiles())
            {
                IEnumerable<GeneratorDataFormat> data;
                if (file.Extension == ".csv")
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                        {
                            csvReader.Configuration.Delimiter = ";";
                            data = csvReader.GetRecords<GeneratorDataFormat>();
                            var ca = float.Parse(file.Name.Substring(3, file.Name.Length - 7));

                            eesObjectsData.Add(new Generator(ca, data.ToList()));
                        }
                    }
                }
            }
            return eesObjectsData;
        }
    }

    public class NodeDataFormat
    {
        public float Time { get; set; }

        public float Voltage { get; set; }

        public float Angle { get; set; }
    }

    public class Node
    {
        public float CommonAddress { get; set; }

        public List<NodeDataFormat> VoltageValues { get; set; }

        public Node(float ca, List<NodeDataFormat> values)
        {
            this.CommonAddress = ca;
            this.VoltageValues = values;
        }
    }

    public class GeneratorDataFormat
    {
        public float Time { get; set; }

        public string Power { get; set; }
    }

    public class Generator
    {
        public float CommonAddress { get; set; }

        public List<GeneratorDataFormat> PowerValues { get; set; }

        public Generator(float ca, List<GeneratorDataFormat> values)
        {
            this.CommonAddress = ca;
            this.PowerValues = values;
        }
    }

    public class CommonData
    {
        public float CommonAddress { get; set; }

        public List<CommonDataFormat> Data { get; set; }

        public CommonData() { }

        public CommonData(float ca, List<CommonDataFormat> data)
        {
            this.CommonAddress = ca;
            this.Data = data;
        }
    }

    public class CommonDataFormat
    {
        public float Time { get; set; }

        public Complex Voltage { get; set; }

        public float Power { get; set; }

        public CommonDataFormat(float time, float voltage, string power)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var strPower = power.Split(new string[] { "," }, StringSplitOptions.None);
            var angle = float.Parse(strPower[1].Substring(0, strPower[1].Length - 1));
            var real = voltage * 15.75 * Math.Cos(angle / 180 * Math.PI);
            var imaginary = voltage * 15.75 * Math.Sin(angle / 180 * Math.PI);
            this.Voltage = new Complex(real, imaginary);
            this.Power = float.Parse(strPower[0].Substring(1));
            this.Time = time;
        }

        public CommonDataFormat(float time, float voltage, float angle)
        {
            var real = voltage * 500 * Math.Cos(angle * 57.3 / 180 * Math.PI);
            var imaginary = voltage * 500 * Math.Sin(angle * 57.3 / 180 * Math.PI);
            this.Voltage = new Complex(real, imaginary);
            this.Power = float.NaN;
            this.Time = time;
        }
    }
}
