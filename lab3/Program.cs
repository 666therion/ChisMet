using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChisMet_lab3
{
    class Program
    {
        /// <summary>
        /// Вывод информации. Заголовок
        /// </summary>
        public static void WriteInformationHeadIteration()
        {
            Console.WriteLine("{0,5}|{1,15}|{2,15}|{3,25}|{4,25}|{5,25}|{6,25}", "Itr", "x", "y", "Норма невязки", "F1", "F2", "Норма якобиана");
        }

        public static void WriteInformationHeadNewton()
        {
            Console.WriteLine("{0,5}|{1,15}|{2,15}|{3,25}|{4,25}|{5,25}", "Itr", "x", "y", "Норма невязки", "F1", "F2");
        }

        public static void WriteInformationHeadGradient()
        {
            Console.WriteLine("{0,5}|{1,15}|{2,15}|{3,15}|{4,25}|{5,25}|{6,25}|{7,25}|{8,5}", "Itr", "x", "y", "Alpha", "Норма невязки", "F1", "F2", "FF", "k");
        }

        /// <summary>
        /// Вывод информации об итерации
        /// </summary>
        public static void WriteInformationIteration(int k, double x, double y, double normR, double F1, double F2, double normYakobi)
        {
            Console.WriteLine("{0,5}|{1,15:0.0000001}|{2,15:0.0000001}|{3,25}|{4,25}|{5,25}|{6,25}", k, x, y, normR, F1, F2, normYakobi);
        }

        public static void WriteInformationNewton(int k, double x, double y, double normR, double F1, double F2)
        {
            Console.WriteLine("{0,5}|{1,15:0.0000001}|{2,15:0.0000001}|{3,25}|{4,25}|{5,25}", k, x, y, normR, F1, F2);
        }

        public static void WriteInformationGradient(int itr, double x, double y, double alpha, double normR, double F1, double F2, double FF, int k)
        {
            Console.WriteLine("{0,5}|{1,15:0.0000001}|{2,15:0.0000001}|{3,15:0.0000001}|{4,25}|{5,25}|{6,25}|{7,25}|{8,5}", itr, x, y, alpha, normR, F1, F2, FF, k);
        }

        /// <summary>
        /// Вывод матрицы
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="n"></param>
        /// <param name="rounded"></param>
        public static void WriteMatrix(double[,] matrix, int n, bool rounded = true)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (rounded)
                        Console.Write("{0,15:0.0000000}", matrix[i, j]);
                    else
                        Console.Write("{0,25}", matrix[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод вектора
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="n"></param>
        /// <param name="rounded"></param>
        public static void WriteVector(double[] vector, int n, bool rounded = true)
        {
            for (int i = 0; i < n; i++)
            {
                if (!rounded)
                    Console.Write("{0,15:0.0000000}", vector[i]);
                else
                    Console.Write("{0,25}", vector[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Копирование матрицы
        /// </summary>
        /// <param name="M"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[,] CopyMatrix(double[,] M, int n) // копирование матрицы
        {
            double[,] res = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = M[i, j];

            return res;
        }

        /// <summary>
        /// Умножение матрицы на вектор
        /// </summary>
        /// <param name="A"></param>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] MultMatrixVector(double[,] A, double[] x, int n)
        {
            double[] res = new double[n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i] += A[i, j] * x[j];
            return res;
        }

        public static double[] DiffVector(double[] A, double[] B, int n)
        {
            double[] C = new double[n];
            for (int i = 0; i < n; i++)
                C[i] = A[i] - B[i];

            return C;
        }

        public static double Function10_1(double x)
        {
            return Math.Sin(x + 2) - 1.5;
        } //10 вариант, 1 функция y = f(x)

        public static double Function10_2(double y)
        {
            return 0.5 - Math.Cos(y - 2);
        } //10 вариант, 2 функция x = f(y)

        public static double Function11_2(double y) //11 вариант, 2 функция x = f(y)
        {
            return Math.Sin(y + 1) - 1.2;
        }

        public static double Function11_1(double x) //11 вариант, 1 функция y = f(x)
        {
            return (2 - Math.Cos(x)) / 2;
        }

        public static double F10_1(double x, double y) //10 вариант, 1 функция F1(x,y)
        {
            return Math.Sin(x + 2) - y - 1.5;
        }

        public static double F10_2(double x, double y) //10 вариант, 2 функция F2(x,y)
        {
            return x + Math.Cos(y - 2) - 0.5;
        }

        public static double F11_1(double x, double y) //11 вариант, 1 функция F1(x,y)
        {
            return 2*y + Math.Cos(x) - 2;
        }

        public static double F11_2(double x, double y) //11 вариант, 2 функция F2(x,y)
        {
            return Math.Sin(y + 1) - x - 1.2;
        }

        public static double dFx10(double x, double y) //10 вариант, производная F(x,y) по x
        {
            return 2 * (Math.Sin(x + 2) - y - 1.5) * Math.Cos(x + 2) + 2 * (x + Math.Cos(y - 2) - 0.5);
        }

        public static double dFy10(double x, double y) //10 вариант, производная F(x,y) по y
        {
            return (-2) * (Math.Sin(x + 2) - y - 1.5) + 2 * (x + Math.Cos(y - 2) - 0.5) * (-Math.Sin(y - 2));
        }

        public static double dFx11(double x, double y) //11 вариант, производная F(x,y) по x
        {
            return 2 * ((-Math.Sin(y + 1) + 1.2 + x) + ((2 * y + Math.Cos(x) - 2) * (-Math.Sin(x))));
        }

        public static double dFy11(double x, double y) //11 вариант, производная F(x,y) по y
        {
            return 2 * ((Math.Sin(y + 1) - 1.2 - x) * Math.Cos(y + 1) + (4 * y + 2 * Math.Cos(x) - 4));
        }

        public static double FF10(double x, double y) //10 вариант F(x,y)
        {
            return Math.Pow((Math.Sin(x + 2) - y - 1.5), 2) + Math.Pow((x + Math.Cos(y - 2) - 0.5), 2);
        }

        public static double FF11(double x, double y) //11 вариант F(x,y)
        {
            return Math.Pow((Math.Sin(y + 1) - 1.2 - x), 2) + Math.Pow((2 * y + Math.Cos(x) - 2), 2);
        }

        public static double Diff10_1(double x, double eps) //10 вариант производная 1 функции y = f(x) по x
        {
            return (Function10_1(x + eps) - Function10_1(x)) / eps;
        }

        public static double Diff10_2(double y, double eps) //10 вариант производная 2 функции x = f(y) по y
        {
            return (Function10_2(y + eps) - Function10_2(y)) / eps;
        }

        public static double Diff11_1(double x, double eps) //11 вариант производная 1 функции y = f(x) по x
        {
            return (Function11_1(x + eps) - Function11_1(x)) / eps;
        }

        public static double Diff11_2(double y, double eps) //11 вариант производная 2 функции x = f(y) по y
        {
            return (Function11_2(y + eps) - Function11_2(y)) / eps;
        }

        public static double CountDiffX10_1(double x) //10 вариант вычисление производной 1 функции y = f(x)
        {
            return Math.Cos(x + 2);
        }

        public static double CountDiffY10_2(double y) //10 вариант вычисление производной 2 функции x = f(y)
        {
            return -Math.Sin(y - 2);
        }

        public static double CountDiffX11_1(double x) //11 вариант вычисление производной 1 функции y = f(x)
        {
            return -Math.Sin(x);
        }

        public static double CountDiffY11_2(double y) //11 вариант вычисление производной 2 функции x = f(y)
        {
            return Math.Cos(y + 1);
        }

        public static double[,] CountYakobi10(double x, double eps) //вычисление якобиана 10 вариант
        {
            double[,] Yakobi = new double[2, 2];

            Yakobi[0, 1] = Diff10_1(x, eps);
            Yakobi[1, 0] = Diff10_2(x, eps);

            return Yakobi;
        }

        public static double[,] CountYakobi11(double x, double eps) //вычисление якобиана 11 вариант
        {
            double[,] Yakobi = new double[2, 2];

            Yakobi[0, 1] = Diff11_1(x, eps);
            Yakobi[1, 0] = Diff11_2(x, eps);

            return Yakobi;
        }

        public static void SimpleIteration10(double eps) //Метод простой итерации 10 вариант
        {
            double PrevX = 0;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[2];
            double normR = 0;
            double F1 = 0;
            double F2 = 0;
            int k = 1;

            Console.WriteLine("Якобиан = ");
            WriteMatrix(CountYakobi10(0, eps), 2);
            Console.WriteLine("Norma = " + NormMatrix(CountYakobi10(0, eps), 2));

            WriteInformationHeadIteration();
            while (true)
            {
                CurrentY = Function10_1(PrevX);
                CurrentX = Function10_2(PrevY);

                double normYakobi = NormMatrix(CountYakobi10(CurrentX, eps), 2);
                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, 2);
                F1 = F10_1(CurrentX, CurrentY);
                F2 = F10_2(CurrentX, CurrentY);

                WriteInformationIteration(k, CurrentX, CurrentY, normR, F1, F2, normYakobi);

                if (normR < eps)
                    break;


                PrevX = CurrentX;
                PrevY = CurrentY;
                k++;
            }
        }

        public static void SimpleIteration11(double eps) //Метод простой итерации 11 вариант
        {
            double PrevX = 0;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[2];
            double normR = 0;
            double F1 = 0;
            double F2 = 0;
            int k = 1;

            Console.WriteLine("Якобиан = ");
            WriteMatrix(CountYakobi11(0, eps), 2);
            Console.WriteLine("Norma = " + NormMatrix(CountYakobi11(0, eps), 2));

            WriteInformationHeadIteration();
            while (true)
            {
                CurrentY = Function11_1(PrevX);
                CurrentX = Function11_2(PrevY);

                double normYakobi = NormMatrix(CountYakobi11(CurrentX, eps), 2);
                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, 2);
                F1 = F11_1(CurrentX, CurrentY);
                F2 = F11_2(CurrentX, CurrentY);

                WriteInformationIteration(k, CurrentX, CurrentY, normR, F1, F2, normYakobi);

                if (normR < eps)
                    break;


                PrevX = CurrentX;
                PrevY = CurrentY;
                k++;
            }
        }
        public static void Newton10(double eps) //Метод Ньютона 10 вариант
        {
            int n = 2;
            double PrevX = 0;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[n];
            double normR = 0;
            double[,] F = new double[n, n];
            double[,] XF = new double[n, n];
            double F1 = 0;
            double F2 = 0;
            int k = 1;

            WriteInformationHeadNewton();
            while (true)
            {
                F[0, 0] = CountDiffX10_1(PrevX);
                F[0, 1] = -1;
                F[1, 1] = CountDiffY10_2(PrevY);
                F[1, 0] = 1;
                int info = 0;
                alglib.matinvreport rep;
                XF = CopyMatrix(F, n);
                alglib.rmatrixinverse(ref XF, n, out info, out rep);
                double[] vector = new double[n];
                F1 = F10_1(PrevX, PrevY); //y
                F2 = F10_2(PrevX, PrevY); //x
                vector[0] = F1;
                vector[1] = F2;

                CurrentX = PrevX - MultMatrixVector(XF, vector, n)[0];
                CurrentY = PrevY - MultMatrixVector(XF, vector, n)[1];

                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, n);

                WriteInformationNewton(k, CurrentX, CurrentY, normR, F1, F2);

                if (normR < eps)
                    break;

                PrevX = CurrentX;
                PrevY = CurrentY;
                k++;
            }
        } 

        public static void Newton11(double eps) //Метод Ньютона 11 вариант
        {
            int n = 2;
            double PrevX = -2;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[n];
            double normR = 0;
            double[,] F = new double[n, n];
            double[,] XF = new double[n, n];
            double F1 = 0;
            double F2 = 0;
            int k = 1;

            WriteInformationHeadNewton();
            while (true)
            {
                F[0, 1] = CountDiffY11_2(PrevY);
                F[0, 0] = -1;
                F[1, 1] = 2;
                F[1, 0] = CountDiffX11_1(PrevX);
                int info = 0;
                alglib.matinvreport rep;
                XF = CopyMatrix(F, n);
                alglib.rmatrixinverse(ref XF, n, out info, out rep);
                double[] vector = new double[n];
                F1 = F11_1(PrevX, PrevY); //y
                F2 = F11_2(PrevX, PrevY); //x
                vector[1] = F1;
                vector[0] = F2;

                CurrentX = PrevX - MultMatrixVector(XF, vector, n)[0];
                CurrentY = PrevY - MultMatrixVector(XF, vector, n)[1];

                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, n);

                WriteInformationNewton(k, CurrentX, CurrentY, normR, F1, F2);

                if (normR < eps)
                    break;

                PrevX = CurrentX;
                PrevY = CurrentY;
                k++;
            }
        }

        public static void Gradient10(double eps) //Метод градиента 10 вариант 
        {
            int n = 2;
            double PrevX = 0;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[n];
            double normR = 0;
            double F1;
            double F2;
            double FF;
            int itr = 1;

            WriteInformationHeadGradient();
            while (true)
            {
                F1 = F10_1(PrevX, PrevY);
                F2 = F10_2(PrevX, PrevY);
                FF = FF10(PrevX, PrevY);

                double Alpha = 0.5;
                int k = 0;

                do
                {
                    CurrentX = PrevX - Alpha * dFx10(PrevX, PrevY);
                    CurrentY = PrevY - Alpha * dFy10(PrevX, PrevY);
                    Alpha /= 2;
                    k++;
                } while (FF10(CurrentX, CurrentY) >= FF10(PrevX, PrevY));

                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, n);

                WriteInformationGradient(itr, CurrentX, CurrentY, Alpha, normR, F1, F2, FF, k);

                if (normR < eps)
                    break;

                PrevX = CurrentX;
                PrevY = CurrentY;
                itr++;
            }
        }

        public static void Gradient11(double eps) //Метод градиента 11 вариант
        {
            int n = 2;
            double PrevX = -2;
            double CurrentX = 0;
            double PrevY = 0;
            double CurrentY = 0;
            double[] R = new double[n];
            double normR = 0;
            double F1;
            double F2;
            double FF;
            int itr = 1;

            WriteInformationHeadGradient();
            while (true)
            {
                F1 = F11_1(PrevX, PrevY);
                F2 = F11_2(PrevX, PrevY);
                FF = FF11(PrevX, PrevY);

                double Alpha = 0.5;
                int k = 0;

                do
                {
                    CurrentX = PrevX - Alpha * dFx11(PrevX, PrevY);
                    CurrentY = PrevY - Alpha * dFy11(PrevX, PrevY);
                    Alpha /= 2;
                    k++;
                } while (FF11(CurrentX, CurrentY) >= FF11(PrevX, PrevY));

                R[0] = Math.Abs(CurrentX - PrevX);
                R[1] = Math.Abs(CurrentY - PrevY);
                normR = NormVector(R, n);

                WriteInformationGradient(itr, CurrentX, CurrentY, Alpha, normR, F1, F2, FF, k);

                if (normR < eps)
                    break;

                PrevX = CurrentX;
                PrevY = CurrentY;
                itr++;
            }
        }

        /// <summary>
        /// Норма матрицы кубическая
        /// </summary>
        /// <param name="A"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double NormMatrix(double[,] A, int n) //норма кубическая
        {
            double norm = double.MinValue;

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < n; j++)
                    sum += Math.Abs(A[i, j]);

                norm = Math.Max(norm, sum);
            }

            return norm;
        }

        /// <summary>
        /// Норма вектора кубическая
        /// </summary>
        /// <param name="A"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double NormVector(double[] A, int n)
        {
            double res = Math.Abs(A[0]);

            for (int i = 1; i < n; i++)
                res = Math.Max(res, Math.Abs(A[i]));

            return res;
        }

        static void Main(string[] args)
        {
            double eps = 0.0001;
            Console.WriteLine("Вариант 10");
            Console.WriteLine("Метод простых итераций");
            SimpleIteration10(eps);
            Console.WriteLine();
            Console.WriteLine("Метод Ньютона");
            Newton10(eps);
            Console.WriteLine();
            Console.WriteLine("Метод градиентного спуска");
            Gradient10(eps);
            Console.WriteLine();
            Console.WriteLine("Вариант 11");
            Console.WriteLine("Метод простых итераций");
            SimpleIteration11(eps);
            Console.WriteLine();
            Console.WriteLine("Метод Ньютона");
            Newton11(eps);
            Console.WriteLine();
            Console.WriteLine("Метод градиентного спуска");
            Gradient11(eps);
            Console.ReadKey();
        }
    }
}
