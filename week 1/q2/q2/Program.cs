using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace q2
{
    class Program
    {
        struct Tuple
        {
            public char x;
            public List<long> y;
        }
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            long n = long.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }

            List<Tuple> trie = BuildTrie(n, patterns);

            List<long> position = new List<long>();

            for (int i = 0; i < text.Length; i++)
            {
                if (Match(trie, i, text))
                    position.Add(i);
            }

            //if (position.Count == 0)
            //    position.Add(-1);

            //return position.ToArray();
            List<string> posStr = new List<string>();
            foreach( var a in position)
            {
                posStr.Add(a.ToString());
            }
            var result = String.Join(" ", posStr.ToArray());
            Console.WriteLine(result);

        }

        private static bool Match(List<Tuple> trie, int n, string text)
        {
            var node = trie.First();

            bool check = true;
            bool checkHaveSign = false;

            for (int i = n; i < text.Length; i++)
            {
                check = false;

                if (node.y.Count == 0)
                    return true;

                for (int j = 0; j < node.y.Count; j++)
                {
                    if (trie[(int)node.y[j]].x == '$')
                        checkHaveSign = true;

                    if (trie[(int)node.y[j]].x == text[i])
                    {
                        node = trie[(int)node.y[j]];
                        check = true;
                        break;
                    }
                }

                if (CheckSign(trie, node))
                    return true;

                if (!check && checkHaveSign)
                    return true;
                else if (!check)
                    return false;
            }

            for (int i = 0; i < node.y.Count; i++)
            {
                if (trie[(int)node.y[i]].x == '$')
                    return true;
            }

            return false;
        }

        private static bool CheckSign(List<Tuple> trie, Tuple node)
        {
            var neighborList = node.y;

            foreach (var i in neighborList)
            {
                if (trie[(int)i].x == '$')
                    return true;
            }

            return false;
        }

        private static List<Tuple> BuildTrie(long n, string[] patterns)
        {
            List <Tuple> trie = new List<Tuple>();

            //root
            //trie.Add(Tuple.Create('r', new List<long>()));
            var root = new Tuple();
            root.x = 'r';
            root.y = new List<long>();
            trie.Add(root) ;

            long count = 1;
            long selector = 0;

            for (int i = 0; i < n; i++)
            {
                string patt = patterns[i];

                for (int p = 0; p < patt.Length; p++)
                {
                    //check
                    if (Check(trie, patt[p], ref selector))
                        continue;
                    else
                    {
                        trie[(int)selector].y.Add(count);
                        var tmp = new Tuple();
                        tmp.x = patt[p];
                        tmp.y = new List<long>();
                        trie.Add(tmp);


                        selector = count;
                        count++;
                    }
                }

                trie[(int)selector].y.Add(count);
                var temp = new Tuple();
                temp.x = '$';
                temp.y = new List<long>();
                trie.Add(temp);
                selector = count;
                count++;

                selector = 0;
            }

            return trie;
        }

        private static bool Check(List<Tuple> trie, char v, ref long selector)
        {
            for (int i = 0; i < trie[(int)selector].y.Count; i++)
            {
                long idx = trie[(int)selector].y[i];
                if (trie[(int)idx].x == v)
                {
                    selector = idx;
                    return true;
                }
            }

            return false;
        }
    }
}
