using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSolvePath
    {
        internal List<Position> SolveAStar(int[,] arrAStar, Position pos)
        {
            arrAStar.GetSize(out int col, out int row);

            string[,] strAStar = arrAStar.ToStringArray(true);

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

            ObjectId objId = clsPolylineAdd.AddPolyLine(acTrans, acDb, lstPoints, intColor, 10);

            Polyline acPline = acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;

            acPline.FilletAll(50, 10);

            gameElements.lstObjAStar.Add(acPline.ObjectId);
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
            if (arr.GetLength(0) > 0)
            {
                string strNum = arr[x, y];
                if (strNum.IsNumeric(out int curNumber))
                {
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
}