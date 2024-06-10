namespace Tools
{
    public class Average
    {
        private float[] all;
        private int counter = 0;

        public int count => all.Length;

        public float average
        {
            get
            {
                float sum = 0;
                int indexs = 0;
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i] != 0)
                    {
                        sum += all[i];
                        indexs++;
                    }
                }
                return sum / indexs;
            }
        }

        public int Counter
        {
            get
            {
                return counter;
            }
            private set
            {
                counter = value;
                if (counter >= all.Length)
                {
                    counter = 0;
                }
            }
        }

        public Average(int count = 5)
        {
            all = new float[count];
        }

        public void Clear()
        {
            for (int i = 0; i < all.Length; i++)
            {
                all[i] = 0;
            }
        }

        public void AddNext(float value)
        {
            all[Counter++] = value;
        }
    }
}