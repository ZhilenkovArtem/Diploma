using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace UsingOfPayload
{
    public class Comparison
    {
        public static string GetMode(string path, string telemetriesPath)
        {
            var pathForTelemetries = telemetriesPath;
            var telemetries = Telemetry.LoadList(pathForTelemetries);
            foreach (var telemetry in telemetries)
            {
                Console.WriteLine($"oa = {telemetry.ObjectAddress}\t" +
                    $"value = {telemetry.Value}\t" +
                    $"type = {telemetry.TypeId}");
            }
            var modeRatingList = new List<ModeRating>();

            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (var item in directory.GetFiles())
            {
                var dataSet = Data.LoadHashSet(item.FullName);

                double telesignalsSum = 0;
                double telesignalsQuantity = 0;
                double telemetriesSum = 0;
                double telemetriesQuantity = 0;
                // Для ТС просто определялось соответствие включенного 
                // состояния, а для ТИ - относительное отклонение
                foreach (var telemetry in telemetries)
                {
                    var data = dataSet.Where(d => d.ID == telemetry.ObjectAddress).FirstOrDefault();
                    if (data != null)
                    {
                        if (telemetry.TypeId == TypeId.SinglePointInformation)
                        {
                            if (data.Value == telemetry.Value)
                            {
                                telesignalsSum++;
                            }
                            telesignalsQuantity++;
                        }
                        else if (telemetry.TypeId == TypeId.MeasuredValueShort)
                        {
                            var deviation = GetValue(Math.Abs(telemetry.Value), Math.Abs(data.Value))
                                * data.Weight;
                            telemetriesSum = telemetriesSum + deviation;
                            telemetriesQuantity++;
                        }
                    }
                }
                var telesignalsCoincidence = telesignalsSum / telesignalsQuantity;
                var telemetriesDiscrepancy = telemetriesSum / telemetriesQuantity;
                var rating = (1 - telesignalsCoincidence) + (telemetriesDiscrepancy);
                modeRatingList.Add(new ModeRating(item.Name, rating));
            }
            Console.WriteLine("count: " + modeRatingList.Count);
            double min = 1;
            string mode = "";
            foreach (var modeRating in modeRatingList)
            {
                Console.WriteLine($"name: {modeRating.name}\trating: {modeRating.rating}");
                if (modeRating.rating < min)
                {
                    min = modeRating.rating;
                    mode = modeRating.name;
                }
            }
            Console.WriteLine();
            return mode;
        }

        /// <summary>
        /// Получить корректное значение формата { С = (А+В) / Мах(А;В) }
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private static double GetValue(double value1, double value2)
        {
            if (value1 == 0 && value2 == 0)
            {
                return 0;
            }
            else
            {
                return Math.Abs(value1 - value2) / Math.Max(value1, value2);
            }
        }

        public struct ModeRating
        {
            public string name;

            public double rating;

            public ModeRating(string name, double rating)
            {
                this.name = name;
                this.rating = rating;
            }
        }
    }
}
