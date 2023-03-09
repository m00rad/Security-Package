using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            string plainText = null;
            string[,] arr = new string[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    arr[i, j] = "";
            }

            HashSet<char> keys = new HashSet<char>();
            for (int i = 0; i < key.Length; i++)
            {
                keys.Add(key[i]);
            }

            int index = 0;
            char letter = 'a';
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index != keys.Count)
                    {
                        arr[i, j] = keys.ElementAt(index).ToString();
                        index++;
                    }
                    else
                    {
                        while (keys.Contains(letter))
                        {
                            letter++;
                        }
                        if (letter == 'j')
                        {
                            letter++;
                        }
                        arr[i, j] = letter.ToString();
                        letter++;
                    }
                }
            }

            int x1, x2, y1, y2;
            x1 = x2 = y1 = y2 = 0;
            for (int i = 0; i < cipherText.Length - 1; i += 2)
            {
                for (int k = 0; k < 5; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (arr[k, j].Equals(cipherText[i].ToString().ToLower()))
                        {
                            x1 = k;
                            y1 = j;
                        }
                        if (arr[k, j].Equals(cipherText[i + 1].ToString().ToLower()))
                        {
                            x2 = k;
                            y2 = j;
                        }
                    }
                }
                if (x1 == x2)
                {
                    if (y1 == 0)
                        y1 = 5;
                    if (y2 == 0)
                        y2 = 5;
                    plainText += arr[x1, y1 - 1];
                    plainText += arr[x2, y2 - 1];
                }
                else if (y1 == y2)
                {
                    if (x1 == 0)
                        x1 = 5;
                    if (x2 == 0)
                        x2 = 5;
                    plainText += arr[x1 - 1, y1];
                    plainText += arr[x2 - 1, y2];
                }
                else
                {
                    plainText += arr[x1, y2];
                    plainText += arr[x2, y1];
                }
            }

            if (plainText.Length % 2 == 0 && plainText[plainText.Length - 1] == 'x')
                plainText = plainText.Remove(plainText.Length - 1);

            for (int i = 0; i < plainText.Length - 3; i += 2)
            {
                if (plainText[i + 1] == 'x')
                {
                    if (plainText[i + 2] == plainText[i])
                    {
                        plainText = plainText.Remove(i + 1, 1);
                        i++;
                    }
                }
            }


            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            string cipherText = null;
            string [,] arr = new string[5,5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    arr[i,j] = "";
            }
            
            for (int i = 0; i < plainText.Length - 1; i+=2)
            {
                if (plainText[i] == plainText[i + 1])
                    plainText = plainText.Insert(i + 1, "x");
            }
            
            if (plainText.Length % 2 != 0)
                plainText += "x";

            HashSet<char> keys = new HashSet<char>();
            for (int i = 0; i < key.Length; i++)
            {
                keys.Add(key[i]);
            }

            int index = 0;
            char letter = 'a';
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (index != keys.Count)
                    {
                        arr[i, j] = keys.ElementAt(index).ToString();
                        index++;
                    }
                    else
                    {
                        while (keys.Contains(letter))
                        {
                            letter++;
                        }
                        if (letter == 'j')
                        {
                            letter++;
                        }
                        arr[i, j] = letter.ToString();
                        letter++;
                    }
                }
            }

            int x1, x2, y1, y2;
            x1 = x2 = y1 = y2 = 0;
            for (int i = 0; i < plainText.Length - 1; i+=2)
            {
                for (int k = 0; k < 5; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (arr[k, j].Equals(plainText[i].ToString()))
                        {
                            x1 = k;
                            y1 = j;
                        }
                        if (arr[k, j].Equals(plainText[i + 1].ToString()))
                        {
                            x2 = k;
                            y2 = j;
                        }
                    }
                }
                if (x1 == x2)
                {
                    cipherText += arr[x1, (y1 + 1) % 5];
                    cipherText += arr[x2, (y2 + 1) % 5];
                }
                else if (y1 == y2)
                {
                    cipherText += arr[(x1 + 1) % 5, y1];
                    cipherText += arr[(x2 + 1) % 5, y2];
                }
                else
                {
                    cipherText += arr[x1, y2];
                    cipherText += arr[x2, y1];
                }
            }

            return cipherText;
        }
    }
}
