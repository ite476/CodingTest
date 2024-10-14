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
     * Complete the 'getTotalX' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER_ARRAY b
     */

    public static int getTotalX(List<int> a, List<int> b)
    {
        // a 의 모든 정수가 약수가 되도록
        // b 의 모든 정수가 배수가 되도록 하는 값의 수

        a.Sort();
        b.Sort();
        int Na = a.Count;
        int Nb = b.Count;

        int startNum = a[a.Count - 1];
        int endNum = b[0];

        int count = 0;

        for (int i = startNum; i <= endNum; i++)
        {
            if (i.HasFactorOfEvery(a)
                && i.IsFactorOfEvery(b))
            {
                count++;
            }
        }

        return count;
    }
}

static class Ext
{
    public static bool HasFactorOfEvery(this int num, List<int> list)
    {
        foreach(int i in list)
        {
            if (num % i > 0) return false;
        }
        return true;
    }

    public static bool IsFactorOfEvery(this int num, List<int> list)
    {
        foreach (int i in list)
        {
            if (i % num > 0) return false;
        }
        return true;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> brr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(brrTemp => Convert.ToInt32(brrTemp)).ToList();

        int total = Result.getTotalX(arr, brr);

        textWriter.WriteLine(total);

        textWriter.Flush();
        textWriter.Close();
    }
}
