public static class Extensions
{

    public static bool IsPossible_BinaryTreeNumber(this long value)
    {
        string binNum = value.ToString_OfPerfectBinaryTree();
        BinaryNode node = new BinaryNode(binNum);

        //Console.WriteLine(binNum);
        //Console.WriteLine($"root : {node.Value}");
        return node.IsPerfectBinaryTree_LogicallyCorrect();
    }

    public static string ToString_OfPerfectBinaryTree(this long value)
    {
        long valueNow = value;
        string result = "";

        while (valueNow > 0)
        {
            result = (valueNow & 1) + result ;
            valueNow = valueNow >> 1;
        }
        //Console.WriteLine($"string made of {value} : {result}");

        int powers = 0;
        while (result.Length > (int)Math.Pow(2,powers) - 1)
        {
            powers++;
        }

        int targetLength = (int)Math.Pow(2, powers) - 1;
        //Console.WriteLine($"Target : {targetLength}");
        while (result.Length < targetLength)
        {
            result = "0" + result;
        }

        return result;
    }

    /*public static bool IsPossibleBinaryTree(this string value)
    {
        return CheckPossibleBinaryTree_ForEveryRootNode(value);        

        int length = value.Length;
        int rootIndex = ((length - 1) / 2);
        // 2 => 0 
        // 1 1
        // R

        // 3 => 1 
        // 1 1 1
        //   R

        // 4 => 1 
        // 1 1 1 1
        //   R
        string leftChild = value.Substring(0, rootIndex);
        string rightChild = value.Substring(rootIndex + 1);
        
        char rootValue = value[rootIndex];

        Console.WriteLine($"{value} => {leftChild},{rootValue},{rightChild}");

        return rootValue == '1'
            && leftChild.IsPerfectBinaryNumber()
            && rightChild.IsPerfectBinaryNumber();
    }*/

    /*private static bool CheckPossibleBinaryTree_ForEveryRootNode(string value)
    {
        int cursor = 0;
        while (cursor < value.Length)
        {
            if (value[cursor] == '1')
            {
                string rightChild = value.Substring(cursor + 1);
                string leftChild = value.Substring(0, cursor);

                if (rightChild.IsPerfectBinaryNumber() 
                    || (leftChild.Length < rightChild.Length 
                        || (leftChild.Length == rightChild.Length 
                            && leftChild.IsPerfectBinaryNumber()) 
                        ) 
                    )
                {
                    return true;
                }
            }
            cursor++;
        }

        return false;
    }*/

    /*public static bool IsPerfectBinaryNumber(this string value)
    {
        BinaryNode root = new BinaryNode(value);

        return root.IsPerfectBinaryTree_LogicallyCorrect();
    }

    public static bool IsCountOfPerfectBinaryTree(this int value)
    {
        value++;
        while(value > 1)
        {
            if ((value & 1) == 1) return false;
            value >>= 1;
        }
        return true;
    }*/
}