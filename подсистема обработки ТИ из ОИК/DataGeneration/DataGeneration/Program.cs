using System;

namespace DataGeneration
{
    class Program
    {
        static void Main()
        {
            string path = @"C:\Users\Artem\Desktop\rg2\BrKr_ONBr4_1975 с Палычем.rg2";
            var dataFromRastrWin = GettingDataFromRastrWin3Library.Processing.GetDataFromRastrWin3(path);

            var pathForDataSet = @"C:\Users\Artem\Desktop\dataSet.dat";
            var pathForTelemetries = @"C:\Users\Artem\Desktop\telemetries.dat";
            SavingData.Processing.DataOutput(dataFromRastrWin, pathForDataSet, pathForTelemetries);

            Console.WriteLine("Файлы созданы");
            Console.ReadKey();
        }
    }
}
