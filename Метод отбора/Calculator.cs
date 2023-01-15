using System;
using System.Data;
using System.Drawing;

namespace Методотбора
{
    public static class Calculator
    {
        private static int SelectionSize = 0;
        private static int Intervals = 0;
        private static double? x2Statistics = null;
        private static double? significancelvl = null;
        private static int[] frequency = null;
        private static double[] probability = null;
        private static double m = 0;

        public static double? X2Statistics
        {
            get { return x2Statistics; }
        }
        public static double? Significancelvl
        {
            get { return significancelvl; }
        }
        public static bool IsFrequenciesNull
        {
            get { return frequency == null || frequency.Length == 0; }
        }
        public static int intervals
        {
            get { return Intervals; }
        }
        public static double GM
        {
            get { return m; }
        }
        private static double missingValue = 0.0;
        private static int missingFrequency = 0;

        //Очистка
        public static void clear()
        {
            SelectionSize = 0;
            Intervals = 0;
            x2Statistics = null;
            significancelvl = null;
            frequency = null;
            probability = null;
        }

        //Вычисление вероятностей попадания чисел в интервалы
        public static void calculateProbabilities(int theIntervals, bool f)
        {
            Intervals = theIntervals;
            probability = new double[Intervals];
            double delta = (Method.Xmax - Method.Xmin) / Intervals;
            double x = Method.Xmin + delta;
            double Fprevious = 0.0;
            double Fcurrent;

            for (int i = 0; i < Intervals; ++i)
            { if (f == true)
                    Fcurrent = Fcomp(x);
                else
                    Fcurrent = Otbor.F(x);
                probability[i] = Fcurrent - Fprevious;
                x += delta;
                Fprevious = Fcurrent;
            }
        }

        //Вычисление частот(количества попаданий чисел в интервалы)
        public static void calculateFrequencies(DataTable theSelectionDataTable, DataTable theFrequenciesDataTable, int theIntervals)
        {
            SelectionSize = theSelectionDataTable.Rows.Count;
            if (SelectionSize == 0)
            {
                frequency = null;
                return;
            }

            frequency = new int[theIntervals];
            int index;
            double xi;
            foreach (DataRow row in theSelectionDataTable.Rows)
            {
                if (!row.IsNull(0))
                {
                    xi = (double)row[0];
                    if (xi < Method.Xmin || xi > Method.Xmax)
                        xi = missingValue;
                    index = (int)((xi - Method.Xmin) / (Method.Xmax - Method.Xmin) * theIntervals);
                    ++frequency[index];
                }
            }
            theFrequenciesDataTable.Clear();
            foreach (int m in frequency)
                theFrequenciesDataTable.Rows.Add(m);
        }

        //Заполнение таблицы частот(количества попаданий чисел в интервалы)
        public static void fillFrequencies(DataTable theFrequenciesDataTable)
        {
            int theIntervals = theFrequenciesDataTable.Rows.Count;
            if (theFrequenciesDataTable == null || theIntervals == 0)
            {
                frequency = null;
                return;
            }
            frequency = new int[theIntervals];
            int m;
            int index = 0;
            foreach (DataRow row in theFrequenciesDataTable.Rows)
            {
                if (!row.IsNull(0))
                {
                    m = (int)row[0];
                    if (m < 0)
                        m = missingFrequency;
                    frequency[index] = m;
                }
                ++index;
            }
        }

        //Вычисление Хи-квадрат
        public static double? calculatex2()
        {
            if (frequency == null || probability == null || frequency.Length == 0 || frequency.Length != probability.Length)
            {
                x2Statistics = null;
                return x2Statistics;
            }
            double term;
            double sum = 0.0;
            double n = SelectionSize;
            for (int i = 0; i < Intervals; ++i)
            {
                term = frequency[i] / n - probability[i];
                sum += term * term / probability[i];
            }
            sum *= n;
            x2Statistics = sum;
            return x2Statistics;
        }

        //Вычисление уровня значимости Хи-квадрат
        public static double significancelvlxi2(double xi, int n)
        {
            if (xi <= 0.0)
                return 1.0;

            int ndivided2 = n / 2;
            double xdivided2 = xi / 2.0;
            double term = 1.0;
            double sum = term;
            for (int i = 1; i < ndivided2; ++i)
            {
                term = term * xdivided2 / i;
                sum += term;
            }
            return sum * Math.Exp(-xdivided2);
        }


