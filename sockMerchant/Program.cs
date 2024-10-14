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
     * Complete the 'sockMerchant' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER_ARRAY ar
     */
    static Dictionary<int, int> SockCount { get; set; } = new Dictionary<int, int>();
    public static int sockMerchant(int n, List<int> ar)
    {
        CountSocks(ar);
        int sum = CountPair();
        return sum;
    }

    private static void CountSocks(List<int> ar)
    {
        foreach (int i in ar)
        {
            SockCount[i] = (SockCount.ContainsKey(i) ? SockCount[i] : 0) + 1;
        }
    }

    private static int CountPair()
    {
        int sum = 0;
        foreach (var i in SockCount.Values)
        {
            sum += i / 2;
        }

        return sum;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> ar = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arTemp => Convert.ToInt32(arTemp)).ToList();

        int result = Result.sockMerchant(n, ar);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
