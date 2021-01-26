using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlActionsSelection;

namespace ServerOfComparison
{
    class TestData
    {
        /// <summary>
        /// Получить тестовые данные
        /// </summary>
        /// <returns></returns>
        public static SliceList GetTestData()
        {
            var ss = new List<SplitSlice>
            {
                new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },
                new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004, 2071, 2072, 2073, 2074, 2076, 2077, 2078,
                        2082, 2086, 2098, 2101, 2107
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045)
                    }
                },
                new SplitSlice
                {
                    FirstGroup = new List<float>
                    {
                        33009, 33010, 33012, 33013, 33014, 33016, 21007,
                        21008, 21009, 21010, 21011, 21012, 22001, 22003,
                        22004
                    },
                    SecondGroup = new List<float>
                    {
                        1012, 1013, 1014, 1015, 1016, 1017, 1018, 1024,
                        1029, 1056, 1058, 1060, 1061, 1062, 2071, 2072,
                        2073, 2074, 2076, 2077, 2078, 2082, 2086, 2098,
                        2101, 2107
                    },
                    Slice = new List<LineSegmentForSplitting>
                    {
                        new LineSegmentForSplitting(60401107, 60401044),
                        new LineSegmentForSplitting(60401108, 60401045),
                        new LineSegmentForSplitting(60401054, 60402097),
                        new LineSegmentForSplitting(60401050, 60401049),
                        new LineSegmentForSplitting(60401052, 60401051),
                        new LineSegmentForSplitting(60403339, 60403037),
                        new LineSegmentForSplitting(60403039, 60403282),
                        new LineSegmentForSplitting(60404082, 60403204),
                        new LineSegmentForSplitting(60404098, 60403433),
                        new LineSegmentForSplitting(60403153, 60403218),
                        new LineSegmentForSplitting(60403154, 60403217),
                        new LineSegmentForSplitting(60404083, 60403508)
                    }
                }
            };
            return new SliceList(ss);
        }
    }
}
