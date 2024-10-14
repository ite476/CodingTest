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
     * Complete the 'timeConversion' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string timeConversion(string s)
    {
        int hour, minute, second;
        bool isAM;

        var strings = s.Substring(0, 8).Split(':');
        var AMPM = s.Substring(8);
        isAM = (AMPM == "AM") ? true : false;

        hour = int.Parse(strings[0]);
        minute = int.Parse(strings[1]);
        second = int.Parse(strings[2]);

        hour = hour % 12 + (isAM ? 0 : 12);

        var answer = $"{hour:D2}:{minute:D2}:{second:D2}";

        return answer;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.timeConversion(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
