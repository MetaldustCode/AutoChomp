using Autodesk.AutoCAD.Geometry;
using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataGhostTarget
    {
        // "Red", "Pink", "Blue", "Orange"
        internal List<Position> GetTarget()
        {
            Position posRed = GetRed();
            Position posPink = GetPink();
            Position posBlue = GetBlue();
            Position posOrange = GetOrange();

            List<Position> target = new List<Position>()
                { posRed, posPink, posBlue, posOrange};

            for (int i = 0; i < target.Count; i++)
            {
                AddCircle(target[i]);
            }

            return target;
        }

        internal Position GetOrange()
        {
            clsBuildAndSolve clsBuildAndSolve = new clsBuildAndSolve();
            Position rtnValue = clsBuildAndSolve.GetCorner(3);

            GamePacman Pacman = clsCommon.GamePacman;

            Point2d ptOrigin = Pacman.ptOrigin;

            clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
            clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position pos);

            Point2d ptOriginOrange = GetOrigin("Orange");
            clsDataAlignToGrid.GetPosition(ref ptOriginOrange, out Position posOrange);

            GetDistance(pos, posOrange, out double dblAngle, out double dblDistance);

            if (dblDistance > 8.0)
                rtnValue = pos;

            return rtnValue;
        }

        internal Position GetRed()
        {
            GamePacman Pacman = clsCommon.GamePacman;

            Point2d ptOrigin = Pacman.ptOrigin;

            clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
            clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position pos);

            return pos;
        }

        internal Position GetBlue()
        {
            GetBlue(out Position pos, out double angle, out double distance);
            clsDataGhostAngle clsDataGhostAngle = new clsDataGhostAngle();
            return clsDataGhostAngle.GetBlueCell(pos, angle, distance);
        }

        internal Position GetPink()
        {
            GamePacman Pacman = clsCommon.GamePacman;

            Point2d ptOrigin = Pacman.ptOrigin;

            clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
            clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position pos);

            Position posPinkyTarget = GetTargetOffset(Pacman.FacingDirection, pos, 4);

            return posPinkyTarget;
        }

        internal void GetBlue(out Position posTarget, out double dblAngle, out double dblDistance)
        {
            GamePacman Pacman = clsCommon.GamePacman;

            Point2d ptOrigin = Pacman.ptOrigin;

            clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
            clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position posPacman);
            posTarget = GetTargetOffset(Pacman.FacingDirection, posPacman, 2);

            Point2d ptOriginRed = GetOrigin("Red");
            clsDataAlignToGrid.GetPosition(ref ptOriginRed, out Position posRed);

            GetDistance(posRed, posTarget, out dblAngle, out dblDistance);


        }

        internal void GetDistance(Position pos1, Position pos2, out double dblAngle, out double dblDistance)
        {
            double x1 = (pos1.X);
            double x2 = (pos2.X);
            double y1 = (pos1.Y);
            double y2 = (pos2.Y);

            // Calculate the angle in radians
            dblAngle = Math.Atan2(y2 - y1, x2 - x1);
            dblAngle = dblAngle.ToDegrees();

            // Calculate the distance
            dblDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        internal Point2d GetOrigin(string strGhost)
        {
            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhosts.Count; i++)
            {
                GameGhost Ghost = lstGhosts[i];

                if (Ghost.strName == strGhost)
                {
                    Point2d ptOrigin = Ghost.ptOrigin;

                    clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
                    clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position pos);

                    return ptOrigin;
                }
            }

            return new Point2d();
        }

        internal void AddCircle(Position pos)
        {
            Point2d pt = new Point2d(pos.X * 100 + 50,
                                     pos.Y * 100 + 50);

            clsCommon.GameDebug.lstCircleOrigin.Add(pt);
        }

        internal Position GetTargetOffset(Direction Dir, Position posStart, int intOffset)
        {
            Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;
            List<Direction>[,] arrXDirection = clsClassTables.arrXDirection;

            arrXGridPath.GetSize(out int col, out int row);

            Position posTemp = posStart;

            List<Position> rtnPos = new List<Position>() { posTemp };

            for (int i = 0; i < intOffset; i++)
            {
                if (Dir == Direction.Right)
                {
                    posTemp = new Position(posTemp.X + 1, posTemp.Y);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y) &&
                        arrXDirection[posTemp.X, posTemp.Y].Count != 0)
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Left)
                {
                    posTemp = new Position(posTemp.X - 1, posTemp.Y);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y) &&
                        arrXDirection[posTemp.X, posTemp.Y].Count != 0)
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Up)
                {
                    posTemp = new Position(posTemp.X, posTemp.Y + 1);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y) &&
                        arrXDirection[posTemp.X, posTemp.Y].Count != 0)
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Down)
                {
                    posTemp = new Position(posTemp.X, posTemp.Y - 1);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y) &&
                        arrXDirection[posTemp.X, posTemp.Y].Count != 0)
                        rtnPos.Add(posTemp);
                }
            }

            return rtnPos[rtnPos.Count - 1];
        }


        internal Position GetTargetOffset2(Direction Dir, Position posStart, int intOffset)
        {
            Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;
            List<Direction>[,] arrXDirection = clsClassTables.arrXDirection;

            arrXGridPath.GetSize(out int col, out int row);

            Position posTemp = posStart;

            List<Position> rtnPos = new List<Position>() { posTemp };

            for (int i = 0; i < intOffset; i++)
            {
                if (Dir == Direction.Right)
                {
                    posTemp = new Position(posTemp.X + 1, posTemp.Y);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y))
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Left)
                {
                    posTemp = new Position(posTemp.X - 1, posTemp.Y);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y))
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Up)
                {
                    posTemp = new Position(posTemp.X, posTemp.Y + 1);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y))
                        rtnPos.Add(posTemp);
                }

                if (Dir == Direction.Down)
                {
                    posTemp = new Position(posTemp.X, posTemp.Y - 1);
                    if (col.IsInGrid(row, posTemp.X, posTemp.Y))
                        rtnPos.Add(posTemp);
                }
            }

            return rtnPos[rtnPos.Count - 1];
        }


    }
}
