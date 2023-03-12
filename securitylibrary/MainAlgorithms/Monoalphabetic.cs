using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        string alpha = "abcdefghijklmnopqrstuvwxyz";
        public int Letter_Index(char letter)
        {
            for (int i = 0; i < alpha.Length; i++)
            {
                if (letter == alpha[i])
                    return i;
            }
            return 0;
        }
        public string Analyse(string plainText, string cipherText)
        {
            List<int> z = new List<int>();
            int plain;
            string key = "";
            string list = "";
            List<char> x = plainText.Distinct().ToList();
            List<char> y = cipherText.ToLower().Distinct().ToList();

            SortedDictionary<char, char> test = new SortedDictionary<char, char>();//(plaintext,cypher)

            string CIPHERText = cipherText.ToLower();
            for (int i = 0; i < x.Count; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (x[i] == alpha[j])
                    {
                        test.Add(x[i], y[i]);
                        //    key += y[i];
                    }
                }
            }
            if (key.Length < 26)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (!test.ContainsKey(alpha[i]))
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if (!test.ContainsValue(alpha[j]))
                            {
                                test.Add(alpha[i], alpha[j]);
                                j = 26;
                            }
                        }
                    }
                }
            }
            foreach (var item in test)
            {
                key += item.Value;
            }
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string plainText = "";
            string CIPHERText = cipherText.ToLower();
            for (int i = 0; i < CIPHERText.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (CIPHERText[i] == key[j])
                    {
                        plainText += alpha[j];
                    }
                }
            }
            return plainText;
        }
        public string Encrypt(string plainText, string key)

        {
            string cyphertext = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (plainText[i] == alpha[j])
                    {
                        cyphertext += key[j];
                    }
                }
            }
            return cyphertext;
        }
        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            //Dictionary <string, double>cities = new Dictionary<string, double>() {-
            // {  "E",12.51} ,   {"T",9.25},{"A",8.04},{ "O",7.60 },{"I",7.26 },{ "N",7.09} ,{"S",6.54 }, {"R",6.12 },{ "H",5.49},{"L",4.14 },{"D",3.99 },{"C",3.06 },{"U",2.71 },{ "M",2.53 } ,{"F",2.30 } ,{"P",2.00 } ,{"G",1.96 } , {"W",1.92},     {"Y",1.73 },{"B",1.54 } ,        {"V",0.99 }  ,      {"K",0.67 },         {"X",0.19 },         {"J",0.16 },       { "Q",0.11 }    ,   {"Z",0.09} 
            // };
            int count = 0;
            cipher = cipher.ToLower();
            string key = "";
            Dictionary<char, int> frequencyAlphabiticsDictionary = new Dictionary<char, int>();
            SortedDictionary<char, char> keydictionary = new SortedDictionary<char, char>();
            string frequencyalphabitics = "ETAOINSRHLDCUMFPGWYBVKXJQZ".ToLower();
            int j = 0;
            while (j < cipher.Length)
            {
                if (frequencyAlphabiticsDictionary.ContainsKey(cipher[j]))
                {
                    frequencyAlphabiticsDictionary[cipher[j]]++;
                }
                else
                {
                    frequencyAlphabiticsDictionary.Add(cipher[j], 0);
                }
                j++;
            }
            frequencyAlphabiticsDictionary = frequencyAlphabiticsDictionary
                .OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in frequencyAlphabiticsDictionary)
            {
                keydictionary.Add(item.Key, frequencyalphabitics[count]);
                count++;
            }
            for (int i = 0; i < cipher.Length; i++)
            {
                key = key + keydictionary[cipher[i]];
            }
            return key;
        }
    }
}

