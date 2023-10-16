using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 개인정보수집유효기간1
{
    public class Personnel
    {
        public int Index { get; set; }

        //public InfoDate
    }

    public class InfoDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public InfoDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public void Calculate()
        {
            Year += Month / 12;
            Month = Month % 12 + Day / 28;
            Day = Day % 28;
        }

        public int ParseToDay() => ((Year * 12) + Month) * 28 + Day;
     
        public static InfoDate Build(string dateString)
        {
            string[] splitStr = dateString.Split('.');
            int year = Convert.ToInt32(splitStr[0]);
            int month = Convert.ToInt32(splitStr[1]);
            int day = Convert.ToInt32(splitStr[2]);

            return new InfoDate(year, month, day);
        }

    }

    public class TermOfUsage
    {
        private Dictionary<char, int> KeyValuePairs = new Dictionary<char, int>();

        public int this[char key]
        {
            get
            {
                return KeyValuePairs[key];
            }
            set
            {
                KeyValuePairs[key] = value;
            }
        }

        public TermOfUsage(string[] terms)
        {
            foreach (var item in terms)
            {
                string[] strings = item.Split(' ');

                char type = Convert.ToChar(strings[0]);
                int months = Convert.ToInt32(strings[1]);

                KeyValuePairs[type] = months;
            }
        }

    }

    public class Privacy
    {
        public InfoDate Date { get; }

        public char Type { get; }

        
        public Privacy(InfoDate date, char type)
        {
            Date = date;
            Type = type;
        }

        public static Privacy Build(string privacy)
        {
            string[] splits = privacy.Split(' ');
            InfoDate date = InfoDate.Build(splits[0]);
            char type = Convert.ToChar(splits[1]);

            return new Privacy(date, type);
        }

        internal static Privacy[] BuildArray(string[] privacies)
        {
            Privacy[] array = new Privacy[privacies.Length];

            for(int i = 0; i < privacies.Length; i++)
            {
                array[i] = Build(privacies[i]);
            }

            return array;
        }

        internal bool isPastLimit(InfoDate today, TermOfUsage term)
        {
            int months = term[this.Type];
            InfoDate destoryDate = new InfoDate(Date.Year, Date.Month + months, Date.Day);
            destoryDate.Calculate();

            return (today.ParseToDay() >= destoryDate.ParseToDay());
        }
    }
    public class Solution
    {
        public int[] solution(string today, string[] terms, string[] privacies)
        {
            InfoDate Today = InfoDate.Build(today);
            TermOfUsage Term = new TermOfUsage(terms);
            Privacy[] PrivacyArr = Privacy.BuildArray(privacies);

            int[] answer = GetPrivacyNumbersToDestory(Today, Term, PrivacyArr);

            Console.WriteLine();
            // 개인정보 n개
            // 약관 여러개 -> 유효기간 상이
            // 지났다면 반드시 파기
            // 모든 달은 28일까지 있음
            // 
            return answer;
        }

        private int[] GetPrivacyNumbersToDestory(InfoDate today, TermOfUsage term, Privacy[] privacyArr)
        {
            List<int> answer = new List<int>();

            for (int i = 0; i < privacyArr.Length; i++)
            {
                Privacy privacy = privacyArr[i];

                if (privacy.isPastLimit(today, term))
                {
                    answer.Add(i + 1);
                }
            }

            return answer.ToArray();
        }
    }
}
