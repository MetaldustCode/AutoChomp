using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Internal.Calculator;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;

namespace AutoChomp.Movement.Pathfinder
{
    internal class clsBuildMap
    {

        internal string[,] BuildMap(int intX, int intY)
        {
            if (intX < 0 || intY < 0) return new string[0, 0];

            string[,] arrNum = BuildString();

            arrNum.GetSize(out int col, out int row);

            if (arrNum[intX, intY] == null)
            {
                arrNum[intX, intY] = "1";

                List<int> aX = new List<int>() { intX };
                List<int> aY = new List<int>() { intY };

                int numBase = 1;

                while (GetNextSequence(ref arrNum, col, row, ref aX, ref aY))
                {
                    numBase++;

                    for (int i = 0; i < aX.Count; i++)
                        arrNum[aX[i], aY[i]] = numBase.ToString();
                }

                return arrNum;
            }
            return new string[0, 0];
        }


        internal string[,] BuildString()
        {
            Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;

            arrXGridPath.GetSize(out int col, out int row);

            string[,] arrNum = new string[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (!arrXGridPath[x, y])
                        arrNum[x, y] = "X";
                }
            }

            return arrNum;
        }

        internal Boolean GetNextSequence(ref String[,] arrNum, int col, int row, ref List<int> lstX, ref List<int> lstY)
        {

            List<int> outX = new List<int>();
            List<int> outY = new List<int>();

            List<String> outPair = new List<String>();

            for (int k = 0; k < lstX.Count; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    int x = lstX[k];
                    int y = lstY[k];
                    GetDirection(i, ref x, ref y);

                    if (col.IsInGrid(row, x, y) && arrNum[x, y] == null)
                    {
                        string strCheck = String.Format("{0},{1}", x, y);

                        if (!outPair.Contains(strCheck))
                        {
                            outX.Add(x);
                            outY.Add(y);
                            outPair.Add(strCheck);
                        }
                    }
                }
            }
            if (lstX.Count > 0)
            {
                lstX = outX.ToList();
                lstY = outY.ToList();
                return true;
            }

            return false;
        }

        internal void GetDirection(int intDir, ref int x, ref int y)
        {
            switch (intDir)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    y++;
                    break;
                case 2:
                    x++;
                    break;
                case 3:
                    x--;
                    break;
            }
        }
    }
}
