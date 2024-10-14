using System;

namespace 프로그래머스_92334_신고_결과_받기
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sol = new Solution();

            string[] id_list = ["muzi", "frodo", "apeach", "neo"];

            string[] report = ["muzi frodo", "apeach frodo", "frodo neo", "muzi neo", "apeach muzi"];

            var k = 2;

            var answer = sol.solution(id_list, report, k);

            foreach( var item in answer )
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(answer);
        }
    }
}
