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
            
            List<int> keyList = new List<int>();
            int size = GetKeySize(plainText, cipherText);
            keyList = ArrangeKeyList(plainText, cipherText, size);
            return keyList;
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
      
        public int GetKeySize(string plainText, string cipherText)
        {
            cipherText = cipherText.ToUpper();
            plainText = plainText.ToLower();
            Console.WriteLine("\t \t In GetKeySize ");
            double Row, Column;
            int index, LetterIndex, KeySize = -1;
            string SubStr = "";
            for (int i = 2; i <= plainText.Length; i++)
            {
                Console.WriteLine("Iterator {0}", i - 1);
                Console.WriteLine(cipherText);
                Column = i;
                Row = Math.Ceiling(cipherText.Length / Column);
                for (int k = 0; k < (int)Row; k++)
                {
                    LetterIndex = k * (int)Column;
                    if (LetterIndex >= plainText.Length)
                        SubStr += "X";
                    else
                        SubStr += plainText[LetterIndex];
                }
                SubStr = SubStr.ToUpper();
                Console.WriteLine("SubStr : {0}", SubStr);

                index = cipherText.IndexOf(SubStr);
                Console.WriteLine("index : {0}", index);

                if (index != -1)
                {
                    KeySize = i;
                    return KeySize;
                }
                else
                    SubStr = "";
            }
            Console.WriteLine("\t Key Size : {0}", KeySize);

            return KeySize;
        }
        public static List<int> ArrangeKeyList(string plainText, string cipherText, int KeySize)
        {
            cipherText = cipherText.ToUpper();
            plainText = plainText.ToLower();
            Console.WriteLine("\t \t In ArrangeKeyList ");
            Console.WriteLine("\t \t {0}", cipherText);
            List<int> KeyList = new List<int>();
            double Row, Column;
            int index, LetterIndex;
            string SubStr = "";
            Column = KeySize;
            Row = Math.Ceiling(cipherText.Length / Column);
            for (int i = 0; i < Column; i++)
            {
                for (int k = 0; k < (int)Row; k++)
                {
                    LetterIndex = (k * (int)Column) + i;
                    if (LetterIndex >= plainText.Length)
                        continue;
                    else
                        SubStr += plainText[LetterIndex];
                }
                SubStr = SubStr.ToUpper();
                index = cipherText.IndexOf(SubStr);
                if (((index) % (int)Row) == 0)
                    KeyList.Add(((index) / (int)Row) + 1);
                else
                    KeyList.Add(((index) / (int)Row) + 2);
                Console.Write("  {0}\t{1}  ", SubStr, index);
                SubStr = "";
            }
            Console.WriteLine();
            return KeyList;
        }
    }
}
