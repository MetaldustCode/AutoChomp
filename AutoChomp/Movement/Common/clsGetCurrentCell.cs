using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGetCurrentCell
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal Position GetPacmanPosition()
        {
            Point2d ptOrigin = clsCommon.GamePacman.Origin;
            Direction direction = clsCommon.GamePacman.Direction;

            return GetCell(ptOrigin, direction);
        }

        internal Position GetCell(Point2d ptOrigin, Direction Direction)
        {
            Position ptCell = new Position();

            if (clsClassTables.lstXGridOrigin.Contains(ptOrigin))
                ptCell = GetMidCell(ptOrigin);
            else
                GetCell(ptOrigin, Direction, ref ptCell);

            return ptCell;
        }

        internal Position GetMidCell(Point2d ptPosition)
        {
            if (clsClassTables.lstXGridOrigin.Contains(ptPosition))
            {
                double dblX = (ptPosition.X - Middle) / Cell;
                double dblY = (ptPosition.Y - Middle) / Cell;

                int x = Convert.ToInt32(dblX);
                int y = Convert.ToInt32(dblY);

                return new Position(x, y);
            }
            return new Position(0, 0);
        }

        private Boolean GetCell(Point2d ptPosition, Direction Direction, ref Position ptXY)
        {
            List<int> lstRow = new List<int>();
            List<int> lstCol = new List<int>();

            if (GetCellLocation2(ptPosition, Direction, ref lstCol, ref lstRow))
            {
                if (lstRow.Count > 0)
                {
                    int col = lstCol[lstCol.Count - 1];
                    int row = lstRow[lstRow.Count - 1];

                    if (!(col == 0 && row == 0))
                    {
                        ptXY = new Position(lstCol[lstCol.Count - 1], lstRow[lstRow.Count - 1]);
                        return true;
                    }
                }
            }

            return false;
        }

        private Boolean GetCellLocation2(Point2d ptPosition, Direction Direction, ref List<int> lstCol, ref List<int> lstRow)
        {
            Boolean rtnValue = false;

            Boolean[,] arrGrid = clsClassTables.arrXGridPath;

            if (GetCellLocation3(arrGrid, ptPosition, ref lstCol, ref lstRow))
            {
                rtnValue = true;

                if (lstRow.Count == 2)
                {
                    int X1 = lstCol[0];
                    int Y1 = lstRow[0];

                    int X2 = lstCol[1];
                    int Y2 = lstRow[1];

                    if (Direction == Direction.Up)
                    {
                        if (Y1 > Y2)
                        {
                            lstRow.Reverse();
                            lstCol.Reverse();
                        }
                    }

                    if (Direction == Direction.Down)
                    {
                        if (Y1 < Y2)
                        {
                            lstRow.Reverse();
                            lstCol.Reverse();
                        }
                    }

                    if (Direction == Direction.Left)
                    {
                        if (X1 < X2)
                        {
                            lstRow.Reverse();
                            lstCol.Reverse();
                        }
                    }

                    if (Direction == Direction.Right ||
                        Direction == Direction.None)
                    {
                        if (X1 > X2)
                        {
                            lstRow.Reverse();
                            lstCol.Reverse();
                        }
                    }
                }
            }

            return rtnValue;
        }

        internal Boolean GetCellLocation3(Boolean[,] arrBorder, Point2d ptPosition,
                                                ref List<int> lstCol, ref List<int> lstRow)
        {
            int col = arrBorder.GetLength(0);
            int row = arrBorder.GetLength(1);

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arrBorder[c, r])
                    {
                        double x1 = c * Cell;
                        double x2 = (c * Cell) + Cell;

                        double y1 = r * Cell;
                        double y2 = (r * Cell) + Cell;

                        if (ptPosition.X >= x1 && ptPosition.X <= x2 &&
                            ptPosition.Y >= y1 && ptPosition.Y <= y2)
                        {
                            lstCol.Add(c);
                            lstRow.Add(r);

                            if (lstRow.Count > 1)
                                break;
                        }
                    }
                }
                if (lstRow.Count > 1)
                    break;
            }

            if (lstRow.Count > 0 && lstCol.Count > 0)
                return true;

            return false;
        }

        internal Boolean GetCellPosition(ref List<Position> lstPosGhost, ref Position posPacman)
        {
            if (clsCommon.lstGameGhost != null)
            {
                for (int i = 0; i < clsCommon.lstGameGhost.Count; i++)
                {
                    Position ptStart = new Position();

                    GetCell(clsCommon.lstGameGhost[i].Origin,
                                    clsCommon.lstGameGhost[i].Direction,
                                    ref ptStart);

                    lstPosGhost.Add(ptStart);
                }

                GetCell(clsCommon.GamePacman.Origin,
                                clsCommon.GamePacman.Direction,
                                ref posPacman);
                return true;
            }

            return false;
        }
    }
}