using AutoChomp.Graphics;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace AutoChomp.Update
{
    internal class clsUpdateGraphics
    {
        internal void UpdateGraphics(Transaction acTrans, Database acDb)
        {
            // if (!clsValues.IsResetRunning)
         
            //Update Pacman
            if (clsCommon.GamePacman.bolGraphicsRequired)
            {
                clsGraphicsPacman clsGraphicsPacman = new clsGraphicsPacman();
                clsGraphicsPacman.UpdatePacmanPositionAndVisibility(acTrans, acDb, ref clsCommon.GamePacman);
                clsCommon.GamePacman.bolGraphicsRequired = false;

                if (clsCommon.GamePacman.bolCellChanged)
                {
                    clsAStar clsAStar = new clsAStar();
                    clsAStar.CreateTextData();
                    clsCommon.GamePacman.bolCellChanged = false;
                }
            }

            //Hide Dots
            if (clsCommon.GameDots.bolGraphicsRequired)
            {
                Graphics.clsGraphicsDots clsEatGraphics = new Graphics.clsGraphicsDots();
                clsEatGraphics.DisplayDots(acTrans);
                clsCommon.GameDots.bolGraphicsRequired = false;
            }

            //Flash Power
            if (clsCommon.GamePower.bolGraphicsRequired)
            {
                Graphics.clsGraphicsDots clsEatGraphics = new Graphics.clsGraphicsDots();
                clsEatGraphics.DisplayPower(acTrans);
                clsCommon.GamePower.bolGraphicsRequired = false;
            }

            if (clsCommon.GameGhostCommon.bolGraphicsRequired)
            {
                List<GameGhost> lstGhost = clsCommon.lstGameGhost;
                // Update Ghost
                Graphics.clsGraphicsGhost clsGhostPosition = new Graphics.clsGraphicsGhost();
                clsGhostPosition.UpdateGhostGraphics(acTrans, acDb, ref lstGhost);
                clsCommon.GameGhostCommon.bolGraphicsRequired = false;
            }

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