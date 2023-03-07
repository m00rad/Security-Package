using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string KeyStream, TestKeyStream, Key = "";
            int PlainTextSize = plainText.Length;
            KeyStream = Decrypt(cipherText, plainText); 
            for (int i = 1; i <= PlainTextSize; i++) 
            {
                Key = KeyStream.Substring(0, i);
                TestKeyStream = GenerateRepeatingkey(Key, PlainTextSize);
                if (TestKeyStream.Equals(KeyStream))
                    return Key;
            }
            return Key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string PlainText = "";
            Char PlainLetter;
            int CharASCIIValue;
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            key = GenerateRepeatingkey(key, cipherText.Length);
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (key[i] == cipherText[i])                 // if the two letters are same
                {
                    PlainLetter = (Char)97;                  // ASCII value of letter 'a'
                }
                else if (key[i] < cipherText[i])             // if the ASCII value of Key's is less than the ASCII of Cipher letter 
                {
                    PlainLetter = (Char)(97 + cipherText[i] - key[i]);
                }
                else                                         // the ASCII value of Cipher is less than the ASCII of Key letter
                {
                    CharASCIIValue = 122 - (0 + key[i] - cipherText[i]) + 1;
                    PlainLetter = (Char)CharASCIIValue;
                }
                PlainText += PlainLetter.ToString();
            }
            return PlainText.ToLower();
        }

        public string Encrypt(string plainText, string key)
        {
            string CipherText = "";
            Char CipherLetter ;
            plainText = plainText.ToLower();
            key = GenerateRepeatingkey(key, plainText.Length);
            for (int i = 0; i < plainText.Length; i++)
            {
                CipherLetter = (Char)(97 + (((0 + plainText[i] + key[i]) % 97) % 26));
                CipherText += CipherLetter.ToString();
            }
            return CipherText.ToUpper();
        }

        public string GenerateRepeatingkey(string Key,int TextSize)
        {
            int KeySize = Key.Length;
            if (KeySize != TextSize)
            {
                for (int i = 0; i < TextSize - KeySize; i++)
                {
                    Key += Key[i % KeySize].ToString();
                }
            }
            return Key.ToLower();
        }
    }
}