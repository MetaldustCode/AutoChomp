using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSolvePath
    {


        internal void DrawingDirectionBoxes(Transaction acTrans, Database acDb,
                                            List<Position> lstPt, int intColor)
        {
            //for (int i = 0; i < lstPt.Count; i++)
            //{
            //    clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            //    BlockReference acBlkRef = clsPolylineAdd.AddBoxBlock(acTrans, acDb, intColor, "GluttonyBox");
            //    acBlkRef.ColorIndex = intColor;
            //    acBlkRef.Position = lstPt[i].GetOrigin().ToPoint3d();
            //    clsCommon.GameObjectId.lstObjPosition.Add(acBlkRef.ObjectId);
            //}
        }



        internal void Solve()
        {


            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;


            for (int i = 0; i < lstGhosts.Count; i++)
            {
                int[,] arrAStar = lstGhosts[i].arrAStar;
                arrAStar.GetSize(out int col, out int row);

                if (col > 0)
                {
                    GameGhost ghost = lstGhosts[i];

                    arrAStar = ghost.arrAStar;

                    Position pos = ghost.posCurrent;

                    ghost.lstPosition = SolveAStar(arrAStar, pos);
                }
            }
        }

        internal List<Boolean> GetSearchVisible()
        {
            clsReg clsReg = new clsReg();
            List<Boolean> rtnValue = new List<bool>();

            rtnValue.Add(clsReg.GetRedSearchVisible());
            rtnValue.Add(clsReg.GetPinkSearchVisible());
            rtnValue.Add(clsReg.GetBlueSearchVisible());
            rtnValue.Add(clsReg.GetOrangeSearchVisible());

            return rtnValue;
        }

        internal string[,] ToStringArray(int[,] arrAStar)
        {
            arrAStar.GetSize(out int col, out int row);

            string[,] rtnValue = new string[col, row];

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    string strValue = arrAStar[i, j].ToString();
                    if (strValue == "0")
                        strValue = "";

                    rtnValue[i, j] = strValue;
                }
            }

            return rtnValue;
        }

        internal List<Position> SolveAStar(int[,] arrAStar, Position pos)
        {
            arrAStar.GetSize(out int col, out int row);

            string[,] strAStar = ToStringArray(arrAStar);

            List<Position> lstPosition = new List<Position>();

            PathFinder(strAStar, col, row, pos.X, pos.Y, ref lstPosition);

            return lstPosition;
        }

        internal void DrawLine(Transaction acTrans, Database acDb, List<Position> lstPos, int intColor)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();

            List<Point2d> lstPoints = new List<Point2d>();

            for (int i = 0; i < lstPos.Count; i++)
            {
                Point2d pt = new Point2d((lstPos[i].X * 100) + 50, (lstPos[i].Y * 100) + 50);
                lstPoints.Add(pt);
            }
            GameObjectId gameElements = clsCommon.GameObjectId;
            if (gameElements == null) return;

            gameElements.lstObjAStar.Add(clsPolylineAdd.AddPolyLine(acTrans, acDb, lstPoints, intColor, 10));
        }

        internal void DeleteLine(Transaction acTrans, Database acDb)
        {
            GameObjectId gameElements = clsCommon.GameObjectId;
            if (gameElements != null)
            {
                clsEntityDelete clsEntityDelete = new clsEntityDelete();
                clsEntityDelete.DeleteObjectId(acTrans, acDb, clsCommon.GameObjectId.lstObjPosition);
                clsCommon.GameObjectId.lstObjPosition.Clear();

                clsEntityDelete.DeleteObjectId(acTrans, acDb, gameElements.lstObjAStar);
                gameElements.lstObjAStar.Clear();
            }
        }

        internal void GetNextDirection(ref int x, ref int y, int col, int row,
                                       ref int curNumber, List<String> maPair, string[,] arr,
                                       List<int> maX, List<int> maY)
        {
            clsBuildMap clsBuildMap = new clsBuildMap();

            for (int i = 0; i < 4; i++)
            {
                int ax = x;
                int ay = y;

                clsBuildMap.GetDirection(i, ref ax, ref ay);

                if (col.IsInGrid(row, ax, ay))
                {
                    string strNum = arr[ax, ay];

                    if (strNum.IsNumeric(out int intNum))
                    {
                        if (intNum < curNumber)
                        {
                            string strPair = string.Format("{0},{1}", ax, ay);
                            if (!maPair.Contains(strPair))
                            {
                                maPair.Add(strPair);
                                maX.Add(ax);
                                maY.Add(ay);
                                return;
                            }
                        }
                    }
                }
            }
        }

        internal void PathFinder(string[,] arr, int col, int row,
                                 int x, int y,
                                 ref List<Position> lstPosition)
        {
            string strNum = arr[x, y];
            if (strNum.IsNumeric(out int curNumber))
            {
                //outX.Add(x);
                //outY.Add(y);

                List<int> maX = new List<int>();
                List<int> maY = new List<int>();

                List<String> maPair = new List<String>();

                GetNextDirection(ref x, ref y, col, row,
                                 ref curNumber, maPair, arr, maX, maY);

                if (maX.Count > 0)
                {
                    int rnd = clsRandomizer.RandomInteger(0, maX.Count - 1);

                    for (int i = 0; i < maX.Count; i++)
                    {
                        lstPosition.Add(new Position(maX[rnd], maY[rnd]));

                        PathFinder(arr, col, row, maX[rnd], maY[rnd], ref lstPosition);
                    }
                }
            }
        }
    }
}