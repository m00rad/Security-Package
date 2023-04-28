using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{

    public class DiffieHellman
    {


        public int Power(int a, int b, int c)
        {
            if (b == 0)
            {
                return 1;
            }
            else if (b % 2 == 0)
            {
                int temp = Power(a, b / 2, c);
                return (temp * temp) % c;
            }
            else
            {
                int temp = Power(a, b, c);
                return temp % c;
            }
        }

        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int user1 = Power(alpha, xa, q);
            int user2 = Power(alpha, xb, q);

            List<int> keys = new List<int>()
             {
                Power(user2, xa, q),
                Power(user1, xb, q)
            };

            return keys;
        }
    }
}