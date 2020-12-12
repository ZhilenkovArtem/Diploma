using System;

namespace Testing
{
    /// <summary>
    /// Тестирование
    /// </summary>
    class Testing
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            var fileName = @"C:\Program Files\R\R-4.0.2\bin\x64\Rscript.exe";
            var workingDirectory = @"C:\Users\Артем Жиленков\Desktop" +
                @"\Магистрский IT\Diploma\подсистема Идентификации возникновения АР";
            var argument = "classifier.R";

            var answer = Estimation.GetAnswer(TestData.GetTestData(), 
                fileName, workingDirectory, argument);

            Console.WriteLine($"{answer}");
            Console.ReadKey();
        }
    }
}
