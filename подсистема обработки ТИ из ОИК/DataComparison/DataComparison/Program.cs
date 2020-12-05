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
    /// <summary>
    /// Класс, описывающий процесс сравнения режима из ОИК и расчетных режимов
    /// </summary>
    class Program
    {
        /// <summary>
        /// Путь к файлам с расчетными данными
        /// </summary>
        private const string PATH = @"C:\Users\Artem\Desktop\rg2\Output\DataDB\";

        /// <summary>
        /// Путь к файлу с телеметрией
        /// </summary>
        private const string TELEMETRIESPATH = @"C:\Users\Artem\Desktop\rg2\Output\Telemetries\telemetries24.dat";

        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
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
                // Для ТС просто определялось соответствие включенного 
                // состояния, а для ТИ - относительное отклонение
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
    }
}
