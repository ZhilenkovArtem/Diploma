using System;

namespace SynchronizedVectorMeasurementProcessing
{
    /// <summary>
    /// Комплексное значение
    /// </summary>
    public class Complex
    {
        /// <summary>
        /// Реальное (x)
        /// </summary>
        public float Real { get; set; }

        /// <summary>
        /// Мнимое (y)
        /// </summary>
        public float Imaginary { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="re">Реальное значение</param>
        /// <param name="im">Мнимое значение</param>
        public Complex(float re, float im)
        {
            this.Real = re;
            this.Imaginary = im;
        }

        /// <summary>
        /// Преобращовать комплексное значение в тригонометрическое
        /// </summary>
        /// <param name="complexValue">Комплексная величина</param>
        /// <returns>Тригонометрическая величина</returns>
        public static float[] ConvertToTrigonometric(Complex complexValue)
        {
            var r = (float)Math.Sqrt(Math.Pow(complexValue.Real, 2)
                + Math.Pow(complexValue.Imaginary, 2));
            var f = (float)(Math.Atan2(complexValue.Imaginary,
                complexValue.Real)*180/Math.PI);
            return new float[] { r, f };
        }
    }
}
