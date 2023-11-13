using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataGhostAngle
    {
        internal Position GetBlueCell(Position posStart, double angle, double distance)
        {
            Position rtnValue = new Position();
            GamePacman Pacman = clsCommon.GamePacman;

            Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;
            List<Direction>[,] arrXDirection = clsClassTables.arrXDirection;

            arrXGridPath.GetSize(out int col, out int row);

            Size size = new Size(col, row);

            List<Position> cells = GetIntersectedCells(size, posStart, angle, distance);

            clsDataGhostTarget clsDataGhostTarget = new clsDataGhostTarget();

            for (int i = cells.Count - 1; i >= 0; i--)
            {
                if (arrXDirection[cells[i].X, cells[i].Y].Count == 0)
                    cells.RemoveAt(i);
            }

            if (cells.Count > 0)
            {
                rtnValue = cells[cells.Count - 1];
            }

            return rtnValue;
        }

        internal List<Position> GetIntersectedCells(Size size, Position posStart, double angle, double distance)
        {
            List<Position> intersectedCells = new List<Position>();

            double angleInRadians = Math.PI * angle / 180.0;
            double endX = posStart.X + distance * Math.Cos(angleInRadians);
            double endY = posStart.Y + distance * Math.Sin(angleInRadians);

            int x = posStart.X;
            int y = posStart.Y;

            int dx = Math.Abs((int)(endX - posStart.X));
            int dy = Math.Abs((int)(endY - posStart.Y));

            int sx = posStart.X < endX ? 1 : -1;
            int sy = posStart.Y < endY ? 1 : -1;

            int err = dx - dy;

            int intExit = 0;

            while (true)
            {
                intExit++;

                if (!CheckDistance(posStart, new Position(x, y), distance))
                    break;

                if (x >= 0 && x < size.Width && y >= 0 && y < size.Height)
                    intersectedCells.Add(new Position(x, y));

                if (x == (int)endX && y == (int)endY)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }

            return intersectedCells;
        }

        internal Boolean CheckDistance(Position posStart, Position posEnd, double dblDistance)
        {
            double x1 = (posStart.X);
            double x2 = (posEnd.X);
            double y1 = (posStart.Y);
            double y2 = (posEnd.Y);

            // Calculate the distance
            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            if (distance < dblDistance)
                return true;
            return false;

        }
    }
}