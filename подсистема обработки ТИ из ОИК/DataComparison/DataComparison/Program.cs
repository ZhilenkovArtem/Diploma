using System;
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
            Comparison.GetMode(PATH, TELEMETRIESPATH);
            Console.ReadKey();
        }
    }
}
