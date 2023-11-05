using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataAlignToGrid
    {
        internal void AlignToGrid()
        {
            AlignPacman();
            AlignGhosts();
        }

        internal void AlignPacman()
        {
            GamePacman Pacman = clsCommon.GamePacman;

            if (Pacman != null)
            {
                clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

                Position pos = clsGetCell.GetCell(Pacman.ptOrigin, Pacman.Direction);

                Point2d pt = pos.GetOrigin();

                clsCommon.GamePacman.ptOrigin = pt;
            }
        }

        internal void AlignGhosts()
        {
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            if (lstGhost != null)
            {
                for (int i = 0; i < lstGhost.Count; i++)
                {
                    clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

                    Position pos = clsGetCell.GetCell(lstGhost[i].ptOrigin, lstGhost[i].Direction);

                    Point2d pt = pos.GetOrigin();

                    lstGhost[i].ptOrigin = pt;
                }

                clsCommon.lstGameGhost = lstGhost;
            }
        }
    }
}