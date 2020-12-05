using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    /// <summary>
    /// Класс, описывающий все данные, полученные из RastrWin3
    /// </summary>
    public class AllData
    {
        /// <summary>
        /// Хэш узлов
        /// </summary>
        public HashSet<Node> Nodes { get; set; }

        /// <summary>
        /// Ветви (или ребра) - сетевые элементы
        /// </summary>
        public Edges EdgeSet { get; set; }

        /// <summary>
        /// Хэш генераторов
        /// </summary>
        public HashSet<Generator> Generator { get; set; }

        /// <summary>
        /// Конструктор данных
        /// </summary>
        /// <param name="nodes">Узлы</param>
        /// <param name="edges">Ветви</param>
        /// <param name="generator">Генераторы</param>
        public AllData(HashSet<Node> nodes, Edges edges, HashSet<Generator> generator)
        {
            this.Nodes = nodes;
            this.EdgeSet = edges;
            this.Generator = generator;
        }
    }
}
