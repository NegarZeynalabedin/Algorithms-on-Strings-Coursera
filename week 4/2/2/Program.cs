using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            long[] suffixArray = BuildSuffixArray(text);

            List<string> suffixStr = new List<string>();
            foreach (var a in suffixArray)
            {
                suffixStr.Add(a.ToString());
            }
            var result = String.Join(" ", suffixStr.ToArray());
            Console.WriteLine(result);
        }

        private static long[] BuildSuffixArray(string text)
        {
            long[] order = SortCharacters(text);
            long[] classes = ComputeCharClasses(text, order);

            long lenght = 1;
            long textLenght = text.Length;

            while (lenght < textLenght)
            {
                order = SortDoubled(text, lenght, order, classes);
                classes = UpdateClasses(order, classes, lenght);
                lenght *= 2;
            }

            return order;
        }

        private static long[] UpdateClasses(long[] newOrder, long[] classes, long lenght)
        {
            long n = newOrder.Length;
            long[] newClass = new long[n];
            newClass[newOrder[0]] = 0;

            for (int i = 1; i < n; i++)
            {
                long cur = newOrder[i];
                long prev = newOrder[i - 1];

                var mid = cur + lenght;
                var midPrev = (prev + lenght) % n;

                if (classes[cur] != classes[prev] || classes[mid] != classes[midPrev])
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }

            return newClass;
        }

        private static long[] SortDoubled(string text, long lenght, long[] order, long[] classes)
        {
            long textLenght = text.Length;
            long[] count = new long[textLenght];

            long[] newOrder = new long[textLenght];

            for (int i = 0; i < textLenght; i++)
                count[classes[i]] += 1;

            for (int j = 1; j < textLenght; j++)
                count[j] = count[j] + count[j - 1];

            for (int k = (int)textLenght - 1; k >= 0; k--)
            {
                long start = (order[k] - lenght + textLenght) % textLenght;
                var cl = classes[start];
                count[cl] -= 1;
                newOrder[count[cl]] = start;
            }

            return newOrder;
        }

        private static long[] ComputeCharClasses(string text, long[] order)
        {
            long textLenght = text.Length;
            long[] classes = new long[textLenght];

            classes[order[0]] = 0;

            for (int i = 1; i < textLenght; i++)
            {
                if (text[(int)order[i]] != text[(int)order[i - 1]])
                    classes[order[i]] = classes[order[i - 1]] + 1;
                else
                    classes[order[i]] = classes[order[i - 1]];
            }

            return classes;
        }

        private static long[] SortCharacters(string text)
        {
            long textLenght = text.Length;
            long[] order = new long[textLenght];

            //dic
            Dictionary<char, long> count = new Dictionary<char, long>();

            count.Add('$', 0);
            count.Add('A', 0);
            count.Add('C', 0);
            count.Add('G', 0);
            count.Add('T', 0);

            for (int i = 0; i < text.Length; i++)
                count[text[i]] += 1;


            long countLenght = count.Count;

            var valueCount = count.Values.ToList();
            var keysCount = count.Keys.ToList();
            for (int j = 1; j < countLenght; j++)
            {
                count[keysCount[j]] = valueCount[j] + valueCount[j - 1];
                valueCount[j] = valueCount[j] + valueCount[j - 1];
            }


            for (int k = (int)textLenght - 1; k >= 0; k--)
            {
                char c = text[k];
                count[c] -= 1;
                order[count[c]] = k;
            }

            return order;
        }
    }
}
