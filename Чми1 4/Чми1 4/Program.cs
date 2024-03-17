using System.Collections.Generic;

namespace Метод_Ньютона
{
    public class Program
    {
        const double eps = 0.01; // Точність
        const double delta = 0.01; // Крок

        static double fx(double x) // Функція
        {
            return (Math.Pow(x, 4) - 1) / Math.Pow(x, 3); // -10 10 300
        }

        static double dfx(double x) // Похідна
        {
            return (fx(x + delta) - fx(x)) / delta;
        }

        static double ЗнайтиКорінь(double x0) // Знайти корінь за допомогою методу Ньютона
        {
            double x1 = x0 - fx(x0) / dfx(x0);
            int ітерації = 0;

            while (Math.Abs(x1 - x0) > eps)
            {
                x0 = x1;
                x1 = x0 - fx(x0) / dfx(x0);
                ітерації++;

                // Обробка випадків, коли метод може не збігатися (необов'язково)
                if (ітерації > 100) // Встановіть граничне значення за потребою
                {
                    Console.WriteLine("Попередження: метод Ньютона може не збігатися для цього початкового наближення.");
                    break;
                }
            }

            return x1;
        }

        public static List<double> МетодНьютона(double a, double b, double accuracy) // Знайти всі корені в заданому інтервалі
        {
            List<double> корені = new List<double>();

            // Перевірка поведінки функції при x = 0 (необов'язково)
            if (double.IsInfinity(fx(0)))
            {
                Console.WriteLine("Попередження: функція має невизначену поведінку при x = 0. Корені в цій області можуть бути неточними.");
            }

            for (double x = a; x <= b; x += accuracy)
            {
                // Перевірка зміни знака для визначення потенційних коренів
                if (Math.Sign(fx(x)) != Math.Sign(fx(x + accuracy)))
                {
                    корені.Add(ЗнайтиКорінь(x)); // Використовуємо поточне x як початкове наближення
                }
            }

            return корені;
        }

        static void Main(string[] args)
        {
            double a = -10; // Початок інтервалу
            double b = 10; // Кінець інтервалу
            double accuracy = 0.1; // Точність

            var корені = МетодНьютона(a, b, accuracy);

            Console.WriteLine("Корені:");
            for (int i = 0; i < корені.Count; i++)
            {
                Console.WriteLine($"   {корені[i]:f6}"); // Відображення коренів з 6 десятковими знаками
            }

            Console.ReadKey();
        }
    }
}
