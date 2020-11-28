using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class AllData
    {
        public HashSet<Node> Nodes { get; set; }

        public Edges EdgeSet { get; set; }

        public HashSet<Generator> Generator { get; set; }

        public AllData(HashSet<Node> nodes, Edges edges, HashSet<Generator> generator)
        {
            this.Nodes = nodes;
            this.EdgeSet = edges;
            this.Generator = generator;
        }
    }
}
