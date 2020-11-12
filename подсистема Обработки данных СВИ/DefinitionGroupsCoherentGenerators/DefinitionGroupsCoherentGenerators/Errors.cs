using System;
using System.Collections.Generic;
using System.Reflection;

namespace DefinitionGroupsCoherentGenerators
{
    /// <summary>
    /// Ошибки (отклонения)
    /// </summary>
    public class Errors
    {
        /// <summary>
        /// Ограничение по отклонению угла генератора от
        /// центра инерции (COI - center of inertia)
        /// </summary>
        private const float LIMIT = 30;

        #region Fields
        /// <summary>
        /// Время
        /// </summary>
        private float _time;

        /// <summary>
        /// Флаг "произошло ли нарушение"
        /// </summary>
        private bool _fault = false;

        /// <summary>
        /// Тут и далее отклонение для телеизмерения
        /// от центра инерции COI
        /// </summary>
        private float _errorForTM33;

        private float _errorForTM34;

        private float _errorForTM35;

        private float _errorForTM36;

        private float _errorForTM37;

        private float _errorForTM38;

        private float _errorForTM39;

        private float _errorForTM40;

        private float _errorForTM41;

        private float _errorForTM42;

        private float _errorForTM43;

        private float _errorForTM44;

        private float _errorForTM45;

        private float _errorForTM46;

        private float _errorForTM47;

        private float _errorForTM48;

        private float _errorForTM49;

        private float _errorForTM50;

        private float _errorForTM51;

        private float _errorForTM52;

        private float _errorForTM53;

        private float _errorForTM54;

        private float _errorForTM55;

        private float _errorForTM56;

        private float _errorForTM57;

        private float _errorForTM58;

        private float _errorForTM59;

        private float _errorForTM60;

        private float _errorForTM61;

        private float _errorForTM62;

        private float _errorForTM63;

        private float _errorForTM64;
        #endregion

        #region Properties
        /// <summary>
        /// Время
        /// </summary>
        public float Time => _time;

        /// <summary>
        /// Флаг "произошло ли нарушение"
        /// </summary>
        public bool Fault => _fault;

        /// <summary>
        /// Тут и далее отклонение для телеизмерения
        /// от центра инерции COI
        /// </summary>
        public float ErrorForTM33 => _errorForTM33;

        public float ErrorForTM34 => _errorForTM34;

        public float ErrorForTM35 => _errorForTM35;

        public float ErrorForTM36 => _errorForTM36;

        public float ErrorForTM37 => _errorForTM37;

        public float ErrorForTM38 => _errorForTM38;

        public float ErrorForTM39 => _errorForTM39;

        public float ErrorForTM40 => _errorForTM40;

        public float ErrorForTM41 => _errorForTM41;

        public float ErrorForTM42 => _errorForTM42;

        public float ErrorForTM43 => _errorForTM43;

        public float ErrorForTM44 => _errorForTM44;

        public float ErrorForTM45 => _errorForTM45;

        public float ErrorForTM46 => _errorForTM46;

        public float ErrorForTM47 => _errorForTM47;

        public float ErrorForTM48 => _errorForTM48;

        public float ErrorForTM49 => _errorForTM49;

        public float ErrorForTM50 => _errorForTM50;

        public float ErrorForTM51 => _errorForTM51;

        public float ErrorForTM52 => _errorForTM52;

        public float ErrorForTM53 => _errorForTM53;

        public float ErrorForTM54 => _errorForTM54;

        public float ErrorForTM55 => _errorForTM55;

        public float ErrorForTM56 => _errorForTM56;

        public float ErrorForTM57 => _errorForTM57;

        public float ErrorForTM58 => _errorForTM58;

        public float ErrorForTM59 => _errorForTM59;

        public float ErrorForTM60 => _errorForTM60;

        public float ErrorForTM61 => _errorForTM61;

        public float ErrorForTM62 => _errorForTM62;

        public float ErrorForTM63 => _errorForTM63;

        public float ErrorForTM64 => _errorForTM64;
        #endregion

