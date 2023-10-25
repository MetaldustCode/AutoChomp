using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp
{
    internal class clsAStar
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void BuildGrid()
        {
            // Clear Tables
            clsGenerateTables clsGenerateTables = new clsGenerateTables();
            clsGenerateTables.GenerateAStarNumber();

            clsGetBlockAge clsGetBlockAge = new clsGetBlockAge();
            bool[,] arrXGrid = clsClassTables.arrXGridPath;

            Point2d ptOrigin = clsCommon.GamePacman.Origin;
            Direction direction = clsCommon.GamePacman.Direction;

            List<Position> lstNextCell = new List<Position>();
            List<Direction> lstValid = clsGetBlockAge.GetValidDirections(arrXGrid, ptOrigin, direction, ref lstNextCell);

            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

            Position posCell = clsGetCell.GetCell(ptOrigin, direction);
        }

        internal void BuildDefaultGrid(Boolean[,] arrXGridPath,
                                       ref int[,] arrAStarNum,
                                       ref DBText[,] arrAStarText,
                                       Transaction acTrans, Database acDb)
        {
            if (arrXGridPath != null)
            {
                arrXGridPath.GetSize(out int col, out int row);


                clsGluttony clsGluttony = new clsGluttony();

                for (int x = 0; x < col; x++)
                {
                    for (int y = 0; y < row; y++)
                    {
                        if (clsClassTables.arrXGridPath[x, y])
                        {
                            arrAStarText[x, y] = clsGluttony.AddText(acTrans, acDb, "0", 2);
                            arrAStarNum[x, y] = 0;

                            double dblX1 = x * Cell + Middle;
                            double dblY1 = y * Cell + Middle;

                            arrAStarText[x, y].MoveEntityXY(dblX1, dblY1);
                        }
                    }
                }
            }
        }


        internal void UpdateTextGrid(int[,] arrAStarNum, ref DBText[,] arrAStarText, Transaction acTrans)
        {
            if (arrAStarNum != null)
            {
                arrAStarText.GetSize(out int col, out int row);

                for (int x = 0; x < col; x++)
                {
                    for (int y = 0; y < row; y++)
                    {
                        if (clsClassTables.arrXGridPath[x, y])
                        {
                            string strTextValue = arrAStarNum[x, y].ToString();

                            DBText acText = acTrans.GetObject(arrAStarText[x, y].ObjectId, OpenMode.ForWrite) as DBText;
                            acText.TextString = strTextValue;
                        }
                    }
                }
            }
        }


        internal void CreateTextData()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;
                    int[,] arrAStarNum = clsClassTables.arrAStarNum;
                    DBText[,] arrAStarText = clsClassTables.arrAStarText;

                    BuildDefaultGrid(arrXGridPath, ref arrAStarNum, ref arrAStarText, acTrans, acDb);

                    clsClassTables.arrAStarNum = arrAStarNum;
                    clsClassTables.arrAStarText = arrAStarText;

                    acTrans.Commit();
                }
            }
        }


        internal void DrawValid()
        {
            int intWidth = -1; int intHeight = -1;

            clsGetGrid clsGetGrid = new clsGetGrid();
            Boolean bolCellSize = clsGetGrid.GetCellSize(ref intWidth, ref intHeight);

            if (bolCellSize)
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database acDb = acDoc.Database;


                clsGetBlockAge clsGetBlockAge = new clsGetBlockAge();
                bool[,] arrXGrid = clsClassTables.arrXGridPath;

                Point2d ptOrigin = clsCommon.GamePacman.Origin;
                Direction direction = clsCommon.GamePacman.Direction;

                List<Position> lstNextCell = new List<Position>();
                List<Direction> lstValid = clsGetBlockAge.GetValidDirections(arrXGrid, ptOrigin, direction, ref lstNextCell);


                // Start a transaction
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    using (DocumentLock @lock = acDoc.LockDocument())
                    {
                        List<Direction>[,] arr = clsClassTables.arrXDirection;

                        for (int x = 0; x < intWidth; x++)
                        {
                            for (int y = 0; y < intHeight; y++)
                            {
                                if (arr[x, y] != null)
                                {
                                    if (arr[x, y].Count > 0)
                                    {
                                        Point2d ptMid = new Point2d(x * Cell + Middle, y * Cell + Middle);

                                        List<Direction> lstDirections = arr[x, y];

                                        for (int d = 0; d < lstDirections.Count; d++)
                                        {
                                            //if (lstDirections[d] == Direction.Up)
                                            //    AddUp(acTrans, acDb, ptMid);

                                            //if (lstDirections[d] == Direction.Down)
                                            //    AddDown(acTrans, acDb, ptMid);

                                            //if (lstDirections[d] == Direction.Right)
                                            //    AddRight(acTrans, acDb, ptMid);

                                            //if (lstDirections[d] == Direction.Left)
                                            //    AddLeft(acTrans, acDb, ptMid);
                                        }
                                    }
                                }
                            }
                        }

                        acTrans.Commit();
                    }
                }
            }
        }
    }
}
