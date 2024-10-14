using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{
    static string findZigZagSequence(int n, List<int> list)
    {
        list = list.OrderBy(i => i).ToList();
        var desclist = list.Skip(MidIndex(n)).Take(LeftCount(n)).OrderByDescending(i => i).ToList();
        return Answer(list, desclist);
    }
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        int t = Convert.ToInt32(Console.ReadLine());
        for (int test = 0; test < t; test++)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var list = Console.ReadLine().Trim().Split(' ').Select(s => Convert.ToInt32(s)).ToList();
            var answer = findZigZagSequence(n, list);
            
            Console.WriteLine(answer);
        }


        foreach (String s in args)
        {
            Console.WriteLine(s);
        }
    }


    private static string Answer(List<int> list, List<int> desclist)
    {
        int n = list.Count();
        int midindex = MidIndex(n);
        int leftcount = LeftCount(n, midindex);
        string str = "";
        for (int i = 0; i < midindex; i++)
        {
            str += (str.Length == 0 ? "" : " ") + $"{list[i]}";
        }
        for (int i = 0; i < leftcount; i++)
        {
            str += (str.Length == 0 ? "" : " ") + $"{desclist[i]}";
        }
        return str;
    }


    private static int LeftCount(int n) => LeftCount(n, MidIndex(n));
    private static int LeftCount(int n, int midindex) => n - midindex;
    private static int MidIndex(int n) => (n - 1) / 2; // 5 -> 3 0 1 2 3 4 
}