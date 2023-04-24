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
            BigInteger b = BigInteger.Pow(c1, x);
            //decimal m = (decimal)(1.0M / b);
            BigInteger m =BigInteger.Remainder(1 , b);
            //BigInteger w = BigInteger.Multiply(c2 ,(BigInteger)m) % q;
            return (int)m;

        }
    }
}
