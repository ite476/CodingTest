namespace 미로주행03
{
    public class BitArray2D
    {
        private List<long> bits;
        private int width;
        private int height;

        public BitArray2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            long numLongs = (width * height + 63) / 64;
            bits = new long[numLongs];
        }

        public void Set(int x, int y, bool value)
        {
            int index = y * width + x;
            int arrayIndex = index / 64;
            int bitIndex = index % 64;

            if (value)
            {
                bits[arrayIndex] |= (1L << bitIndex);
            }
            else
            {
                bits[arrayIndex] &= ~(1L << bitIndex);
            }
        }

        public bool Get(int x, int y)
        {
            int index = y * width + x;
            int arrayIndex = index / 64;
            int bitIndex = index % 64;
            return (bits[arrayIndex] & (1L << bitIndex)) != 0;
        }

        public void SetRowTrue(int row)
        {
            for (int x = 0; x < width; x++)
            {
                Set(x, row, true);
            }
        }

        internal void SetRowTrue(int startX, int endX, int y)
        {
            for (int x = startX; x < endX; x++)
            {
                Set(x, y, true);
            }
        }

        public int GetLength(int dimension)
        {
            return dimension == 0 ? width : height;
        }

        
    }
}