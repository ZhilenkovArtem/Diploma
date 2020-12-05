using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UsingOfPayload
{
    /// <summary>
    /// Описание формата данных, получаемых из БД
    /// </summary>
    [Serializable]
    public class Data
    {
        /// <summary>
        /// Идентификатор телеметрии
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Весовой коэффициент
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Конструктор данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="value">Значение</param>
        /// <param name="controlParametrCoefficient">Коэффициент контрольного 
        /// параметра (номинальное напряжение или номинальная мощность)</param>
        /// <param name="districtCoefficient">Коэффициент района</param>
        public Data(int id, double value,
            double controlParametrCoefficient, double districtCoefficient)
        {
            this.ID = id;
            this.Value = value;
            this.Weight = controlParametrCoefficient * districtCoefficient;
        }

        /// <summary>
        /// Сохранение хэша с данными
        /// </summary>
        /// <param name="dataSet">Хэш с данными</param>
        /// <param name="path">Путь к месту сохранения</param>
        public static void SaveHashSet(HashSet<Data> dataSet, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, dataSet);
            }
        }

        /// <summary>
        /// Загрузка хэша с данными
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Хэш с данными</returns>
        public static HashSet<Data> LoadHashSet(string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (HashSet<Data>)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}
