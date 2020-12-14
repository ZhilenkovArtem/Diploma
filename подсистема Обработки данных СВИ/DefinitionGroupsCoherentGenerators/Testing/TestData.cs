using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class TestData
    {
        /*IEnumerable<GeneratorsData> generatorsData;
            
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
            }*/
    }
}
