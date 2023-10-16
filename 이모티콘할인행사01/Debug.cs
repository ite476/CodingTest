using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 이모티콘할인행사01
{
    public static class DebugClass
    {
        public static void PrintPrices(int[] emoticons)
        {
            int length = emoticons.Length;

            Console.Write("{ ");
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{emoticons[i]}" + (i < length - 1 ? ", " : ""));
            }
            Console.WriteLine(" }");
        }
    }
}
