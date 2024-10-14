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
     * Complete the 'truckTour' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY petrolpumps as parameter.
     */

    static List<List<int>> PetrolPumps;
    static int N { get => PetrolPumps.Count; }
    public static int truckTour(List<List<int>> petrolpumps)
    {
        // 원이 있다
        // N개 석유 펌프가 있다
        // 0번부터 인덱싱
        // 제공 석유량 / 다음 펌프와의 거리
        // 석유 1리터당 1km
        // 한바퀴 돌 수 있게 하는 시작위치?

        // N -> petrol, distance
        // petrol < distance -> impossible

        PetrolPumps = petrolpumps;
        for(int i = 0; i < N; i++)
        {
            if (IsPossibleFrom(i))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool IsPossibleFrom(int i)
    {
        int fuel = 0;
        for (int x = 0; x < N; x++)
        {
            int index = (i + x) % N;
            fuel += Petrol(index) - Distance(index);
            if (fuel < 0) return false;
        }
        return true;
    }

    private static int Petrol(int index) => PetrolPumps[index][0];
    private static int Distance(int index) => PetrolPumps[index][1];
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> petrolpumps = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            petrolpumps.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(petrolpumpsTemp => Convert.ToInt32(petrolpumpsTemp)).ToList());
        }

        int result = Result.truckTour(petrolpumps);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
