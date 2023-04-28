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
            double c1, c2;
            double K = PowerWithMod(y, k,q) ;    
            c1 = PowerWithMod(alpha, k,q);
            c2 = K * m % q;
            List<long> list = new List<long> { (long)c1,(long)c2};
            return list;

        }
        public int PowerWithMod(int Base, int Power, int Mod)
        {
            int result= 1;
            for (int i = 0; i < Power; i++)
            {
                result *= Base;
                result %= Mod;
            }
            return result;
        }
        public int GCD(int Num1, int Num2)
        {
            int result = 1;
            int loops;
            if (Num1 > Num2)
                loops = Num1 / 2;
            else
                loops = Num2 / 2;
            for (int Divisor = 2; Divisor <= loops; Divisor++)
            {
                if (Num1 % Divisor == 0 && Num2 % Divisor == 0)
                    result = Divisor;
            }
            return result;
        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            int k = PowerWithMod(c1, x, q);
            int gcd = GCD(k, q);
            if (gcd != 1)
            {
                return 0;
            }
            int x1 = 1, x2 = 0, x3, y1 = 0, y2 = 1, y3, q1 = q, c, r;
            while (q1 != 0)
            {
                c = (int)Math.Floor(((double)k / q1));
                r = k % q1;
                k = q1;
                q1 = r;
                x3 = x1 - c * x2;
                x1 = x2;
                x2 = x3;
                y3 = y1 - c * y2;
                y1 = y2;
                y2 = y3;
            }
            return (c2 * x1) % q; //int m = (c2 * x1) % q;
        }

    }
    
}