        //Нахождение максимума функции r, "Оценка сверху"
        public static double M()
        {
            double i = Method.Xmin, t, r = 0;
            while (i <= Method.Xmax)
            {
                if (Method.IsRcheck == true)
                {
                    t = rcomp(i);
                    if (t > r)
                        r = t;
                }
                else
                {
                    t = Otbor.r(i);
                    if (t > r)
                        r = t;
                }
                i += 0.00001;
            }
            if (r < 1 && r > 0.9999)
                return Math.Ceiling(r);
            else return r;
        }

        //Подсчет r = P(x) / Q(x)
        public static double rcomp(double x)
        {
            if (x < Method.Xmin)
                return 0.0;
            else if (x <= Method.Xmax && Otbor.q(x) != 0)
                return Otbor.p(x) / Otbor.q(x);
            else
                return 0.0;
        }
        //Подсчет F численным методом(методом Симпсона/Парабол)	
        public static double Fcomp(double x)
        {
            if (x < Method.Xmin)
                return 0.0;
            else if (x <= Method.Xmax)
            {
                double a = Method.Xmin, b = x, eps = Service.getFp;  //Нижний и верхний пределы интегрирования (a, b), погрешность (eps).
                double I = eps + 1, I1 = 0;                     //I-предыдущее вычисленное значение интеграла, I1-новое, с большим N.
                for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
                {
                    if (N > 10000000)
                        return I1;
                    double h, sum2 = 0, sum4 = 0, sum = 0;
                    h = (b - a) / (2 * N);                      //Шаг интегрирования.
                    for (int i = 1; i <= 2 * N - 1; i += 2)
                    {
                        sum4 += Otbor.p(a + h * i);                   //Значения с нечётными индексами, которые нужно умножить на 4.
                        sum2 += Otbor.p(a + h * (i + 1));             //Значения с чётными индексами, которые нужно умножить на 2.
                    }
                    sum = Otbor.p(a) + 4 * sum4 + 2 * sum2 - Otbor.p(b);    //Отнимаем значение P(b) так как ранее прибавили его дважды. 
                    I = I1;
                    I1 = (h / 3) * sum;
                }
                return I1;
            }
            else
                return 1;
        }

        //Проверка площади плотности P методом Симпсона/Парабол
        public static double Pcheck()
        {
            double a = Method.Xmin, b = Method.Xmax, eps = Service.getPQp;   //Нижний и верхний пределы интегрирования (a, b), погрешность (eps).
            double I = eps + 1, I1 = 0;                                      //I-предыдущее вычисленное значение интеграла, I1-новое, с большим N.
            for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
            {
                if (N > 10000000)
                    return I1;
                double h, sum2 = 0, sum4 = 0, sum = 0;
                h = (b - a) / (2 * N);                    //Шаг интегрирования.
                for (int i = 1; i <= 2 * N - 1; i += 2)
                {
                    sum4 += Otbor.p(a + h * i);                //Значения с нечётными индексами, которые нужно умножить на 4.
                    sum2 += Otbor.p(a + h * (i + 1));          //Значения с чётными индексами, которые нужно умножить на 2.
                }
                sum = Otbor.p(a) + 4 * sum4 + 2 * sum2 - Otbor.p(b);//Отнимаем значение P(b) так как ранее прибавили его дважды. 
                I = I1;
                I1 = (h / 3) * sum;
            }
            return I1;
        }

        //Проверка площади плотности Q методом Симпсона/Парабол
        public static double Qcheck()
        {
            double a = Method.Xmin, b = Method.Xmax, eps = Service.getPQp; //Нижний и верхний пределы интегрирования (a, b), погрешность (eps).
            double I = eps + 1, I1 = 0;                                    //I-предыдущее вычисленное значение интеграла, I1-новое, с большим N.
            for (int N = 2; (N <= 4) || (Math.Abs(I1 - I) > eps); N *= 2)
            {
                if (N > 10000000)
                    return I1;
                double h, sum2 = 0, sum4 = 0, sum = 0;
                h = (b - a) / (2 * N);                    //Шаг интегрирования.
                for (int i = 1; i <= 2 * N - 1; i += 2)
                {
                    sum4 += Otbor.q(a + h * i);                 //Значения с нечётными индексами, которые нужно умножить на 4.
                    sum2 += Otbor.q(a + h * (i + 1));           //Значения с чётными индексами, которые нужно умножить на 2.
                }
                sum = Otbor.q(a) + 4 * sum4 + 2 * sum2 - Otbor.q(b);
                I = I1;
                I1 = (h / 3) * sum;      //Отнимаем значение Q(b) так как ранее прибавили его дважды. 
            }
            return I1;
        }
        
