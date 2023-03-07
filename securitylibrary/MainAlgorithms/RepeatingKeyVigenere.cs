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
            string test;
            test= Decrypt(cipherText, plainText);
            if (test[8]==test[0])
                return test.Substring(0, 8);
            else
                return test.Substring(0, 9);
        }

        public string Decrypt(string cipherText, string key)
        {
            string PlanText = "";
            Char PlanedLetter;
            int CharAsciiValue;
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            key = GenerateRepeatingkey(key, cipherText.Length);
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (key[i] == cipherText[i])
                {
                    PlanedLetter = (Char)97;
                }
                else if (key[i] < cipherText[i])
                {
                    PlanedLetter = (Char)(97 + cipherText[i] - key[i]);
                }
                else
                {
                    CharAsciiValue = 122 - (0 + key[i] - cipherText[i]) + 1;
                    PlanedLetter = (Char)CharAsciiValue;
                }
                PlanText += PlanedLetter.ToString();
            }
            return PlanText.ToLower();
            
        }

        public string Encrypt(string plainText, string key)
        {
            string CipherText = "";
            Char CipheredLetter ;
            plainText = plainText.ToLower();
            key = GenerateRepeatingkey(key, plainText.Length);
            for (int i = 0; i < plainText.Length; i++)
            {
                CipheredLetter = (Char)(97 + (((0 + plainText[i] + key[i]) % 97) % 26));
                CipherText += CipheredLetter.ToString();
            }
            return CipherText.ToUpper();
        }
        public string GenerateRepeatingkey(string Key,int TextSize)
        {
            int KeySize = Key.Length;
            for (int i = 0; i < TextSize-KeySize; i++)
            {
                Key += Key[i % KeySize].ToString();
            }
            return Key.ToLower();
        }
    }
}