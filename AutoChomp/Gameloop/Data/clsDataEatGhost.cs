using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataEatGhost
    {
        internal void EatGhosts()
        {
            GamePacman Pacman = clsCommon.GamePacman;
            Point2d ptPacman = Pacman.ptOrigin;

            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhost.Count; i++)
            {
                Point2d ptOrigin = lstGhost[i].ptOrigin;

                if (lstGhost[i].GhostState == GhostState.Afraid)
                {
                    if (IsOverLap(ptOrigin, ptPacman, 25))
                    {
                        lstGhost[i].ptOrigin = AlignToNearest(ptOrigin);

                        lstGhost[i].GhostState = GhostState.Dead;
                        lstGhost[i].Color = GhostColor.Default;
                        lstGhost[i].bolIsEaten = true;

                        clsNAudio clsNAudio = new clsNAudio();
                        clsNAudio.PlayEatGhost();

                        clsCommon.GameGhostCommon.bolEatGhost = true;
                        clsCommon.GamePacman.bolGraphicsRequired = true;
                        break;
                    }
                }
            }

            clsCommon.lstGameGhost = lstGhost;
        }

        internal Point2d AlignToNearest(Point2d pt)
        {
            clsReg clsReg = new clsReg();
            clsReg.GetSpacing(out double dblSpacing);

            GameDots GameDots = clsCommon.GameDots;
            List<Point2d> lstDotsPosition = GameDots.lstOrigin;

            if ((pt.X % dblSpacing == 0 &&
                pt.Y % dblSpacing == 0) ||
                lstDotsPosition.Contains(pt))
            {
                return pt;
            }
            else
            {
                Debug.Print("");
            }

            return pt;
        }

        internal void GetSpacing()
        {
        }

        internal Boolean IsOverLap(Point2d pt1, Point2d pt2, double dblValue)
        {
            if (GetDistance(pt1.X, pt1.Y, pt2.X, pt2.Y) < dblValue)
                return true;

            return false;
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
}