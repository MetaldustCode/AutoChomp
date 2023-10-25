using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp.Data
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

            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

            Position pos = clsGetCell.GetCell(Pacman.Origin, Pacman.Direction);

            Point2d pt = pos.GetOrigin();

            clsCommon.GamePacman.Origin = pt;
        }

        internal void AlignGhosts()
        {
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhost.Count; i++)
            {
                clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

                Position pos = clsGetCell.GetCell(lstGhost[i].Origin, lstGhost[i].Direction);

                Point2d pt = pos.GetOrigin();

                lstGhost[i].Origin = pt;
            }

            clsCommon.lstGameGhost = lstGhost;
        }
    }
}