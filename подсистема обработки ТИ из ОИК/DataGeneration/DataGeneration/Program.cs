using System;
using System.IO;

namespace DataGeneration
{
    /// <summary>
    /// Класс, описывающий крупные действия по переводе данных из RastrWin3
    /// в нужный формат
    /// </summary>
    class Program
    {
        /// <summary>
        /// Путь к файлам режима rg2
        /// </summary>
        private const string INPUTPATH = @"C:\Users\Artem\Desktop\rg2\Folder";

        /// <summary>
        /// Путь, по которому надо сохранять расчетные данные, которые будут 
        /// содержаться в БД
        /// </summary>
        private const string OUTPUTPATH1 = 
            @"C:\Users\Artem\Desktop\rg2\Output\DataDB\";

        /// <summary>
        /// Путь, по которому надо сохранять данные формата ОИК
        /// </summary>
        private const string OUTPUTPATH2 = 
            @"C:\Users\Artem\Desktop\rg2\Output\Telemetries\";

        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Поехали!");

            var number = 1;

            DirectoryInfo directory = new DirectoryInfo(INPUTPATH);
            foreach (var item in directory.GetFiles())
            {
                // Собираем данные из RastrWin3
                var dataFromRastrWin = GettingDataFromRastrWin3Library.
                    Processing.GetDataFromRastrWin3(item.FullName);

                // Выгружаем данные в виде БД и в виде ОИК
                var pathForDataSet = $"{OUTPUTPATH1}dataSet{number}.dat";
                var pathForTelemetries = $"{OUTPUTPATH2}telemetries{number}.dat";
                TransformationRastrData.Processing.DataOutput(dataFromRastrWin, pathForDataSet, pathForTelemetries);

                Console.WriteLine($"С {item.Name} закончили");
                number++;
            }

            Console.WriteLine("Выпонение программы завершено");
            Console.ReadKey();
        }
    }
}
