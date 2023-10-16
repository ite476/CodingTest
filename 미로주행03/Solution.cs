namespace 미로주행03
{
    public class Solution
    {
        public long solution(int n, int m, int[,] tests)
        {
            tests.GetLength(0); // 테스트수
            tests.GetLength(1); // 1~4 인수들..

            int index = 0;
            (Car Start, bool hitFlag) = (new Car(tests[index, 0], tests[index, 1], tests[index, 2]), tests[index, 3] == 1);

            PathFinder pf = new PathFinder(Start, new Coordinate(n, m));
            BitArray2D dict = pf.GetExploredCoordinates();
            
            long answer = 67;
            return answer;
            /*
             * 좌표 (0,0) -> (n,m)
             * (a,b) > 표지판
             * 출발좌표, 남은연료량의 최대주행거리, 표지판도달여부
             * tests [x, y, d, flag]
             * 
             * 
             * 
             * 
             * 
            */
        }
    }
}