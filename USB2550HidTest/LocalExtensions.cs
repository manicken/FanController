using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace USB2550HidTest
{
    public static class ArrayExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisDataTable"></param>
        /// <param name="colIndex"></param>
        /// <param name="numberBase">the number base, ex. 2 for binary, 10 for decimals, 16 for hexa-decimals</param>
        /// <returns></returns>
        public static ushort[] GetFrom_DataTable_Col(this DataTable thisDataTable, int colIndex, int numberBase)
        {
            ushort[] dataArray = new ushort[thisDataTable.Rows.Count];
            for (int ri = 0; ri < thisDataTable.Rows.Count; ri++)
            {
                dataArray[ri] = Convert.ToUInt16(thisDataTable.Rows[ri][colIndex].ToString(), numberBase);
            }
            return dataArray;
        }
        public static byte[] ToByteArray(this ushort[] this_ushortArray)
        {
            List<byte> byteArray = new List<byte>(this_ushortArray.Length * 2);
            byte[] temp;
            for (int i = 0; i < this_ushortArray.Length; i++)
            {
                temp = BitConverter.GetBytes(this_ushortArray[i]);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(temp);
                byteArray.AddRange(temp);
            }
            return byteArray.ToArray();
        }
        public static void PutInto_DataTable_Col(this int[] thisIntArray, DataTable dt, int colIndex)
        {
            string skippedData = "";
            for (int ri = 0; ri < thisIntArray.Length; ri++)
            {
                try
                {
                    dt.Rows[ri][colIndex] = thisIntArray[ri].ToString("x4");
                }
                catch
                {
                    skippedData += "skipped: " + thisIntArray[ri].ToString("x4") + Environment.NewLine;
                }
            }
            //MessageBox.Show(skippedData);
        }
        public static int[] ToIntArray(this byte[] thisByteArray, int trimStart, int trimEnd, int bytesInGroup, bool bigEndian)
        {
            if (thisByteArray == null)
                throw new Exception("input == null");
            if (thisByteArray.Length == 0)
                throw new Exception("input.Length == 0");

            if (trimStart == -1) trimStart = 0;
            if (trimEnd == -1) trimEnd = thisByteArray.Length;



            int length = trimEnd - trimStart;
            if (length % bytesInGroup != 0)
                throw new Exception(length + " % " + bytesInGroup + " != 0");

            int newLength = length / bytesInGroup;
            int[] newArray = new int[newLength];
            int nabigi = 0; // new array byte in group index 
            int nai = 0; // new array index
            for (int tbai = trimStart; tbai < trimEnd; nai++)
            {
                newArray[nai] = 0;
                if (bigEndian)
                {
                    nabigi = bytesInGroup - 1;
                    while (nabigi >= 0)
                    {
                        newArray[nai] += (int)thisByteArray[tbai++] * IntPow(256, nabigi--);
                    }
                }
                else
                {
                    nabigi = 0; ;
                    while (nabigi < bytesInGroup)
                    {
                        newArray[nai] += (int)thisByteArray[tbai++] * IntPow(256, nabigi++);
                    }
                }

            }
            return newArray;
        }
        public static int IntPow(int x, int y)
        {
            return (int)Math.Pow((double)x, (double)y);
        }
        public static string ToHexString(this int[] thisIntArray, string seperator)
        {
            if (thisIntArray == null)
                return "input == null";
            if (thisIntArray.Length == 0)
                return "input.Length == 0";

            string result = string.Empty;

            bool notFirst = false;

            for (int i = 0; i < thisIntArray.Length; i++)
            {
                if (notFirst)
                {
                    result += seperator;
                }
                result += thisIntArray[i].ToString("X4");
                notFirst = true;
            }

            return result;
        }
    }
}
