using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            //throw new NotImplementedException();
            int n = p * q;
            int c = calc_mod(M, e, n);
            return c;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            //throw new NotImplementedException();
            int n = p * q;
            int euler = (p - 1) * (q - 1);
            int d = calc_e_inve(e, euler);
            int P = calc_mod(C, d, n);
            return P;
        }
        public int calc_e_inve(int e,int euler)
        {
            int i = euler;
            int j = e;
            int tmp;
            List<int>tmp_list= new List<int>();
            while (j > 0)
            {
                tmp_list.Add(i / j);
                tmp = j;
                j = i - ((i/j)* j);
                i = tmp;
            }
            for(int x = tmp_list.Count - 2; x >= 0; x--)
            {
                tmp = i;
                i = tmp_list[x] * i + j;
                j = tmp;
            }
            int res = (euler * j) - (e * i);
            if (res == 1)
            {
                return euler - (i % euler);
            }
            return i;
        }
        public int calc_mod(int M,int e,int n)
        {
            int res = 1;
            for(int i = 0; i < e; i++)
            {
                res = (res * M) % n;
            }
            return res;
        }
    }
}
