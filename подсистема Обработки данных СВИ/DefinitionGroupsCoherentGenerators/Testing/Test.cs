using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefinitionGroupsCoherentGenerators;
using System.Collections;
using CsvHelper;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using System.Threading;

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
            IEnumerable<GeneratorsData> generatorsData;

            var folder = Environment.CurrentDirectory;
            var path = Path.Combine(folder, "slice7.csv");

            using (StreamReader reader = new StreamReader(path))
            {
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.Delimiter = ";";

                    generatorsData = csvReader.GetRecords<GeneratorsData>();
                    var generatorsList = generatorsData.ToList();
                    
                    foreach (var generators in generatorsList)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        var faultFlag = FaultDetection(generators);

                        stopwatch.Stop();
                        TimeSpan timespan = stopwatch.Elapsed;
                        string elapsedTime = String.Format("{0:0000}", timespan.Milliseconds);
                        Console.WriteLine($"Execution time: {elapsedTime} ms\n");

                        if (faultFlag == true)
                        {
                            break;
                        }

                        Thread.Sleep(20);
                    }
                }
            }

            Console.WriteLine("фсё");
            Console.ReadKey();
        }

        /// <summary>
        /// Индикация нарушения
        /// </summary>
        /// <param name="generators">данные по генераторам</param>
        /// <returns>Флаг "произошло ли нарушение"</returns>
        private static bool FaultDetection(GeneratorsData generators)
        {
            var errors = new Errors(generators);

            Console.WriteLine(errors.Time);

            if (errors.Fault == true)
            {
                var tuple = errors.GetGroupsCoherentGenerators();

                Console.WriteLine("First group:");
                foreach (var generator in tuple.Item1)
                {
                    Console.Write($"{generator} ");
                }
                Console.WriteLine("\nSecond group:");
                foreach (var generator in tuple.Item2)
                {
                    Console.Write($"{generator} ");
                }
            }
            Console.WriteLine();
            return errors.Fault;
        }
    }
}
