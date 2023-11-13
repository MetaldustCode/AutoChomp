using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsBuildAndSolve
    {
        internal void BuildToPacman(ref GameGhost Ghost, int i)
        {
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

            GamePacman Pacman = clsCommon.GamePacman;
            //Position posPacman = clsGetCurrentCell.GetCell(Pacman.ptOrigin, Pacman.Direction);

            Gameloop.Data.clsDataAlignToGrid clsDataAlignToGrid = new Gameloop.Data.clsDataAlignToGrid();
            Position posPacman = clsDataAlignToGrid.GetPosition(Pacman.ptOrigin);


            Build(ref Ghost, i, posPacman);
        }

        internal void Build(ref GameGhost Ghost, int i, Position posHome)
        {
            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
            // Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

            Gameloop.Data.clsDataAlignToGrid clsDataAlignToGrid = new Gameloop.Data.clsDataAlignToGrid();
            Position posGhost = clsDataAlignToGrid.GetPosition(Ghost.ptOrigin);


            clsAStar clsAStar = new clsAStar();
            int[,] arrAStar = null;
            if (Ghost.GhostState == GhostState.Afraid)
            {
                Position posCorner = GetCorner(i);
                arrAStar = clsAStar.GenerateAStar(posCorner, posGhost, Ghost.Direction);
            }

            if (Ghost.GhostState == GhostState.Alive)
                arrAStar = clsAStar.GenerateAStar(posHome, posGhost, Ghost.Direction);


            if (Ghost.GhostState == GhostState.Dead)
            {
                Position posHouse = new Position(15, 21);
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

        // + 4 From Pacman
        internal void CalculatePinky()
        {
            
        }

        internal Position GetCorner(int i)
        {
            List<Position> lstPosition = new List<Position>()
            {
                new Position(3, 3),
                new Position(3, 31),
                new Position(28, 3),
                new Position(28, 31)
            };

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