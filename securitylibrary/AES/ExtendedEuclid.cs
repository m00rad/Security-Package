using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            //throw new NotImplementedException();
            int q;
            int i = 1;
            List<int> a1 = new List<int>();
            List<int> a2 = new List<int>();
            List<int> a3 = new List<int>();
            List<int> b1 = new List<int>();
            List<int> b2 = new List<int>();
            List<int> b3 = new List<int>();

            a1.Add(1);
            a2.Add(0);
            a3.Add(baseN);
            b1.Add(0);
            b2.Add(1);
            b3.Add(number);

            while (true)
            {
                q = a3[i - 1]/b3[i - 1];
                a1.Add(b1[i - 1]);
                a2.Add(b2[i - 1]);
                a3.Add(b3[i - 1]);
                b1.Add(a1[i - 1] - (q * b1[i - 1]));
                b2.Add(a2[i - 1] - (q * b2[i - 1]));
                b3.Add(a3[i - 1] - (q * b3[i - 1]));

                if (b3.Last() == 0)
                    return -1;
                else if (b3.Last() == 1)
                {
                    if (b2.Last() < 0)
                        return b2.Last() + baseN;
                    else
                        return b2.Last();
                }

                i++;
            }
        }
    }
}
