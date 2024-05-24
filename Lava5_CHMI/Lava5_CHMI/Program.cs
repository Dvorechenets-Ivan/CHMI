using System;

class Program
{
    static void Main()
    {
        // Інтервал інтегрування
        double a = 0;
        double b = 60;

        // Кількість підінтервалів (повинна бути парною)
        int n = 10;

        // Крок
        double h = (b - a) / n;

        // Функція швидкості
        Func<double, double> v = t => (t / 3) + 2;

        // Змінна для накопичення результату інтегрування
        double integral = 0;

        // Формула Сімпсона
        integral += v(a) + v(b);

        for (int i = 1; i < n; i++)
        {
            double t = a + i * h;
            if (i % 2 == 0)
            {
                integral += 2 * v(t);
            }
            else
            {
                integral += 4 * v(t);
            }
        }

        integral *= h / 3;

        // Вивід результату
        Console.WriteLine("Пройдений шлях: " + integral + " м");
    }
}
