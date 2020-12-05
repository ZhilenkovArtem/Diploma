using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingOfPayload;

namespace TransformationRastrData
{
    /// <summary>
    /// Описание всех данных, которые являются результатом обработки
    /// данных, полученных из RastrWin3
    /// </summary>
    public class DataToExit
    {
        /// <summary>
        /// Хэш с расчетными данными для БД
        /// </summary>
        public HashSet<Data> Datas { get; set; }

        /// <summary>
        /// Список данных, которые должны быть получены из ОИК
        /// </summary>
        public List<Telemetry> Telemetries { get; set; }

        /// <summary>
        /// Индекс крайнего элемента (число всех элементов)
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Конструктор данных
        /// </summary>
        /// <param name="datas">Хэш с данными БД</param>
        /// <param name="telemetries">Список с данными ОИК</param>
        /// <param name="index">Индекс крайнего элемента</param>
        public DataToExit(HashSet<Data> datas, List<Telemetry> telemetries, 
            int index)
        {
            this.Datas = datas;
            this.Telemetries = telemetries;
            this.Index = index;
        }
    }
}
