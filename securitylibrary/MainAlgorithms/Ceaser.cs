using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
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
        public string Encrypt(string plainText, int key)
        {
            char[] characters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] buffer = plainText.ToCharArray();
            char[] charsofciphertext = new char[plainText.Length];
            char[] letters = alpha.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (buffer[i] == letters[j])
                    {
                        int newbit = (key + j) % 26;
                        char newchar = characters[newbit];
                        charsofciphertext[i] = newchar;
                    }
                }
            }
            string cipherMessage = String.Join("", charsofciphertext);
            return cipherMessage;
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] characters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] buffer = cipherText.ToLower().ToCharArray();
            char[] charsofplaintext = new char[cipherText.ToLower().Length];
            char[] letters = alpha.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (buffer[i] == letters[j])
                    {
                        int newbit = (j - key) % 26;
                        if (newbit < 0)
                        {
                            newbit += 26;
                        }
                        char newchar = characters[newbit];
                        charsofplaintext[i] = newchar;
                    }
                }
            }
            string plainMessage = String.Join("", charsofplaintext);
            return plainMessage;
        }
        public int Analyse(string plainText, string cipherText)
        {
            int key = (Letter_Index(char.ToLower(cipherText[0])) - Letter_Index(plainText[0]));
            if (key < 0)
            {
                key = key + 26;
            }
            else
            {
                key = key % 26;
            }
            return key;
        }
    }
}
