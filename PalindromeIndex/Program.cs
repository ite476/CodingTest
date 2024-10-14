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


static class Extensions
{
    public static bool IsOdd(this int n) => n % 2 > 0;
    public static bool IsEven(this int n) => !n.IsOdd();
}

class Result
{

    /*
     * Complete the 'palindromeIndex' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING s as parameter.
     */

    public static int palindromeIndex(string s)
    {
        // a-z ascii
        // 단 한개 인덱스의 값을 제거하여 Palindrome이 된다면 인덱스 반환,
        // 이 외 -1 (이미 Palindrome인 경우도)

        // odd -> 0, n-1 / 1, n-2 ... -> midindex (n / 2) or (n-1 /2) -> 제외
        // even -> midindex 4 0 1 2 3 1.5 -> midindex (n /2) 미만

        int n = s.Length;
        Console.WriteLine(n);
        int midindex = n / 2;

        for (int i = 0; i < midindex; i++)
        {
            int opp = n - 1 - i;
            if (s[i] != s[opp])
            {
                if (CheckWithOut(i) == true) { return i; }
                else if (CheckWithOut(opp) == true) { return opp; }
                else { return -1; }
            }
        }

        return -1; // already palindrome

        bool CheckWithOut(int index)
        {
            Console.Write($"Called CheckWithOut({index}) : ");

            var str = s.Substring(0, index) + s.Substring(index + 1);

            

            int n_recheck = str.Length;

            Console.Write($" {str}({n_recheck}) ");

            int midindex_recheck = n_recheck / 2;
            bool result;
            for (int x = 0; x < midindex_recheck; x++)
            {
                int y = n_recheck - 1 - x;
                Console.WriteLine($"Checking : {x} vs {y} ");
                if (str[x] != str[y])
                {
                    return false;
                }
            }
            result = true;
            Console.WriteLine($"result : {result} ");
            return result;
        }
    }



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

            int result = Result.palindromeIndex(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
