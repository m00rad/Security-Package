﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

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
        public static List<int> IP = new List<int>() {
            58 ,50,42,34,26,18,10,2,
            60   , 52   ,44    ,36   , 28   ,20 ,   12,    4,
            62   , 54   ,46    ,38    ,30   ,22,    14,    6,
            64    ,56   ,48    ,40    ,32   ,24 ,   16,    8,
            57    ,49   ,41   , 33    ,25  , 17 ,    9,    1,
            59    ,51   ,43   , 35    ,27  , 19  ,  11,    3,
            61    ,53   ,45   , 37    ,29 ,  21   , 13,    5,
            63    ,55   ,47   , 39    ,31 ,  23    ,15,    7};
        public static List<int> E_BIT = new List<int>(){
                 32 ,    1 ,   2 ,    3 ,    4 ,   5, 
                  4 ,    5 ,   6 ,    7 ,    8 ,   9,
                  8 ,    9 ,  10 ,   11 ,   12 ,  13,
                 12 ,   13 ,  14 ,   15 ,   16 ,  17,
                 16 ,   17 ,  18 ,   19 ,   20 ,  21,
                 20 ,   21 ,  22 ,   23 ,   24 ,  25,
                 24 ,   25 ,  26 ,   27 ,   28 ,  29,
                 28 ,   29 ,  30 ,   31 ,   32 ,   1
        };
        public static List<int> p =new List<int>()
        {
             16,   7,  20,  21,
            29,  12,  28,  17,
            1,  15,  23,  26,
             5,  18,  31,  10,
            2,   8,  24,  14,
            32,  27,   3,   9,
            19,  13,  30,   6,
            22,  11,   4, 25,
        };
        public static List<int> ipInverse = new List<int>()
        {
             40,8,48,16,56,24,64,32,
             39,7,47,15,55,23,63,31,
             38,6,46,14,54,22,62,30,
             37,5,45,13,53,21,61,29,
             36,4,44,12,52,20,60,28,
             35,3,43,11,51,19,59,27,
             34,2,42,10,50,18,58,26,
             33,1,41,9,49,17,57,25
        };
        public static int[,,] s={{
            { 14,  4,  13,  1,   2, 15,  11,  8,   3, 10,   6, 12,   5,  9,   0,  7},
            { 0, 15,   7,  4,  14,  2,  13,  1 , 10 , 6 , 12 ,11 ,  9 , 5  , 3 , 8 },
            { 4,  1,  14,  8,  13,  6,   2, 11,  15, 12,   9 , 7 ,  3 ,10 ,  5  ,0 },
            { 15, 12,   8,  2,   4,  9,   1,  7,   5, 11,   3, 14 , 10 , 0 ,  6 ,13 }},
            {
            { 15,  1,   8, 14,   6, 11,   3,  4,   9,  7,   2, 13,  12,  0,   5, 10 },
            { 3, 13,   4,  7,  15,  2,   8, 14,  12,  0,   1, 10,   6,  9,  11,  5 },
            { 0 ,14,   7, 11,  10,  4,  13,  1,   5,  8,  12,  6,   9,  3,   2, 15 },
            { 13,  8,  10,  1,   3, 15,   4,  2,  11,  6,   7, 12,   0,  5,  14,  9 }},
            {
            { 10,  0,   9, 14,   6,  3,  15,  5,   1, 13,  12,  7,  11,  4,   2,  8 },
            { 13,  7,   0,  9,   3,  4,   6, 10,   2,  8,   5, 14,  12, 11,  15,  1 },
            { 13,  6,   4,  9,   8, 15,   3,  0,  11,  1,   2, 12,   5, 10,  14,  7 },
            { 1, 10,  13,  0,   6,  9,   8,  7,   4, 15,  14,  3,  11,  5 ,  2, 12 }},
            {
            { 7, 13,  14,  3,   0,  6,   9, 10,   1,  2 ,  8 , 5 , 11 ,12,   4, 15, },
            { 13 , 8 , 11,  5,   6, 15  , 0 , 3 ,  4 , 7 ,  2 ,12 ,  1 ,10 , 14,  9 },
            { 10,  6 ,  9,  0,  12, 11 ,  7 ,13 , 15 , 1 ,  3, 14 ,  5 , 2 ,  8,  4 },
            { 3 ,15 ,  0 , 6 , 10 , 1 , 13 , 8 ,  9 , 4 ,  5, 11 , 12 , 7   ,2 ,14 }},
            {
            { 2, 12,   4,  1,   7, 10,  11,  6,   8,  5,   3, 15,  13,  0 , 14,  9 },
            { 14 ,11  , 2 ,12 ,  4 , 7 , 13 , 1  , 5 , 0 , 15, 10  , 3 , 9   ,8  ,6 },
            { 4,  2,   1, 11,  10, 13,   7,  8,  15,  9,  12 , 5 ,  6 , 3 ,  0, 14 },
            { 11,  8,  12,  7,   1, 14,   2, 13,   6, 15,   0,  9,  10,  4,   5,  3 }},
            {
            { 12,  1,  10, 15,   9,  2,   6,  8,   0, 13,   3 , 4 , 14,  7 ,  5 ,11 },
            { 10, 15,   4,  2 ,  7 ,12 ,  9,  5  , 6 , 1 , 13, 14 ,  0 ,11  , 3 , 8 },
            { 9 ,14 , 15,  5 ,  2 , 8 , 12 , 3  , 7 , 0 ,  4 ,10 ,  1 ,13 , 11,  6 },
            { 4,  3,   2, 12  , 9 , 5 , 15, 10,  11, 14,   1,  7 ,  6 , 0  , 8 ,13 }},
            {
            { 4, 11,   2, 14,  15,  0,   8, 13 ,  3 ,12 ,  9 , 7 ,  5, 10,   6,  1 },
            { 13,  0,  11,  7,   4,  9,   1, 10,  14,  3,   5, 12,   2, 15,   8,  6 },
            { 1 , 4 , 11 ,13 , 12,  3  , 7 ,14,  10, 15 ,  6 , 8,   0 , 5  , 9  ,2 },
            { 6 ,11 , 13 , 8 ,  1 , 4 , 10 , 7  , 9 , 5  , 0 ,15 , 14 , 2  , 3, 12 }},
            {
            { 13,  2,   8,  4,   6, 15,  11,  1,  10,  9,   3, 14,   5,  0,  12,  7  },
            { 1 ,15  ,13  ,8  ,10  ,3   ,7 , 4 , 12 , 5 ,  6, 11  , 0 ,14 ,  9 , 2 },
            { 7 ,11,   4,  1,   9, 12 , 14,  2 ,  0 , 6 , 10, 13,  15,  3,   5,  8 },
            { 2 , 1 , 14 , 7 ,  4, 10 ,  8 ,13 , 15, 12  , 9 , 0  , 3 , 5  , 6 ,11 }} };
          
        public override string Decrypt(string cipherText, string key)
        {
            string plainText = ApplyDes(cipherText, key, false);
            return plainText;
        }

        public override string Encrypt(string plainText, string key)
        {
            return ApplyDes(plainText, key,true);
        }
        public static string ApplyDes(string plainText, string key, bool Type)
        {
            keys = new List<string>();
            keys_withPerm = new List<string>();
            key = convert2binary(key);
            key = Apply_Permutation(key, pc_1);

            List<char> C0 = key.Substring(0, 28).ToList<char>();
            List<char> D0 = key.Substring(28, 28).ToList<char>();
            List<List<char>> C = generate_half_keys(C0);
            List<List<char>> D = generate_half_keys(D0);
            concatenate_keys(C, D, 0);
            int len = keys[0].Length;
            int len1 = keys_withPerm[0].Length;
            string M = convert2binary(plainText);
            string ip = Apply_Permutation(M, IP);
            string L0 = ip.Substring(0, 32);
            string R0 = ip.Substring(32, 32);
            string ipinverse;
            if (Type)
            {
                ipinverse = Apply_Permutation(encode_message(L0, R0, Type, 0), ipInverse);
            }
            else
            {
                ipinverse = Apply_Permutation(encode_message(L0, R0, Type, 15), ipInverse);
            }
            string Text = Convert.ToString(Convert.ToInt64(ipinverse.ToString(), 2), 16).ToUpper();

            return GetLeadingZeros(Text);
        }

        public static string convert2binary(string hex)
        {
            string binary = "";
            for (int i = 2 ; i < hex.Length; i++)
            {
                binary += Convert.ToString(Convert.ToInt32(hex[i].ToString(), 16), 2).PadLeft(4, '0');
            }
             
            return binary;
        }
        public static string XOR(string n1,string n2)
        {
            string res = "";
            for(int i =0;i<n1.Length;i++)
            {
                res += (int.Parse(n1[i].ToString()) ^ int.Parse(n2[i].ToString())).ToString();
            }
            
            return res;
        }

        public static string GetLeadingZeros(string Hex)
        {
            if (Hex.Length < 16)
            {
                string leadingZeros = "";
                for (int i = 0; i < 16 - Hex.Length; i++)
                {
                    leadingZeros += "0";
                }
                Hex = leadingZeros + Hex;
            }
            return "0x" + Hex.ToUpper();
        }
        public static string encode_message(string L0, string R0 ,bool Type, int rounds = 0)
        {
            int RoundCheck , RoundChange;
            string L_new = "", R_new = "";
            if (Type)
            {
                RoundCheck = 16;
                RoundChange = 1;

            }
            else
            {
                RoundCheck = -1;
                RoundChange = -1;
            }
            if (rounds == RoundCheck)
            {
                string result = R0 + L0;
                return result;
            }
            R_new = Apply_Permutation(R0.ToString(), E_BIT);
            L_new = R0.ToString();
            R_new = XOR(keys_withPerm[rounds], R_new);
            R_new = split_parts(R_new);
            R_new = XOR(R_new, L0);
            return encode_message(L_new, R_new, Type, rounds + RoundChange);

        }
        public static string Apply_Permutation(string key,List<int>pc)
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
        public static string split_parts(string num)
        {
            string res="";
            string sub, row_num, col_num;
            string o;
            int row_index, col_index;
            for(int i =0;i<8;i++)
            {
                sub = num.Substring(i*6, 6);
                
                row_num = sub[0].ToString() + sub[5].ToString();
                col_num = sub.Substring(1, 4);
                row_index = Convert.ToInt32(row_num, 2);
                col_index = Convert.ToInt32(col_num,2);
                o = Convert.ToString(Convert.ToInt32(s[i, row_index, col_index].ToString(),10),2).PadLeft(4,'0');
                res += o;
            }
            res = Apply_Permutation(res, p);
            return res;
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
            keys_withPerm.Add(Apply_Permutation(key, pc_2));
            concatenate_keys(C, D, counter);
            return null;
        }
    }
}
