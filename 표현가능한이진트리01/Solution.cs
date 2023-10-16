using System;

public partial class Solution
{
    public int[] solution(long[] numbers)
    {
        int length = numbers.Length;
        int[] answer = new int[length];
        /// 1. 이진수를 저장할 빈 문자열 생성
        /// 2. 주어진 이진트리에 더미 노드를 추가 -> 포화 이진트리로 생성 (루트는 그대로 유지)
        /// 3. 만들어진 포화 이진트리의 노드들을 가장 왼쪽부터 오른쪽까지 순서대로 살펴봄. (높이는 순서에 영향 없음)
        /// 4. 살펴본 노드가 더미 노드라면, 문자열 뒤에 0을 추가하고 아니라면 1을 추가
        /// 5. 문자열에 저장된 이진수를 십진수를 변환
        /// 
        /// ++ 이진트리에서 리프노드가 아닌 노드는 자신의 왼쪽 자식이 루트인 서브트리의 노드들보다 오른쪽에 있으며
        /// ++ 자신의 오른쪽 자식이 루트인 노드들보다 왼쪽에 있다.
        /// 
        /// Depth 에 따라 필수적인 형태가 있음..
        /// 

        /// -> number -> 이진변환했을 때, 맨 중앙이 1이고 (비대칭인 경우 왼쪽이 0~~으로 시작한다고 가정)
        /// 양쪽 날개 값들의 깊이가 같으며
        /// 깊이 이상의 1이 양쪽 날개에 있어야 함

        
        for (int i = 0; i < length; i++)
        {
            long number = numbers[i];
            answer[i] = number.IsPossible_BinaryTreeNumber()? 1 : 0;     
        }

        return answer;


    }
}