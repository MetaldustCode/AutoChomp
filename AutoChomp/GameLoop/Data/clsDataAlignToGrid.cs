using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataAlignToGrid
    {
        internal void AlignToGrid(Boolean bolCircleOnly)
        {
            AlignPacman(bolCircleOnly);
            AlignGhosts(bolCircleOnly);
        }

        internal void AlignPacman(Boolean bolCircleOnly)
        {
            GamePacman Pacman = clsCommon.GamePacman;

            if (Pacman != null)
            {
                Point2d pt = Pacman.ptOrigin;
                if (IsPositionValid(ref pt, out Position pos, out Direction direction))
                {
                    if (!bolCircleOnly)
                    {
                        Pacman.Reset_Update = true;
                        Pacman.Reset_ptOrigin = pt;
                        Pacman.Reset_Direction = direction;
                        Pacman.Reset_FacingDirection = direction;
                    }
                }
            }
        }

        internal Boolean IsPositionValid(ref Point2d pt, out Position pos, out Direction direction)
        {
            Boolean rtnValue = false;
            direction = Direction.None;

            Boolean[,] arrXGridPath = clsClassTables.arrXGridPath;
            List<Direction>[,] arrXDirection = clsClassTables.arrXDirection;

            //pt = new Point2d(935.375, 950);

            Point2d ptCopy = new Point2d(pt.X, pt.Y);

            if (GetPosition(ref pt, out pos))
            {
                Boolean bolValue = arrXGridPath[pos.X, pos.Y];
                if (bolValue)
                {
                    List<Direction> lstDirection = arrXDirection[pos.X, pos.Y];
                    direction = GetDirection(lstDirection);
                }
                else
                {
                    Debug.Print("");
                }
                rtnValue = true;
            }
            else
            {
                Debug.Print("");
            }
            return rtnValue;
        }

        internal void AlignGhosts(Boolean bolCircleOnly)
        {
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            if (lstGhost != null)
            {
                for (int i = 0; i < lstGhost.Count; i++)
                {
                    GameGhost Ghost = lstGhost[i];

                    if (Ghost.HouseState == HouseState.OutHouse)
                    {
                        Point2d pt = Ghost.ptOrigin;

                        if (IsPositionValid(ref pt, out Position pos, out Direction direction))
                        {
                            if (!bolCircleOnly)
                            {
                                Ghost.Reset_Update = true;
                                Ghost.Reset_Direction = direction;
                                Ghost.Reset_ptOrigin = pt;
                            }
                        }
                    }
                }

                clsCommon.lstGameGhost = lstGhost;
            }
        }

        internal Direction GetDirection(List<Direction> lstDirection)
        {
            if (lstDirection.Count > 0)
            {
                int index = clsRandomizer.RandomInteger(0, lstDirection.Count - 1);
                return lstDirection[index];
            }
            else
                return Direction.None;
        }

        // Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

        internal Position GetPosition(Point2d pt)
        {
            Point2d ptCopy = new Point2d(pt.X, pt.Y);
            GetPosition(ref ptCopy, out Position pos);

            return pos;
        }

        internal Boolean GetPosition(ref Point2d pt, out Position pos)
        {
            pos = new Position();

            Boolean rtnValue = true;

            double Cell = 100;
            // Find the nearest valid cell 

            Double X1 = (pt.X / Cell);
            Double Y1 = (pt.Y / Cell);

            int intX = (int)(Math.Floor(X1));
            int intY = (int)(Math.Floor(Y1));
            pos = new Position(intX, intY);

            double dblX = (Math.Floor(X1));
            double dblY = (Math.Floor(Y1));

            X1 = (Math.Floor(dblX) + .5) * Cell;
            Y1 = (Math.Floor(dblY) + .5) * Cell;

            pt = new Point2d(X1, Y1);

            clsCommon.GameDebug.lstCircleOrigin.Add(new Point2d(X1, Y1));

            return rtnValue;
        }
    }
}