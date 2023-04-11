using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {
        public static List<string> keys = new List<string>();
        public static List<string> keys_withPerm = new List<string>();
        public static List<int> pc_1 = new List<int>() {57,49,41,33,25,17,9,
                                            1,58,50,42,34,26,18,
                                            10,2,59,51,43,35,27,
                                            19,11,3,60,52,44,36,
                                            63,55,47,39,31,23,15,
                                            7,62,54,46,38,30,22,
                                            14,6,61,53,45,37,29,
                                            21,13,5,28,20,12,4};
        public static List<int> pc_2 = new List<int>() {14,17,11,24,1,5,
                                               3,28,15,6,21,10,
                                               23,19,12,4,26,8,
                                               16,7,27,20,13,2,
                                               41,52,31,37,47,55,
                                               30,40,51,45,33,48,
                                               44,49,39,56,34,53,
                                               46,42,50,36,29,32};
        public static List<int> IP = new List<int>() {58,50,42,34,26,18,10,2,
            60   , 52   ,44    ,36   , 28   ,20 ,   12,    4,
            62   , 54   ,46    ,38    ,30   ,22,    14,    6,
            64    ,56   ,48    ,40    ,32   ,24 ,   16,    8,
            57    ,49   ,41   , 33    ,25  , 17 ,    9,    1,
            59    ,51   ,43   , 35    ,27  , 19  ,  11,    3,
            61    ,53   ,45   , 37    ,29 ,  21   , 13,    5,
            63    ,55   ,47   , 39    ,31 ,  23    ,15,    7};
        
        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            key = convert2binary(key);
            key = permutation(key, pc_1);

            List<char> C0 = new List<char>();
            List<char> D0 = new List<char>();
            for (int i = 0; i < key.Length; i++)
            {
                if (i < key.Length / 2)
                    C0.Add(Convert.ToChar(key[i]));
                else
                    D0.Add(Convert.ToChar(key[i]));
            }
            List<List<char>> C = generate_half_keys(C0);
            List<List<char>> D = generate_half_keys(D0);
            concatenate_keys(C, D,0);
            int len = keys[0].Length;
            int len1 = keys_withPerm[0].Length;
            string M = convert2binary(plainText);
            string ip = permutation(M, IP);
            return null;
        }
        public static string convert2binary(string hex)
        {
            string binary = "";
            for (int i = 2; i < hex.Length; i++)
            {
                binary += Convert.ToString(Convert.ToInt32(hex[i].ToString(), 16), 2).PadLeft(4, '0');
            }
            return binary;
        }
        public static string permutation(string key,List<int>pc)
        {
            string new_key = "";
            for (int i = 0; i < pc.Count; i++)
            {
                new_key += key[pc[i]-1];
            }
            return new_key;
        }
        public static List<List<char>> generate_half_keys(List<char>half_key)
        {

            List<List<char>> ret_list = new List<List<char>>();
            char temp;
            for(int i = 1; i <= 16; i++)
            {
                if (i == 1 || i == 2 || i == 9 || i == 16)
                {
                    temp = half_key[0];
                    half_key.RemoveAt(0);
                    half_key.Add(temp);
                    ret_list.Add(new List<char>(half_key));                   
                }
                else
                {
                    temp = half_key[0];
                    half_key.RemoveAt(0);
                    half_key.Add(temp);
                    temp = half_key[0];
                    half_key.RemoveAt(0);
                    half_key.Add(temp);
                    ret_list.Add(new List<char>(half_key));
                }
            }
            return ret_list;
        }
        
        public static List<string> concatenate_keys(List<List<char>> C, List<List<char>> D,int counter)
        {
            string key = "";
            if (counter == 16)
            {
                return keys;
            }
            for (int j = 0; j < C[counter].Count; j++)
            {
                key += C[counter][j];

            }
            for (int j = 0; j < C[counter].Count; j++)
            {
                key += D[counter][j];
            }
            counter++;
            keys.Add(key);
            keys_withPerm.Add(permutation(key, pc_2));
            concatenate_keys(C, D, counter);
            return null;
        }
    }
}
