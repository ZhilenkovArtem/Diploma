using System;
using System.Collections.Generic;

namespace DefinitionGroupsCoherentGenerators
{
    /// <summary>
    /// Данные по генераторам
    /// </summary>
    public class GeneratorsData
    {
        #region Properties
        /// <summary>
        /// Время
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// Тут и далее телеизмерение (telemetry)
        /// </summary>
        public float TM1 { get; set; }

        public float TM2 { get; set; }
                     
        public float TM3 { get; set; }
                     
        public float TM4 { get; set; }
                     
        public float TM5 { get; set; }
                     
        public float TM6 { get; set; }
                     
        public float TM7 { get; set; }
                     
        public float TM8 { get; set; }
                     
        public float TM9 { get; set; }
                     
        public float TM10 { get; set; }
                     
        public float TM11 { get; set; }
                     
        public float TM12 { get; set; }
                     
        public float TM13 { get; set; }
                     
        public float TM14 { get; set; }
                     
        public float TM15 { get; set; }
                     
        public float TM16 { get; set; }
                     
        public float TM17 { get; set; }
                     
        public float TM18 { get; set; }
                     
        public float TM19 { get; set; }
                     
        public float TM20 { get; set; }
                     
        public float TM21 { get; set; }
                     
        public float TM22 { get; set; }
                     
        public float TM23 { get; set; }
                     
        public float TM24 { get; set; }
                     
        public float TM25 { get; set; }
                     
        public float TM26 { get; set; }
                     
        public float TM27 { get; set; }
                     
        public float TM28 { get; set; }
                     
        public float TM29 { get; set; }
                     
        public float TM30 { get; set; }
                     
        public float TM31 { get; set; }
                     
        public float TM32 { get; set; }

        public float TM33 { get; set; }

        public float TM34 { get; set; }

        public float TM35 { get; set; }

        public float TM36 { get; set; }

        public float TM37 { get; set; }

        public float TM38 { get; set; }

        public float TM39 { get; set; }

        public float TM40 { get; set; }

        public float TM41 { get; set; }

        public float TM42 { get; set; }

        public float TM43 { get; set; }

        public float TM44 { get; set; }

        public float TM45 { get; set; }

        public float TM46 { get; set; }

        public float TM47 { get; set; }

        public float TM48 { get; set; }

        public float TM49 { get; set; }

        public float TM50 { get; set; }

        public float TM51 { get; set; }

        public float TM52 { get; set; }

        public float TM53 { get; set; }

        public float TM54 { get; set; }

        public float TM55 { get; set; }

        public float TM56 { get; set; }

        public float TM57 { get; set; }

        public float TM58 { get; set; }

        public float TM59 { get; set; }

        public float TM60 { get; set; }

        public float TM61 { get; set; }

        public float TM62 { get; set; }

        public float TM63 { get; set; }

        public float TM64 { get; set; }
        #endregion

        #region Dictionary
        /// <summary>
        /// Словарь пар телеизмерение (ТИ) мощности - ТИ угла 
        /// </summary>
        public Dictionary<int, Tuple<string, string>> generatorsData = 
            new Dictionary<int, Tuple<string, string>>(32)
        {
            {
                1, new Tuple<string, string>("TM1", "TM33")
            },
            {
                2, new Tuple<string, string>("TM2", "TM34")
            },
            {
                3, new Tuple<string, string>("TM3", "TM35")
            },
            {
                4, new Tuple<string, string>("TM4", "TM36")
            },
            {
                5, new Tuple<string, string>("TM5", "TM37")
            },
            {
                6, new Tuple<string, string>("TM6", "TM38")
            },
            {
                7, new Tuple<string, string>("TM7", "TM39")
            },
            {
                8, new Tuple<string, string>("TM8", "TM40")
            },
            {
                9, new Tuple<string, string>("TM9", "TM41")
            },
            {
                10, new Tuple<string, string>("TM10", "TM42")
            },
            {
                11, new Tuple<string, string>("TM11", "TM43")
            },
            {
                12, new Tuple<string, string>("TM12", "TM44")
            },
            {
                13, new Tuple<string, string>("TM13", "TM45")
            },
            {
                14, new Tuple<string, string>("TM14", "TM46")
            },
            {
                15, new Tuple<string, string>("TM15", "TM47")
            },
            {
                16, new Tuple<string, string>("TM16", "TM48")
            },
            {
                17, new Tuple<string, string>("TM17", "TM49")
            },
            {
                18, new Tuple<string, string>("TM18", "TM50")
            },
            {
                19, new Tuple<string, string>("TM19", "TM51")
            },
            {
                20, new Tuple<string, string>("TM20", "TM52")
            },
            {
                21, new Tuple<string, string>("TM21", "TM53")
            },
            {
                22, new Tuple<string, string>("TM22", "TM54")
            },
            {
                23, new Tuple<string, string>("TM23", "TM55")
            },
            {
                24, new Tuple<string, string>("TM24", "TM56")
            },
            {
                25, new Tuple<string, string>("TM25", "TM57")
            },
            {
                26, new Tuple<string, string>("TM26", "TM58")
            },
            {
                27, new Tuple<string, string>("TM27", "TM59")
            },
            {
                28, new Tuple<string, string>("TM28", "TM60")
            },
            {
                29, new Tuple<string, string>("TM29", "TM61")
            },
            {
                30, new Tuple<string, string>("TM30", "TM62")
            },
            {
                31, new Tuple<string, string>("TM31", "TM63")
            },
            {
                32, new Tuple<string, string>("TM32", "TM64")
            }
        };
        #endregion
    }
}
