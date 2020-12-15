using System;
using System.Numerics;

namespace SynchronizedVectorMeasurementProcessing
{
    public class Converting
    {
        /// <summary>
        /// Преобразовать комплексное значение в тригонометрическое
        /// </summary>
        /// <param name="complexValue">Комплексная величина</param>
        /// <returns>Тригонометрическая величина</returns>
        public static float[] ConvertComplexToTrigonometric(Complex complexValue)
        {
            var r = (float)Math.Sqrt(Math.Pow(complexValue.Real, 2)
                + Math.Pow(complexValue.Imaginary, 2));
            var f = (float)(Math.Atan2(complexValue.Imaginary,
                complexValue.Real) * 180 / Math.PI);
            return new float[] { r, f };
        }
    }
}
