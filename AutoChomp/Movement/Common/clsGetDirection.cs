using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGetDirection
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal List<Direction> GetPossibleDirection(Position ptPosition)
        {
            double x = (ptPosition.X * Cell) + Middle;
            double y = (ptPosition.Y * Cell) + Middle;

            Point2d ptOrigin = new Point2d(x, y);

            return GetPossibleDirection(ptOrigin);
        }

        internal List<Direction> GetPossibleDirection(Point2d ptOrigin)
        {
            List<Direction> rtnValue = new List<Direction>();
            List<Direction> lstDirection = GetDirection();

            for (int i = 0; i < lstDirection.Count; i++)
            {
                List<Position> lstNextCell = new List<Position>();
                if (CanCharacterMove(ptOrigin, lstDirection[i], ref lstNextCell))
                    rtnValue.Add(lstDirection[i]);
            }

            return rtnValue;
        }

        internal List<Direction> GetValidDirection(Point2d pt, Direction direction, Boolean bolIncludeReverse)
        {
            List<Direction>[,] arrXCanMove = clsClassTables.arrXDirection;

            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();
            Position ptCell = clsGetCell.GetCell(pt, direction);

            if (ptCell != new Position(0, 0))
            {
                List<Direction> lstDirection = arrXCanMove[ptCell.X, ptCell.Y];

                if (!bolIncludeReverse)
                {
                    clsGetDirection clsGetDirection = new clsGetDirection();
                    direction = clsGetDirection.GetReverse(direction);

                    if (lstDirection != null)
                    {
                        if (lstDirection.Contains(direction))
                        {
                            int intPos = lstDirection.IndexOf(direction);
                            lstDirection.RemoveAt(intPos);
                        }
                    }
                }

                return lstDirection;
            }

            return new List<Direction>();
        }

        internal List<Direction> GetValidDirection(Position ptCell, Direction direction, Boolean bolIncludeReverse)
        {
            List<Direction>[,] arrXCanMove = clsClassTables.arrXDirection;

            Boolean[,] arrXGrid = clsClassTables.arrXGridPath;

            if (ptCell != new Position(0, 0))
            {
                arrXGrid.GetSize(out int col, out int row);

                if (col.IsInGrid(row, ptCell.X, ptCell.Y))
                {
                    // Get copy of cell, don't modify the original value
                    List<Direction> lstDirection = new List<Direction>(arrXCanMove[ptCell.X, ptCell.Y]);

                    if (!bolIncludeReverse)
                    {
                        clsGetDirection clsGetDirection = new clsGetDirection();
                        direction = clsGetDirection.GetReverse(direction);

                        if (lstDirection != null)
                        {
                            if (lstDirection.Contains(direction))
                            {
                                int intPos = lstDirection.IndexOf(direction);
                                lstDirection.RemoveAt(intPos);
                            }
                        }
                    }

                    return lstDirection;
                }
            }

            return new List<Direction>();
        }

        internal List<Direction> GetDirection()
        {
            return new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        }

        internal Boolean CanCharacterMove(Point2d ptPosition, Direction Direction, ref List<Position> lstNextCell)
        {
            bool[,] arrXGrid = clsClassTables.arrXGridPath;

            List<int> lstRow = new List<int>();
            List<int> lstCol = new List<int>();

            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

            if (clsGetCurrentCell.GetCellLocation3(arrXGrid, ptPosition, ref lstCol, ref lstRow))
            {
                Boolean bolMove = false;

                if (Direction == Direction.Up)
                    bolMove = ProcessUp(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell);

                if (Direction == Direction.Down)
                    bolMove = ProcessDown(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell);

                if (Direction == Direction.Left)
                    bolMove = ProcessLeft(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell);

                if (Direction == Direction.Right)
                    bolMove = ProcessRight(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell);

                return bolMove;
            }

            return false;
        }

        internal Boolean ProcessRight(Boolean[,] arr, Point2d ptPosition, List<int> lstRow, List<int> lstCol, ref List<Position> lstNextCell)
        {
            Boolean bolMove = false;

            int intRow = -1;
            int intCol = -1;

            GetLastIndex(lstRow, lstCol, ref intRow, ref intCol);

            lstNextCell.Add(new Position(intCol, intRow));

            double dblX1 = ptPosition.X;
            double dblX2 = intCol * Cell;

            if (arr.IsInGrid(intCol, intRow))
            {
                if (arr.GetLength(0) - 1 != intCol)
                {
                    Boolean bolCell = arr[intCol + 1, intRow];

                    if (!bolCell)
                    {
                        if ((dblX1 < dblX2 + Middle) && InMiddle(ptPosition.Y))
                            bolMove = true;
                    }
                    else
                    {
                        if (InMiddle(ptPosition.Y))
                            bolMove = true;
                    }
                }
                else
                    bolMove = true;
            }

            return bolMove;
        }

        internal Boolean ProcessLeft(Boolean[,] arr, Point2d ptPosition, List<int> lstRow, List<int> lstCol, ref List<Position> lstNextCell)
        {
            Boolean bolMove = false;

            int intRow = -1;
            int intCol = -1;

            GetLastIndex(lstRow, lstCol, ref intRow, ref intCol);

            lstNextCell.Add(new Position(intCol, intRow));

            if (arr.IsInGrid(intCol, intRow))
            {
                if (intCol != 0)
                {
                    double dblX1 = ptPosition.X;
                    double dblX2 = intCol * Cell;

                    Boolean bolCell = arr[intCol - 1, intRow];

                    if (!bolCell)
                    {
                        if ((dblX1 > dblX2 + Middle) && InMiddle(ptPosition.Y))
                            bolMove = true;
                    }
                    else
                    {
                        if (InMiddle(ptPosition.Y))
                            bolMove = true;
                    }
                }
                else
                    bolMove = true;
            }

            return bolMove;
        }

        internal Boolean ProcessUp(Boolean[,] arr, Point2d ptPosition, List<int> lstRow, List<int> lstCol, ref List<Position> lstNextCell)
        {
            Boolean bolMove = false;

            int intRow = -1;
            int intCol = -1;

            GetLastIndex(lstRow, lstCol, ref intRow, ref intCol);

            lstNextCell.Add(new Position(intCol, intRow));

            if (arr.IsInGrid(intCol, intRow))
            {
                if (arr.GetLength(1) - 1 != intRow)
                {
                    double dblY1 = ptPosition.Y;
                    double dblY2 = intRow * Cell;

                    Boolean bolCell = arr[intCol, intRow + 1];

                    if (!bolCell)
                    {
                        if ((dblY1 < dblY2 + Middle) && InMiddle(ptPosition.X))
                            bolMove = true;
                    }
                    else
                    {
                        if (InMiddle(ptPosition.X))
                            bolMove = true;
                    }
                }
                else
                    bolMove = true;
            }
            return bolMove;
        }

        internal Boolean ProcessDown(Boolean[,] arr, Point2d ptPosition, List<int> lstRow, List<int> lstCol, ref List<Position> lstNextCell)
        {
            Boolean bolMove = false;

            int intRow = -1;
            int intCol = -1;

            GetLastIndex(lstRow, lstCol, ref intRow, ref intCol);

            lstNextCell.Add(new Position(intCol, intRow));

            if (arr.IsInGrid(intCol, intRow))
            {
                if (intRow != 0)
                {
                    double dblY1 = ptPosition.Y;
                    double dblY2 = intRow * Cell;

                    Boolean bolCell = arr[intCol, intRow - 1];

                    if (!bolCell)
                    {
                        if ((dblY1 > dblY2 + Middle) && InMiddle(ptPosition.X))
                            bolMove = true;
                    }
                    else
                    {
                        if (InMiddle(ptPosition.X))
                            bolMove = true;
                    }
                }
                else
                    bolMove = true;
            }

            return bolMove;
        }

        internal void GetLastIndex(List<int> lstRow, List<int> lstCol,
                                   ref int intRow, ref int intCol)
        {
            intRow = lstRow[lstRow.Count - 1];
            intCol = lstCol[lstCol.Count - 1];
        }

        internal Boolean InMiddle(double dblValue)
        {
            if ((dblValue % 100 == Middle))
                return true;

            return false;
        }

        internal Direction GetRandomDirection(Point2d ptPosition, Direction CurrentDirection, int intBias)
        {
            if (clsClassTables.lstXGridOrigin.Contains(ptPosition) || CurrentDirection == Direction.None)
            {
                clsGetDirection clsCharacterDirection = new clsGetDirection();
                List<Direction> lstDirection = clsCharacterDirection.GetPossibleDirection(ptPosition);

                Direction ReverseDirection = GetReverse(CurrentDirection);

                if (lstDirection.Contains(ReverseDirection))
                    lstDirection.RemoveAt(lstDirection.IndexOf(ReverseDirection));

                if (lstDirection.Contains(CurrentDirection))
                    for (int i = 0; i < intBias; i++)
                        lstDirection.Add(CurrentDirection);

                if (lstDirection.Count > 0)
                {
                    int index = clsRandomizer.RandomInteger(0, lstDirection.Count - 1);
                    return lstDirection[index];
                }
            }

            return CurrentDirection;
        }

        internal Direction GetReverse(Direction direction)
        {
            if (direction != Direction.None)
            {
                List<Direction> lstDirection = new List<Direction>() { Direction.Up, Direction.Down, Direction.Right, Direction.Left };
                List<Direction> lstReverse = new List<Direction>() { Direction.Down, Direction.Up, Direction.Left, Direction.Right };

                for (int i = 0; i < lstDirection.Count; i++)
                {
                    if (lstDirection[i] == direction)
                        return lstReverse[i];
                }
            }

            return Direction.None;
        }

    }
}