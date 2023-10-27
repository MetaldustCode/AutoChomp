using AutoChomp.Movement.Pathfinder;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp
{
    internal class clsAStar
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        //internal void BuildGrid()
        //{
        //    // Clear Tables
        //    clsGenerateTables clsGenerateTables = new clsGenerateTables();
        //    clsGenerateTables.GenerateAStarNumber();

        //    clsGetBlockAge clsGetBlockAge = new clsGetBlockAge();
        //    bool[,] arrXGrid = clsClassTables.arrXGridPath;

        //    Point2d ptOrigin = clsCommon.GamePacman.Origin;
        //    Direction direction = clsCommon.GamePacman.Direction;

        //    List<Position> lstNextCell = new List<Position>();
        //    List<Direction> lstValid = clsGetBlockAge.GetValidDirections(arrXGrid, ptOrigin, direction, ref lstNextCell);

        //    clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

        //    Position posCell = clsGetCell.GetCell(ptOrigin, direction);
        //}

        //internal void BuildDefaultGrid(Boolean[,] arrXGridPath,
        //                               ref int[,] arrAStarNum,
        //                               ref DBText[,] arrAStarText,
        //                               Transaction acTrans, Database acDb)
        //{
        //    if (arrXGridPath != null)
        //    {
        //        arrXGridPath.GetSize(out int col, out int row);

        //        clsGluttony clsGluttony = new clsGluttony();

        //        for (int x = 0; x < col; x++)
        //        {
        //            for (int y = 0; y < row; y++)
        //            {
        //                if (clsClassTables.arrXGridPath[x, y])
        //                {
        //                    arrAStarText[x, y] = clsGluttony.AddText(acTrans, acDb, "0", 2);
        //                    arrAStarNum[x, y] = 0;

        //                    double dblX1 = x * Cell + Middle;
        //                    double dblY1 = y * Cell + Middle;

        //                    arrAStarText[x, y].MoveEntityXY(dblX1, dblY1);
        //                }
        //            }
        //        }
        //    }
        //}

        //internal void BuildDefaultGrid(int[,] arrAStarNum,
        //                               ref DBText[,] arrAStarText,
        //                               Transaction acTrans, Database acDb)
        //{
        //    if (arrAStarNum != null)
        //    {

        //        arrAStarNum.GetSize(out int col, out int row);

        //        clsGluttony clsGluttony = new clsGluttony();

        //        for (int x = 0; x < col; x++)
        //        {
        //            for (int y = 0; y < row; y++)
        //            {
        //                if (clsClassTables.arrXGridPath[x, y])
        //                {
        //                    arrAStarText[x, y] = clsGluttony.AddText(acTrans, acDb, "0", 2);
        //                    arrAStarNum[x, y] = 0;

        //                    double dblX1 = x * Cell + Middle;
        //                    double dblY1 = y * Cell + Middle;

        //                    arrAStarText[x, y].MoveEntityXY(dblX1, dblY1);
        //                }
        //            }
        //        }
        //    }
        //}



        //internal void UpdateTextGrid(int[,] arrAStarNum, ref DBText[,] arrAStarText, Transaction acTrans)
        //{
        //    if (arrAStarNum != null)
        //    {
        //        arrAStarText.GetSize(out int col, out int row);

        //        for (int x = 0; x < col; x++)
        //        {
        //            for (int y = 0; y < row; y++)
        //            {
        //                if (clsClassTables.arrXGridPath[x, y])
        //                {
        //                    string strTextValue = arrAStarNum[x, y].ToString();

        //                    DBText acText = acTrans.GetObject(arrAStarText[x, y].ObjectId, OpenMode.ForWrite) as DBText;
        //                    acText.TextString = strTextValue;
        //                }
        //            }
        //        }
        //    }
        //}

        internal static DBText[,] arrDBText;

        internal void CreateTextData()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    int[,] arrNum = clsCommon.lstGameGhost[3].arrAStar;

                    if (arrDBText != null)
                    {
                        List<ObjectId> lstObjectId = GetObjectId(arrDBText);

                        clsEntityDelete clsEntityDelete = new clsEntityDelete();
                        clsEntityDelete.DeleteObjectId(acTrans, acDb, lstObjectId);
                    }

                    string[,] arrText = arrNum.ConvertIntToString();
                    arrText.GetSize(out int col, out int row);
                    arrDBText = new DBText[col, row];
                    DrawText(acTrans, acDb, arrText, ref arrDBText);

                    acTrans.Commit();
                }
            }
        }

        internal void UpdateAStar(ref GameGhost Ghost)
        {
            Point2d ptOrigin = Ghost.Origin;
            Direction direction = Ghost.Direction;
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

            Position posCurrent = clsGetCurrentCell.GetCell(ptOrigin, direction);
            if (!PositionMatch(Ghost.posCurrent, posCurrent))
            {
                Ghost.posCurrent = posCurrent;
                Ghost.bolCellChanged = true;

                clsAStar clsAStar = new clsAStar();
                Ghost.arrAStar = GenerateAStar(posCurrent);
            }

        }

        internal void UpdateAStar(ref GamePacman Pacman)
        {
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

            Position posCurrent = clsGetCurrentCell.GetPacmanPosition();
            if (!PositionMatch(Pacman.posCurrent, posCurrent))
            {
                Pacman.posCurrent = posCurrent;
                Pacman.bolCellChanged = true;

                Pacman.arrAStar = GenerateAStar(posCurrent);
            }
        }

        internal Boolean HasPosition(List<Position> lstPosition, Position Pos)
        {
            for (int i = 0; i < lstPosition.Count; i++)
            {
                if (PositionMatch(lstPosition[i], Pos))
                    return true;
            }
            return false;
        }

        internal Boolean PositionMatch(Position p1, Position p2)
        {
            if (p1.X != p2.X || p1.Y != p2.Y)
                return false;
            return true;
        }


        internal void GenerateAStarHouse()
        {
            Position posCell = new Position(15, 21);

            clsCommon.GameGhostCommon.arrAStarHouse = GenerateAStar(posCell);
        }

        internal int[,] GenerateAStar(Position posCell)
        {
            clsBuildMap clsBuildMap = new clsBuildMap();

            string[,] arrText = clsBuildMap.BuildMap(posCell.X, posCell.Y);

            return arrText.ConvertStringToInt();
        }


        internal List<ObjectId> GetObjectId(DBText[,] arrNum)
        {
            arrNum.GetSize(out int col, out int row);

            List<ObjectId> rtnValue = new List<ObjectId>();

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (arrNum[x, y] != null)
                        rtnValue.Add(arrNum[x, y].ObjectId);
                }
            }

            return rtnValue;
        }

        internal void DrawText(Transaction acTrans, Database acDb, String[,] arrText, ref DBText[,] arrDBText)
        {
            if (arrText != null)
            {
                arrText.GetSize(out int col, out int row);


                clsGluttony clsGluttony = new clsGluttony();

                for (int x = 0; x < col; x++)
                {
                    for (int y = 0; y < row; y++)
                    {
                        if (arrText[x, y] != "0" && arrText[x, y] != null)
                        {
                            string strValue = arrText[x, y];
                            arrDBText[x, y] = clsGluttony.AddText(acTrans, acDb, strValue, 2);

                            double dblX1 = x * Cell + Middle;
                            double dblY1 = y * Cell + Middle;

                            arrDBText[x, y].MoveEntityXY(dblX1, dblY1);
                        }
                    }
                }
            }
        }
    }
}
