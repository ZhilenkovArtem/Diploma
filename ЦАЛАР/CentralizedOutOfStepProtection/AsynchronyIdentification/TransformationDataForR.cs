using System.Collections.Generic;
using System.Globalization;
using SynchronizedVectorMeasurementProcessing;

namespace AsynchronyIdentification
{
    /// <summary>
    /// Преобразование входных данных в строку
    /// </summary>
    public class TransformationDataForR
    {
        /// <summary>
        /// Получить строку входных данных
        /// </summary>
        /// <param name="slicesInTime">Срез данных за промежуток времени</param>
        /// <returns>Строка входных данных</returns>
        public static string GetStringWithData(
            List<ConfigurationRedonePmuData> slicesInTime)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            var columns = slicesInTime.Count;
            var rows = slicesInTime[0].Data.Count * 2;

            string[,] massive = new string[rows, columns];
            int j = 0;
            foreach (var slice in slicesInTime)
            {
                int i = 0;
                foreach (var parametrs in slice.Data)
                {
                    massive[i, j] = parametrs.VoltagePhase.ToString("0.00");
                    i++;
                    massive[i, j] = 
                        parametrs.VoltageMagnitude.ToString("0.00");
                    i++;
                }
                j++;
            }

            string str = null;
            for (int i = 0; i < rows; i++)
            {
                str += " ";
                for (j = 0; j < columns; j++)
                {
                    str += massive[i, j] + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }
    }
}
