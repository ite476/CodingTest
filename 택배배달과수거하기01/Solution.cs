using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution
{
    public long solution(int cap, int n, int[] deliveries, int[] pickups)
    {
        //일렬 나열 n개 집에 배달량, 수거량 주어짐
        // 트럭에 상자 최대 cap개 실음
        // 최소거리로 모든 집의 배달, 수거하기
        // -> 무조건 최대거리부터 처리할 것. -> 최대거리부터 돌아오면서 처리하도록 알고리즘 짜기


        // 배달/수거할 집의 번지수 : n번째 집을 초기값으로
        int zipDelivery = n;
        int zipPickups = n;

        long distance_Total = 0;
        while(zipDelivery > 0 || zipPickups > 0)
        {
            int[] farthestZipcode = new int[2];
            farthestZipcode[0] = HandleFromTheFarthest(deliveries, ref zipDelivery, cap);
            farthestZipcode[1] = HandleFromTheFarthest(pickups, ref zipPickups, cap);
            distance_Total += Math.Max(farthestZipcode[0], farthestZipcode[1]);
        }

        return distance_Total * 2;
    }

    private int HandleFromTheFarthest(int[] array, ref int lastVisit, int load)
    {
        int result = 0;

        while (load > 0 && lastVisit > 0)
        {
            int numberThisHouse = array[lastVisit - 1];

            if (result == 0 
                &&  numberThisHouse > 0)
            { 
                result = lastVisit;
            }

            load -= numberThisHouse;
            array[lastVisit - 1] = 0;

            if (load >= 0)
            {
                lastVisit--;
            }
            else
            {
                array[lastVisit - 1] -= load;
                load = 0;
            }
        }

        return result;
    }
}