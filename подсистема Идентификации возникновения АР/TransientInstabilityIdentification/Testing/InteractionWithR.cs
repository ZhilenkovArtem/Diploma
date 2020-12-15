using System;
using System.Collections.Generic;

namespace Testing
{
    /// <summary>
    /// Класс, где выполняется подключение
    /// к R и получение выходных данных
    /// </summary>
    public class InteractionWithR
    {
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
        public static string GetAnswer(List<List<NodesParameters>> initialData, 
            string fileName, string workingDirectory, string argument)
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
