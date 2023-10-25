using AutoChomp.Data;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsFrame
    {
        // update position
        internal void UpdateFrame(GameGhost Ghost, int i)
        {
            Direction direction = Ghost.Direction;

            if (!Ghost.bolInHouse)
            {
                clsGetDirection clsGetDirection = new clsGetDirection();
                direction = clsGetDirection.GetRandomDirection(Ghost.Origin, Ghost.Direction, 6);
            }
            else
            {
                clsHouse clsHouseMovement = new clsHouse();

                // Set the correct order to exit the house
                List<GameGhost> lstInHouse =
                    clsHouseMovement.ReOrder(clsCommon.GameGhostCommon.lstInHouse);

                clsTimerEvents clsTimerEvents = new clsTimerEvents();

                // Get the elapsed time to start exiting
                int intElapsed = clsTimerEvents.GetTimerExit();

                // Match the current ghost the the correct exit order
                if (lstInHouse[lstInHouse.Count - 1] == Ghost)
                {
                    // Check if the ghost can start exiting
                    // Exit immediatly if there is more than 2 ghosts in the house
                    if (intElapsed > clsTimers.GameElapsedTime.intPowerExit || lstInHouse.Count > 2)
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

                // Save updated list to global
                clsCommon.GameGhostCommon.lstInHouse = lstInHouse;

                // Disable timer when all ghosts have escaped
                if (lstInHouse.Count == 0)
                    clsTimerEvents.StopTimerExit();
            }

            // Update positon
            clsDataWrapAround clsWrapAround = new clsDataWrapAround();
            clsWrapAround.SetNextGhostPosition(ref Ghost, direction);
            clsGridValues.lstFrameCounter[i]++;

            clsCommon.GameGhostCommon.bolGraphicsRequired = true;
        }
    }
}