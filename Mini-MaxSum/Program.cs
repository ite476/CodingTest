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
     * Complete the 'miniMaxSum' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void miniMaxSum(List<int> arr)
    {
        int length = 5;
        int? min = null;
        int? max = null;
        int sum = 0;
        for (int x = 0; x < length; x++)
        {
            int number = arr[x];
            min = (min == null) ? number : Math.Min((int)min, number);
            max = (max == null) ? number : Math.Max((int)max, number);
            sum += number;
        }

        Console.WriteLine($"{sum - (int)max} {sum - (int)min}");
    }

}

class Solution
{
    public static void Main(string[] args)
    {

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.miniMaxSum(arr);
    }
}
