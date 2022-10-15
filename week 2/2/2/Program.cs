using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2
{
    class Program
    {
        struct Tuple: IEquatable<Tuple>
        {
            public char x;
            public long y;

            public bool Equals(Tuple other)
            {
                return other.x == this.x && other.y == this.y;
            }

            public override int GetHashCode()
            {
                return x.GetHashCode() ^ y.GetHashCode();
            }

        }

        static void Main1(string[] args)
        {
            Tuple t = new Tuple();
            t.x = 'A';
            t.y = 5;
            Console.WriteLine(t.GetHashCode());
            Tuple w = new Tuple();
            w.x = 'B';
            w.y = 5;
            Console.WriteLine(t.Equals(w));
            Console.WriteLine(w.GetHashCode());
            t.x = 'B';
            t.y = 5;
            Console.WriteLine(t.GetHashCode());
            t.x = 'B';
            t.y = 9;
            Console.WriteLine(t.GetHashCode());
        }

        static void Main(string[] args)
        {
            string bwt;
            if (args.Length > 0)
            {
                Console.WriteLine(args[0]);
                bwt = File.ReadAllText(args[0]);
            }
            else
                bwt = Console.ReadLine();

            string bwtSort = SortedStr(bwt);

            long lenght = bwt.Length;

            Dictionary<Tuple, Tuple> firstAndLastColumn = new Dictionary<Tuple, Tuple>();

            Tuple[] bwtSortNum = Numbering(bwtSort);
            Tuple[] bwtNum = Numbering(bwt);

            for (int i = 0; i < lenght; i++)
                firstAndLastColumn.Add(bwtSortNum[i], bwtNum[i]);

            string answer=ComputeAnswer1(firstAndLastColumn, bwt.Length);
            Console.WriteLine(answer);
        }

        private static string ComputeAnswer1(Dictionary<Tuple, Tuple> firstAndLastColumn, int length)
        {
            char[] answerArray = new char[length];
            answerArray[length - 1] = '$';
            length -= 1;

            var tmp = new Tuple();
            tmp.x = '$';
            tmp.y = 1;
            var selector = tmp;

            do
            {
                answerArray[length] = firstAndLastColumn[selector].x;
                selector = firstAndLastColumn[selector];
                length -= 1;

            } while (selector.x != '$');

            length = answerArray.Length;

            //for (int i = 0; i < length; i++)
            //{
            //    if (i != 0)
            //        answer += answerArray[i];
            //}

            //answer += '$';

            return new string(answerArray, 1, answerArray.Length-1) + '$';
        }

        private static Tuple[] Numbering(string bwtSort)
        {
            var numbering = new Tuple[bwtSort.Length];

            long countA = 1;
            long countC = 1;
            long countT = 1;
            long countG = 1;

            for (int i = 0; i < bwtSort.Length; i++)
            {
                if (bwtSort[i] == 'A')
                {
                    numbering[i].x = 'A';
                    numbering[i].y=countA;
                    countA += 1;
                    continue;
                }

                if (bwtSort[i] == 'C')
                {
                    numbering[i].x = 'C';
                    numbering[i].y = countC;
                    countC += 1;
                    continue;
                }

                if (bwtSort[i] == 'T')
                {
                    numbering[i].x = 'T';
                    numbering[i].y = countT;
                    countT += 1;
                    continue;
                }

                if (bwtSort[i] == 'G')
                {
                    numbering[i].x = 'G';
                    numbering[i].y = countG;
                    countG += 1;
                    continue;
                }

                else
                {
                    numbering[i].x = '$';
                    numbering[i].y = (long)1;
                }
                    
            }

            return numbering;
        }

        private static string SortedStr(string bwt)
        {
            char[] bwtCharArray = bwt.ToCharArray();
            Array.Sort(bwtCharArray);
            return new string(bwtCharArray);
        }
    }
}
