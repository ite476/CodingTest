namespace 미로주행02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Solution sol = new Solution();
            int[,] tests = {
                { 0,0,199997,1},
                { 5, 6, 7, 8 }
                };

            sol.solution(99999, 99999, tests);
        }
    }
}