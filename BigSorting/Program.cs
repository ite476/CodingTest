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
     * Complete the 'bigSorting' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts STRING_ARRAY unsorted as parameter.
     */

    public static List<string> bigSorting(List<string> unsorted)
    {
        unsorted.Sort(0, unsorted.Count, Comparer<string>.Create((a, b) => bigSortingCompareTo(a, b)));
        return unsorted;
    }

    private static int bigSortingCompareTo(string a, string b)
    {
        int lenA = a.Length;
        int lenB = b.Length;
        if (lenA < lenB) return -1;
        else if (lenA == lenB)
        {
            for(int i = 0; i < lenA; i++) 
            {
                if (a[i] < b[i]) return -1;
                if (a[i] > b[i]) return 1;
            }

            return 0;
        }
        else return 1;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> unsorted = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string unsortedItem = Console.ReadLine();
            unsorted.Add(unsortedItem);
        }

        List<string> result = Result.bigSorting(unsorted);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
