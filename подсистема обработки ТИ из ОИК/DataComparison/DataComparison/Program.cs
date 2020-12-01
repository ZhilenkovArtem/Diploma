using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UsingOfPayload;

namespace DataComparison
{
    class Program
    {
        private const string PATH = @"C:\Users\Artem\Desktop\rg2\Output\DataDB\";
        private const string TELEMETRIESPATH = @"C:\Users\Artem\Desktop\rg2\Output\Telemetries\telemetries24.dat";

        static void Main(string[] args)
        {
            var pathForTelemetries = TELEMETRIESPATH;
            var telemetries = Telemetry.LoadList(pathForTelemetries);

            DirectoryInfo directory = new DirectoryInfo(PATH);
            foreach (var item in directory.GetFiles())
            {
                var dataSet = Data.LoadHashSet(item.FullName);

                double telesignalsSum = 0;
                double telesignalsQuantity = 0;
                double telemetriesSum = 0;
                double telemetriesQuantity = 0;
                foreach (var telemetry in telemetries)
                {
                    var data = dataSet.Where(d => d.ID == telemetry.Address).FirstOrDefault();
                    if (data != null)
                    {
                        if (telemetry.IsTelesignal == true)
                        {
                            if (data.Value == telemetry.Value)
                            {
                                telesignalsSum++;
                            }
                            telesignalsQuantity++;
                        }
                        else
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
                Console.WriteLine($"Для {item.Name}: {telesignalsCoincidence}\t{telemetriesDiscrepancy}");
            }

            Console.ReadKey();
        }

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
    }
}
