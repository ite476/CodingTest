using System;

public class Program
{
    public static void Main()
    {
        Solution solution = new Solution();
        var answer = solution.solution(2);

        for(int i = 0; i < answer.GetLength(0); i++) 
        {
            Console.WriteLine($"{answer[i, 0]}{answer[i,1]}");
        }

    }
}