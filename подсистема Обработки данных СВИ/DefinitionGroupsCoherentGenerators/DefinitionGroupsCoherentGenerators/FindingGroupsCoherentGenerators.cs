using System;
using System.Collections.Generic;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Поиск групп когерентных генраторов
    /// </summary>
    public class FindingGroupsCoherentGenerators
    {
        /// <summary>
        /// Возвращает группы когерентных генраторов в виде листов
        /// </summary>
        /// <param name="devList">Список отклонений по всем PMU</param>
        /// <returns>Группы когерентных генраторов</returns>
        public static Tuple<List<int>, List<int>> GetGroupsCoherentGenerators(List<PMUsDeviation> devList)
        {
            var firstGroup = new List<int>();
            var secondGroup = new List<int>();
            
            foreach (var pmu in devList)
            {
                if (pmu.DeviationFromCOI > 0)
                {
                    firstGroup.Add(pmu.PMUsIDCode);
                }
                else
                {
                    secondGroup.Add(pmu.PMUsIDCode);
                }
            }

            return Tuple.Create(firstGroup, secondGroup);
        }
    }
}
