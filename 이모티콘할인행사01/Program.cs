using System.Reflection.Metadata;

namespace 이모티콘할인행사01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Solution solution = new Solution();
            var answer = solution.solution(new int[,]
            {
                { 40, 10000 },
                { 25, 10000 }
            },
            new int[]
            {
                7000,9000
            });

            Console.WriteLine(answer);

            long count = (long)Math.Pow(4, 7) * 100;
            Console.WriteLine(count);
        }
    }
}