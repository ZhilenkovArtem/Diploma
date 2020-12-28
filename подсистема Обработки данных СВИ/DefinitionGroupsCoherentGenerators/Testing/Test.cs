using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynchronizedVectorMeasurementProcessing;
using System.Collections;
using CsvHelper;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Numerics;

namespace Testing
{
    /// <summary>
    /// Тестовый класс
    /// </summary>
    class Test
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            var confDevInTime = new ConfigDeviationsInWithin60ms();
            var frameList = TestData.GetTestData();
            var frameListWithoutNodes = GetFrameListWithoutNodes(frameList);
            var frameListWithoutGenerators = GetFrameListWithoutGenerators(frameList);

            DetectDisturbance(confDevInTime, frameListWithoutNodes);

            //foreach (var pmu in frameList[0].Data)
            //{
            //    Console.WriteLine($"IDCode = {pmu.IDCode}\tvoltage = " +
            //        $"{pmu.Voltage./*Real*/Magnitude}/{pmu.Voltage./*Imaginary*/Phase * 180 / Math.PI}\tpower = {pmu.Power}");
            //}

            Console.ReadKey();
        }

        private static void DetectDisturbance(ConfigDeviationsInWithin60ms confDevInTime, List<ConfigurationFrame> frameList)
        {
            var previousRedoneConfig = new ConfigurationRedonePmuData();

            for (int i = 0; i < frameList.Count; i++)
            {
                //var devlist = new DeviationsList();
                var redoneConfig = new ConfigurationRedonePmuData();

                if (i > 0)
                {
                    redoneConfig = GetActualConfig(frameList[i], previousRedoneConfig);
                    //devlist = new DeviationsList(frameList[i], frameList[i - 1]);
                    //devlist = new DeviationsList(frameList[i]);
                    previousRedoneConfig = redoneConfig;
                }
                else if (i == 0)
                {
                    redoneConfig = GetFirstConfig(frameList[i]);
                    previousRedoneConfig = redoneConfig;
                }

                var devlist = new DeviationsList(redoneConfig);  
                
                confDevInTime = confDevInTime.AddNewDevList(confDevInTime, devlist);
                var countDeviationsInTime = confDevInTime.DeviationsInWithin60ms.Count;
                var isFault = DetectionOfFault.DetectFault(devlist.DevList);

                Console.WriteLine($"Time = {devlist.Time}\tcoi = {devlist.CenterOfInertia}\tisFault = {isFault}");
                foreach (var pMUsDeviation in devlist.DevList)
                {
                    Console.WriteLine($"ID = {pMUsDeviation.PMUsIDCode}\t" +
                        $"dev = {pMUsDeviation.DeviationFromCOI}\t" +
                        $"prevDev = {pMUsDeviation.DeltaRelativeToPreviousValue}\t" +
                        $"acDev = {pMUsDeviation.AccumulatedDeviation}");
                }
                Console.WriteLine();

                if (isFault && countDeviationsInTime ==
                    ConfigDeviationsInWithin60ms.DEVIATIONSLISTSCOUNT)
                {
                    var groups = FindingGroupsCoherentGenerators.GetGroupsCoherentGenerators(devlist.DevList);
                    confDevInTime = new ConfigDeviationsInWithin60ms();
                
                    Console.Write("1 group:");
                    foreach (var gen in groups.Item1)
                    {
                        Console.WriteLine(gen);
                    }
                    Console.Write("2 group:");
                    foreach (var gen in groups.Item2)
                    {
                        Console.WriteLine(gen);
                    }
                }
            }
        }

        private static ConfigurationRedonePmuData GetFirstConfig(ConfigurationFrame frame)
        {
            var pmuDataList = new List<RedonePmuData>();

            for (int i = 0; i < frame.Data.Count; i++)
            {
                var angleOfCurrentFrame = (float)(frame.Data[i].Voltage.Phase * 180 / Math.PI);

                pmuDataList.Add(new RedonePmuData(frame.Data[i].IDCode,
                    (float)frame.Data[i].Voltage.Magnitude, 
                    angleOfCurrentFrame, frame.Data[i].Power));
            }
            return new ConfigurationRedonePmuData(pmuDataList, frame.Time);
        }

        private static ConfigurationRedonePmuData GetActualConfig(ConfigurationFrame currentFrame, ConfigurationRedonePmuData previousConfig)
        {
            var pmuDataList = new List<RedonePmuData>();

            for (int i = 0; i < currentFrame.Data.Count; i++)
            {
                var voltageOfCurrentFrame = (float)currentFrame.Data[i].Voltage.Magnitude;
                var angleOfCurrentFrame = (float)(currentFrame.Data[i].Voltage.Phase * 180 / Math.PI);
                var angleOfPreviousFrame = previousConfig.Data[i].VoltagePhase;

                if ((angleOfCurrentFrame - angleOfPreviousFrame) < -250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame + 360;
                }
                else if ((angleOfCurrentFrame - angleOfPreviousFrame) > 250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame - 360;
                }

                pmuDataList.Add(new RedonePmuData(currentFrame.Data[i].IDCode, 
                    voltageOfCurrentFrame, angleOfCurrentFrame, 
                    currentFrame.Data[i].Power));
            }
            return new ConfigurationRedonePmuData(pmuDataList, currentFrame.Time);
        }

        private static List<ConfigurationFrame> GetFrameListWithoutNodes(List<ConfigurationFrame> frameList)
        {
            var frameListWithoutNodes = new List<ConfigurationFrame>();
            foreach (var frame in frameList)
            {
                var newData = frame.Data.Where(pmu => !float.IsNaN(pmu.Power)).ToList();
                frameListWithoutNodes.Add(new ConfigurationFrame(newData, frame.Time));
            }
            return frameListWithoutNodes;
        }

        private static List<ConfigurationFrame> GetFrameListWithoutGenerators(List<ConfigurationFrame> frameList)
        {
            var frameListWithoutGenerators = new List<ConfigurationFrame>();
            foreach (var frame in frameList)
            {
                var newData = frame.Data.Where(pmu => float.IsNaN(pmu.Power)).ToList();
                frameListWithoutGenerators.Add(new ConfigurationFrame(newData, frame.Time));
            }
            return frameListWithoutGenerators;
        }
    }
}
