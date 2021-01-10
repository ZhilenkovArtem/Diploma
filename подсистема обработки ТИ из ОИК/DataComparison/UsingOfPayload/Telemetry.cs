using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UsingOfPayload
{
    /// <summary>
    /// Описание формата данных, которые будут получены от ОИК
    /// </summary>
    [Serializable]
    public class Telemetry
    {
        /// <summary>
        /// Адрес телеметрии
        /// </summary>
        public int ObjectAddress { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Является ли телесигналом (ТС)
        /// </summary>
        public TypeId TypeId { get; set; }

        /// <summary>
        /// Конструктор данных
        /// </summary>
        /// <param name="address">Адрес</param>
        /// <param name="value">Значение</param>
        /// <param name="flag">Флаг "Является ли ТС"</param>
        public Telemetry(int address, double value, TypeId typeId)
        {
            this.ObjectAddress = address;
            this.Value = value;
            this.TypeId = typeId;
        }

        /// <summary>
        /// Сохранить список с данными
        /// </summary>
        /// <param name="telemetries">Список данных</param>
        /// <param name="path">Путь к месту сохранения</param>
        public static void SaveList(List<Telemetry> telemetries, string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path,
                    FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, telemetries);
            }
        }

        /// <summary>
        /// Загрузить список с данными
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Список с данными</returns>
        public static List<Telemetry> LoadList(string path)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (List<Telemetry>)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}
