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
     * Complete the 'flippingMatrix' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
     */
    static List<List<int>> Matrix;
    static int N { get => Matrix.Count / 2; }
    public static int flippingMatrix(List<List<int>> matrix)
    {
        // n x n -> 각 좌표별 대응되는 거울위치 4군데 중 최대값
        // 거울위치 좌표
        // x, y -> 0 ~ 2n - 1
        // x, y / 2n - 1 - x, y / x, 2n - 1 - y, / 2n - 1 - x, 2n - 1 - y
        Matrix = matrix;
        List<int> maxs = new List<int>();
        for (int x = 0; x < N; x++)
        {            
            
            for (int y = 0; y < N; y++)
            {
                maxs.Add(GetMax(x, y));
            }
        }

        return maxs.Sum();
    }

    private static int GetMax(int x, int y)
    {
        List<int> list = new List<int>
        {
            Value(x, y),
            Value(Flip(x), y),
            Value(x, Flip(y)),
            Value(Flip(x), Flip(y))
        };
        return list.Max();
    }
    private static int Flip(int x) => 2 * N - 1 - x;
    private static int Value(int x, int y) => Matrix[x][y];
   
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<int>> matrix = new List<List<int>>();

            for (int i = 0; i < 2 * n; i++)
            {
                matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
            }

            int result = Result.flippingMatrix(matrix);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
