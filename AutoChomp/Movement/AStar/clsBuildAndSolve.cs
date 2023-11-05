using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsBuildAndSolve
    {
        internal void BuildToPacman(ref GameGhost Ghost)
        {
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

            GamePacman Pacman = clsCommon.GamePacman;
            Position posPacman = clsGetCurrentCell.GetCell(Pacman.ptOrigin, Pacman.Direction);

            Build(ref Ghost, posPacman);
        }

        internal void BuildToHouse(ref GameGhost Ghost)
        {
            Position posHome = new Position(16, 21);
            Build(ref Ghost, posHome);
        }

        internal void BuildToCorner(ref GameGhost Ghost, int i)
        {
            Position posHome = GetCorner(i);
            Build(ref Ghost, posHome);
        }

        internal void Build(ref GameGhost Ghost, Position posHome)
        {
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
            Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

            clsAStar clsAStar = new clsAStar();
            int[,] arrAStar = null;
            if (Ghost.GhostState == GhostState.Alive || Ghost.GhostState == GhostState.Afraid)
                arrAStar = clsAStar.GenerateAStar(posHome, posGhost, Ghost.Direction);

            if (Ghost.GhostState == GhostState.Dead)
            {
                Position posHouse = new Position(16, 21);
                arrAStar = clsAStar.GenerateAStar(posHouse, posGhost, Ghost.Direction);
            }

            clsSolvePath clsSolvePath = new clsSolvePath();
            List<Position> lstAStarPosition = clsSolvePath.SolveAStar(arrAStar, posGhost);

            // Get int Numbers
            List<int> lstAStarNumber = GetNumbers(arrAStar, lstAStarPosition);

            if (lstAStarNumber.Count > 0)
            {
                Ghost.arrAStarGrid = arrAStar;
                Ghost.lstAStarPosition = lstAStarPosition;
                Ghost.lstAStarNumber = lstAStarNumber;
            }
            else
            {
                Ghost.arrAStarGrid = new int[0, 0];
                Ghost.lstAStarPosition = new List<Position>();
                Ghost.lstAStarNumber = new List<int>();
            }
        }

        internal Position GetCorner(int i)
        {
            List<Position> lstPosition = new List<Position>();
            lstPosition.Add(new Position(3, 3));
            lstPosition.Add(new Position(3, 31));
            lstPosition.Add(new Position(28, 3));
            lstPosition.Add(new Position(28, 31));

            lstPosition = lstPosition.Multiply();

            return lstPosition[i];
        }

        internal Boolean HasMatch(List<Position> lstPos, Position pos)
        {
            for (int i = 0; i < lstPos.Count; i++)
            {
                if (lstPos[i].X == pos.X && lstPos[i].Y == pos.Y)
                {
                    if (i != lstPos.Count - 1)
                        return true;
                }
            }
            return false;
        }

        internal List<int> GetNumbers(int[,] arrAStar, List<Position> lstAStarPosition)
        {
            List<int> lstValue = new List<int>();

            if (arrAStar != null && arrAStar.Length > 0)
            {
                for (int j = 0; j < lstAStarPosition.Count; j++)
                    lstValue.Add(arrAStar[lstAStarPosition[j].X, lstAStarPosition[j].Y]);
            }

            return lstValue;
        }
    }
}