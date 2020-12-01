using System;
using System.IO;

namespace DataGeneration
{
    class Program
    {
        private const string INPUTPATH = @"C:\Users\Artem\Desktop\rg2\Folder";
        private const string OUTPUTPATH1 = @"C:\Users\Artem\Desktop\rg2\Output\DataDB\";
        private const string OUTPUTPATH2 = @"C:\Users\Artem\Desktop\rg2\Output\Telemetries\";

        static void Main()
        {
            Console.WriteLine("Поехали!");

            var number = 1;

            DirectoryInfo directory = new DirectoryInfo(INPUTPATH);
            foreach (var item in directory.GetFiles())
            {
                var dataFromRastrWin = GettingDataFromRastrWin3Library.
                    Processing.GetDataFromRastrWin3(item.FullName);

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
