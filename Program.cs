using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChisMet_lab1
{
    class Functions
    {
        public static void ReadMatrixFromFile(out double [,]arr, int size, string FileName)
        {
            arr = new double[size, size];

            int i = 0;
            StreamReader sr = new StreamReader(FileName);

            while (!sr.EndOfStream)
            {
                string []str = sr.ReadLine().Split(' ');

                for (int j = 0; j < size; j++)
                    if (!double.TryParse(str[j], out arr[i, j]))
                        throw new Exception();
                i++;
            }

            sr.Close();
        }

        public static void WriteMatrixToFile(double [,]arr, int size, string FileName)
        {
            StreamWriter sw = new StreamWriter(FileName);

            for (int i = 0; i < size; i++)
            {
                string str = "";

                for (int j = 0; j < size; j++)
                    str += arr[i, j] + " ";

                sw.WriteLine(str);
            }

            sw.Close();
        }

        public static double[,] MultMatrix(double [,]a1, double [,]a2, int size)
        {
            double[,] res = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double elem = 0;
                    for (int k = 0; k < size; k++)
                        elem += a1[i, k] * a2[k, j];

                    res[i, j] = elem;
                }
            }

            return res;
        }

        //public static 
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                double[,] arr1, arr2, res;
                Functions.ReadMatrixFromFile(out arr1, 3, "input.txt");
                Functions.ReadMatrixFromFile(out arr2, 3, "input.txt");
                res = Functions.MultMatrix(arr1, arr2, 3);
                Functions.WriteMatrixToFile(res, 3, "output.txt");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
