using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            BigInteger c1, c2;
            BigInteger K = (BigInteger.Pow(y, k) % q);    
            c1 = BigInteger.Pow(alpha, k) % q;
            c2 = K * m % q;
            List<long> list = new List<long> { (long)c1,(long)c2};
            return list;

        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            BigInteger k = BigInteger.Pow(c1, x)%q;
            BigInteger gcd = BigInteger.GreatestCommonDivisor(k, q);
            if(gcd!=1)
            {
                return 0;
            }
            int x1 = 1, x2 = 0, y1 = 0, y2 = 1,q1 = q;
            while (q1 != 0)
            {
                double c = Math.Floor((double)(k / q1));
                double r = (double)k % q1;
                k = q1;
                q1 = (int)r;
                int x3 = x1 - (int)c * x2;
                x1 = x2;
                x2 = x3;
                int y3 = y1 - (int)c * y2;
                y1 = y2;
                y2 = y3;
            }
            int m = (c2 * x1) % q;
            return m;

        }
    }
}
