using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            return Encrypt(cipherText, key);
        }

        public override string Encrypt(string plainText, string key)
        {
            char[] s = new char[256];
            char[] t = new char[256];

            int f = 0;
            if (plainText.StartsWith("0x") && key.StartsWith("0x"))
            {
                f = 1;
                string ConvertHexStringToString(string hexString)
                {
                    var stringBuilder = new StringBuilder();
                    for (int i = 2; i < hexString.Length; i += 2)
                    {
                        int value = Convert.ToInt32(hexString.Substring(i, 2), 16);
                        stringBuilder.Append((char)value);
                    }
                    return stringBuilder.ToString();
                }

                plainText = ConvertHexStringToString(plainText);
                key = ConvertHexStringToString(key);
            }

            for (int i = 0; i < 256; i++)
            {
                s[i] = (char)i;
                t[i] = key[i % key.Length];
            }

            int j = 0;
            char temp;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;
                temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }

            char[] keys = new char[plainText.Length];
            int x = 0;
            int y = 0;
            string cipherText = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                x = (x + 1) % 256;
                y = (y + s[x]) % 256;
                temp = s[x];
                s[x] = s[y];
                s[y] = temp;
                keys[i] = s[(s[x] + s[y]) % 256];
                cipherText += (char)(plainText[i] ^ keys[i]);
            }

            if (f == 1)
            {
                cipherText = "0x" + string.Concat(cipherText.Select(v => $"{(int)v:x2}"));
            }

            return cipherText;
        }
    }
}
