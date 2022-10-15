using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4
{
    class Program
    {
        struct Tuple
        {
            public long x;
            public string y;
        }

        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            List<Tuple> matrix = CreateMatrix(text);

            matrix = matrix.OrderBy(d => d.y).ToList();

            List<long> answer = new List<long>(text.Length);

            foreach (var m in matrix)
                answer.Add(m.x);

            string[] ans = new string[answer.Count];
            for(int i=0;i<answer.Count; i++)
            {
                ans[i] = answer[i].ToString();
            }

            string a = String.Join(" ", ans);
            Console.WriteLine(a);
        }

        private static List<Tuple> CreateMatrix(string text)
        {
            long length = text.Length;

            var matrix = new List<Tuple>();

            for (int i = 0; i < length; i++)
            {
                string t = "";

                for (int j = i; j < length; j++)
                {

                    if (t.Length == length)
                        break;

                    t += text[j];
                }

                var tmp = new Tuple();
                tmp.x = (long)i;
                tmp.y = t;
                matrix.Add(tmp);
            }

            return matrix;
        }
    }
}
