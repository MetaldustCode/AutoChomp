using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGetBlockAge
    {
        internal List<Position> GetBlockage(Boolean[,] arrXGrid, Point2d ptPosition, Position Position, Direction direction)
        {
            List<Position> lstNextCell = new List<Position>();

            List<Direction> lstValid = GetValidDirections(arrXGrid, ptPosition, direction, ref lstNextCell);

            if (lstValid.Contains(direction))
            {
                lstValid = new List<Direction> { direction };
            }

            List<Direction> lstDirection = GetInverse(lstValid);

            List<Position> lstBlockage = GetPosition(lstDirection, Position);

            return lstBlockage;
        }

        internal List<Position> GetReverseBlockage(Position Position, Direction direction)
        {
            clsGetDirection clsGetDirection = new clsGetDirection();
            Direction dirReverse = clsGetDirection.GetReverse(direction);

            List<Direction> lstDirection = new List<Direction>() { dirReverse };
            return GetPosition(lstDirection, Position);
        }

        public List<Direction> GetValidDirections(Boolean[,] arrXGrid, Point2d ptPosition, Direction Direction, ref List<Position> lstNextCell)
        {
            List<Direction> rtnValue = new List<Direction>();

            if (Direction != Direction.None)
            {
                clsGetDirection clsGetDirection = new clsGetDirection();
                Direction Reverse = clsGetDirection.GetReverse(Direction);

                List<int> lstRow = new List<int>();
                List<int> lstCol = new List<int>();

                clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

                if (clsGetCurrentCell.GetCellLocation3(arrXGrid, ptPosition, ref lstCol, ref lstRow))
                {
                    if (clsGetDirection.ProcessUp(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell))
                        rtnValue.Add(Direction.Up);

                    if (clsGetDirection.ProcessDown(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell))
                        rtnValue.Add(Direction.Down);

                    if (clsGetDirection.ProcessLeft(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell))
                        rtnValue.Add(Direction.Left);

                    if (clsGetDirection.ProcessRight(arrXGrid, ptPosition, lstRow, lstCol, ref lstNextCell))
                        rtnValue.Add(Direction.Right);
                }

                for (int i = rtnValue.Count - 1; i >= 0; i--)
                {
                    if (rtnValue[i] == Reverse)
                        rtnValue.RemoveAt(i);
                }
            }

            return rtnValue;
        }

        internal List<Position> GetPosition(List<Direction> lstDirection, Position Position)
        {
            List<Position> rtnValue = new List<Position>();
            for (int i = 0; i < lstDirection.Count; i++)
            {
                int x = Position.X;
                int y = Position.Y;

                lstDirection[i].GetDirection(ref x, ref y);
                rtnValue.Add(new Position(x, y));
            }
            return rtnValue;
        }

        internal List<Direction> GetInverse(List<Direction> lstDirection)
        {
            List<Direction> lstAll = new List<Direction>() { Direction.Up, Direction.Down, Direction.Right, Direction.Left };

            for (int i = lstAll.Count - 1; i >= 0; i--)
            {
                if (lstDirection.Contains(lstAll[i]))
                    lstAll.RemoveAt(i);
            }

            return lstAll;
        }
    }
}