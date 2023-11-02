using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGetGrid
    {
        private readonly double Cell = clsGridValues.Cell;

        internal Boolean GetCellSize(ref int intWidth, ref int intHeight)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            if (clsCommon.lstGameMaze != null)
            {
                List<String> lstGrid = clsCommon.lstGameMaze[index].lstGridIsland;

                for (int i = 0; i < lstGrid.Count; i++)
                {
                    if (i == 0)
                    {
                        string strRow = lstGrid[i];
                        intWidth = strRow.Length;
                    }
                    else
                        break;
                }

                intHeight = lstGrid.Count;
                return true;
            }
            return false;
        }

        internal Boolean GetCellSize(ref double dblWidth, ref double dblHeight)
        {
            int intWidth = 0;
            int intHeight = 0;
            Boolean rtnValue = GetCellSize(ref intWidth, ref intHeight);

            dblWidth = (double)intWidth;
            dblHeight = (double)intHeight;
            return rtnValue;
        }

        internal Boolean GetGridSize(ref double dblWidth, ref double dblHeight)
        {
            int intWidth = 0;
            int intHeight = 0;
            Boolean rtnValue = GetCellSize(ref intWidth, ref intHeight);

            dblWidth = (double)intWidth * Cell;
            dblHeight = (double)intHeight * Cell;

            return rtnValue;
        }
    }
}