using System;
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
    /// <summary>
    /// Класс, описывающий методы для преобразования данных, полученных из
    /// RastrWin3, в данные вида ОИК и данные вида, полученные расчетым путем
    /// </summary>
    public class Processing
    {
        /// <summary>
        /// Сохранить данные
        /// </summary>
        /// <param name="data">данные из RastrWin3</param>
        /// <param name="pathForDataSet">Путь к сохранению данных БД</param>
        /// <param name="pathForTelemetries">Путь к сохранению данных ОИК</param>
        public static void DataOutput(AllData data, string pathForDataSet, 
            string pathForTelemetries)
        {
            var nodes = data.Nodes;
            var lineSegments = data.EdgeSet.LineSegments;
            var transformers = data.EdgeSet.Transformers;
            var switches = data.EdgeSet.Switches;
            var generators = data.Generator;

            DataToExit dataToExit = new DataToExit(
                new HashSet<Data>(), new List<Telemetry>(), 1);

            dataToExit = PutAllValuesFromObjectInSets(dataToExit, nodes);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, lineSegments);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, transformers);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, switches);
            dataToExit = PutAllValuesFromObjectInSets(dataToExit, generators);

            Data.SaveHashSet(dataToExit.Datas, pathForDataSet);
            Telemetry.SaveList(dataToExit.Telemetries, pathForTelemetries);
        }

        /// <summary>
        /// Получить все значения по узлам
        /// </summary>
        /// <param name="dataToExit">выходные данные</param>
        /// <param name="items">входные данные</param>
        /// <returns>обновленные выходные данные</returns>
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

        /// <summary>
        /// Получить все значения по линиям
        /// </summary>
        /// <param name="dataToExit">выходные данные</param>
        /// <param name="items">входные данные</param>
        /// <returns>обновленные выходные данные</returns>
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

        /// <summary>
        /// Получить все значения по трансформаторам
        /// </summary>
        /// <param name="dataToExit">выходные данные</param>
        /// <param name="items">входные данные</param>
        /// <returns>обновленные выходные данные</returns>
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
        /// <summary>
        /// Получить все значения по выключателям
        /// </summary>
        /// <param name="dataToExit">выходные данные</param>
        /// <param name="items">входные данные</param>
        /// <returns>обновленные выходные данные</returns>
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
        /// <summary>
        /// Получить все значения по генераторам
        /// </summary>
        /// <param name="dataToExit">выходные данные</param>
        /// <param name="items">входные данные</param>
        /// <returns>обновленные выходные данные</returns>
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

        /// <summary>
        /// Получить данные вида RastrWin3 и положить в dataToExit
        /// </summary>
        /// <param name="dataToExit">Выходные данные</param>
        /// <param name="attribute">Атрибут объекта</param>
        /// <param name="objectType">Тип объекта</param>
        /// <param name="controlParametrCoefficient">Коэффициент 
        /// контрольного параметра</param>
        /// <param name="districtCoefficient">Коэффициент района</param>
        /// <returns>обновленные выходные данные</returns>
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
    }
}
