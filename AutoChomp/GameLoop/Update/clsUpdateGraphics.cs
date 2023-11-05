using AutoChomp.Gameloop.Graphics;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutoChomp.Gameloop.Update
{
    internal class clsUpdateGraphics
    {
        internal void UpdateGraphics(Transaction acTrans, Database acDb)
        {
            // if (!clsValues.IsResetRunning)

            if (clsCommon.GamePacman.bolGraphicsRequired || clsCommon.GameGhostCommon.bolGraphicsRequired)
            {
                //clsSolvePath clsSolvePath = new clsSolvePath();
                //clsSolvePath.Solve();

                clsReg clsReg = new clsReg();
                clsAStar clsAStar = new clsAStar();

                if (clsReg.GetNumbersVisible())
                    clsAStar.CreateTextData();
                else
                    clsAStar.DeleteText();

                // UpdateAStarLine(acTrans, acDb);
            }

            //Update Pacman
            if (clsCommon.GamePacman.bolGraphicsRequired)
            {
                clsGraphicsPacman clsGraphicsPacman = new clsGraphicsPacman();
                clsGraphicsPacman.UpdatePacmanPositionAndVisibility(acTrans, acDb, ref clsCommon.GamePacman);
                clsCommon.GamePacman.bolGraphicsRequired = false;

                //if (clsCommon.GamePacman.bolCellChanged)
                //{
                //    clsReg clsReg = new clsReg();
                //    clsAStar clsAStar = new clsAStar();
                //    clsCommon.GamePacman.bolCellChanged = false;
                //}
            }

            //Hide Dots
            if (clsCommon.GameDots.bolGraphicsRequired)
            {
                Gameloop.Graphics.clsGraphicsDots clsEatGraphics = new Gameloop.Graphics.clsGraphicsDots();
                clsEatGraphics.DisplayDots(acTrans);
                clsCommon.GameDots.bolGraphicsRequired = false;
            }

            //Flash Power
            if (clsCommon.GamePower.bolGraphicsRequired)
            {
                Gameloop.Graphics.clsGraphicsDots clsEatGraphics = new Gameloop.Graphics.clsGraphicsDots();
                clsEatGraphics.DisplayPower(acTrans);
                clsCommon.GamePower.bolGraphicsRequired = false;
            }

            if (clsCommon.GameGhostCommon.bolGraphicsRequired)
            {
                List<GameGhost> lstGhost = clsCommon.lstGameGhost;
                // Update Ghost
                Gameloop.Graphics.clsGraphicsGhost clsGhostPosition = new Gameloop.Graphics.clsGraphicsGhost();
                clsGhostPosition.UpdateGhostGraphics(acTrans, acDb, ref lstGhost);
                clsCommon.GameGhostCommon.bolGraphicsRequired = false;
            }
        }

        internal void UpdateAStarLine(Transaction acTrans, Database acDb)
        {
            clsSolvePath clsSolvePath = new clsSolvePath();
            clsSolvePath.DeleteLine(acTrans, acDb);

            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            List<int> lstColor = new List<int>() { 1, 241, 4, 30 }.Multiply();

            for (int i = 0; i < lstGhosts.Count; i++)
            {
                if (lstGhosts[i].bolAStarShowLine)
                {
                    List<Position> lstPos = lstGhosts[i].lstAStarPosition;
                    if (lstPos.Count > 0)
                    {
                        GamePacman Pacman = clsCommon.GamePacman;

                        clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
                        Position posPacman = clsGetCurrentCell.GetCell(Pacman.ptOrigin, Pacman.Direction);

                        string strPos = String.Format("{0} {1} / ", lstPos[lstPos.Count - 1].X, lstPos[lstPos.Count - 1].Y);
                        strPos += String.Format("{0} {1}", posPacman.X, posPacman.Y);

                        int intPos = HasPosition(lstPos, posPacman);

                        //if (intPos > -1 && intPos != lstPos.Count - 1)
                        //{
                        //    List<Position> lstNew = new List<Position>();

                        //    for (int k = 0; k < intPos; k++)
                        //        lstNew.Add(lstPos[k]);

                        //    lstPos = new List<Position>(lstNew);
                        //}

                        clsSolvePath.DrawLine(acTrans, acDb, lstPos, lstColor[i]);
                    }
                }
            }
        }

        internal int HasPosition(List<Position> lstPosition, Position Pos)
        {
            for (int i = 0; i < lstPosition.Count; i++)
            {
                if (PositionMatch(lstPosition[i], Pos))
                    return i;
            }
            return -1;
        }

        internal Boolean PositionMatch(Position p1, Position p2)
        {
            if (p1.X != p2.X || p1.Y != p2.Y)
                return false;
            return true;
        }

        internal void UpdatePacmanBoxes(Transaction acTrans, Database acDb)
        {
            GamePacman Pacman = clsCommon.GamePacman;
            clsReg clsReg = new clsReg();

            clsGluttony clsGluttony = new clsGluttony();

            Boolean bolShowDirection = clsReg.GetShowDirection();
            Boolean bolShowSuggestion = clsReg.GetShowSuggestion();
            Boolean bolShowHistory = clsReg.GetShowHistory();

            if (Pacman.GameLoop.bolBoxDirectionUpdate)
            {
                if (!bolShowDirection)
                    Pacman.GameLoop.lstBoxDirection.Clear();

                clsGluttony.DrawingDirectionBoxes(acTrans, acDb, Pacman.GameLoop.lstBoxDirection,
                                                 clsGluttony.GetColor(Direction.Up));
                Pacman.GameLoop.bolBoxDirectionUpdate = false;
            }

            if (Pacman.GameLoop.bolBoxSuggestionUpdate)
            {
                if (!bolShowSuggestion)
                    clsCommon.GamePacman.GameLoop.lstBoxSuggestion.Clear();

                clsGluttony.DrawingSuggestionBox(acTrans, acDb, Pacman.GameLoop.lstBoxSuggestion,
                                                 clsGluttony.GetColor(Direction.Right));

                Pacman.GameLoop.bolBoxSuggestionUpdate = false;
            }

            if (Pacman.GameLoop.bolHistoryUpdate)
            {
                List<Direction> lstDirection = Pacman.GameLoop.arrHistory.ToList(out List<Position> lstPosition);

                if (!bolShowHistory)
                {
                    lstDirection.Clear();
                    lstPosition.Clear();
                }

                clsGluttony.DrawingHistoryBox(acTrans, acDb, lstPosition, lstDirection,
                                              clsGluttony.GetColor(Direction.Down));

                Pacman.GameLoop.bolHistoryUpdate = false;
            }

            clsCommon.GamePacman = Pacman;
        }

        internal void UpdateGraphics()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    clsUpdateGraphics clsUpdateGraphics = new clsUpdateGraphics();

                    UpdateGraphics(acTrans, acDb);

                    UpdatePacmanBoxes(acTrans, acDb);

                    UpdateAStarLine(acTrans, acDb);

                    clsScore clsScore = new clsScore();
                    clsScore.ShowValue(acTrans, acDb);

                    acTrans.Commit();
                }
                try
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}