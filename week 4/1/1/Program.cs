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
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();

            List<long> pos = new List<long>();

            //KMP algorithm
            pos = FindAllOccurences(pattern, text);

            List<string> posStr = new List<string>();
            foreach (var a in pos)
                posStr.Add(a.ToString());
            string ans = String.Join(" ", posStr.ToArray());
            Console.WriteLine(ans);
        }

        private static List<long> FindAllOccurences(string pattern, string text)
        {
            string str = pattern + "$" + text;
            long pattLenght = pattern.Length;

            long[] prefix = ComputePrefixFunction(str);

            List<long> possition = new List<long>();

            for (int i = (int)pattLenght + 1; i < str.Length; i++)
                if (prefix[i] == pattLenght)
                    possition.Add(i - (2 * pattLenght));

            return possition;
        }

        private static long[] ComputePrefixFunction(string str)
        {
            long strLenght = str.Length;

            long[] result = new long[strLenght];
            result[0] = 0;

            long border = 0;

            for (int i = 1; i < strLenght; i++)
            {
                while (border > 0 && str[i] != str[(int)border])
                    border = result[(int)border - 1];

                if (str[i] == str[(int)border])
                    border += 1;
                else
                    border = 0;

                result[i] = border;
            }

            return result;
        }
    }
}
