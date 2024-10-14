using System;
using System.Collections.Generic;

public class Solution
{
    int RequiredGold { get; set; }
    int RequiredSilver { get; set; }
    int[] Golds { get; set; }
    int[] Silvers { get; set; }
    int[] WeightLimits { get; set; }
    int[] Times { get; set; }
    int Count { get => Golds.Length; }
    double[][] Efficiencies { get; set; }

    List<int> timeRank = new List<int>();

    public long solution(int a, int b, int[] g, int[] s, int[] w, int[] t)
    {
        RequiredGold = a;
        RequiredSilver = b;
        Golds = g;
        Silvers = s;
        WeightLimits = w;
        Times = t;
        timeRank = SetTimeRank();

        Efficiencies = InitializeEfficiencies();
        
        long answer = -1;

        return 50;
        return answer;
    }

    private List<int> SetTimeRank()
    {
        timeRank = new List<int>();
        for (int i = 0; i < Count; i++)
        {
            timeRank.Add(GetEstimatedTime_OneWay(i));
        }        
    }

    private double[][] InitializeEfficiencies()
    {
        Efficiencies = new double[Count];
        for (int i = 0; i < Count; i++)
        {
            Efficiencies[i] = GetEfficiency(i);
        }
        
    }

    private double[] GetEfficiency(int index)
    {
        return new double[2] { GetGoldEfficiency(index), GetSilverEfficiency(index) };
    }

    
    int GetLeftGold(int index) => Golds[index];
    int GetLeftSilver(int index) => Silvers[index];
    int GetWeightLimit(int index) => WeightLimits[index];
    int GetEstimatedTime_OneWay(int index) => Times[index];
    int GetEstimatedTime_TwoWay(int index) => 2 * GetEstimatedTime_OneWay(index);

}