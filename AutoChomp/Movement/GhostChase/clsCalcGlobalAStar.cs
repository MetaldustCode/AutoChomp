using AutoChomp.Gameloop.Data;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp
{
    internal class clsCalcGlobalAStar
    {
        internal Boolean GetNextDirection(ref Direction direction)
        {
            if (clsCommon.GamePacman != null && IsAfraid(clsCommon.lstGameGhost))
            {
                GamePacman Pacman = clsCommon.GamePacman;

                clsDataPacmanMove clsDataPacmanMove = new clsDataPacmanMove();
                if (clsDataPacmanMove.IsAtGrid(Pacman) &&
                    clsCommon.GamePacman.lstCurrentChase.Count > 0)
                {
                    Point2d ptOrigin = Pacman.ptOrigin;

                    clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
                    clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position posPacman);


                    List<Position> lstPos = clsCommon.GamePacman.lstCurrentChase;

                    if (lstPos.Count == 1)
                    {
                        if (posPacman.IsMatched(lstPos[0]))
                        {
                            direction = GetLocalDirection(Pacman.ptOrigin,
                                                          Pacman.GhostCurrentChase.ptOrigin);
                            return true;
                        }
                    }

                    if (lstPos.Count > 1)
                    {
                        //List<String> lstItems = new List<string>();
                        //for (int k = 0; k < lstPos.Count; k++)
                        //    lstItems.Add(String.Format("{0},{1}", lstPos[k].X, lstPos[k].Y));

                        for (int k = 0; k < lstPos.Count; k++)
                        {
                            if (!posPacman.IsMatched(lstPos[k]))
                            {
                                Position posNext = lstPos[k];
                                clsGetNextDirection clsGetNextDirection = new clsGetNextDirection();
                                direction = clsGetNextDirection.GetNextDirection(posPacman, posNext);
                                return true;
                            }
                        }
                    }
                }

                if (clsCommon.GamePacman.lstCurrentChase.Count > 0)
                    return true;
            }
            else
            {
                clsCommon.GamePacman.GhostCurrentChase = null;
                clsCommon.GamePacman.arrCurrentAStar = new int[0, 0];
                clsCommon.GamePacman.GhostCurrentChase = null;
                clsCommon.GamePacman.lstCurrentChase = new List<Position>();
            }

            return false;
        }

        internal void clsAStar()
        {
            if (clsCommon.GamePacman != null && IsAfraid(clsCommon.lstGameGhost))
            {
                GamePacman Pacman = clsCommon.GamePacman;

                Point2d ptOrigin = Pacman.ptOrigin;

                clsDataAlignToGrid clsDataAlignToGrid = new clsDataAlignToGrid();
                clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position posPacman);

                clsDataPacmanMove clsDataPacmanMove = new clsDataPacmanMove();

                if (clsDataPacmanMove.IsAtGrid(Pacman))// && !posPacman.IsMatched(Pacman.CurrentGhostPosition))
                {
                    Pacman.CurrentPacmanPosition = posPacman;

                    clsAStar clsAStar = new clsAStar();
                    //int[,] arrAStar = clsAStar.GenerateAStar(posPacman, posPacman, Pacman.Direction);
                    int[,] arrAStar = clsAStar.GenerateAStar(posPacman);
                    Pacman.arrCurrentAStar = arrAStar;
                    List<GameGhost> lstGameGhost = clsCommon.lstGameGhost;

                    List<GameGhost> lstGhost = new List<GameGhost>();
                    List<List<Position>> lstLstPosition = new List<List<Position>>();

                    for (int i = 0; i < lstGameGhost.Count; i++)
                    {
                        GameGhost Ghost = lstGameGhost[i];
                        Point2d ptGhostOrigin = Ghost.ptOrigin;
                        clsDataAlignToGrid.GetPosition(ref ptGhostOrigin, out Position posGhost);

                        //  Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

                        if (Ghost.GhostState == GhostState.Afraid)
                        {
                            clsSolvePath clsSolvePath = new clsSolvePath();
                            List<Position> lstPos = clsSolvePath.SolveAStar(arrAStar, posGhost);
                            lstPos.Reverse();

                            if (lstPos.Count > 0)
                            {
                                lstPos.Add(posGhost);
                                lstPos.RemoveAt(0);
                                lstGhost.Add(Ghost);
                                lstLstPosition.Add(lstPos);
                            }
                        }
                    }

                    if (lstLstPosition.Count > 0)
                    {
                        GameGhost gameGhost = clsCommon.GamePacman.GhostCurrentChase;

                        if (gameGhost != null && IsAfraid(gameGhost))
                            GetCurrent(gameGhost, lstGhost, lstLstPosition);
                        else
                            GetClosest(lstGhost, lstLstPosition);

                    }
                }
            }
        }

        internal Boolean IsAfraid(List<GameGhost> lstGameGhost)
        {
            for (int i = 0; i < lstGameGhost.Count; i++)
            {
                GameGhost Ghost = lstGameGhost[i];

                if (Ghost.GhostState == GhostState.Afraid &&
                    Ghost.HouseState == HouseState.OutHouse)
                    return true;
            }

            return false;

        }


        internal Boolean IsAfraid(GameGhost Ghost)
        {
            if (Ghost.GhostState == GhostState.Afraid &&
                Ghost.HouseState == HouseState.OutHouse)
                return true;

            return false;

        }


        internal Direction GetLocalDirection(Point2d pt1, Point2d pt2)
        {
            clsGetNextDirection clsGetNextDirection = new clsGetNextDirection();
            return clsGetNextDirection.GetNextDirection(pt1, pt2);
        }

        internal void GetCurrent(GameGhost Ghost, List<GameGhost> lstGhost, List<List<Position>> lstLstPosition)
        {
            for (int i = 0; i < lstLstPosition.Count; i++)
            {
                if (lstGhost[i] == Ghost)
                {
                    clsCommon.GamePacman.GhostCurrentChase = lstGhost[i];
                    clsCommon.GamePacman.lstCurrentChase = lstLstPosition[i];
                    break;
                }
            }
        }

        internal void GetClosest(List<GameGhost> lstGhost, List<List<Position>> lstLstPosition)
        {
            List<int> lstCount = new List<int>();

            for (int i = 0; i < lstLstPosition.Count; i++)
                lstCount.Add(lstLstPosition[i].Count);

            int intMin = lstCount.Min();

            for (int i = 0; i < lstLstPosition.Count; i++)
            {
                if (lstLstPosition[i].Count == intMin)
                {
                    clsCommon.GamePacman.GhostCurrentChase = lstGhost[i];
                    clsCommon.GamePacman.lstCurrentChase = lstLstPosition[i];
                    break;
                }
            }
        }
    }
}
