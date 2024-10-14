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
     * Complete the 'pageCount' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER p
     */

    public static int pageCount(int n, int p)
    {
        // 0.5

        // 0 1 -> 1     0 0.5
        // 2 3 -> 2     1 1.5...
        // 4 5 -> 3... -> 
        // number is on 1+(number/2) page.
        // Math.min(Math.abs(Page(p) - 1), Math.abs(Page(p) - Page(n)))

        // n -> n.5 even

        // (n-1).5 odd

        return Math.Min(Math.Abs(Page(p) - 1), Math.Abs(Page(p) - Page(n)));

    }

    private static int Page(int p) => 1 + (p / 2);
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        int p = Convert.ToInt32(Console.ReadLine().Trim());

        int result = Result.pageCount(n, p);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
