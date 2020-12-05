using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Ветви (ребра) - электросетевые элементы
    /// </summary>
    public class Edges
    {
        /// <summary>
        /// Хэш линий электропередач
        /// </summary>
        public HashSet<LineSegment> LineSegments {get; set;}

        /// <summary>
        /// Хэш трансформаторов
        /// </summary>
        public HashSet<Transformer> Transformers { get; set; }

        /// <summary>
        /// Хэш выключателей
        /// </summary>
        public HashSet<Switch> Switches { get; set; }

        /// <summary>
        /// Конструктор данных ветвей
        /// </summary>
        /// <param name="lineSegments">линии</param>
        /// <param name="transformers">трансформаторы</param>
        /// <param name="switches">выключатели</param>
        public Edges(HashSet<LineSegment> lineSegments, 
            HashSet<Transformer> transformers, HashSet<Switch> switches)
        {
            this.LineSegments = lineSegments;
            this.Transformers = transformers;
            this.Switches = switches;
        }
    }
}
