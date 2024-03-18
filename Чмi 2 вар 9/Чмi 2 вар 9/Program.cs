using System;

namespace CHMI_lab_2
{
    internal class Program
    {
        // Функція для розв'язання системи лінійних рівнянь методом простої ітерації
        static double[] IterativeSolve(double[,] A, double[] b, int maxIterations, double tolerance)
        {
            int n = A.GetLength(0);
            double[,] B = new double[n, n];
            double[] c = new double[n];

            // Побудова матриці ітераційного процесу B та вектора c
            for (int i = 0; i < n; i++)
            {
                double diagonal = A[i, i];
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        B[i, j] = 0;
                    else
                        B[i, j] = -A[i, j] / diagonal;
                }
                c[i] = b[i] / diagonal;
            }

            // Початкове наближення
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = c[i];
            }

            // Ітераційний процес
            for (int iter = 0; iter < maxIterations; iter++)
            {
                double[] nextX = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += B[i, j] * x[j];
                    }
                    nextX[i] = sum + c[i];
                }

                // Перевірка збіжності
                double maxDiff = double.MinValue;
                for (int i = 0; i < n; i++)
                {
                    maxDiff = Math.Max(maxDiff, Math.Abs(nextX[i] - x[i]));
                }

                if (maxDiff < tolerance)
                {
                    Console.WriteLine("Збігся на {0} ітераціях.", iter + 1);
                    return nextX;
                }

                x = nextX;
            }

            Console.WriteLine("Досягнуто максимальну кількість ітерацій.");
            return x;
        }

        // Точка входу програми
        static void Main(string[] args)
        {
            // Задання матриці A
            double[,] A = {
                { -2, 1, 1 },
                { 1, -2, 1 },
                { -1, 3, -6 }
            };

            // Задання вектора b
            double[] b = { 15, 10, 12 };

            // Максимальна кількість ітерацій та толерантність
            int maxIterations = 1000;
            double tolerance = 0.0001;

            // Вирішення системи лінійних рівнянь методом простої ітерації
            double[] x = IterativeSolve(A, b, maxIterations, tolerance);

            // Виведення результатів
            Console.WriteLine("Знайдені корені:");

            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("x[{0}] = {1}", i, Math.Round(x[i], 3));
            }
        }
    }
}
