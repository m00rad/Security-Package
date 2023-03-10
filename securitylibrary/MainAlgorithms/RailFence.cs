using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            string test;
            int key = -1;
            for (int i = 1; i < plainText.Length; i++)
            {
                test = Encrypt(plainText, i);
                if (test.Equals(cipherText))
                {
                    key = i;
                    return key;

                }

            }
            return key;
        }
        public string Decrypt(string cipherText, int key)
        {
            string plainText = "";
            double row, column = 0;
            row = key;
            column = Math.Ceiling(cipherText.Length / row);
            char[,] matrix = new char[(int)row, (int)column];
            int k = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (k < cipherText.Length)
                    {
                        matrix[i, j] = cipherText[k];
                        k++;
                    }
                }
            }

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    plainText += Char.ToLower(matrix[j, i]);
                }
            }
            return plainText;
        }
        public string Encrypt(string plainText, int key)
        {
            string cipherText = "";
            double row, column = 0;
            row = key; 
            column = Math.Ceiling(plainText.Length / row); 
            char[,] matrix = new char[(int)row, (int)column];
            int k = 0;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (k >= plainText.Length)
                        matrix[j, i] = 'X';
                    else
                        matrix[j, i] = plainText[k];
                    k++;
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (Char.ToUpper(matrix[i, j]) == 'X')
                        continue;
                    else
                        cipherText += Char.ToUpper(matrix[i, j]);
                }
            }
            return cipherText;
        }

    }
}
