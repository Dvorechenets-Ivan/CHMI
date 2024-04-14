using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    internal class Program
    {
        static double eps = 0.001; // Точність обчислень

        static void Main(string[] args)
        {
            var a = Zeidel(Math.PI / 9, 0.4, out int k); // Початкові значення та виклик методу Зейделя
            for (int i = 0; i < a.Count; i++)
            {
                Console.WriteLine(a.ElementAt(i)); // Виведення результатів ітераційного процесу
                if (i % 2 == 1)
                {
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        // Функція для обчислення x за значенням y
        static double function_x_iter(double y)
        {
            return Math.Asin((3 * y + 0.56) / 8);
        }

        // Функція для обчислення y за значенням x
        static double function_y_iter(double x)
        {
            return Math.Acos(Math.Sqrt(x));
        }

        // Метод Зейделя для розв'язання системи рівнянь
        static public Dictionary<string, double> Zeidel(double x0, double y0, out int k)
        {
            var solutions = new Dictionary<string, double>()
            {
                { "x_0",  x0},
                { "y_0", y0}
            };
            k = 0;
            do
            {
                k++;
                solutions.Add($"x_{k}", function_x_iter(solutions[$"y_{k - 1}"])); // Обчислення нових значень x та y
                solutions.Add($"y_{k}", function_y_iter(solutions[$"x_{k}"]));
            }
            while (IsIterationStopCondition(solutions, k)); // Перевірка критерію зупинки
            return solutions;
        }

        // Перевірка критерію зупинки за допомогою точності
        private static bool IsIterationStopCondition(Dictionary<string, double> solutions, int k)
        {
            var nev_x = (Math.Abs(solutions[$"x_{k - 1}"] - solutions[$"x_{k}"])); // Різниця між попереднім та поточним значеннями x
            var nev_y = (Math.Abs(solutions[$"y_{k - 1}"] - solutions[$"y_{k}"])); // Різниця між попереднім та поточним значеннями y

            if (nev_x < eps || nev_y < eps) // Перевірка чи досягнута необхідна точність
            {
                return false; // Якщо досягнута, то завершити ітерації
            }
            return true; // Якщо не досягнута, продовжити ітерації
        }
    }
}
