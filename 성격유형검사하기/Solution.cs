using System;

public class Solution
{
    public string solution(string[] survey, int[] choices)
    {
        /// R T 
        /// C F
        /// J M
        /// A N 지표
        /// 7단계 선택지 설문 -3 -> +3 까지 점수
        /// 동점시 사전순으로 결정
        /// 
        int[] MBTIScore = new int[4];
        string[] MBTIChars = new string[4] {
            "RT",
            "CF",
            "JM",
            "AN"
            };

        

        for (int i = 0; i < survey.Length; i++)
        {
            int choice = choices[i];
            string category = survey[i];

            switch (category)
            {
                case "RT":
                    MBTIScore[0] += choice - 4;
                    break;
                case "TR":
                    MBTIScore[0] -= choice - 4;
                    break;
                case "CF":
                    MBTIScore[1] += choice - 4;
                    break;
                case "FC":
                    MBTIScore[1] -= choice - 4;
                    break;
                case "JM":
                    MBTIScore[2] += choice - 4;
                    break;
                case "MJ":
                    MBTIScore[2] -= choice - 4;
                    break;
                case "AN":
                    MBTIScore[3] += choice - 4;
                    break;
                case "NA":
                    MBTIScore[3] -= choice - 4;
                    break;
                default:
                    break;
            }
        }
       
        string answer = "";

        for (int i = 0; i < 4; i++)
        {
            if (MBTIScore[i] > 0)
            {
                answer += MBTIChars[i][1];
            }
            else
            {
                answer += MBTIChars[i][0];
            }
        }
        
        return answer;
    }
}