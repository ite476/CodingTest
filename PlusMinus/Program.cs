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
     * Complete the 'plusMinus' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void plusMinus(List<int> arr)
    {
        // int + - 0
        // decimal value 6 places after

        int n = arr.Count;
        int[] count = new int[3];
        for (int i = 0; i < n; i++)
        {
            int number = arr[i];
            count[(number > 0) ? 0 : (number < 0) ? 1 : 2]++;
        }

        Console.WriteLine($"{(double)count[0] / n}");
        Console.WriteLine($"{(double)count[1] / n}");
        Console.WriteLine($"{(double)count[2] / n}");


    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.plusMinus(arr);
    }
}
