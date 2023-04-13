using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {

        string[,] RCON = new string[4, 10]
         {{"01","02","04","08","10","20","40","80","1b","36" },
          {"00","00","00","00","00","00","00","00","00","00" },
          {"00","00","00","00","00","00","00","00","00","00" },
          {"00","00","00","00","00","00","00","00","00","00" }};



        string[,] sbox = new string[16, 16]
           {{"63", "7C", "77", "7B", "F2", "6B", "6F", "C5", "30", "01", "67", "2B", "FE", "D7", "AB", "76"},
            {"CA", "82", "C9", "7D", "FA", "59", "47", "F0", "AD", "D4", "A2", "AF", "9C", "A4", "72", "C0"},
            {"B7", "FD", "93", "26", "36", "3F", "F7", "CC", "34", "A5", "E5", "F1", "71", "D8", "31", "15"},
            {"04", "C7", "23", "C3", "18", "96", "05", "9A", "07", "12", "80", "E2", "EB", "27", "B2", "75"},
            {"09", "83", "2C", "1A", "1B", "6E", "5A", "A0", "52", "3B", "D6", "B3", "29", "E3", "2F", "84"},
            {"53", "D1", "00", "ED", "20", "FC", "B1", "5B", "6A", "CB", "BE", "39", "4A", "4C", "58", "CF"},
            {"D0", "EF", "AA", "FB", "43", "4D", "33", "85", "45", "F9", "02", "7F", "50", "3C", "9F", "A8"},
            {"51", "A3", "40", "8F", "92", "9D", "38", "F5", "BC", "B6", "DA", "21", "10", "FF", "F3", "D2"},
            {"CD", "0C", "13", "EC", "5F", "97", "44", "17", "C4", "A7", "7E", "3D", "64", "5D", "19", "73"},
            {"60", "81", "4F", "DC", "22", "2A", "90", "88", "46", "EE", "B8", "14", "DE", "5E", "0B", "DB"},
            {"E0", "32", "3A", "0A", "49", "06", "24", "5C", "C2", "D3", "AC", "62", "91", "95", "E4", "79"},
            {"E7", "C8", "37", "6D", "8D", "D5", "4E", "A9", "6C", "56", "F4", "EA", "65", "7A", "AE", "08"},
            {"BA", "78", "25", "2E", "1C", "A6", "B4", "C6", "E8", "DD", "74", "1F", "4B", "BD", "8B", "8A"},
            {"70", "3E", "B5", "66", "48", "03", "F6", "0E", "61", "35", "57", "B9", "86", "C1", "1D", "9E"},
            {"E1", "F8", "98", "11", "69", "D9", "8E", "94", "9B", "1E", "87", "E9", "CE", "55", "28", "DF"},
            {"8C", "A1", "89", "0D", "BF", "E6", "42", "68", "41", "99", "2D", "0F", "B0", "54", "BB", "16"} };


        public static string[,] sboxInverse =
                 {
                { "52", "09", "6a", "d5", "30", "36", "a5", "38", "bf", "40", "a3", "9e", "81", "f3", "d7", "fb" },
                { "7c", "e3", "39", "82", "9b", "2f", "ff", "87", "34", "8e", "43", "44", "c4", "de", "e9", "cb" },
                { "54", "7b", "94", "32", "a6", "c2", "23", "3d", "ee", "4c", "95", "0b", "42", "fa", "c3", "4e" },
                { "08", "2e", "a1", "66", "28", "d9", "24", "b2", "76", "5b", "a2", "49", "6d", "8b", "d1", "25" },
                { "72", "f8", "f6", "64", "86", "68", "98", "16", "d4", "a4", "5c", "cc", "5d", "65", "b6", "92" },
                { "6c", "70", "48", "50", "fd", "ed", "b9", "da", "5e", "15", "46", "57", "a7", "8d", "9d", "84" },
                { "90", "d8", "ab", "00", "8c", "bc", "d3", "0a", "f7", "e4", "58", "05", "b8", "b3", "45", "06" },
                { "d0", "2c", "1e", "8f", "ca", "3f", "0f", "02", "c1", "af", "bd", "03", "01", "13", "8a", "6b" },
                { "3a", "91", "11", "41", "4f", "67", "dc", "ea", "97", "f2", "cf", "ce", "f0", "b4", "e6", "73" },
                { "96", "ac", "74", "22", "e7", "ad", "35", "85", "e2", "f9", "37", "e8", "1c", "75", "df", "6e" },
                { "47", "f1", "1a", "71", "1d", "29", "c5", "89", "6f", "b7", "62", "0e", "aa", "18", "be", "1b" },
                { "fc", "56", "3e", "4b", "c6", "d2", "79", "20", "9a", "db", "c0", "fe", "78", "cd", "5a", "f4" },
                { "1f", "dd", "a8", "33", "88", "07", "c7", "31", "b1", "12", "10", "59", "27", "80", "ec", "5f" },
                { "60", "51", "7f", "a9", "19", "b5", "4a", "0d", "2d", "e5", "7a", "9f", "93", "c9", "9c", "ef" },
                { "a0", "e0", "3b", "4d", "ae", "2a", "f5", "b0", "c8", "eb", "bb", "3c", "83", "53", "99", "61" },
                { "17", "2b", "04", "7e", "ba", "77", "d6", "26", "e1", "69", "14", "63", "55", "21", "0c", "7d" }};








        static string[,] multipicationmatrix = new string[4, 4]
        {
        {"2","3","1","1"},
        {"1","2","3","1" },
        {"1","1","2","3" },
        {"3","1","1","2" }
        };

        static string[,] multipicationmatrixD = new string[4, 4]
        {
            { "E", "B", "D", "9"},
            { "9", "E", "B", "D"},
            { "D", "9", "E" ,"B" },
            { "B", "D", "9", "E" }
        };


        static string _1b = "00011011";

        public override string Decrypt(string cipherText, string key)
        {

            cipherText = cipherText.Remove(0, 2);
            key = key.Remove(0, 2);
            string[,] state = BS(cipherText);
            string[,] rk = BS(key);
            List<string[,]> temp = GenerateExpandKeyReverse(rk);
            int i = 0;
            while (i < 10)

            {
                state = XOR(state, temp[i]);

                if (i > 0)
                {
                    state = inverseMixColumns(state);
                }
                state = shiftRowsinverse(state);
                state = invsubbyte(state);
                i++;
            }

            state = XOR(state, rk);

            string result = MatrixToString(state);
            result = result.ToUpper();
            result = result.Insert(0, "0x");

            return result;

        }


        public override string Encrypt(string plainText, string key)
        {

            plainText = plainText.Remove(0, 2);
            key = key.Remove(0, 2);

            string[,] state = BS(plainText);

            string[,] rk = BS(key);

            state = XOR(state, rk);

            int i = 0;
            while (i < 10)

            {
                rk = GenrateKey(rk, i);

                state = subBytes(state);

                state = shiftRows(state);
                if (i != 9)
                {
                    state = mixCol(state);
                }
                state = XOR(state, rk);
                i++;
            }
            string result = MatrixToString(state);
            result = result.ToUpper();
            result = result.Insert(0, "0x");

            return result;

        }


        private string[,] BS(string text)
        {
            string[,] matrix = new string[4, 4];
            int index = 0;

            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    string cell = text.Substring(index, 2).ToLower();
                    matrix[row, col] = cell;
                    index += 2;
                }
            }

            return matrix;
        }


        private string MatrixToString(string[,] matrix)
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    sb.Append(matrix[i, j]);
                }
            }

            return sb.ToString();
        }



        private string[,] subBytes(string[,] matrix)
        {
            string[,] sbox = new string[16, 16]
           {{"63", "7C", "77", "7B", "F2", "6B", "6F", "C5", "30", "01", "67", "2B", "FE", "D7", "AB", "76"},
            {"CA", "82", "C9", "7D", "FA", "59", "47", "F0", "AD", "D4", "A2", "AF", "9C", "A4", "72", "C0"},
            {"B7", "FD", "93", "26", "36", "3F", "F7", "CC", "34", "A5", "E5", "F1", "71", "D8", "31", "15"},
            {"04", "C7", "23", "C3", "18", "96", "05", "9A", "07", "12", "80", "E2", "EB", "27", "B2", "75"},
            {"09", "83", "2C", "1A", "1B", "6E", "5A", "A0", "52", "3B", "D6", "B3", "29", "E3", "2F", "84"},
            {"53", "D1", "00", "ED", "20", "FC", "B1", "5B", "6A", "CB", "BE", "39", "4A", "4C", "58", "CF"},
            {"D0", "EF", "AA", "FB", "43", "4D", "33", "85", "45", "F9", "02", "7F", "50", "3C", "9F", "A8"},
            {"51", "A3", "40", "8F", "92", "9D", "38", "F5", "BC", "B6", "DA", "21", "10", "FF", "F3", "D2"},
            {"CD", "0C", "13", "EC", "5F", "97", "44", "17", "C4", "A7", "7E", "3D", "64", "5D", "19", "73"},
            {"60", "81", "4F", "DC", "22", "2A", "90", "88", "46", "EE", "B8", "14", "DE", "5E", "0B", "DB"},
            {"E0", "32", "3A", "0A", "49", "06", "24", "5C", "C2", "D3", "AC", "62", "91", "95", "E4", "79"},
            {"E7", "C8", "37", "6D", "8D", "D5", "4E", "A9", "6C", "56", "F4", "EA", "65", "7A", "AE", "08"},
            {"BA", "78", "25", "2E", "1C", "A6", "B4", "C6", "E8", "DD", "74", "1F", "4B", "BD", "8B", "8A"},
            {"70", "3E", "B5", "66", "48", "03", "F6", "0E", "61", "35", "57", "B9", "86", "C1", "1D", "9E"},
            {"E1", "F8", "98", "11", "69", "D9", "8E", "94", "9B", "1E", "87", "E9", "CE", "55", "28", "DF"},
            {"8C", "A1", "89", "0D", "BF", "E6", "42", "68", "41", "99", "2D", "0F", "B0", "54", "BB", "16"} };

            string[,] result = new string[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // Convert the hex value to an integer and use it as the index
                    // to look up the corresponding value from the S-box.
                    int hex = Convert.ToInt32(matrix[i, j], 16);
                    string sboxValue = sbox[hex >> 4, hex & 0x0F].ToLower();
                    result[i, j] = sboxValue;
                }
            }

            return result;
        }



        private string[,] invsubbyte(string[,] matrix)
        {


            string[,] result = new string[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {

                    int hex = Convert.ToInt32(matrix[i, j], 16);
                    string sboxValue = sboxInverse[hex >> 4, hex & 0x0F].ToLower();
                    result[i, j] = sboxValue;
                }
            }

            return result;
        }



        private string[,] shiftRows(string[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);
            string[,] shiftedMatrix = new string[numRows, numCols];

            // Shift first row (no shift)
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[0, i] = matrix[0, i];
            }

            // Shift second row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[1, i] = matrix[1, (i + 1) % numCols];
            }

            // Shift third row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[2, i] = matrix[2, (i + 2) % numCols];
            }

            // Shift fourth row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[3, i] = matrix[3, (i + 3) % numCols];
            }

            return shiftedMatrix;
        }


        private string[,] shiftRowsinverse(string[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);
            string[,] shiftedMatrix = new string[numRows, numCols];
            // Shift first row (no shift)
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[0, i] = matrix[0, i];
            }

            // Shift second row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[1, (i + 1) % numCols] = matrix[1, i];
            }

            // Shift third row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[2, (i + 2) % numCols] = matrix[2, i];
            }

            // Shift fourth row
            for (int i = 0; i < numCols; i++)
            {
                shiftedMatrix[3, (i + 3) % numCols] = matrix[3, i];
            }

            return shiftedMatrix;
        }

        private string usebinary(string val)
        {
            string binary_num = val;
            bool flag = false;
            if (val.ElementAt(0) == '1')
            {
                flag = true;
            }



            binary_num = binary_num.Remove(0, 1);
            binary_num = binary_num.Insert(binary_num.Length, "0");

            if (flag)
            {

                binary_num = xorBinary_1b(binary_num);
            }
            return binary_num;


        }
        private string hexatobin(string hexaValue)
        {
            int num = Convert.ToInt32(hexaValue, 16);
            string binary = Convert.ToString(num, 2).PadLeft(8, '0');
            return binary;
        }
        public string BinaryToHex(string binary)
        {
            // Pad binary string to multiple of 4
            int padLength = 4 - binary.Length % 4;
            binary = binary.PadLeft(binary.Length + padLength, '0');

            // Convert to hex
            StringBuilder hex = new StringBuilder(binary.Length / 4);
            for (int i = 0; i < binary.Length; i += 4)
            {
                string chunk = binary.Substring(i, 4);
                hex.Append(Convert.ToInt32(chunk, 2).ToString("X"));
            }

            return hex.ToString();
        }
        private string xorBinary(string number1, string number2)
        {
            int i = 0;
            while (i < 8)
            {
                if (number1.ElementAt(i) == number2.ElementAt(i))
                {
                    number1 = number1.Remove(i, 1);
                    number1 = number1.Insert(i, "0");
                }
                else
                {
                    number1 = number1.Remove(i, 1);
                    number1 = number1.Insert(i, "1");
                }
                i++;
            }

            return number1;
        }

        private string xorBinary_1b(string x)
        {
            int i = 0;
            while (i < 8)
            {
                if (_1b.ElementAt(i) == x.ElementAt(i))
                {
                    x = x.Remove(i, 1);
                    x = x.Insert(i, "0");
                }
                else
                {
                    x = x.Remove(i, 1);
                    x = x.Insert(i, "1");
                }
                i++;
            }

            return x;
        }

        private string[,] mixCol(string[,] matrix)
        {
            string[,] mixedCol = new string[4, 4];
            string[,] tempMatrix = new string[4, 4];
            string temp = "";

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    tempMatrix[j, i] = hexatobin(matrix[j, i]);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string[] results = new string[4];
                    for (int k = 0; k < 4; k++)
                    {
                        if (multipicationmatrix[j, k] == "2")
                        {
                            results[k] = usebinary(tempMatrix[k, i]);
                        }
                        else if (multipicationmatrix[j, k] == "3")
                        {
                            temp = tempMatrix[k, i];
                            results[k] = usebinary(tempMatrix[k, i]);
                        }
                        else if (multipicationmatrix[j, k] == "1")
                        {
                            results[k] = tempMatrix[k, i];
                        }
                    }
                    mixedCol[j, i] = BinaryToHex(xorBinary(xorBinary(xorBinary(xorBinary(results[0], results[1]), results[2]), results[3]), temp)).ToLower();
                }
            }

            return mixedCol;
        }



        private string[,] GenrateKey(string[,] matrix, int roundNumber)
        {
            string[,] roundMatrix = new string[4, 4];
            string[,] lastCol = new string[4, 1];

            lastCol[3, 0] = matrix[0, 3];
            int i = 0;
            while (i < 3)
            {
                lastCol[i, 0] = matrix[i + 1, 3];
                i++;
            }

            lastCol = subBytes(lastCol);

            string[,] column = new string[4, 1];
            string[,] newColumn = new string[4, 1];
            string[,] rconCol = new string[4, 1];

            int col = 0;
            while (col < 4)
            {
                int row = 0;
                while (row < 4)
                {
                    if (col != 0)
                    {
                        newColumn[row, 0] = roundMatrix[row, col - 1];
                    }
                    column[row, 0] = matrix[row, col];
                    rconCol[row, 0] = RCON[row, roundNumber];
                    row++;
                }

                if (col == 0)
                {
                    column = XOR(column, lastCol);
                    column = XOR(column, rconCol);
                }
                else
                {
                    column = XOR(column, newColumn);
                }

                row = 0;
                while (row < 4)
                {
                    roundMatrix[row, col] = column[row, 0];
                    row++;
                }

                col++;
            }

            return roundMatrix;
        }


        private string[,] XOR(string[,] mat1, string[,] mat2)
        {
            String[,] result = new string[mat1.GetLength(0), mat1.GetLength(1)];

            for (int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat1.GetLength(1); j++)
                {
                    long M1 = Convert.ToInt64(mat1[i, j], 16);
                    long M2 = Convert.ToInt64(mat2[i, j], 16);
                    long M3 = M1 ^ M2;
                    result[i, j] = M3.ToString("X2").ToLower();
                }
            }

            return result;
        }




        public List<string[,]> GenerateExpandKeyReverse(string[,] firstKey)
        {
            string[,] rk = firstKey;
            List<string[,]> temp = new List<string[,]>();
            int i = 0;

            while (i < 10)
            {
                rk = GenrateKey(rk, i);
                temp.Add(rk);
                i++;
            }

            temp.Reverse();

            return temp;
        }

        private string[,] inverseMixColumns(string[,] matrix)
        {
            // Convert from hexa to binary nested loop
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[i, j] = hexatobin(matrix[i, j]);
                }
            }

            string[,] mixedCol = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string[] results = new string[4];
                    string[,] tempMatrix = new string[4, 4];

                    // Copy matrix to tempMatrix
                    for (int x = 0; x < 4; x++)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            tempMatrix[x, y] = matrix[x, y];
                        }
                    }

                    for (int k = 0; k < 4; k++)
                    {
                        string res = matrix[k, i];
                        string tmp = matrix[k, i];

                        // Compute result for current cell
                        switch (multipicationmatrixD[j, k])
                        {
                            case "9":
                                for (int t = 0; t < 3; t++)
                                {
                                    res = usebinary(res);
                                }
                                results[k] = xorBinary(res, tmp);
                                break;
                            case "B":
                                for (int t = 0; t < 5; t++)
                                {
                                    if (t == 2 || t == 4)
                                    {
                                        res = xorBinary(res, tmp);
                                    }
                                    else
                                    {
                                        res = usebinary(res);
                                    }
                                }
                                results[k] = res;
                                break;
                            case "D":
                                for (int t = 0; t < 5; t++)
                                {
                                    if (t == 1 || t == 4)
                                    {
                                        res = xorBinary(res, tmp);
                                    }
                                    else
                                    {
                                        res = usebinary(res);
                                    }
                                }
                                results[k] = res;
                                break;
                            case "E":
                                for (int t = 0; t < 5; t++)
                                {
                                    if (t == 1 || t == 3)
                                    {
                                        res = xorBinary(res, tmp);
                                    }
                                    else
                                    {
                                        res = usebinary(res);
                                    }
                                }
                                results[k] = res;
                                break;
                        }
                    }

                    // Compute mixed column value for current cell
                    var cell = xorBinary(results[0], results[1]);
                    cell = xorBinary(cell, results[2]);
                    cell = xorBinary(cell, results[3]);

                    mixedCol[j, i] = BinaryToHex(cell).ToLower();
                }
            }

            return mixedCol;
        }



    }
}