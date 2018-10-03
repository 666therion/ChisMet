using System;
using System.IO;
using System.Threading.Tasks;


namespace Lab3_Чисмет
{
    class Program
    {
        /// <summary>
        /// Вывод информации. Заголовок
        /// </summary>
        public static void WriteInformationHead()
        {
            Console.WriteLine("{0,15}|{1,15}|{2,15}|{3,15}|{4,18}|{5,15}|{6,15}|{7,15}|{8,15}", "Itr", "tau", "q", "Норма невязки", "Оценка погрешности", "x[1]", "x[2]", "x[3]", "x[4]");
        }

        /// <summary>
        /// Вывод информации об итерации
        /// </summary>
        public static void WriteInformation(int k, double tau, double q, double norm, double delta, double[] x, bool last = false)
        {
            if (k < 50 || k % 100 == 0 || last)
                Console.WriteLine("{0,15}|{1,15:0.00000}|{2,15:0.00000}|{3,15:0.00000}|{4,18:0.00000}|{5,15:0.00000}|{6,15:0.00000}|{7,15:0.00000}|{8,15:0.00000}", k, tau, q, norm, delta, x[0], x[1], x[2], x[3]);
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
        ///  Чтение матрицы из файла
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="n"></param>
        public static void ReadMatrix(double[,] matrix, int n, string FileName)
        {
            string s;
            int j = 0;
            StreamReader Fr = null;
            try
            {
                Fr = new StreamReader(FileName);
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

        /// <summary>
        /// Умножение вектора на вектор
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MultVectors(double[] a, double[] b, int n)
        {
            double res = 0;

            for (int i = 0; i < n; i++)
                res += a[i] * b[i];

            return res;
        }

        /// <summary>
        /// Умножение вектора на число
        /// </summary>
        /// <param name="x"></param>
        /// <param name="vector"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] MultDigitVector(double x, double[] vector, int n)
        {
            double[] res = new double[n];

            for (int i = 0; i < n; i++)
                res[i] = x * vector[i];

            return res;
        }

        /// <summary>
        /// Проверка диагональных элементов на наличие нуля
        /// </summary>
        /// <param name="A"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MainDiagonalElements(double[,] A, int n)
        {
            double res = 1;
            for (int i = 0; i < n; i++)
                res *= A[i, i];
            return res;
        }

        /// <summary>
        /// Сложение векторов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] SumVector(double[] A, double[] B, int n)
        {
            double[] C = new double[n];
            for (int i = 0; i < n; i++)
                C[i] = A[i] + B[i];

            return C;
        }

        public static double[] DiffVector(double[] A, double[] B, int n)
        {
            double[] C = new double[n];
            for (int i = 0; i < n; i++)
                C[i] = A[i] - B[i];

            return C;
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
        /// Вектор невязки
        /// </summary>
        /// <param name="A"></param>
        /// <param name="PreviousValues"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] VectorR(double[,] A, double[] PreviousValues, double[] b, int n)
        {
            double[] r = MultMatrixVector(A, PreviousValues, n);

            for (int i = 0; i < n; i++)
                r[i] = b[i] - r[i];

            return r;
        }

        /// <summary>
        /// Метод смежных градиентов.Альфа 
        /// </summary>
        /// <param name="PreviousR"></param>
        /// <param name="PreviousPreviousR"></param>
        /// <param name="T"></param>
        /// <param name="PreviousT"></param>
        /// <param name="PreviousAlpha"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double GradientAlpha(double[] PreviousR, double[] PreviousPreviousR, double T,
            double PreviousT, double PreviousAlpha, int n)
        {
            return 1 / (1 - (T * MultVectors(PreviousR, PreviousR, n)) /
                (PreviousT * PreviousAlpha * MultVectors(PreviousPreviousR, PreviousPreviousR, n)));
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

        /// <summary>
        /// Оценка нормы матрицы перехода
        /// </summary>
        /// <param name="v"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double AssessmentOfNorms(double[] CurrentValues, double[] PreviousValues, double[] PrePreviousValues, int n)
        {
            double[] A = new double[n];
            double[] B = new double[n];

            for (int i = 0; i < n; i++)
            {
                A[i] = Math.Abs(CurrentValues[i] - PreviousValues[i]);
                B[i] = Math.Abs(PreviousValues[i] - PrePreviousValues[i]);
            }

            double n1 = NormVector(A, n);
            double n2 = NormVector(B, n);

            if (n2 == 0)
                n2 = 1;

            return n1 / n2;
        }

        /// <summary>
        /// Копирование вектора
        /// </summary>
        /// <param name="M"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] CopyVector(double[] v, int n)
        {
            double[] res = new double[n];
            for (int i = 0; i < n; i++)
                res[i] = v[i];
            return res;
        }

        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[,] MultMatrix(double[,] A, double[,] B, int n)
        {
            double[,] AB = new double[n, n];
            for (int t = 0; t < n; t++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        AB[t, i] += A[t, j] * B[j, i];
            return AB;
        }

        /// <summary>
        /// Транспонированная матрица
        /// </summary>
        /// <param name="A"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[,] RevertMatrix(double[,] A, int n)
        {
            double[,] res = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = A[j, i];
            return res;
        }

        /// <summary>
        /// Расчетная формула. Метод простых итераций
        /// </summary>
        /// <param name="args"></param>
        /// 
        public static double[] SimpleIteration(double[,] A, double[] b, double[] PreviousValues, int n)
        {
            double y = 0.9;
            double[] CurrentValues = new double[n];
            if (MainDiagonalElements(A, n) != 0)
            {
                double sum = 0;

                for (int i = 0; i < n; i++)
                {
                    sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += A[i, j] * PreviousValues[j];
                    }
                    CurrentValues[i] = PreviousValues[i] + (y * 2) / NormMatrix(A, n) * (b[i] - sum);
                }
            }
            return CurrentValues;
        }

        /// <summary>
        /// Метод простых итераций
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] MetodOfSimpleIteration(double[,] A, double[] b, int n, double eps)
        {
            double y = 0.9;
            double q;
            double tau;
            double normR;
            double[] PrePreviousValues = new double[n];
            double[] PreviousValues = new double[n];
            double[] CurrentValues = new double[n];
            int k = 1;

            WriteInformationHead();

            while (true)
            {
                // значения неизвестных на текущей итерации
                CurrentValues = SimpleIteration(A, b, PreviousValues, n);

                // текущая погрешность относительно предыдущей итерации
                q = AssessmentOfNorms(CurrentValues, PreviousValues, PrePreviousValues, n);
                normR = NormVector(VectorR(A, PreviousValues, b, n), n)/NormVector(CurrentValues, n);
                tau = (y * 2) / NormMatrix(A, n);

                WriteInformation(k, tau, q, normR, 0, PreviousValues);

                if (normR < eps)
                    break;

                //Переходим к следующей итерации. Предыдущие значения неизвестных становятся значениями на предпредыдущей итерации
                PrePreviousValues = CopyVector(PreviousValues, n);
                //Текущие значения неизвестных становятся значениями на предыдущей итерации
                PreviousValues = CopyVector(CurrentValues, n);

                k++;
            }
            WriteInformation(k, tau, q, normR, 0, PreviousValues, true);
            Console.WriteLine();
            Console.WriteLine("Количество итераций = " + k);
            Console.WriteLine();
            return PreviousValues;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="r"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double Tau(double[,] A, double[] r, int n)
        {
            return MultVectors(r, r, n) / MultVectors(MultMatrixVector(A, r, n), r, n);
        }

        /// <summary>
        /// Расчетная формула. Метод смежных градиентов
        /// </summary>
        /// <param name="args"></param>
        /// 
        public static double[] Gradient(double[,] A, double[] PreviousValues, double[] PreviousPreviousValues,
            double Alpha, double[] b, int n)
        {
            double[] PreviousR = VectorR(A, PreviousPreviousValues, b, n);

            return
                SumVector(
                    SumVector(
                        MultDigitVector(Alpha, PreviousValues, n),
                        MultDigitVector((1 - Alpha), PreviousPreviousValues, n), n),
                    MultDigitVector(
                        Tau(A, VectorR(A, PreviousValues, b, n), n) * Alpha,
                        VectorR(A, PreviousValues, b, n), n), n);
        }

        /// <summary>
        /// Метод сопряженных градиентов
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <param name="eps"></param>
        /// <returns></returns>
        public static double[] MetodGradient(double[,] A, double[] b, int n, double eps)
        {
            double[] PrePreviousValues = new double[n];
            double[] PreviousValues = new double[n];
            double[] CurrentValues = new double[n];
            int k = 1;
            double Alpha = 1;
            double q;
            double normR;
            double tau;

            WriteInformationHead();

            while (true)
            {
                // значения неизвестных на текущей итерации
                CurrentValues = Gradient(A, PreviousValues, PrePreviousValues, Alpha, b, n);

                // текущая погрешность относительно предыдущей итерации
                q = AssessmentOfNorms(CurrentValues, PreviousValues, PrePreviousValues, n);
                normR = NormVector(VectorR(A, PreviousValues, b, n), n) / NormVector(CurrentValues, n);
                tau = Tau(A, VectorR(A, PreviousValues, b, n), n);

                if (normR < eps)
                    break;

                WriteInformation(k, tau, q, normR, 0, PreviousValues);

                //Переходим к следующей итерации. Предыдущие значения неизвестных становятся значениями на предпредыдущей итерации
                PrePreviousValues = CopyVector(PreviousValues, n);
                //Текущие значения неизвестных становятся значениями на предыдущей итерации
                PreviousValues = CopyVector(CurrentValues, n);

                double[] R = VectorR(A, PreviousValues, b, n);
                double[] PreviousR = VectorR(A, PrePreviousValues, b, n);

                Alpha = GradientAlpha(
                    R,
                    PreviousR,
                    Tau(A, R, n),
                    Tau(A, PreviousR, n),
                    Alpha,
                    n);

                k++;
            }
            WriteInformation(k, tau, q, normR, 0, PreviousValues, true);
            Console.WriteLine();
            Console.WriteLine("Количество итераций = " + k);
            Console.WriteLine();
            return PreviousValues;
        }

        /// <summary>
        /// Метод наискорейшего спуска.Альфа 
        /// </summary>
        /// <param name="PreviousR"></param>
        /// <param name="PreviousPreviousR"></param>
        /// <param name="T"></param>
        /// <param name="PreviousT"></param>
        /// <param name="PreviousAlpha"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double SteepestDescentTau(double[,] A, double[] PreviousR, int n)
        {
            return MultVectors(PreviousR, PreviousR, n) / MultVectors(MultMatrixVector(A, PreviousR, n), PreviousR, n);
        }

        /// <summary>
        /// Расчетная формула. Метод наискорейшего спуска
        /// </summary>
        /// <param name="args"></param>
        /// 
        public static double[] SteepestDescent(double[,] A, double[] CurrentValues, double[] PreviousValues, double[] b, int n)
        {
            double[] PreviousR = VectorR(A, CurrentValues, b, n);
            return SumVector(PreviousValues, MultDigitVector(SteepestDescentTau(A, PreviousR, n), PreviousR, n), n);
        }

        /// <summary>
        /// Метод наискорейшего спуска
        /// </summary>
        /// <param name="args"></param>
        public static double[] MetodSteepestDescent(double[,] A, double[] b, int n, double eps)
        {
            double[] PrePreviousValues = new double[n];
            double[] PreviousValues = new double[n];
            double[] CurrentValues = new double[n];
            double q;
            double normR;
            double tau;
            int k = 1;

            WriteInformationHead();

            while (true)
            {
                // значения неизвестных на текущей итерации
                CurrentValues = SteepestDescent(A, CurrentValues, PreviousValues, b, n);

                // текущая погрешность относительно предыдущей итерации
                q = AssessmentOfNorms(CurrentValues, PreviousValues, PrePreviousValues, n);
                normR = NormVector(VectorR(A, PreviousValues, b, n), n) / NormVector(CurrentValues, n);
                tau = Tau(A, VectorR(A, PreviousValues, b, n), n);

                if (normR < eps)
                    break;

                WriteInformation(k, tau, q, 0, normR, PreviousValues);

                //Переходим к следующей итерации. Предыдущие значения неизвестных становятся значениями на предпредыдущей итерации
                PrePreviousValues = CopyVector(PreviousValues, n);
                //Текущие значения неизвестных становятся значениями на предыдущей итерации
                PreviousValues = CopyVector(CurrentValues, n);

                double[] R = VectorR(A, PreviousValues, b, n);
                double[] PreviousR = VectorR(A, PrePreviousValues, b, n);

                k++;
            }
            WriteInformation(k, tau, q, normR, 0, PreviousValues, true);
            Console.WriteLine();
            Console.WriteLine("Количество итераций = " + k);
            Console.WriteLine();
            return PreviousValues;
        }

        public static double[] PVRW(double[,] A, double[] b, int n, double eps)
        {
            double min_itr = double.MaxValue;
            double target_w = 0;
            int k;
            for (double w = 0.10; w < 2; w += 0.10)
            {
                k = 1;
                MetodPVR(A, b, w, n, 0.01, out k);
                Console.WriteLine("{0,10}{1,10}", "w = " + w, "  itr = " + k);
                if (min_itr > k)
                {
                    min_itr = k;
                    target_w = w;
                }
            }
            k = 1;
            Console.WriteLine();
            Console.WriteLine("w = " + target_w + "  min_itr = " + min_itr);
            Console.WriteLine();
            return MetodPVR(A, b, target_w, n, eps, out k, true);
        }

        /// <summary>
        /// Расчетная формула.Метод ПВР
        /// </summary>
        /// <param name="A"></param>
        /// <param name="PreviousValues"></param>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static double[] PVR(double[,] A, double[] PreviousValues, double[] b, int n, double w)
        {
            double[] CurrentValues = new double[n];
            double sum1, sum2;
            for (int i = 0; i < n; i++)
            {
                sum1 = 0;
                sum2 = 0;
                for (int j = 0; j < i; j++)
                {
                    sum1 += A[i, j] * CurrentValues[j];
                }
                for (int j = i + 1; j < n; j++)
                {
                    sum2 += A[i, j] * PreviousValues[j];
                }
                CurrentValues[i] = PreviousValues[i] + w * (1 / A[i, i] * (b[i] - sum1 - sum2) - PreviousValues[i]);
            }
            return CurrentValues;
        }

        /// <summary>
        /// Метод ПВР
        /// </summary>
        /// <param name="args"></param>
        public static double[] MetodPVR(double[,] A, double[] b, double w, int n, double eps, out int k, bool WriteRes = false)
        {
            double[] PrePreviousValues = new double[n];
            double[] PreviousValues = new double[n];
            double[] CurrentValues = new double[n];
            double q;
            double normR;
            k = 1;

            if (WriteRes)
                WriteInformationHead();

            while (true)
            {
                // значения неизвестных на текущей итерации
                CurrentValues = PVR(A, PreviousValues, b, n, w);

                // текущая погрешность относительно предыдущей итерации
                q = AssessmentOfNorms(CurrentValues, PreviousValues, PrePreviousValues, n);
                normR = NormVector(VectorR(A, PreviousValues, b, n), n) / NormVector(CurrentValues, n);

                if (normR < eps)
                    break;

                if (WriteRes)
                    WriteInformation(k, w, q, normR, 0, PreviousValues);

                //Переходим к следующей итерации. Предыдущие значения неизвестных становятся значениями на предпредыдущей итерации
                PrePreviousValues = CopyVector(PreviousValues, n);
                //Текущие значения неизвестных становятся значениями на предыдущей итерации
                PreviousValues = CopyVector(CurrentValues, n);

                double[] R = VectorR(A, PreviousValues, b, n);
                double[] PreviousR = VectorR(A, PrePreviousValues, b, n);

                k++;
            }
            if (WriteRes)
            {
                WriteInformation(k, w, q, normR, 0, PreviousValues, true);
                Console.WriteLine();
                Console.WriteLine("Количество итераций = " + k);
                Console.WriteLine();
            }
            return PreviousValues;
        }

        //public static double Cond(double[,] A, double[] x, int n)
        //{

        //}

        static void Main(string[] args)
        {
            double eps = 0.0001;
            int n = 4;//размерность матрицы
            double[] b = { 1, 2, 3, 4 }; //new double[n]; // вектор b
            double[,] A = new double[n, n];//матрица А
            double[] SI = new double[n]; // вектор x
            double[] G = new double[n]; // вектор x
            double[] SD = new double[n]; // вектор x
            double[] PVR = new double[n]; // вектор x

            ReadMatrix(A, n, "Input11.txt");// считываем матрицу A из файл

            Console.WriteLine("eps:" + eps);// выведем e

            Console.WriteLine("A: ");// выведем матрицу А
            WriteMatrix(A, n);


            //b = MultMatrixVector(A, x, n); //найдем решение уравнения
            Console.WriteLine("b: ");// выведем вектор b
            WriteVector(b, n);

            Console.WriteLine("Норма матрицы = " + NormMatrix(A, n));
            Console.WriteLine();

            Console.WriteLine("Метод простых итераций: ");
            SI = MetodOfSimpleIteration(A, b, n, eps);
            Console.WriteLine("x: ");// выведем вектор X
            WriteVector(SI, n);

            Console.WriteLine("Метод наискорейшего спуска: ");
            SD = MetodSteepestDescent(A, b, n, eps);
            Console.WriteLine("x: ");// выведем вектор X
            WriteVector(SD, n);

            Console.WriteLine("Метод ПВР: ");
            PVR = PVRW(A, b, n, eps);
            Console.WriteLine("x: ");// выведем вектор X
            WriteVector(PVR, n);

            Console.WriteLine("Метод сопряженных градиентов: ");
            G = MetodGradient(A, b, n, eps);
            Console.WriteLine("x: ");// выведем вектор X
            WriteVector(G, n);

            Console.ReadKey();
        }
    }
}