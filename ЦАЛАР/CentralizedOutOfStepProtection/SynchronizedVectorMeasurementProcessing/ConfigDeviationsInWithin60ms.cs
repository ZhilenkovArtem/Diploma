using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Конфигурация объекта, представляющего собой список отклонений для
    /// каждой PMU для нескольких срезов данных (например, 4 для 60 мс)
    /// </summary>
    public class ConfigDeviationsInWithin60ms
    {
        /// <summary>
        /// Число анализируемых срезов данных (4 среза = 60 мс)
        /// </summary>
        public const int DEVIATIONSLISTSCOUNT = 4;

        /// <summary>
        /// Список отклонений по срезам для 60 мс
        /// </summary>
        public List<DeviationsList> DeviationsInWithin60ms { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigDeviationsInWithin60ms() 
        {
            this.DeviationsInWithin60ms = new List<DeviationsList>();
        }

        /// <summary>
        /// Добавить новый список отклонений (срез) в список срезов 
        /// (во времени)
        /// </summary>
        /// <param name="devLists">Список срезов</param>
        /// <param name="newDevList">Новый срез</param>
        /// <returns>Обновленный список срезов</returns>
        public ConfigDeviationsInWithin60ms AddNewDevList(
            ConfigDeviationsInWithin60ms configDevLists, 
            DeviationsList newDevList)
        {
            var devLists = configDevLists.DeviationsInWithin60ms;
            var devListsCount = devLists.Count;
            if (devListsCount == DEVIATIONSLISTSCOUNT)
            {
                for (int i = 0; i < DEVIATIONSLISTSCOUNT - 1; i++)
                {
                    devLists[i] = devLists[i + 1];
                }
                devLists[DEVIATIONSLISTSCOUNT - 1] = newDevList;

                devLists = ChangeParametersDeviationsLists(devLists);
            }
            else if (0 < devListsCount && 
                devListsCount < DEVIATIONSLISTSCOUNT)
            {
                devLists.Add(newDevList);

                devLists = ChangeParametersDeviationsLists(devLists);
            }
            else if (devListsCount == 0)
            {
                devLists.Add(newDevList);
            }
            else
            {
                throw new Exception($"Число devList в списке не лежит в " +
                    $"диапазоне от 0 до {DEVIATIONSLISTSCOUNT}");
            }
            configDevLists.DeviationsInWithin60ms = devLists;
            return configDevLists;
        }

        /// <summary>
        /// Изменение параметров отклонений для отдельных срезов в 
        /// списке срезов
        /// </summary>
        /// <param name="devLists"></param>
        /// <returns></returns>
        private List<DeviationsList> ChangeParametersDeviationsLists(
            List<DeviationsList> devLists)
        {
            var devListsCount = devLists.Count;

            foreach (var pmu in devLists[0].DevList)
            {
                pmu.DeltaRelativeToPreviousValue = 0;
                pmu.AccumulatedDeviation = 0;
            }

            for (int i = 1; i < devListsCount; i++)
            {
                foreach (var pmu in devLists[i].DevList)
                {
                    var idCode = pmu.PMUsIDCode;
                    var previousPMU = devLists[i - 1].DevList.Where(anyPMU
                        => anyPMU.PMUsIDCode == idCode).FirstOrDefault();
                    pmu.DeltaRelativeToPreviousValue =
                        pmu.DeviationFromCOI - previousPMU.DeviationFromCOI;
                    pmu.AccumulatedDeviation =
                        previousPMU.AccumulatedDeviation +
                        pmu.DeltaRelativeToPreviousValue;
                }
            }
            return devLists;
        }
    }
}