        #region Dictionary
        /// <summary>
        /// Словарь пар "отклонение ТИ от COI - ТИ"
        /// </summary>
        private Dictionary<int, Tuple<string, string>> errors =
            new Dictionary<int, Tuple<string, string>>(32)
        {
            {
                1, new Tuple<string, string>("_errorForTM33", "TM33")
            },
            {
                2, new Tuple<string, string>("_errorForTM34", "TM34")
            },
            {
                3, new Tuple<string, string>("_errorForTM35", "TM35")
            },
            {
                4, new Tuple<string, string>("_errorForTM36", "TM36")
            },
            {
                5, new Tuple<string, string>("_errorForTM37", "TM37")
            },
            {
                6, new Tuple<string, string>("_errorForTM38", "TM38")
            },
            {
                7, new Tuple<string, string>("_errorForTM39", "TM39")
            },
            {
                8, new Tuple<string, string>("_errorForTM40", "TM40")
            },
            {
                9, new Tuple<string, string>("_errorForTM41", "TM41")
            },
            {
                10, new Tuple<string, string>("_errorForTM42", "TM42")
            },
            {
                11, new Tuple<string, string>("_errorForTM43", "TM43")
            },
            {
                12, new Tuple<string, string>("_errorForTM44", "TM44")
            },
            {
                13, new Tuple<string, string>("_errorForTM45", "TM45")
            },
            {
                14, new Tuple<string, string>("_errorForTM46", "TM46")
            },
            {
                15, new Tuple<string, string>("_errorForTM47", "TM47")
            },
            {
                16, new Tuple<string, string>("_errorForTM48", "TM48")
            },
            {
                17, new Tuple<string, string>("_errorForTM49", "TM49")
            },
            {
                18, new Tuple<string, string>("_errorForTM50", "TM50")
            },
            {
                19, new Tuple<string, string>("_errorForTM51", "TM51")
            },
            {
                20, new Tuple<string, string>("_errorForTM52", "TM52")
            },
            {
                21, new Tuple<string, string>("_errorForTM53", "TM53")
            },
            {
                22, new Tuple<string, string>("_errorForTM54", "TM54")
            },
            {
                23, new Tuple<string, string>("_errorForTM55", "TM55")
            },
            {
                24, new Tuple<string, string>("_errorForTM56", "TM56")
            },
            {
                25, new Tuple<string, string>("_errorForTM57", "TM57")
            },
            {
                26, new Tuple<string, string>("_errorForTM58", "TM58")
            },
            {
                27, new Tuple<string, string>("_errorForTM59", "TM59")
            },
            {
                28, new Tuple<string, string>("_errorForTM60", "TM60")
            },
            {
                29, new Tuple<string, string>("_errorForTM61", "TM61")
            },
            {
                30, new Tuple<string, string>("_errorForTM62", "TM62")
            },
            {
                31, new Tuple<string, string>("_errorForTM63", "TM63")
            },
            {
                32, new Tuple<string, string>("_errorForTM64", "TM64")
            }
        };
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса "Ошибки (отклонения)"
        /// </summary>
        /// <param name="generators">данные по генераторам</param>
        public Errors(GeneratorsData generators)
        {
            var coi = GetCenterOfInertia(generators);

            Type myType = typeof(Errors);
            var fault = myType.GetField("_fault",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            var time = myType.GetField("_time",
                    BindingFlags.NonPublic | BindingFlags.Instance);

            time.SetValue(this, generators.Time);
            for (int i = 1; i <= generators.generatorsData.Count; i++)
            {
                var tuple = errors[i];
                var power = float.Parse(generators.GetType().
                    GetProperty(tuple.Item2).GetValue(generators).ToString());
                var difference = power - coi;

                FieldInfo error = myType.GetField(tuple.Item1,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                error.SetValue(this, difference);

                if (Math.Abs(difference) >= LIMIT)
                {
                    fault.SetValue(this, true);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Возвращает центра инерции COI
        /// </summary>
        /// <param name="generators">данные по генераторам</param>
        /// <returns>центр инерции COI</returns>
        private float GetCenterOfInertia(GeneratorsData generators)
        {
            float numerator = 0;
            float denominator = 0;
            for (int i = 1; i <= generators.generatorsData.Count; i++)
            {
                var tuple = generators.generatorsData[i];
                var power = float.Parse(generators.GetType().
                    GetProperty(tuple.Item1).GetValue(generators).ToString());
                var angle = float.Parse(generators.GetType().
                    GetProperty(tuple.Item2).GetValue(generators).ToString());

                numerator += power * angle;
                denominator += power;
            }
            return numerator / denominator;
        }

        /// <summary>
        /// Возвращает группы когерентных генраторов в виде листов
        /// </summary>
        /// <returns>группы когерентных генраторов</returns>
        public Tuple<List<string>, List<string>> GetGroupsCoherentGenerators()
        {
            List<string> firstGroup = new List<string>();
            List<string> secondGroup = new List<string>();
            Type myType = typeof(Errors);

            for (int i = 1; i <= errors.Count; i++)
            {
                var tuple = errors[i];
                FieldInfo error = myType.GetField(tuple.Item1,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (float.Parse(error.GetValue(this).ToString()) > 0)
                {
                    firstGroup.Add(tuple.Item2);
                }
                else
                {
                    secondGroup.Add(tuple.Item2);
                }
            }

            return Tuple.Create(firstGroup, secondGroup);
        }
        #endregion
    }
}
