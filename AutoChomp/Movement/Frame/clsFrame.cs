using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Internal.Calculator;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsFrame
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        // update position
        internal void UpdateFrame(GameGhost Ghost, int i)
        {

            if (Ghost.GhostState == GhostState.Dead && Ghost.HouseState == HouseState.OutHouse)
            {
                Point2d pt = new Point2d((16 * Cell), (21 * Cell) + Middle);

                if (pt.X == Ghost.ptOrigin.X && pt.Y == Ghost.ptOrigin.Y)
                {
                    Ghost.Direction = Direction.None;
                    Ghost.HouseState = HouseState.ReturnHouse;
                }
            }

            if (Ghost.HouseState != HouseState.ReturnHouse)
            {
                Ghost.Direction = ExitHouse(Ghost);

                Ghost.Direction = UpdateAStar(Ghost, Ghost.InputMode);

                SetNext(Ghost, i);
            }

            if (Ghost.HouseState == HouseState.ReturnHouse)
            {
                Ghost.Direction = EnterHouse(Ghost);

                SetNext(Ghost, i);
            }

            if (Ghost.HouseState == HouseState.LeaveHouse)
            {
                Ghost.Direction = LeaveHouse(Ghost);

                SetNext(Ghost, i);
            }
        }

        internal void SetNext(GameGhost Ghost, int i)
        {
            // Update positon
            Gameloop.Data.clsDataWrapAround clsWrapAround = new Gameloop.Data.clsDataWrapAround();
            clsWrapAround.SetNextGhostPosition(ref Ghost, Ghost.Direction);
            clsGridValues.lstFrameCounter[i]++;

            clsCommon.GameGhostCommon.bolGraphicsRequired = true;

        }

        internal Direction UpdateAStar(GameGhost Ghost, InputMode inputMode)
        {
            Direction direction = Ghost.Direction;

            clsGetDirection clsGetDirection = new clsGetDirection();

            if (Ghost.HouseState == HouseState.OutHouse)
            {
                if (inputMode == InputMode.AStar)
                {
                    clsGetNextDirection clsGetNextDirection = new clsGetNextDirection();

                    if (clsClassTables.lstXGridOrigin.Contains(Ghost.ptOrigin))
                    {
                        List<Position> lstPos = Ghost.lstAStarPosition;
                        clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();

                        if (lstPos.Count > 0)
                        {
                            Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

                            for (int k = 0; k < lstPos.Count; k++)
                            {
                                Position posNext = lstPos[k];

                                direction = clsGetNextDirection.GetNextDirection(posGhost, posNext);
                                break;
                            }
                        }
                        else
                            direction = clsGetDirection.GetRandomDirection(Ghost.ptOrigin, Ghost.Direction, 6);
                    }
                    else
                        direction = clsGetDirection.GetRandomDirection(Ghost.ptOrigin, Ghost.Direction, 6);
                }
                else
                    direction = clsGetDirection.GetRandomDirection(Ghost.ptOrigin, Ghost.Direction, 6);
            }

            return direction;
        }

        internal Direction EnterHouse(GameGhost Ghost)
        {
            Direction direction = Ghost.Direction;

            clsHouse clsHouseMovement = new clsHouse();

            Boolean bolInPosition = false;
            if (clsHouseMovement.MoveDown(ref Ghost, ref direction))
            {
                if (Ghost.strName == "Orange")
                    bolInPosition = clsHouseMovement.MoveRight(ref Ghost, ref direction);

                if (Ghost.strName == "Blue")
                    bolInPosition = clsHouseMovement.MoveLeft(ref Ghost, ref direction);

                if (Ghost.strName == "Red" || Ghost.strName == "Pink")
                    bolInPosition = true;
            }

            if (bolInPosition)
            {
                Ghost.GhostState = GhostState.Alive;
                Ghost.HouseState = HouseState.LeaveHouse;
            }
            return direction;

            //return direction;
        }

        internal Direction LeaveHouse(GameGhost Ghost)
        {
            Direction direction = Ghost.Direction;

            clsHouse clsHouseMovement = new clsHouse();


            if (clsHouseMovement.MoveMiddle(ref Ghost, ref direction))
            {
                if (clsHouseMovement.MoveUp(ref Ghost, ref direction))
                {
                    Ghost.HouseState = HouseState.OutHouse;
                }
            }

            return direction;

            //return direction;
        }
        internal Direction ExitHouse(GameGhost Ghost)
        {
            Direction direction = Ghost.Direction;

            if (Ghost.HouseState == HouseState.InHouse)
            {
                clsHouse clsHouseMovement = new clsHouse();

                // Set the correct order to exit the house
                List<GameGhost> lstInHouse =
                    clsHouseMovement.ReOrder(clsCommon.GameGhostCommon.lstInHouse);

                clsTimerEvents clsTimerEvents = new clsTimerEvents();

                // Get the elapsed time to start exiting
                int intElapsed = clsTimerEvents.GetTimerExit();

                if (lstInHouse.Count > 0)
                {
                    // Match the current ghost the the correct exit order
                    if (lstInHouse[lstInHouse.Count - 1] == Ghost)
                    {
                        // Check if the ghost can start exiting
                        // Exit immediatly if there is more than 2 ghosts in the house
                        if (intElapsed > clsTimers.GameElapsedTime.intHouseExit || lstInHouse.Count > 2)
                        {
                            // Set new direction to exit
                            direction = clsHouseMovement.ExitHouse(Ghost, out Boolean bolEscaped);
                            if (bolEscaped)
                            {
                                // When Ghost has escaped
                                // Remove from index and start again with the next ghost
                                lstInHouse.RemoveAt(lstInHouse.Count - 1);
                                clsTimerEvents.RestartTimerExit();
                            }
                        }
                        else
                            // Move ghost up and down while it waits it's turn
                            direction = clsHouseMovement.MoveUpandDown(Ghost);
                    }
                    else
                        // Move ghost up and down while it waits it's turn
                        direction = clsHouseMovement.MoveUpandDown(Ghost);
                }

                // Save updated list to global
                clsCommon.GameGhostCommon.lstInHouse = lstInHouse;

                // Disable timer when all ghosts have escaped
                if (lstInHouse.Count == 0)
                    clsTimerEvents.StopTimerExit();
            }
            return direction;
        }
    }
}