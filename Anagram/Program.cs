using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;



class Result
{

    /*
     * Complete the 'anagram' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING s as parameter.
     */

    public static int anagram(string s)
    {
        // a-z 만 있는 s
        // 같은 길이 두 부분으로 Substring
        int n = s.Length;
        if (n.IsOdd()) return -1;

        int mid = n / 2;
        string left = s.Substring(0, mid);
        string right = s.Substring(mid);
        int[] lcounts = new int[26] ;
        int[] rcounts = new int[26];
        for (int i = 0; i < mid; i++)
        {
            lcounts[left[i] - 'a']++;
            rcounts[right[i] - 'a']++;
        }

        int count = 0;
        for (int i = 0; i < 26; i++)
        {
            rcounts[i] = Math.Max(0, rcounts[i] - lcounts[i]);
            count += rcounts[i];
        }

        return count;
    }

}

static class ex
{
    public static bool IsOdd(this int number) => number % 2 > 0;
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string s = Console.ReadLine();

            int result = Result.anagram(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
