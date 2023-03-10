using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            // throw new NotImplementedException();
            List<int> key = new List<int>();

            return key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            //throw new NotImplementedException();
            //mainCipher3 = "ctipscoeemrnuce"
            string plainText = "";
            double row, column = 0;
            column = key.Count; // 7 columns
            SortedDictionary<int, int> keyMapping = new SortedDictionary<int, int>();

            row = Math.Ceiling(cipherText.Length / column); //25/7 = 3.5 ~ 3 
            char[,] matrix = new char[(int)row, (int)column];
            int k = 0;

            for (int i = 0; i < column; i++)
            {
                keyMapping[key[i]] = i;
            }

            for (int i = 0; i < column; i++)
            {
                foreach (var item in keyMapping)
                {
                    for (int j = 0; j < row && k < cipherText.Length; j++)
                    {
                        matrix[j, item.Value] = cipherText[k];
                        k++;
                    }
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
        public string Encrypt(string plainText, List<int> key)
        {
            //throw new NotImplementedException();
            //plainText = attackpostponeduntiltwoam
            string cipherText = "";
            double row, column = 0;
            column = key.Count; // 7 columns
            SortedDictionary<int, int> keyMapping = new SortedDictionary<int, int>();

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
                keyMapping[key[i]] = i;
            }
            foreach (var item in keyMapping)
            {
                for (int r = 0; r < row; r++)
                {
                    cipherText += Char.ToUpper(matrix[r, item.Value]);
                }
            }
            return cipherText;
        }
    }
}
