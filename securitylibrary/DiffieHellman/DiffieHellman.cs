using System.Collections.Generic;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int user1 = power(alpha, xa, q);
            int user2 = power(alpha, xb, q);

            List<int> keys = new List<int>()
            {
                power(user2, xa, q),
                power(user1, xb, q)
            };
            return keys;
        }
        public int power(int a, int b, int c)
        {
            int sum = 1;
            int i = 0;
            while (i < b)
            {
                sum = (sum * a) % c;
                i++;
            }
            return sum;
        }
    }
}