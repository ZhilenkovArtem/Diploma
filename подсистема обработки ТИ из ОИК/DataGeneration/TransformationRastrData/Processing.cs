﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDataFromRastrWin3Library;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UsingOfPayload;

namespace TransformationRastrData
{
    public class Processing
    {
        public static void DataOutput(AllData data, string pathForDataSet, string pathForTelemetries)
        {
            var nodes = data.Nodes;
            var lineSegments = data.EdgeSet.LineSegments;
            var transformers = data.EdgeSet.Transformers;
            var switches = data.EdgeSet.Switches;
            var generators = data.Generator;

            DataToExit dataToExit = new DataToExit(new HashSet<Data>(), new List<Telemetry>(), 1);

            dataToExit = PutAllValuesFromObjectInSets(dataToExit, nodes);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, lineSegments);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, transformers);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, switches);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, generators);

            SaveData(dataToExit.Datas, pathForDataSet);
            SaveData(dataToExit.Telemetries, pathForTelemetries);
        }

        private static DataToExit PutAllValuesFromObjectInSets(DataToExit dataToExit, HashSet<Node> items)
        {
            foreach (var item in items)
            {
                var controlParametrCoefficient = double.Parse(item.GetType().GetProperty("ControlParametrCoefficient").GetValue(item).ToString());
                var districtCoefficient = double.Parse(item.GetType().GetProperty("DistrictCoefficient").GetValue(item).ToString());
                foreach (var attribute in item.GetNamesOfProperties())
                {
                    dataToExit = PutSomeValueInSets(dataToExit, attribute, item, controlParametrCoefficient, districtCoefficient);
                }
            }
            return dataToExit;
        }
        private static DataToExit PutAllValuesFromObjectInSets(DataToExit dataToExit, HashSet<LineSegment> items)
        {
            foreach (var item in items)
            {
                var controlParametrCoefficient = double.Parse(item.GetType().GetProperty("ControlParametrCoefficient").GetValue(item).ToString());
                var districtCoefficient = double.Parse(item.GetType().GetProperty("DistrictCoefficient").GetValue(item).ToString());
                foreach (var attribute in item.GetNamesOfProperties())
                {
                    dataToExit = PutSomeValueInSets(dataToExit, attribute, item, controlParametrCoefficient, districtCoefficient);
                }
            }
            return dataToExit;
        }
        private static DataToExit PutAllValuesFromObjectInSets(DataToExit dataToExit, HashSet<Transformer> items)
        {
            foreach (var item in items)
            {
                var controlParametrCoefficient = double.Parse(item.GetType().GetProperty("ControlParametrCoefficient").GetValue(item).ToString());
                var districtCoefficient = double.Parse(item.GetType().GetProperty("DistrictCoefficient").GetValue(item).ToString());
                foreach (var attribute in item.GetNamesOfProperties())
                {
                    dataToExit = PutSomeValueInSets(dataToExit, attribute, item, controlParametrCoefficient, districtCoefficient);
                }
            }
            return dataToExit;
        }
        private static DataToExit PutAllValuesFromObjectInSets(DataToExit dataToExit, HashSet<Switch> items)
        {
            foreach (var item in items)
            {
                var controlParametrCoefficient = double.Parse(item.GetType().GetProperty("ControlParametrCoefficient").GetValue(item).ToString());
                var districtCoefficient = double.Parse(item.GetType().GetProperty("DistrictCoefficient").GetValue(item).ToString());
                foreach (var attribute in item.GetNamesOfProperties())
                {
                    dataToExit = PutSomeValueInSets(dataToExit, attribute, item, controlParametrCoefficient, districtCoefficient);
                }
            }
            return dataToExit;
        }
        private static DataToExit PutAllValuesFromObjectInSets(DataToExit dataToExit, HashSet<Generator> items)
        {
            foreach (var item in items)
            {
                var controlParametrCoefficient = double.Parse(item.GetType().GetProperty("ControlParametrCoefficient").GetValue(item).ToString());
                var districtCoefficient = double.Parse(item.GetType().GetProperty("DistrictCoefficient").GetValue(item).ToString());
                foreach (var attribute in item.GetNamesOfProperties())
                {
                    dataToExit = PutSomeValueInSets(dataToExit, attribute, item, controlParametrCoefficient, districtCoefficient);
                }
            }
            return dataToExit;
        }

        private static DataToExit PutSomeValueInSets(DataToExit dataToExit, 
            string attribute, Object objectType, 
            double controlParametrCoefficient, double districtCoefficient)
        {
            var datas = dataToExit.Datas;
            var telemetries = dataToExit.Telemetries;
            var index = dataToExit.Index;

            if (attribute == "State")
            {
                var value = int.Parse(objectType.GetType().GetProperty(
                    attribute).GetValue(objectType).ToString());
                datas.Add(new Data(index, value, controlParametrCoefficient, 
                    districtCoefficient));
                telemetries.Add(new Telemetry(index, value, true));
            }
            else
            {
                var value = double.Parse(objectType.GetType().GetProperty(
                    attribute).GetValue(objectType).ToString());
                datas.Add(new Data(index, value, controlParametrCoefficient,
                    districtCoefficient));
                telemetries.Add(new Telemetry(index, value, false));
            }
            index++;

            return new DataToExit(datas, telemetries, index);
        }

        private static void SaveData(HashSet<Data> dataSet, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, dataSet);
            }
        }

        private static void SaveData(List<Telemetry> telemetries, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, telemetries);
            }
        }
    }
}
