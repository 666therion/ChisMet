using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2_Чисмет
{
    class Program
    {
        //Вывод матриsцы
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

        public static void WriteVector(double[] vector, int n, bool rounded = true)// вывод вестора на коноль
        {
            //StreamWriter Fw = new StreamWriter("Output10.txt");
            for (int i = 0; i < n; i++)
            {
                if (rounded)
                    Console.Write("{0,15:0.0000000}", vector[i]);
                else
                    Console.Write("{0,25}", vector[i]);
            }
            Console.WriteLine();
            //Fw.Close();
        }

        public static void ReadMatrix(double[,] matrix, int n) // чтение матрицы из файла
        {
            string s;
            int j = 0;

            StreamReader Fr = null;//??
            try
            {
                Fr = new StreamReader("Inputtest.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }

            //записываем матрицу в двумерный массив
            while ((s = Fr.ReadLine()) != null)
            {
                for (int i = 0; i < n; i++)
                    matrix[j, i] = double.Parse(s.Split(' ')[i]);
                j++;
            }

            Fr.Close();
        }

        public static double[,] CopyMatrix(double[,] M, int n)
        {
            double[,] res = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = M[i, j];

            return res;
        }

        public static void LUDifferential(double[,] A, ref double[,] X, ref double[,] U, ref double[,] L, ref double[,] P, out int sign, int n)
        {
            int[] Permutations = new int[n];

            sign = 1;
            U = CopyMatrix(A, n);
            bool[] swap = new bool[n];

            for (int i = 0; i < n; i++)
            {
                double tekmax = U[i, i];// текущий максимальный в столбце
                int MaxIndex = i; // индекс текущего максимального в столбце

                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(U[j, i]) > tekmax)
                    {
                        tekmax = Math.Abs(U[j, i]);
                        MaxIndex = j;
                    }
                }

                if (MaxIndex != i)
                {
                    P[MaxIndex, i] = 1;
                    P[i, MaxIndex] = 1;
                    Permutations[i] = MaxIndex;
                    Permutations[MaxIndex] = i;
                    swap[i] = true;
                    swap[MaxIndex] = true;
                    sign *= -1;
                }
                else if (!swap[i])
                {
                    Permutations[i] = i;
                    P[i, i] = 1;
                }

                double temp; // вспомогательная переменная для перестановки строк
                Console.WriteLine("m = " + (MaxIndex + 1) + " k = " + (i + 1));
                if (MaxIndex != i)//перестановка строк матрицы
                {
                    for (int k = 0; k < n; k++)
                    {
                        temp = U[MaxIndex, k];
                        U[MaxIndex, k] = U[i, k];
                        U[i, k] = temp;
                    }
                }
                L[i, i] = U[i, i];
                for (int l = 0; l < i; l++)
                {
                    L[i, l] = U[i, l];
                    U[i, l] = 0;
                }
                TriangleMatrix(ref U, n, i); // вызываем метод в котором строка нормализуется и происходит вычетание этой строки,умноженной на коэффициент, из других 
                //U[i, i] = L[i, i];
                Console.WriteLine("L: ");
                WriteMatrix(L, n);


            }

            double[,] M = CopyMatrix(A, n);
            X = new double[n, n];
            double[,] E = new double[n, n];
            double[] Y = new double[n];

            for (int i = 0; i < n; i++)
            {
                E[i, i] = 1;

                //Решение Ly = b
                for (int k = 0; k < n; k++)
                {
                    double t = 0;

                    for (int l = 0; l < k; l++)
                        t += L[k, l] * Y[l];
                    //Обращаемся к матрице E в  соответствии с перестановкой
                    Y[k] = (E[i, Permutations[k]] - t) / L[k, k];
                }

                //Решение Ux = y
                for (int k = n - 1; k >= 0; k--)
                    for (int l = k + 1; l < n; l++)
                        Y[k] -= U[k, l] * Y[l];

                for (int j = 0; j < n; j++)
                    E[i, j] = Y[j];
            }

            //инвертируем
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    X[i,j] = E[j, i];
        }// поиск максимального в столбце и перестановка строк

        public static void TriangleMatrix(ref double[,] U, int n, int i)
        {
            double temp = U[i, i];

            if (temp != 0)
            {
                for (int j = i; j < n; j++)
                    U[i, j] /= temp;

                for (int j = i + 1; j < n; j++)
                {
                    temp = U[j, i];
                    for (int k = i + 1; k < n; k++)
                        U[j, k] -= temp * U[i, k];
                }
                Console.WriteLine("U: ");
                WriteMatrix(U, n);
            }
        } // нормализыция и преведение к верхнеугольному виду

        public static double[,] MultMatrix(double[,] A, double[,] B, int n)
        {
            double[,] AB = new double[n, n];

            for (int t = 0; t < n; t++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        AB[t, i] += A[t, j] * B[j, i];

            return AB;
        } // L*U

        public static double[,] CheckLUDiff(double[,] L, double[,] U, double[,]A, double[,] P, int n)
        {
            double[,] PA = MultMatrix(P, A, n);
            double[,] LU = MultMatrix(L, U, n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    PA[i, j] -= LU[i, j];

            return PA;
        }

        public static double[] MultMatrixVector(double[,] A, double[] x, int n)
        {
            double[] res = new double[n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i] += A[i, j] * x[j];

            return res;
        }

        public static double TriangleDeteriminant(double[,] U, int sign, int n) // определитель матрицы 
        {
            double det = 1;

            for (int i = 0; i < n; i++)
                det *= U[i, i];

            return sign*det;
        }

        public static double norm1(double[,] A, int n)
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

        public static double norm2(double[,] A, int n)
        {
            double norm = double.MinValue;

            for (int j = 0; j < n; j++)
            {
                double sum = 0;

                for (int i = 0; i < n; i++)
                    sum += Math.Abs(A[i, j]);

                norm = Math.Max(norm, sum);
            }

            return norm;
        }

        public static double norm3(double[,] A, int n)
        {
            double maxlambda = double.MinValue;

            double[] lambda;
            double[,] z;
           
            alglib.smatrixevd(MultMatrix(RevertMatrix(A, n), A, n), n, 0, false, out lambda, out z);

            for (int i = 0; i < n; i++)
                maxlambda = Math.Max(maxlambda, Math.Abs(lambda[i]));

            return Math.Sqrt(maxlambda);
        }

        public static double[,] RevertMatrix(double[,] A, int n)
        {
            double[,] res = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = A[j, i];

            return res;
        }

        public static double[] Solve(double [,] A, double[] x, double[] b, int n)
        {
            double[] res = MultMatrixVector(A, x, n);

            for (int i = 0; i < n; i++)
                res[i] -= b[i];

            return res;
        }

        static void Main(string[] args)
        {
            int n = 4;
            int sign;
            double[] x = { 1, 2, 3 ,4};
            double[] b = new double[n];
            double[,] A = new double[n, n]; // матрица на входе
            double[,] InvA = new double[n, n]; // матрица на входе
            double[,] U = new double[n, n]; // матрица U
            double[,] L = new double[n, n]; // матрица L
            double[,] R = new double[n, n]; // матрица L*U для проверки
            double[,] P = new double[n, n];
            double[,] X = new double[n, n];

            ReadMatrix(A, n); // считываем матрицу A из файла
             
            Console.WriteLine("x:");// выведем вектор x
            WriteVector(x, n);

            Console.WriteLine("A:"); // выведем матрицу А
            WriteMatrix(A, n);

            b = MultMatrixVector(A, x, n);
            Console.WriteLine("b: ");
            WriteVector(b, n);

            LUDifferential(A, ref X, ref U, ref L, ref P, out sign, n); //

            WriteVector(MultMatrixVector(P, x, n), n);
            Console.WriteLine();
            Console.WriteLine("L*U - P*A");
            WriteMatrix(CheckLUDiff(L, U, A, P, n), n, false);

            Console.WriteLine("Determinant: " + TriangleDeteriminant(L, sign, n));
            Console.WriteLine();

            Console.WriteLine("x:");// выведем вектор x
            WriteVector(x, n);

            Console.WriteLine("A:"); // выведем матрицу А
            WriteMatrix(A, n);

            Console.WriteLine("A^-1:"); // выведем матрицу А^-1
            WriteMatrix(X, n);

            Console.WriteLine("A*A^-1:");
            WriteMatrix(MultMatrix(A, X, n), n, false);

            Console.WriteLine("Норма 1: " + norm1(A, n) * norm1(X, n));
            Console.WriteLine("Норма 2: " + norm2(A, n) * norm2(X, n));
            Console.WriteLine("Норма 3: " + norm3(A, n) * norm3(X, n));

            Console.WriteLine("A*x-b:");
            WriteVector(Solve(A, x, b, n), n, false);

            Console.ReadKey();
        }
    }
}
