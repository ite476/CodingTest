using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public int[] solution(string[] userList, string[] reports, int threshold)
    {
        Dictionary<string, User> users = new Dictionary<string, User>();
        User.Threshold = threshold;

        foreach(string id in userList)
        {
            users[id] = new User(id);
        }

        foreach(string reportInfo  in reports)
        {
            string[] names = reportInfo.Split(" ");
            var goodGuy = users[names[0]]; 
            var badGuy = users[names[1]];

            goodGuy.Report(badGuy);
        }

        var result = new List<int>();

        foreach (var user in users.Values)
        {
            result.Add(user.MailedCount);
        }

        return result.ToArray();
    }

    internal class User
    {
        private int _reportedCount = 0;
        public int ReportedCount
        {
            get => _reportedCount;
            set
            {
                _reportedCount = value;
                if (_reportedCount >= User.Threshold)
                {
                    Ban();
                }
            }
        }

        public Dictionary<string, User> ReportedUsers { get; set; } = new Dictionary<string, User>();

        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }

        


        
        public bool IsAvailable { get; set; } = true;
        public int MailedCount { get; set; }
        public static int Threshold { get; set; }

        public void Ban()
        {
            IsAvailable = false;

            foreach (var user in ReportedUsers.Values)
            {
                NotifyThisUserGotBanned(user);
            }
        }

        private void NotifyThisUserGotBanned(User another)
        {
            another.MailedCount++;
        }

        public void Report(User another)
        {
            if (another.ReportedUsers.ContainsKey(this.Name)) { return; }

            another.ReportedUsers.Add(this.Name, this);
            another.ReportedCount++;
        }
    }
}