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
            //  throw new NotImplementedException();
            int key = 0;

            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            // throw new NotImplementedException();
            string plainText = "";
            double row, column = 0;
            column = key; // 7 columns
            row = Math.Ceiling(cipherText.Length / column); //25/7 = 3.5 ~ 3 
            char[,] matrix = new char[(int)row, (int)column];
            int k = 0;

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row && k < cipherText.Length; j++)
                {
                    matrix[j, i] = cipherText[k];
                    k++;
                }

            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    plainText += Char.ToUpper(matrix[i, j]);
                }
            }
            return plainText;
        }

        public string Encrypt(string plainText, int key)
        {
            //throw new NotImplementedException();
            string cipherText = "";
            double row, column = 0;
            column = key; // 7 columns

            row = Math.Ceiling(plainText.Length / column); //25/7 = 3.5 ~ 3 
            char[,] matrix = new char[(int)row, (int)column];
            int k = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column && k < plainText.Length; j++)
                {
                    matrix[i, j] = plainText[k];
                    k++;
                }
            }

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    cipherText += Char.ToUpper(matrix[j, i]);
                }
            }
            return cipherText;
        }
    }
}
