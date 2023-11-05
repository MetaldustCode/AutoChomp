using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataDot
    {
        internal void SetDataDots()
        {
            EatDotsPacman();
            EatPowerPacman();
            EatDotsGhost();
            SetPowerFlash();
        }

        internal void EatDotsPacman()
        {
            clsReg clsReg = new clsReg();

            if (!clsReg.GetPacmanEatDots())
                return;

            if (clsCommon.GameDots != null)
            {
                GameDots GameDots = clsCommon.GameDots;

                Point2d ptPacPosition = clsCommon.GamePacman.ptOrigin;
                List<Point2d> lstDotsPosition = GameDots.lstOrigin;
                int intPos = -1;
                if (PositionMatch(ptPacPosition, lstDotsPosition, GameDots.lstIsEaten, ref intPos))
                {
                    if (intPos > -1)
                    {
                        GameDots.lstIsEaten[intPos] = true;
                        GameDots.bolGraphicsRequired = true;

                        clsGetCurrentCell clsGetCell = new clsGetCurrentCell();
                        Position pos = clsGetCell.GetMidCell(ptPacPosition);
                        clsClassTables.arrXDots[pos.X, pos.Y] = false;

                        clsScoreValues.intPacmanScore += 10;

                        if (clsReg.GetPlaySound())
                        {
                            clsNAudio.PlayMunch();
                        }
                    }
                }
            }
        }

        internal void EatDotsGhost()
        {
            clsReg clsReg = new clsReg();

            if (!clsReg.GetGhostEatDots())
                return;

            GameDots GameDots = clsCommon.GameDots;
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhost.Count; i++)
            {
                Point2d ptPacPosition = lstGhost[i].ptOrigin;
                List<Point2d> lstPowerPosition = GameDots.lstOrigin;
                int intPos = -1;
                clsDataDot clsDotData = new clsDataDot();

                if (clsDotData.PositionMatch(ptPacPosition, lstPowerPosition, GameDots.lstIsEaten, ref intPos))
                {
                    if (intPos > -1)
                    {
                        GameDots.lstIsEaten[intPos] = true;
                        GameDots.bolGraphicsRequired = true;

                        clsGetCurrentCell clsGetCell = new clsGetCurrentCell();
                        Position pos = clsGetCell.GetMidCell(ptPacPosition);
                        clsClassTables.arrXDots[pos.X, pos.Y] = false;
                    }
                }
            }
        }

        internal void EatPowerPacman()
        {
            if (clsCommon.GamePower != null)
            {
                GamePower GamePower = clsCommon.GamePower;

                Point2d ptPacPosition = clsCommon.GamePacman.ptOrigin;
                List<Point2d> lstPowerPosition = GamePower.lstOrigin;

                int intPos = -1;
                if (PositionMatch(ptPacPosition, lstPowerPosition, GamePower.lstIsEaten, ref intPos))
                {
                    if (intPos > -1)
                    {
                        GamePower.lstIsEaten[intPos] = true;
                        GamePower.bolGraphicsRequired = true;

                        clsCommon.GameGhostCommon.bolPowerPelletEaten = true;

                        clsScoreValues.intPacmanScore += 50;
                    }
                }
            }
        }

        internal Boolean PositionMatch(Point2d ptPacPosition, List<Point2d> lstDotPosition, List<Boolean> lstEatenDot, ref int intPos)
        {
            if (lstDotPosition.Contains(ptPacPosition))
            {
                intPos = lstDotPosition.IndexOf(ptPacPosition);

                if (intPos > -1)
                {
                    if (lstEatenDot[intPos] == false)
                        return true;
                }
            }

            return false;
        }

        internal void SetPowerFlash()
        {
            GamePower GamePower = clsCommon.GamePower;
            clsTimerEvents clsTimerEvents = new clsTimerEvents();
            //int intElapsed = clsTimerEvents.GetTimerPellet();
            //clsCommon.GameForm.txtInfo.Text = intElapsed.ToString();

            if (GamePower.lstIsEaten.Contains(false))
            {
                int intElapsed = clsTimerEvents.GetTimerPellet();

                if (intElapsed > clsTimers.GameElapsedTime.intIntervalPellettFlash)
                {
                    GamePower.bolIsFlashOn = !GamePower.bolIsFlashOn;

                    clsTimerEvents.RestartTimerPellet();
                    GamePower.bolGraphicsRequired = true;
                }
            }
            else
                clsTimerEvents.StopTimerPellet();
        }
    }
}