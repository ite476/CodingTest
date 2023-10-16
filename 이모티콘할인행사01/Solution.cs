using System;

public class Solution
{
    public int[] solution(int[,] users, int[] emoticons)
    {
        User[] userArr = User.GetUsers(users);
        SalesOption sales = new SalesOption(emoticons);

        int[] answer = sales.GetMaxPlusUsers_AndHighestSales(userArr);

        return answer;
    }
}

public class User
{
    public int DiscountThreshold;
    public int EmoticonPlusThreshold;
    public int TotalPurchase;
    public bool IsPlusUser;

    public void Reset()
    {
        this.TotalPurchase = 0;
        this.IsPlusUser = false;
    }

    public User(int discountThreshold, int emoticonPlusThreshold)
    {
        DiscountThreshold = discountThreshold;
        EmoticonPlusThreshold = emoticonPlusThreshold;
    }

    public static User[] GetUsers(int[,] users)
    {
        int count = users.GetLength(0);
        User[] userArray = new User[count];
        for(int i = 0; i < count; i++)
        {
            userArray[i] = new User(users[i, 0], users[i, 1]);
        }
        return userArray;
    }

    public bool DecidesToPurchase(int ratio)
    {
        return ratio >= DiscountThreshold;
    }

    public bool HasPurchasedToMuch()
    {
        return TotalPurchase >= EmoticonPlusThreshold;
    }
}

public class SalesOption
{
    int[] Emoticons;
    int[] DiscountIndexes;
    int[] DiscountAmmount = new int[4]{
        10,20,30,40
    };
    public SalesOption(int[] emoticons)
    {
        this.Emoticons = emoticons;
        DiscountIndexes = new int[emoticons.Length];

        /*PrintPrices(emoticons);*/
    }
    /*public void PrintPrices(int[] emoticons)
    {
        int length = emoticons.Length;

        Console.Write("{ ");
        for (int i = 0; i < length; i++)
        {
            Console.Write($"{emoticons[i]}" + (i < length - 1 ? ", " : ""));
        }
        Console.WriteLine(" }");
    }*/



    public int[] GetMaxPlusUsers_AndHighestSales(User[] userArr)
    {
        int[] result = new int[2];
        do
        {
            //PrintIndex();
            int EmoPlus = 0;
            int EmoSales = 0;
            int userCount = userArr.Length;
            int emoCount = Emoticons.Length;
            for (int x = 0; x < emoCount; x++)
            {
                int EmoPrice = GetEmoPrice(x);

                for (int y = 0; y < userCount; y++)
                {
                    User user = userArr[y];

                    ResetIfFirstRun(x, user);
                    if (user.IsPlusUser) { continue; }

                    if (user.DecidesToPurchase(GetDiscountRatio(x)))
                    {
                        user.TotalPurchase += EmoPrice;
                        if (user.HasPurchasedToMuch())
                        {
                            user.IsPlusUser = true;
                            user.TotalPurchase = 0;
                            continue;
                        }
                    }
                }
            }

            (EmoPlus, EmoSales) = GetEmoPlus_AndEmoSales(userArr);

            //Console.WriteLine($" => {EmoPlus},{EmoSales}");
            if (MeetsPolicy(result, EmoPlus, EmoSales))
            {
                result[0] = EmoPlus;
                result[1] = EmoSales;
            }
        }
        while (NextIndex());



        return result;
    }

    private static bool MeetsPolicy(int[] result, int EmoPlus, int EmoSales)
    {
        return EmoPlus > result[0]
            || (
                (EmoPlus == result[0]) && (EmoSales > result[1])
                );
    }

    private (int , int) GetEmoPlus_AndEmoSales(User[] userArr)
    {
        int EmoPlus = 0;
        int EmoSales = 0;

        for (int i = 0; i < userArr.Length; i++)
        {
            User user = userArr[i];
            EmoPlus += user.IsPlusUser ? 1 : 0;
            EmoSales += user.TotalPurchase;
        }

        return (EmoPlus, EmoSales);
    }

    private static void NewMethod(User[] userArr, ref int EmoPlus, ref int EmoSales)
    {
        
    }

    private static void ResetIfFirstRun(int x, User user)
    {
        if (x == 0) { user.Reset(); }
    }

    /*private void PrintIndex()
    {
        int length = Emoticons.Length;
        for (int i = 0; i < length; i++)
        {
            Console.Write(DiscountIndexes[i] + ((i != length - 1)? ", " : ""));
        }
    }*/

    private int GetEmoPrice(int x)
    {
        return Emoticons[x] * (100 - GetDiscountRatio(x)) / 100;
    }

    private int GetDiscountRatio(int x)
    {
        return DiscountAmmount[DiscountIndexes[x]];
    }

    private bool NextIndex()
    {
        int length = Emoticons.Length;
        DiscountIndexes[0] += 1;
        for (int i = 0; i < length; i++)
        {
            if (DiscountIndexes[i] < 4)
            {
                break;
            }

            if (i == length - 1)
            {
                return false;
            }

            DiscountIndexes[i] = 0;
            DiscountIndexes[i + 1]++;
        }

        return true;
    }
}