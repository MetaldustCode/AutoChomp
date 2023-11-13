using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //internal void CreateTextData()
        //{
        //    Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    Database acDb = acDoc.Database;

        //    // Start a transaction
        //    using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
        //    {
        //        using (DocumentLock @lock = acDoc.LockDocument())
        //        {
        //            if (lstText != null && lstText.Count > 0)
        //            {
        //                clsEntityDelete clsEntityDelete = new clsEntityDelete();
        //                clsEntityDelete.DeleteObjectId(acTrans, acDb, lstText);
        //            }

        //            lstText = new List<DBText>();

        //            List<Position> lstPos = new List<Position>();
        //            List<int> lstNumbers = new List<int>();

        //            for (int i = 0; i < clsCommon.lstGameGhost.Count; i++)
        //            {
        //                GameGhost Ghost = clsCommon.lstGameGhost[i];

        //                List<Position> _lstPos = Ghost.lstAStarPosition;
        //                List<int> _lstNumbers = Ghost.lstAStarNumber;

        //                for (int j = 0; j < _lstPos.Count; j++)
        //                {
        //                    lstPos.Add(_lstPos[j]);
        //                    lstNumbers.Add(_lstNumbers[j]);
        //                }

        //                break;
        //            }

        //            //RemoveDuplicates(ref lstPos, ref lstNumbers);

        //            DrawText(acTrans, acDb, lstPos, lstNumbers, ref lstText);

        //            acTrans.Commit();
        //        }
        //    }
        //}

        internal static List<DBText> _lstText;

        internal void CreateTextData()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    if (_lstText != null && _lstText.Count > 0)
                    {
                        clsEntityDelete clsEntityDelete = new clsEntityDelete();
                        clsEntityDelete.DeleteObjectId(acTrans, acDb, _lstText);
                    }

                    GameGhost Ghost = clsCommon.lstGameGhost[0];

                    //  int[,] arrAStarNum = Ghost.arrAStarGrid;

                    int[,] arrAStarNum = clsCommon.GamePacman.arrCurrentAStar;

                    arrAStarNum.GetSize(out int col, out int row);

                    DBText[,] arrDBText = new DBText[col, row];
                    DrawText(acTrans, acDb, arrAStarNum, ref arrDBText);

                    _lstText = new List<DBText>();

                    for (int c = 0; c < col; c++)
                    {
                        for (int r = 0; r < row; r++)
                        {
                            if (arrDBText[c, r] != null)
                                _lstText.Add(arrDBText[c, r]);
                        }
                    }

                    acTrans.Commit();
                }
            }
        }

        internal void RemoveDuplicates(ref List<Position> lstPos, ref List<int> lstNum)
        {
            if (lstPos.Count > 0)
            {
                List<Tuple<int, int>> lstTuple = new List<Tuple<int, int>>();

                for (int i = 0; i < lstPos.Count; i++)
                    lstTuple.Add(new Tuple<int, int>(lstPos[i].X, lstPos[i].Y));

                lstTuple = lstTuple.Distinct().ToList();

                List<int> _lstNum = new List<int>();
                List<Position> _lstPos = new List<Position>();

                for (int i = 0; i < lstTuple.Count; i++)
                    _lstPos.Add(new Position(lstTuple[i].Item1, lstTuple[i].Item2));

                for (int i = 0; i < _lstPos.Count; i++)
                {
                    for (int k = 0; k < lstPos.Count; k++)
                    {
                        if (_lstPos[i].X == lstPos[i].X &&
                            _lstPos[i].Y == lstPos[i].Y)
                        {
                            _lstNum.Add(lstNum[k]);
                        }
                    }
                }

                lstNum = new List<int>(_lstNum);
                lstPos = new List<Position>(_lstPos);
            }
        }

        internal void DeleteText()
        {
            if (_lstText != null && _lstText.Count > 0)
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database acDb = acDoc.Database;

                // Start a transaction
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    using (DocumentLock @lock = acDoc.LockDocument())
                    {
                        if (_lstText != null && _lstText.Count > 0)
                        {
                            clsEntityDelete clsEntityDelete = new clsEntityDelete();
                            clsEntityDelete.DeleteObjectId(acTrans, acDb, _lstText);
                        }

                        _lstText = new List<DBText>();
                        acTrans.Commit();
                    }
                }
            }
        }

        //internal void DeleteText()
        //{
        //    Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    Database acDb = acDoc.Database;

        //    // Start a transaction
        //    using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
        //    {
        //        using (DocumentLock @lock = acDoc.LockDocument())
        //        {
        //            int[,] arrNum = clsCommon.GamePacman.arrAStar;

        //            if (arrDBText != null)
        //            {
        //                List<ObjectId> lstObjectId = GetObjectId(arrDBText);

        //                clsEntityDelete clsEntityDelete = new clsEntityDelete();
        //                clsEntityDelete.DeleteObjectId(acTrans, acDb, lstObjectId);
        //            }

        //            acTrans.Commit();
        //        }
        //    }
        //}

        //internal void UpdateGhostAStar(ref GameGhost Ghost)
        //{
        //    GamePacman Pacman = clsCommon.GamePacman;

        //    Direction dirGhost = Ghost.Direction;
        //    Direction dirPacman = Pacman.Direction;

        //    clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
        //    Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, dirGhost);

        //    Position posPacman = clsGetCurrentCell.GetCell(Pacman.ptOrigin, dirPacman);

        //    Ghost.ptPosition = posGhost;
        //    Pacman.ptPosition = posPacman;

        //    clsCommon.GamePacman = Pacman;

        //    Ghost.arrAStarGrid = GenerateAStar(posPacman, posGhost, dirGhost);

        //    //clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
        //    //Ghost.arrAStar = GenerateAStar(posCurrent, direction);

        //    ////Position posCurrent = clsGetCurrentCell.GetCell(ptOrigin, direction);
        //    ////if (!PositionMatch(Ghost.posCurrent, posCurrent))
        //    ////{
        //    //Ghost.posCurrent = posCurrent;
        //    //Ghost.bolCellChanged = true;

        //    //clsAStar clsAStar = new clsAStar();
        //    //Ghost.arrAStar = GenerateAStar(posCurrent, direction);
        //    // }
        //}

        //internal void UpdatePacmanAStar(ref GamePacman Pacman)
        //{
        //    clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

        //    Position posCurrent = clsGetCurrentCell.GetPacmanPosition();
        //    if (!PositionMatch(Pacman.posCurrent, posCurrent))
        //    {
        //        Pacman.posCurrent = posCurrent;
        //        Pacman.bolCellChanged = true;

        //        Pacman.arrAStar = GenerateAStar(posCurrent, Pacman.Direction);
        //    }
        //}

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

        internal int[,] GenerateAStar(Position posPacman)
        {
            clsBuildMap clsBuildMap = new clsBuildMap();

            string[,] arrText = clsBuildMap.BuildMap(posPacman);

            return arrText.ConvertStringToInt();
        }

        internal int[,] GenerateAStar(Position posPacman, Position posGhost, Direction dirGhost)
        {
            clsBuildMap clsBuildMap = new clsBuildMap();

            string[,] arrText = clsBuildMap.BuildMap(posPacman, posGhost, dirGhost);

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

        //internal void DrawText(Transaction acTrans, Database acDb,
        //                       List<Position> lstPos, List<int> lstNumber,
        //                       ref List<DBText> lstText)
        //{
        //    if (lstPos.Count > 0)
        //    {
        //        clsGluttony clsGluttony = new clsGluttony();

        //        for (int i = 0; i < lstPos.Count; i++)
        //        {
        //            int x = lstPos[i].X;
        //            int y = lstPos[i].Y;
        //            double dblX1 = x * Cell + Middle;
        //            double dblY1 = y * Cell + Middle;

        //            DBText acText = clsGluttony.AddText(acTrans, acDb, lstNumber[i].ToString(), 2);

        //            acText.MoveEntityXY(dblX1, dblY1);

        //            lstText.Add(acText);
        //        }
        //    }
        //}

        internal void DrawText(Transaction acTrans, Database acDb, int[,] arrText, ref DBText[,] arrDBText)
        {
            if (arrText != null)
            {
                arrText.GetSize(out int col, out int row);

                clsGluttony clsGluttony = new clsGluttony();

                for (int x = 0; x < col; x++)
                {
                    for (int y = 0; y < row; y++)
                    {
                        if (arrText[x, y] != 0)
                        {
                            string strValue = arrText[x, y].ToString();
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