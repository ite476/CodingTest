namespace 표현가능한이진트리01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Solution solution = new Solution();
            long[] numbers = new long[]
            {
                7, 42, 5
            };
            numbers = new long[]
            {
                63, 111, 95
            };

            var answer = solution.solution(numbers);

            int length = answer.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(answer[i]);
            }
            
        }
    }
}