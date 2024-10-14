using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution2
{
    static string findZigZagSequence(int n, List<int> list)
    {
        throw new NotImplementedException();
    }
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

        int t = Convert.ToInt32(Console.ReadLine()); for (int test = 0; test < t; test++) { int n = Convert.ToInt32(Console.ReadLine()); var list = Console.ReadLine().Trim().Split(' ').Select(s => Convert.ToInt32(s)).ToList(); list = list.OrderBy(i => i).ToList(); var desclist = list.Skip((n - 1) / 2).Take(n - (n - 1) / 2).OrderByDescending(i => i).ToList(); int midindex = (n - 1) / 2; int leftcount = n - (n - 1) / 2; string answer = ""; for (int i = 0; i < midindex; i++) { answer += (answer.Length == 0 ? "" : " ") + $"{list[i]}"; } for (int i = 0; i < leftcount; i++) { answer += (answer.Length == 0 ? "" : " ") + $"{desclist[i]}"; } Console.WriteLine(answer); }


        foreach (string hhh in args)
        {
            Console.WriteLine(hhh);
        }
        var s = "";
    }


    private static string Answer(List<int> list, List<int> desclist)
    {
        var answer = "";
        return answer;
    }
}