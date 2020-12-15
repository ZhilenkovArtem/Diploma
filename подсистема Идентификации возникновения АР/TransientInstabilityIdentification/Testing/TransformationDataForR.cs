using System.Collections.Generic;
using System.Globalization;

namespace Testing
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
        public static string GetStringWithData(List<List<NodesParameters>> slicesInTime)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            string[,] massive = new string[22,4];
            int j = 0;
            foreach (var slice in slicesInTime)
            {
                int i = 0;
                foreach (var parametrs in slice)
                {
                    massive[i, j] = parametrs.Angle.ToString("0.00");
                    i++;
                    massive[i, j] = parametrs.Voltage.ToString("0.00");
                    i++;
                }
                j++;
            }

            string str = null;
            for (int i = 0; i < 22; i++)
            {
                str += " ";
                for (j = 0; j < 4; j++)
                {
                    str += massive[i, j] + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }
    }
}
