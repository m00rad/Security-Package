using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        int r = 0;
        public string Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            string key = "";
            char[] alphabets =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g','h','i', 'j', 'k','l','m',
                'n', 'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w','x','y','z'
            };
            for (int i = 0; i < cipherText.Length; i++)
            {
                int z = 0;

                for (int j = Array.IndexOf(alphabets, plainText[i]); j < alphabets.Length; j++)
                {
                    if (alphabets[j] == cipherText[i])
                    {
                        key += alphabets[z];
                        break;
                    }
                    if (j == 25)
                    {
                        j = -1;
                        z++;
                        continue;
                    }
                    z++;
                }

            }
            r = key.IndexOf(plainText.Substring(0, 10));
            key = key.Remove(r);

            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            string plainText = "";
            char[] alphabets =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g','h','i', 'j', 'k','l','m',
                'n', 'o', 'p', 'q', 'r', 's', 't','u', 'v', 'w','x','y','z'
            };
            int isP_k_equal = Math.Abs(cipherText.Length - key.Length);
            for (int i = 0; i < cipherText.Length; i++)
            {
                int z = 0;

                for (int j = Array.IndexOf(alphabets, key[i]); j < alphabets.Length; j++)
                {
                    if (alphabets[j] == cipherText[i])
                    {
                        plainText += alphabets[z];
                        key += alphabets[z];
                        break;
                    }
                    if (j == 25)
                    {
                        j = -1;
                        z++;
                        continue;
                    }
                    z++;
                }

            }
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToLower();
            key = key.ToLower();
            string cipherText = "";
            char[] alphabets = { 'a', 'b', 'c', 'd', 'e', 'f', 'g','h','i', 'j', 'k', 'l', 'm', 'n', 'o', 'p','q'
            ,'r','s','t','u','v','w','x','y','z'};
            int isP_k_equal = Math.Abs(plainText.Length - key.Length);
            if (plainText.Length > key.Length)
            {
                key = key + plainText.Substring(0, plainText.Length - key.Length);
            }
            for (int i = 0; i < key.Length; i++)
            {
                int z = 0;

                for (int j = Array.IndexOf(alphabets, plainText[i]); j < alphabets.Length; j++)
                {
                    if (alphabets[z] == key[i])
                    {
                        cipherText += alphabets[j];
                        break;
                    }
                    if (j == 25)
                    {
                        j = -1;
                        z++;
                        continue;
                    }
                    z++;
                }

            }
            return cipherText;
        }
    }
}
