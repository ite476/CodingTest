
public class BinaryNode
{
    public static BinaryNode Dummy = new BinaryNode(0);
    public static BinaryNode NormalNode = new BinaryNode(1);

    public long Value { get; set; }
    public BinaryNode LeftChild { get; set; } = null;
    public BinaryNode RightChild { get; set; } = null;

    public BinaryNode(long value, BinaryNode leftChild = null, BinaryNode rightChild = null)
    {
        Value = value;
        LeftChild = leftChild;
        RightChild = rightChild;
    }

    public BinaryNode(string value)
    {
        int length = value.Length;
        int rootIndex = (length - 1) / 2;

        this.Value = value[rootIndex] - '0';
        if (length > 1)
        {
            this.LeftChild = new BinaryNode(value.Substring(0, rootIndex));
            this.RightChild = new BinaryNode(value.Substring(rootIndex + 1));
        }
    }

    public bool IsPerfectBinaryTree_LogicallyCorrect()
    {
        if (this.Value == BinaryNode.Dummy.Value)
        {
            return this.HasValue_BySelfOrBelow() == false;
        }
        
        return (this.LeftChild?.IsPerfectBinaryTree_LogicallyCorrect() ?? true)
            && (this.RightChild?.IsPerfectBinaryTree_LogicallyCorrect() ?? true);
    }

    private bool HasValue_BySelfOrBelow()
    {
        return this.Value == BinaryNode.NormalNode.Value
            || (this.LeftChild?.HasValue_BySelfOrBelow() ?? false)
            || (this.RightChild?.HasValue_BySelfOrBelow() ?? false);
    }


    /*public int CountSelfAndBelow()
    {
        int leftcount = LeftChild?.CountSelfAndBelow() ?? 0;
        int rightcount = RightChild?.CountSelfAndBelow() ?? 0;
        return leftcount + rightcount + 1;
    }*/
}

