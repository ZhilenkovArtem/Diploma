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
        private const string PATH = @"C:\Users\Артем Жиленков\Desktop\" + 
            @"Магистрский IT\Diploma\подсистема обработки ТИ из ОИК\Output\" + 
            @"DataDB\";

        /// <summary>
        /// Путь к файлу с телеметрией
        /// </summary>
        private const string TELEMETRIESPATH = @"C:\Users\Артем Жиленков\" + 
            @"Desktop\Магистрский IT\Diploma\подсистема обработки ТИ из ОИК\" + 
            @"Output\Telemetries\telemetries24.dat";

        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            Console.WriteLine("начали");
            Console.WriteLine(Comparison.GetMode(PATH, TELEMETRIESPATH));
            Console.WriteLine("закончили");
            Console.ReadKey();
        }
    }
}
