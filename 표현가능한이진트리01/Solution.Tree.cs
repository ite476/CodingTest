
public class BinaryTree
{
    public int Count { get; set; } = 0;
    public List<BinaryNode> Data { get; set; }
    public BinaryNode RootNode { get; set; }


    public BinaryTree(long[] input)
    {
        RootNode = BinaryNode.NormalNode;
        //RootNode.AddChild(BinaryNode.NormalNode);

        int length = input.Length;
        //Data = new BinaryNode[length];
        for (int i = 0; i < length; i++)
        {

        }
    }

    public BinaryTree(string value)
    {
    }

    public BinaryTree Saturate()
    {

        return this;
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }

    private int index;
    private int lastIndex;

    private string ReadFromLeftToRight()
    {
        index = 0;
        string result = "";
        while (Reading())
        {
            result += (data[index] != BinaryTree.Dummy) ? "1" : "0";
        }
        return result;
    }

    public long[] data;

    public static long Dummy = -1;

    private bool Reading()
    {
        bool result = index <= lastIndex;
        index++;
        return result;
    }
}



