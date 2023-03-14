using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            
            return null;
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            int arr_sqrt = (int)Math.Sqrt(key.Count);
            List<int> key_inv = new List<int>();
            key_inv = calc_inverse(key);
            List<int> decrypted_list = Encrypt(cipherText, key_inv); 

           return decrypted_list;
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int temp = 0;
            int j = 0;
            int sqrt = (int)Math.Sqrt(key.Count);
            List<int> cipher_text = new List<int>();
            for(int i = 0; ; i++)
            {
                if (plainText.Count == 0)
                {
                    return cipher_text;
                }
                temp += key[i] * plainText[j];
                j++;
                if (j==sqrt)
                {
                    temp = calc_mod(temp,26);
                    cipher_text.Add(temp);
                    temp = 0;
                    j = 0;
                    if (i == key.Count - 1)
                    {
                        for (int x = 0; x < sqrt; x++)
                        {
                            plainText.RemoveAt(0);
                        }
                        i = -1;
                    }
                }
            }
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            List<int> plain_inv = calc_inverse(plainText);
            List<int> cipher_t = matrix_t(convert2_arr(cipherText, (int)Math.Sqrt(cipherText.Count)));
            List<int> key = Encrypt(cipher_t, plain_inv);
            return key;
        }
        public void ShowList(List<int> list, string text = "")
        {
            Console.Write("{0} List : \t", text);
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} \t", list.ElementAt(i));
            }
            Console.WriteLine();
        }
        public int[,] convert2_arr(List<int>list,int sqrt)
        {
            int[,] list_arr = new int[sqrt, sqrt];
            for (int i = 0; i < sqrt; i++)
            {
                for (int j = 0; j < sqrt; j++)
                {
                    list_arr[i, j] = list[0];
                    list.RemoveAt(0);
                }
            }
            return list_arr;
        }
        public int calc_det(int[,] arr,int arr_sqrt)
        {
            int det = 0;
            if(arr_sqrt == 2)
            {
                det = (arr[0, 0] * arr[1, 1]) - (arr[0, 1] * arr[1, 0]);
            }
            else 
            {
                for(int i = 0; i < arr_sqrt; i++)
                {
                    det += (arr[0, i] * (arr[1, (i + 1) % arr_sqrt] * arr[2, (i + 2) % arr_sqrt]
                        - arr[1, (i + 2) % arr_sqrt] * arr[2, (i + 1) % arr_sqrt]));
                }
                det = calc_mod(det, 26);
            }
            return det;
        }
        public int[,] calc_adj(int[,] arr,int arr_sqrt)
        {
            int[,] adj = new int[arr_sqrt, arr_sqrt];
            for(int i = 0; i < arr_sqrt; i++)
            {
                for(int j = 0; j < arr_sqrt; j++)
                {
                   adj[i,j] = (arr[(i+1)%arr_sqrt, (j + 1) % arr_sqrt] * arr[(i+2)%arr_sqrt, (j + 2) % arr_sqrt]
                       - arr[(i+1)%arr_sqrt, (j + 2) % arr_sqrt] * arr[(i+2)%arr_sqrt, (j + 1) % arr_sqrt]);
                }
            }

            //transpose the matrix 
            int temp = 0;
            for (int i = 0; i < arr_sqrt; i++)
            {
                temp = adj[i, (i + 1) % arr_sqrt];
                adj[i, (i + 1) % arr_sqrt] = adj[(i + 1) % arr_sqrt, i];
                adj[(i + 1) % arr_sqrt, i] = temp;
            }

            return adj;
        }
        public List<int>calc_inverse(List<int>list)
        {
            List<int> arr_inv = new List<int>();
            int sqrt = (int)Math.Sqrt(list.Count);
            int[,] arr = convert2_arr(list,sqrt);
            int det = calc_det(arr,sqrt);
            if (sqrt == 2)
            {
                int temp = 0;
                temp = arr[0, 0];
                arr[0, 0] = arr[1, 1];
                arr[1, 1] = temp;
                arr[0, 1] = -1 * arr[0, 1];
                arr[1, 0] = -1 * arr[1, 0];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        arr_inv.Add(arr[i, j] / det);
                    }
                }
            }
            else
            {
                int c = calc_c(det);
                int b = 26 - c;
                int[,] adj = calc_adj(arr, sqrt);
                for (int i = 0; i < sqrt; i++)
                {
                    for (int j = 0; j < sqrt; j++)
                    {
                        arr_inv.Add(calc_mod((b * adj[i, j]), 26));
                    }
                }
            }
            return arr_inv;
        }
        public int calc_c(int det)
        {
            int x = 26 - det;
            for (int i = 0; i < 26; i++)
            {
                if (calc_mod((i*x),26) == 1)
                {
                    return i;
                }
            }
            return -1;
        }
        public int calc_mod(int num,int n)
        {
            if (num >= 0)
            {
                return num % n;
            }
            else
            {
                return (n - ((-1)*num % n));
            }
        }
        public List<int> matrix_t (int[,] arr)
        {
            List<int> list = new List<int>();
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    list.Add(arr[j,i]);
                }
            }
            return list;
        }

    }
}
