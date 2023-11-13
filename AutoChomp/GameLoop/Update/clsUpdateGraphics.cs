using AutoChomp.Gameloop.Graphics;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace AutoChomp.Gameloop.Update
{
    internal class clsUpdateGraphics
    {
        internal void UpdateGraphics()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {

                    UpdateGraphics(acTrans, acDb);

                    UpdatePacmanBoxes(acTrans, acDb);

                    UpdateAStarLine(acTrans, acDb);

                    ShowScore(acTrans, acDb);

                    acTrans.Commit();
                }

                UpdateScreen();
            }
        }


        internal void UpdateGraphics(Transaction acTrans, Database acDb)
        {
            clsReg clsReg = new clsReg();

            // if (!clsValues.IsResetRunning)
            clsGraphicsDebug clsGraphicsDebug = new clsGraphicsDebug();
            clsGraphicsDebug.DeleteCircle();

            if (clsReg.GetCurrentCell())
                clsGraphicsDebug.DrawCircle();

            // if (clsCommon.GamePacman.bolGraphicsRequired || clsCommon.GameGhostCommon.bolGraphicsRequired)
            {
                //clsSolvePath clsSolvePath = new clsSolvePath();
                //clsSolvePath.Solve();


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

            // Update Ghost Spite
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
            clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();
            clsSolvePath clsSolvePath = new clsSolvePath();
            clsSolvePath.DeleteLine(acTrans, acDb);

            clsReg clsReg = new clsReg();
            if (clsReg.GetPacmanSearchVisible() &&
                clsCommon.GamePacman.lstCurrentChase.Count > 0)
            {
                clsSolvePath.DrawLine(acTrans, acDb, clsCommon.GamePacman.lstCurrentChase, 2);
            }

            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            List<int> lstColor = new List<int>() { 1, 241, 4, 30 }.Multiply();

            for (int i = 0; i < lstGhosts.Count; i++)
            {
                // clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();


                if (lstGhosts[i].bolAStarShowLine &&
                    lstGhosts[i].GhostState == GhostState.Alive &&
                    !clsCalcGlobalAStar.IsAfraid(clsCommon.lstGameGhost))
                {
                    List<Position> lstPos = lstGhosts[i].lstAStarPosition;

                    if (lstPos.Count > 0)
                    {
                        //GamePacman Pacman = clsCommon.GamePacman;
                        //Point2d ptOrigin = Pacman.ptOrigin;
                        //Gameloop.Data.clsDataAlignToGrid clsDataAlignToGrid = new Gameloop.Data.clsDataAlignToGrid();
                        //clsDataAlignToGrid.GetPosition(ref ptOrigin, out Position Position);
                        //Position posPacman = Position;

                        //clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
                        //Position posPacman = clsGetCurrentCell.GetCell(Pacman.ptOrigin, Pacman.Direction);

                        //string strPos = String.Format("{0} {1} / ", lstPos[lstPos.Count - 1].X, lstPos[lstPos.Count - 1].Y);
                        //strPos += String.Format("{0} {1}", posPacman.X, posPacman.Y);

                        // int intPos = HasPosition(lstPos, posPacman);                  

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
                clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();

                if (!bolShowDirection || clsCalcGlobalAStar.IsAfraid(clsCommon.lstGameGhost))
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

        internal void ShowScore(Transaction acTrans, Database acDb)
        {
            clsScore clsScore = new clsScore();
            clsScore.ShowValue(acTrans, acDb);
        }

        internal void UpdateScreen()
        {
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