        //Генерация выборки
        public static void generate(DataTable theSelectionDataTable, int theSelectionSize, double k, double percent, bool r)
        {
            if (k == 0)
                m = M() * (1.0 + percent / 100.0);
            else m = k * (1.0 + percent / 100.0);
            if (m < 1)
                return;

            Random random = new Random();
            double alpha, eta, t;
            int j=0;
            theSelectionDataTable.Clear();
            double min = Method.Xmin;
            double max = Method.Xmax;
            if (r != true)
                for (int i = 0; i < theSelectionSize; ++i)
                {
                    do
                    {
                        if (j != 10000)
                        {
                            alpha = random.NextDouble();
                            eta = Otbor.ETA(random.NextDouble() * (max - min) + min);
                            t = Otbor.r(eta);
                            if (t <= 0)
                                j++;
                            else
                                j = 0;
                        }
                        else
                            return;
                    } while (t <= m * alpha);
                    if (t > 0)
                        theSelectionDataTable.Rows.Add(eta);
                }
            else
                for (int i = 0; i < theSelectionSize; ++i)
                {
                    do
                    {
                        if (j != 10000)
                        {
                            alpha = random.NextDouble();
                            eta = Otbor.ETA(random.NextDouble() * (max - min) + min);
                            t = rcomp(eta);
                            if (t <= 0)
                                j++;
                            else
                                j = 0;
                        }
                        else
                            return;  
                    } while (t <= m * alpha);
                    if (t > 0)
                        theSelectionDataTable.Rows.Add(eta);
                }
        }


        //Рисование полос по оси X/Y
        private static void drawStringOX(Graphics g, string s, Font f, Brush b, float x, float y)
        {
            SizeF sizeString = g.MeasureString(s, f);
            g.DrawString(s, f, b, x - sizeString.Width / 2, y);
        }
        private static void drawStringOY(Graphics g, string s, Font f, Brush b, float x, float y)
        {
            SizeF sizeString = g.MeasureString(s, f);
            g.DrawString(s, f, b, x - sizeString.Width, y - sizeString.Width / 2);
        }

