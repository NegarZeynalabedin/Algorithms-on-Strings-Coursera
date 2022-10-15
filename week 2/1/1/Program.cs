using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            List<string> matris = CreateMatrix(text);
            Sort(matris);
            string answer= ComputeLastColumn(matris, text.Length);
            Console.WriteLine(answer);
        }

        private static string ComputeLastColumn(List<string> matris, int length)
        {
            string BWT = "";

            foreach (var t in matris)
                BWT += t[(int)length - 1];

            return BWT;
        }

        private static void Sort(List<string> matrix)
        {
            matrix.Sort();
        }

        private static List<string> CreateMatrix(string text)
        {
            long length = text.Length;

            var matrix = new List<string>();

            for (int i = 0; i < length; i++)
            {
                string t = "";

                for (int j = i; j < length; j++)
                {

                    if (t.Length == length)
                        break;

                    t += text[j];

                    if (j + 1 == length)
                        j = -1;
                }

                matrix.Add(t);
            }

            return matrix;
        }
    }
}
