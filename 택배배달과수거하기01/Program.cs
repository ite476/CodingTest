
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Solution solution = new Solution();
        long answer;
        //answer = solution.solution(4, 5, new int[] { 1, 0, 3, 1, 2 }, new int[] { 0, 3, 0, 4, 0 });
        //answer = solution.solution(3, 5, new int[] { 2, 2, 2, 2, 2 }, new int[] { 2, 2, 2, 2, 2 });
        answer = solution.solution(3, 10,
                      //1  2  3  4  5  6  7  8  9 10
            new int[] { 2, 2, 2, 0, 0, 0, 0, 2, 2, 2 },
            new int[] { 0, 0, 0, 2, 2, 2, 2, 2, 2, 0 });
        Console.WriteLine(answer);
    }
}
