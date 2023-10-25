using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsFilter
    {
        internal String[,] Filter(String[,] arrGrid, String strValue)
        {
            String[,] a = arrGrid;
            int row = a.GetLength(0);
            int col = a.GetLength(1);

            String[,] rtn = new String[row, col];

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    String X = a[r, c];
                    if (X == strValue)
                        rtn[r, c] = strValue;
                    else
                        rtn[r, c] = " ";
                }
            }

            return rtn;
        }

        internal String[,] Filter(String[,] arrGrid, List<String> lstValue)
        {
            String[,] a = arrGrid;
            int row = a.GetLength(0);
            int col = a.GetLength(1);

            String[,] rtn = new String[row, col];

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    String X = a[r, c];
                    if (lstValue.Contains(X))
                        rtn[r, c] = X;
                    else
                        rtn[r, c] = " ";
                }
            }

            return rtn;
        }

        internal String[,] ConvertToGrid(List<String> a)
        {
            int intWidth = 0;
            int intHeight = a.Count;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Length > intWidth)
                    intWidth = a[i].Length;
            }

            String[,] arrGrid = new String[intHeight, intWidth];

            for (int r = 0; r < a.Count; r++)
            {
                for (int c = 0; c < a[r].Length; c++)
                {
                    arrGrid[r, c] = a[r][c].ToString();
                }
            }

            return arrGrid;
        }

        internal Boolean[,] ConvertToBooleanGrid(List<String> a, List<String> lstValue)
        {
            int intWidth = 0;
            int intHeight = a.Count;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Length > intWidth)
                    intWidth = a[i].Length;
            }

            Boolean[,] arrGrid = new Boolean[intWidth, intHeight];

            for (int x = 0; x < intWidth; x++)
            {
                for (int y = 0; y < intHeight; y++)
                {
                    if (lstValue.Contains(a[y][x].ToString()))
                    {
                        arrGrid[x, y] = true;
                    }
                }
            }

            return arrGrid;
        }

        internal Boolean[,] ConvertToBooleanGrid(List<String> a, char chr)
        {
            a.GetSize(out int col, out int row);

            Boolean[,] arrGrid = new Boolean[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (chr == a[y][x])
                        arrGrid[x, y] = true;
                }
            }

            return arrGrid;
        }
    }
}