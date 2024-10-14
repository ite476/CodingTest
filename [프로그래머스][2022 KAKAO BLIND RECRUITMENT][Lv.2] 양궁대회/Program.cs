using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _프로그래머스__2022_KAKAO_BLIND_RECRUITMENT__Lv._2__양궁대회
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var solution = new MySolution();

            int[] info = new int[] 
            {                 
                1,  // 0
                2,  // 1
                3,  // 2
                4,  // 3
                5,  // 4
                6,  // 5
                7,  // 6
                8,  // 7
                9,  // 8
                10, // 9
                11  // 10
            };

            int n = info.Sum(); 

            var answer = solution.solution(n, info);

            var stringBuilder = new StringBuilder();

            foreach(var hit in answer)
            {
                stringBuilder.AppendLine(hit.ToString());
            }

            var line = stringBuilder.ToString();

            Console.WriteLine(line);
            Console.ReadLine();
        }


        public class ScoreBoard
        {
            public int[] Hits { get; }
            public int Score { get; }
        }

        public static class ScoreCalcualator
        {

        }


        public class MySolution
        {
            public int[] EnemyHits { get; set; }
            public int EnemyScore { get; set; }

            public int[] solution(int n, int[] info)
            {
                EnemyHits = info;
                EnemyScore = Enumerable.Range(0, 11).Zip(info, (s, h) => new 
                { 
                    Score = s, 
                    Hits = h 
                })
                .Where(x => x.Hits > 0)
                .Sum(x => x.Score);

                var answer = 깊이우선탐색(new SearchParameters()
                {
                    Skip = 0,
                    Ammo = n,
                });

                return answer.Hits.ToArray();
            }

            public class SearchParameters
            {
                public int Skip { get; set; }
                public int Ammo { get; set; }
            }

            public class SearchOutputs
            {
                public static SearchOutputs Wrong => new SearchOutputs()
                {
                    Hits = Enumerable.Empty<int>().ToList(),
                    Score = -1,
                };

                public List<int> Hits { get; set; }
                public int Score { get; set; }

                public bool IsPossible => Score >= 0;
            }

            SearchOutputs 깊이우선탐색(SearchParameters parameters)
            {
                if (parameters.Skip == 10)
                {
                    return new SearchOutputs()
                    {
                        Hits = Enumerable.Empty<int>().ToList(),
                        Score = 0,
                    };
                }

                if (parameters.Ammo == 0)
                {
                    return new SearchOutputs()
                    {
                        Hits = Enumerable.Repeat<int>(0, 11).Skip(parameters.Skip).ToList(),
                        Score = 0,
                    };
                }
                else if (parameters.Ammo < 0) 
                {
                    return SearchOutputs.Wrong;
                }

                int currentTargetScore = parameters.Skip;
                int currentEnemyHits = EnemyHits.Skip(parameters.Skip).First();
                int nextSkip = parameters.Skip + 1;

                var outputWin = 깊이우선탐색(new SearchParameters()
                {
                    Skip = nextSkip,
                    Ammo = parameters.Ammo - (currentEnemyHits + 1),
                });

                if (outputWin.IsPossible)
                {
                    outputWin.Score += currentTargetScore;
                }

                var outputDraw = 깊이우선탐색(new SearchParameters()
                {
                    Skip = nextSkip,
                    Ammo = parameters.Ammo - currentEnemyHits,
                });

                var outputLost = 깊이우선탐색(new SearchParameters()
                {
                    Skip = nextSkip,
                    Ammo = parameters.Ammo,
                });

                var outputBetter = CompareOutputs(outputWin, outputDraw);
                var outputBest = CompareOutputs(outputBetter, outputLost);

                var answer = new SearchOutputs()
                {
                    Hits = Enumerable.Empty<int>().Append(outputBest.Hits.First()).Concat(outputBest.Hits).ToList(),
                    Score = outputBest.Score,
                };

                return answer;
            }

            SearchOutputs CompareOutputs(SearchOutputs first, SearchOutputs second)
            {
                if (first.IsPossible)
                {
                    if (second.IsPossible)
                    {
                        if (first.Score >= second.Score)
                        {
                            return first;
                        }

                        return second;
                    }

                    return first;
                }


                if (second.IsPossible) return second;

                return SearchOutputs.Wrong;
            }
        }

        public class CopySolution
        {
            public List<int> EnemyHits { get; set; }
            public int Ammo { get; set; }

            public int RyanScore { get; set; }
            public int ApeachScore { get; set; }

            public List<List<int>> Answers { get; set; }
            public List<int> Hits { get; set; }

            public int[] solution(int n, int[] info)
            {
                EnemyHits = info.ToList();
                Ammo = n;
                RyanScore = 0;
                ApeachScore = 0;
                Answers = new List<List<int>>();

                var scoreBoard = Enumerable.Range(0, 11).Zip(EnemyHits,(s, h) => new
                {
                    Score = s,
                    Hits = h
                });

                foreach (var current in scoreBoard)
                {
                    if (current.Hits > 0)
                    {
                        ApeachScore += current.Score;
                    }
                }

                Hits = Enumerable.Repeat(0, 11).ToList();

                Dfs(0, n, 0);

                if (ApeachScore >= RyanScore)
                {
                    return new int[] { -1 };
                }

                int answer = 0;
                int max1 = 0;
                int max2 = 0;

                for (int i = 0; i < Answers.Count; ++i)
                {
                    for (int j = Answers[i].Count - 1; j >= 0; --j)
                    {
                        if (Answers[i][j] > 0)
                        {
                            if (max1 < j || (max1 == j && max2 < Answers[i][j]))
                            {
                                answer = i;
                                max1 = j;
                                max2 = Answers[i][j];
                                break;
                            }
                        }
                    }
                }

                return Answers[answer].ToArray();
            }

            void Dfs(int idx, int rest, int sum)
            {
                if (rest == 0)
                {
                    if (RyanScore <= sum)
                    {
                        if (RyanScore < sum)
                        {
                            Answers.Clear();
                        }
                        RyanScore = sum;
                        Answers.Add(new List<int>(Hits));
                    }

                    return;
                }

                int promising = 
                    (10 - idx) 
                    * (11 - idx) 
                    / 2 
                    * 2;
                
                if (10 - idx - rest > 0)
                {
                    promising -= 
                        (10 - idx - rest) 
                        * (11 - idx - rest) 
                        / 2 * 2;
                }

                if (sum + promising <= ApeachScore 
                    || sum + promising < RyanScore)
                {
                    return;
                }

                // 선택하는 경우
                if (idx < 10 
                    && rest 
                    > EnemyHits[idx])
                {
                    Hits[idx] = EnemyHits[idx] + 1;
                    
                    int tmp = 
                        Hits[idx] > 1 
                        ? sum + (10 - idx) * 2 
                        : sum + (10 - idx);

                    Dfs(idx + 1, 
                        rest - Hits[idx], 
                        tmp);
                }

                // 선택하지 않는 경우
                Hits[idx] = idx == 10 
                    ? rest 
                    : 0;

                Dfs(idx + 1, 
                    rest - Hits[idx], 
                    sum);

                Hits[idx] = 0;
            }
        }
    }
}
