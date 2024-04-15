using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Interpolation; // Додайте посилання на MathNet.Numerics через NuGet для використання spline

class Program
{
    static void Main()
    {
        // Вхідні дані
        double[] x_data = { 0, 3, 5, 7, 11 };
        double[] y_data = { 10, 6, 4, 1, 3 };
        double h = 0.2; // крок побудови графіків

        // Обчислення поліному Лагранжа
        double[] x_interp = Enumerable.Range(0, (int)(12 / h) + 1).Select(i => i * h).ToArray();
        double[] y_interp_lagrange = LagrangeInterpolation(x_data, y_data, x_interp);

        // Обчислення кубічного сплайну
        CubicSpline spline = CubicSpline.InterpolateNatural(x_data, y_data);
        double[] y_interp_spline = x_interp.Select(x => spline.Interpolate(x)).ToArray();

        // Побудова графіків
        Console.WriteLine("x_data\ty_data");
        for (int i = 0; i < x_data.Length; i++)
        {
            Console.WriteLine($"{x_data[i]}\t{y_data[i]}");
        }

        Console.WriteLine("\nx_interp\ty_interp_lagrange\ty_interp_spline");
        for (int i = 0; i < x_interp.Length; i++)
        {
            Console.WriteLine($"{x_interp[i]}\t{y_interp_lagrange[i]}\t{y_interp_spline[i]}");
        }
        Console.ReadKey();
    }

    // Функція для обчислення поліному Лагранжа
    static double[] LagrangeInterpolation(double[] x_data, double[] y_data, double[] x_interp)
    {
        double[] y_interp = new double[x_interp.Length];

        for (int j = 0; j < x_interp.Length; j++)
        {
            double sum = 0;
            for (int i = 0; i < x_data.Length; i++)
            {
                double term = y_data[i];
                for (int k = 0; k < x_data.Length; k++)
                {
                    if (k != i)
                    {
                        term *= (x_interp[j] - x_data[k]) / (x_data[i] - x_data[k]);
                    }
                }
                sum += term;
            }
            y_interp[j] = sum;
        }

        return y_interp;
    }
}