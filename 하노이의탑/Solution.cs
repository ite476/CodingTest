using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class Solution
{
    public Queue<(int, int)> AnswerQ { get; set; } = new Queue<(int, int)>();

    public int[,] solution(int n)
    {
        CallRoutine(n, 1, 3, 2);

        return ExtractAnswer();
    }

    private int[,] ExtractAnswer()
    {
        int count = AnswerQ.Count;
        int[,] answer = new int[count, 2];
        for (int i = 0; i < count; i++)
        {
            var item = AnswerQ.Dequeue();
            answer[i, 0] = item.Item1;
            answer[i, 1] = item.Item2;
        }
        return answer;
    }
    private void CallRoutine(int currentNumber, int from, int to, int placeholder)
    {
        if (currentNumber > 1)
        {
            CallRoutine(currentNumber - 1, from, placeholder, to);
        }

        MoveThis(currentNumber, from, to);

        if (currentNumber > 1)
        {
            CallRoutine(currentNumber - 1, placeholder, to, from);
        }
    }
    private void MoveThis(int currentNumber, int from, int to) => AnswerQ.Enqueue((from, to));
}

public static class Extensions
{
    public static bool IsOdd(this int value) => value % 2 == 1;
    public static bool IsEven(this int value) => ! value.IsOdd();

}