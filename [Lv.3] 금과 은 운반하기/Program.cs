using System;
using System.Collections.Generic;
using System.Linq;

namespace _Lv._3__금과_은_운반하기
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Solution
    {
        public long solution(int goldNeeded, int silverNeeded, int[] g, int[] s, int[] w, int[] t)
        {
            List<City> cityList = new List<City>();

            foreach (var city in g.Zip(s, (gold, silver) => new { Gold = gold, Silver = silver })
                                  .Zip(w, (gs, weight) => new {gs.Gold, gs.Silver, Weight = weight})
                                  .Zip(t, (gsw, time) => new City()
                                  {
                                      Gold = gsw.Gold,
                                      Silver = gsw.Silver,
                                      TruckCapacity = gsw.Weight,
                                      Distance = time,
                                  }))
            {
                cityList.Add(city);
            }

            var search = new BinarySearchMachine()
            {
                Start = 0,
                End = (long)Math.Pow(10, 14) * 4,
            };

            var answer = search.BinarySearch(time =>
            {
                return IsEnoughTime(cityList, time, goldNeeded, silverNeeded);
            });

            return answer;
        }

        public class BinarySearchMachine
        {
            public long Start { get; set; }
            public long End { get; set; }
            public long Mid => (Start + End) / 2;

            public long BinarySearch(Predicate<long> predicate)
            {
                while (Start.CompareTo(End) < 0)
                {
                    if (predicate(Mid))
                    {
                        End = Mid;
                    }
                    else
                    {
                        Start = Mid + 1;
                    }
                }

                return Start;
            }
        }

        public bool IsEnoughTime(List<City> cityList, long time, int goldNeeded, int silverNeeded)
        {
            long totalCurrency = 0;
            long totalGold = 0;
            long totalSilver = 0;

            long totalNeeded = (long)goldNeeded + silverNeeded;

            foreach (var city in cityList)
            {
                long gatherCount = time / (2L * city.Distance);

                if (time % (2L * city.Distance) >= city.Distance)
                {
                    gatherCount++;
                }

                long totalAvailable = gatherCount * city.TruckCapacity;

                totalCurrency += Math.Min(totalAvailable, city.Gold + city.Silver);
                totalGold += Math.Min(totalAvailable, city.Gold);
                totalSilver += Math.Min(totalAvailable, city.Silver);

                if (totalCurrency >= totalNeeded
                    && totalGold >= goldNeeded
                    && totalSilver >= silverNeeded)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class City
    {
        public int Gold { get; set; }

        public int Silver { get; set; }

        public int TruckCapacity { get; set; }

        public int Distance { get; set; }

        public double TruckEfficiency => (double)TruckCapacity * Distance;
    }
}
