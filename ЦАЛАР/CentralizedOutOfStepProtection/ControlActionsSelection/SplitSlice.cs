using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlActionsSelection
{
    public class SplitSlice
    {
        public List<float> FirstGroup { get; set; }

        public List<float> SecondGroup { get; set; }

        public List<LineSegmentForSplitting> Slice { get; set; }
    }

    public struct LineSegmentForSplitting
    {
        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public LineSegmentForSplitting(int startNode, int endNode)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
        }
    }

    public class SliceSelection
    {
        public static List<LineSegmentForSplitting> SelectSlice(List<float> group1, List<float> group2)
        {
            var sliceList = TestData.GetTestData();
            bool flag;

            foreach (var slice in sliceList)
            {
                flag = true;
                foreach (var gen in group1)
                {
                    if (!slice.FirstGroup.Contains(gen))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    foreach (var gen in group2)
                    {
                        if (!slice.SecondGroup.Contains(gen))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag == true)
                {
                    return slice.Slice;
                    break;
                }
            }
            return new List<LineSegmentForSplitting>();
        }
    }
}
