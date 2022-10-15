using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A1
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for(int i = 0; i < n; i++)
            {
                string p = Console.ReadLine();
                patterns[i] = p;
            }
            string [] answer = BuildTrieAndCompute(n, patterns);
            answer.ToList().ForEach(d => { Console.WriteLine(d); });
            //Console.WriteLine();
        }

        private static string[] BuildTrieAndCompute(long n, string[] patterns)
        {
            var trie = new List<List<long>>();
            var trieChar = new List<char>();

            trie.Add(new List<long>());
            trieChar.Add('r');

            long count = 1;
            long selector = 0;

            List<string> answer = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string patt = patterns[i];

                for (int p = 0; p < patt.Length; p++)
                {
                    //check
                    if (check(trie , trieChar, patt[p], ref selector))
                        continue;
                    else
                    {
                        trie[(int)selector].Add(count);

                        trie.Add(new List<long>());
                        trieChar.Add(patt[p]);

                        string temp = selector +"->"+ count + ":" + patt[p];
                        answer.Add(temp);

                        selector = count;
                        count++;
                    }
                }

                selector = 0;
            }

            return answer.ToArray();
        }

        private static bool check(List<List<long>> trie, List<char> trieChar, char v, ref long selector)
        {
            for (int i = 0; i < trie[(int)selector].Count; i++)
            {
                long idx = trie[(int)selector][i];
                if (trieChar[(int)idx] == v)
                {
                    selector = idx;
                    return true;
                }
            }

            return false;
        }
    }
}
