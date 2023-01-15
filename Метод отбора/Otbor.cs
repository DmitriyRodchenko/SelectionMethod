using System;
namespace Методотбора
{
    class Otbor
    {
        //Плотность распределения p
        public static double p(double x)
        {
            if (x < 0.0)
                return 0.0;
            else if (x <= 1.0)
                return 3.0 * x * x;
            else
                return 0.0;     
        }

        //Плотность распределения q
        public static double q(double x)
        {     
            if (x < 0.0)
                return 0.0;
            else if (x <= 1.0)
                return 2.0 * x;
            else
                return 0.0;
        }

        //Моделирующая формула eta
        public static double ETA(double a)
        {
            return Math.Sqrt(a);
            /* Пример возможной записи моделирующей формулы для eta 
            if (a > 0.0)
                return Math.Sqrt(a);
            else if (a <= 1.0)
                return Math.Sqrt(a);
            else ...
            */
        }

        //Функция распределения F 
        public static double F(double x)
        {
            if (x < Method.Xmin)
                return 0;
            else
                return 1;
            /* Пример записи функции распределения F
            if (x < 13.0)
                return 0.0;
            else if (x <= 15.0)
                return (3 * x * x - 74.0 * x + 455.0) / 68.0;
            else if (x <= 19.0)
                return (-(x * x) + 46.0 * x - 445.0) / 68.0;
            else
                return 1.0;   
            */
        }

        //Функция r = p(x)/q(x)
        public static double r(double x)
        {
            if (x < 0.0)
                return 0.0;
            else if (x < 1.0)
                return 1.5 * x;
            else
                return 0.0;
        }   
    }
}
