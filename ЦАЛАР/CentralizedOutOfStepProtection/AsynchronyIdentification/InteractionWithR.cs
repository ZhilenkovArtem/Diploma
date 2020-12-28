using System;
using System.Collections.Generic;
using SynchronizedVectorMeasurementProcessing;

namespace AsynchronyIdentification
{
    /// <summary>
    /// Класс, где выполняется подключение
    /// к R и получение выходных данных
    /// </summary>
    public class InteractionWithR
    {
        private const string fileName = @"C:\Program Files\R\R-4.0.2\bin\x64\Rscript.exe";
        private const string workingDirectory = @"C:\Users\Артем Жиленков\Desktop" +
            @"\Магистрский IT\Diploma\ЦАЛАР";
        private const string argument = "classifier.R";
        /// <summary>
        /// Получить результат вычисления:
        /// 0 - устойчивый режим,
        /// 1 - режим, когда устойчивость нарушается до действия АПНУ,
        /// 2 - режим, когда устойчивость нарушается после действия АПНУ.
        /// </summary>
        /// <param name="initialData">Список с входными данными</param>
        /// <param name="fileName">Путь к приложению</param>
        /// <param name="workingDirectory">Путь к скрипту</param>
        /// <param name="argument">Имя скрипта</param>
        /// <returns>Результат вычисления</returns>
        public static string GetAnswer(List<ConfigurationRedonePmuData> initialData)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.Arguments = argument;
            process.StartInfo.Arguments += 
                TransformationDataForR.GetStringWithData(initialData);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            string[] stringSeparators = new string[] { "\r\n" };
            string[] identificationIndex = output.Split(stringSeparators, 
                StringSplitOptions.None);
            var errorMessage = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (output != null)
            {
                return identificationIndex[1];
            }
            else
            {
                return errorMessage;
            }
        }
    }
}
