using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDataFromRastrWin3Library
{
    public class Edges
    {
        public HashSet<LineSegment> LineSegments {get; set;}

        public HashSet<Transformer> Transformers { get; set; }

        public HashSet<Switch> Switches { get; set; }

        public Edges(HashSet<LineSegment> lineSegments, HashSet<Transformer> transformers, HashSet<Switch> switches)
        {
            this.LineSegments = lineSegments;
            this.Transformers = transformers;
            this.Switches = switches;
        }
    }
}