        //Рисование графика функции, гистограммы
        public static Bitmap plotHistogram(int width, int height, float min, float max, float GM)
        {
            if (IsFrequenciesNull)
                return null;

            Bitmap myPicture = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(myPicture);
            g.Clear(Color.White);
            float minX = min;
            float maxX = max;
            float minY = 0F;
            int Intervals = frequency.Length;
            int maxFrequency = 0;
            int n = 0; 
           
            foreach (int m in frequency)
            {
                if (m > maxFrequency)
                    maxFrequency = m;
                n += m;
            }
            if (maxFrequency == 0)
                return null;

            float xMin = 35F;
            float xMax = width - 20F;

            float yMin = 20F;
            float yMax = height - 35F;

            float xMid = (xMax + xMin) / 2;
            float yMid = (yMax + yMin) / 2;

            float h = Intervals / (maxX - minX) / (float)n;
            float maxY = Math.Max(0.3F, h * maxFrequency);

            float ax = (xMax - xMin) / (maxX - minX);
            float bx = (maxX * xMin - xMax * minX) / (maxX - minX);
            float ay = (yMin - yMax) / (maxY - minY);
            float by = (maxY * yMax - yMin * minY) / (maxY - minY);
            
            Func<float, float> changeX = varX => ax * varX + bx;
            Func<float, float> changeY = varY => ay * varY + by;

            float x = minX;
            float y = minY;
            float ix, iy;
            float ixPrev = changeX(x), iyPrev = changeY(y);
            float a=1,b=1;
            if (Math.Abs(max - min) > 10)
                a = (Math.Abs(max - min) / 10);
            if (Math.Abs(maxY - minY) > 3)
                b = (Math.Abs(maxY - minY) / 10);
              
            //Метки на осях(Полосы)
            float xDelta = 3;
            //X
            if (Math.Abs(max - min) > 10)
            {
                for (x = min; x <= maxX; x += a)
                {
                    if (x == 0)
                        continue;
                    ix = changeX(x);
                    if (x == Math.Floor(x))
                        g.DrawLine(Pens.Black, ix, yMax - xDelta, ix, yMax + xDelta);
                }
            }
            else
            {
                for (x = min; x <= maxX; x += 1F)
                {
                    if (x == 0)
                        continue;
                    ix = changeX(x);
                    if (x == Math.Floor(x))
                        g.DrawLine(Pens.Black, ix, yMax - xDelta, ix, yMax + xDelta);
                }
            }
            //Y
            if (Math.Abs(maxY - minY) > 3)
            {
                for (y = minY; y <= maxY; y += b)
                {
                    iy = changeY(y);
                    g.DrawLine(Pens.Black, xMin - xDelta, iy, xMin + xDelta, iy);
                }
            }
            else
            {
                for (y = minY; y <= maxY; y += 0.1F)
                {
                    iy = changeY(y);
                    g.DrawLine(Pens.Black, xMin - xDelta, iy, xMin + xDelta, iy);
                }
            }
            //Метки на осях(числа)
            Font picFont = new Font("Microsoft Sans Serif", 10);
            Font picFontLarge = new Font("Microsoft Sans Serif", 9);
            //X
            if (Math.Abs(max - min) > 10)
            {
                for (float i = min; i <= maxX; i += a)
                {
                    ix = changeX(i);
                    if (i == Math.Floor(i))
                        drawStringOX(g, i.ToString("F0"), picFont, Brushes.Black, ix, yMax + 5F);
                    else
                        drawStringOY(g, i.ToString("F1"), picFontLarge, Brushes.Black, ix, yMax);
                }
            }
            else
            {
                for (float i = min; i <= maxX; i += 1F)
                {
                    ix = changeX(i);
                    if (i == Math.Floor(i))
                        drawStringOX(g, i.ToString("F0"), picFont, Brushes.Black, ix, yMax + 5F);
                    else
                        drawStringOY(g, i.ToString("F1"), picFont, Brushes.Black, ix, yMax);
                }
            }
            //Y
            if (Math.Abs(maxY - minY) > 3)
            {
                for (float i = minY; i <= maxY + 0.001F; i += b)
                {
                    iy = changeY(i);
                    if (i == 0)
                    {
                        drawStringOY(g, i.ToString("F0"), picFontLarge, Brushes.Black, xMin - 4F, iy);
                        continue;
                    }
                    drawStringOY(g, i.ToString("F0"), picFontLarge, Brushes.Black, xMin - 5F, iy-1F);
                }
            }
            else
            {
                for (float i = minY; i <= maxY + 0.001F; i += 0.1F)
                {
                    iy = changeY(i);
                    if (i == 0)
                    {
                        drawStringOY(g, i.ToString("F0"), picFont, Brushes.Black, xMin - 3F, iy);
                        continue;
                    }
                    if (i == Math.Floor(i))
                        drawStringOY(g, i.ToString("F0"), picFont, Brushes.Black, xMin - 3F, iy + 7F);
                    else
                        drawStringOY(g, i.ToString("F2"), picFont, Brushes.Black, xMin - 3F, iy + 7F);
                }
            }
            //График P
            float delta = 2 * (maxX - minX) / (xMax - xMin);
            ixPrev = changeX(minX);
            iyPrev = yMid;
            for (x = minX; x <= maxX; x+= delta)
            {
                y = (float)Otbor.p(x);
                ix = changeX(x);
                iy = changeY(y);

                g.DrawLine(Pens.Red, ixPrev, iyPrev, ix, iy);

                ixPrev = ix;
                iyPrev = iy;
            }
            //График Q
            ixPrev = changeX(minX);
            iyPrev = yMid;
            for (x = minX; x <= maxX; x += delta)
            {
                y = (float)Otbor.q(x);
                ix = changeX(x);
                iy = changeY(y);

                g.DrawLine(Pens.Green, ixPrev, iyPrev, ix, iy);

                ixPrev = ix;
                iyPrev = iy;
            }
            //Прямоугольники
            delta = (xMax - xMin) / Intervals;   
            ix = xMin;
            for (int i = 0; i < Intervals; ++i)
            {
                y = frequency[i] * h;
                iy = changeY(y);
                g.DrawRectangle(Pens.Blue, ix, iy, delta, yMax - iy);
                ix += delta;
            }
            //Оси
            g.DrawLine(Pens.Black, xMin, yMax, xMax, yMax);//X
            g.DrawLine(Pens.Black, xMin, yMin, xMin, yMax);//Y
            return myPicture;
        }
    }
}
