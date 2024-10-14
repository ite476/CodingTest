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
     * Complete the 'fizzBuzz' function below.
     *
     * The function accepts INTEGER n as parameter.
     */

    public static void fizzBuzz(int n)
    {
        // i 가 3, 5의 공배수 -> FizzBuzz
        // i 가 3의 배수(5의 배수는 아님) -> Fizz
        // i 가 5의 배수(3의 배수가 아님) Buzz
        for (int i = 1; i <= n; i++)
        {
            string str = "";

            if (i % 3 == 0) { str += "Fizz"; }
            if (i % 5 == 0) { str += "Buzz"; }
            if (str.Length == 0) { str += i; }

            Console.WriteLine(str);
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Result.fizzBuzz(n);
    }
}
