using System;
using System.Collections.Generic;

public class Solution
{
    public int solution(int[] queue1, int[] queue2)
    {
        /// 하나의 큐를 골라 pop -> insert하여 큐의 합이 같도록 하게 함
        /// 
        var Q1 = new MyQueue();
        var Q2 = new MyQueue();

        int length = queue1.Length;
        int sum = 0;
        int Max = 0;
        for (int i = 0; i < length; i++)
        {
            long x = queue1[i];
            long y = queue2[i];
            Q1.EnQ(x);
            Q2.EnQ(y);
            sum += (int)x + (int)y;
            Max = Math.Max(Max, (int)Math.Max(x, y));
        }

        

        if (sum % 2 == 1 || Max > sum / 2)
        {
            return -1;
        }

        long sumTarget = sum / 2;
        int answer = 0;
        bool Q1DQed;
        int overCount = 10;
        while (Q1.Sum != sumTarget)
        {
            answer++;
            if (Q1.Sum > Q2.Sum)
            {
                long value = Q1.DQ();
                Q2.EnQ(value);
            }
            else
            {
                long value = Q2.DQ();
                Q1.EnQ(value);
            }
            if (answer > queue1.Length * 3)
            {
                return -1;
            }
        }
        
        return answer;
    }
}

internal class MyQueue : Queue<long> 
{
    public long Sum;

    public MyQueue() : base() { }

    public void EnQ(long x)
    {
        Sum += x;
        this.Enqueue(x);
    }

    public long DQ()
    {
        long x = this.Dequeue();
        Sum -= x;
        return x;
    }
}