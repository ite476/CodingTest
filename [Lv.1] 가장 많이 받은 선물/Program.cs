using System;
using System.Collections.Generic;
using System.Linq;

namespace _Lv._1__가장_많이_받은_선물
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Solution();

            var answer = s.solution(
                new string[] { "muzi", "ryan", "frodo", "neo" },
                new string[] { "muzi frodo", "muzi frodo", "ryan muzi", "ryan muzi", "ryan muzi", "frodo muzi", "frodo ryan", "neo muzi"}
                );

            Console.WriteLine(answer);
            Console.ReadLine();
        }


        public class Solution
        {
            public int solution(string[] friends, string[] gifts)
            {
                Dictionary<string, GiftUser> users = new Dictionary<string, GiftUser>();
                Dictionary<string, int> nextMonthRecieve = new Dictionary<string, int>();

                foreach (string id in friends)
                {
                    var user = new GiftUser()
                    {
                        Id = id,
                    };

                    users.Add(user.Id, user);
                    nextMonthRecieve.Add(user.Id, 0);
                }

                EveryEachOther(users.Values, (x, y) =>
                {
                    x.SendHistory.Add(y.Id, 0);
                    x.RecievedHistory.Add(y.Id, 0);

                    y.SendHistory.Add(x.Id, 0);
                    y.RecievedHistory.Add(x.Id, 0);
                });

                foreach (var record in gifts)
                {
                    var ids = record.Split(' ');
                    var from = ids[0];
                    var to = ids[1];

                    users[from].SendGift(users[to]);
                }

                EveryEachOther(users.Values, (x, y) =>
                {
                    var nextMonthTarget = x.WhoWillGetNextMonth(y);
                    if (nextMonthTarget != null)
                    {
                        nextMonthRecieve[nextMonthTarget]++;
                    }
                });

                return nextMonthRecieve.Values.Max();
            }

            public void EveryEachOther<T>(IEnumerable<T> values, Action<T, T> action)
            {
                var count = values.Count();
                foreach (var now in Enumerable.Range(0, count).Zip(values, 
                    (index, value) => new
                    {
                        Index = index,
                        This = value,
                        Anothers = values.Skip(index + 1),
                    }))
                {
                    foreach (var another in now.Anothers)
                    {
                        action(now.This, another);
                    }
                }
            }

            public class GiftUser
            {
                public string Id { get; set; }

                public Dictionary<string, int> SendHistory { get; set; } = new Dictionary<string, int>();

                public Dictionary<string, int> RecievedHistory { get; set; } = new Dictionary<string, int>();

                private Lazy<int> lazyGiftCoefficient { get; }
                public int GiftCoefficient => lazyGiftCoefficient.Value;

                public GiftUser()
                {
                    lazyGiftCoefficient = new Lazy<int>(() =>
                    {
                        var totalSent = SendHistory.Sum(x => x.Value);
                        var totalRecieved = RecievedHistory.Sum(x => x.Value);

                        return totalSent - totalRecieved;
                    });
                }

                public void SendGift(GiftUser anotherUser)
                {
                    SendHistory[anotherUser.Id]++;
                    anotherUser.RecievedHistory[Id]++;
                }

                public string WhoWillGetNextMonth(GiftUser anotherUser)
                {
                    var send = SendHistory[anotherUser.Id];
                    var recieved = RecievedHistory[anotherUser.Id];

                    if (send > recieved)
                    {
                        return this.Id;
                    }
                    else if (send == recieved)
                    {
                        if (this.GiftCoefficient > anotherUser.GiftCoefficient)
                        {
                            return this.Id;
                        }
                        else if (this.GiftCoefficient == anotherUser.GiftCoefficient)
                        {
                            return null;
                        }
                        else return anotherUser.Id;
                    }
                    else
                    {
                        return anotherUser.Id;
                    }
                }
            }
        }

        
    }
}
