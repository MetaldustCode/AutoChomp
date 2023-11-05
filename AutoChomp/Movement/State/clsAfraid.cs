using System;

namespace AutoChomp
{
    internal class clsAfraid
    {
        internal Boolean EatGhost()
        {
            // While the first timer is running
            if (clsCommon.GameGhostCommon.bolEatGhost)
            {
                clsTimerEvents clsTimerEvents = new clsTimerEvents();
                int intStartElapsed = clsTimerEvents.GetTimerEatGhost();

                if (intStartElapsed > clsTimers.GameElapsedTime.intEatGhost)
                {
                    // Stop first timer
                    clsTimerEvents.StopTimerEatGhost();
                    clsCommon.GameGhostCommon.bolEatGhost = false;
                    clsCommon.GamePacman.bolGraphicsRequired = true;
                }
                else
                    return true;
            }

            return false;
        }

        internal void EatPellet()
        {
            // Cancel existing timers and start afraid timer
            if (clsCommon.GameGhostCommon.bolPowerPelletEaten)
            {
                CancelAfraid();
                StartAfraid();
            }

            // While the first timer is running
            if (clsCommon.GameGhostCommon.bolPowerTimerStart)
            {
                clsTimerEvents clsTimerEvents = new clsTimerEvents();
                int intStartElapsed = clsTimerEvents.GetTimerPowerStart();

                if (intStartElapsed > clsTimers.GameElapsedTime.intPowerStartTime)
                {
                    // Stop first timer
                    clsTimerEvents.StopTimerPowerStart();

                    // Start second timer
                    clsCommon.GameGhostCommon.bolPowerTimerStart = false;
                    clsCommon.GameGhostCommon.bolPowerTimerFlash = true;
                    clsTimers.GameElapsedTime.intPowerFlashCount = 0;
                }
            }

            // Toggle color for second timer
            if (clsCommon.GameGhostCommon.bolPowerTimerFlash)
            {
                clsSetAfraid clsSetAfraid = new clsSetAfraid();
                clsTimerEvents clsTimerEvents = new clsTimerEvents();
                int intElapsed = clsTimerEvents.GetTimerPowerFlash();

                if (intElapsed > clsTimers.GameElapsedTime.intPowerFlashTime)
                {
                    // Toggle color
                    clsTimerEvents.RestartTimerPowerFlash();
                    clsSetAfraid.SetGhostState(GhostState.Afraid, true);
                    clsTimers.GameElapsedTime.intPowerFlashCount++;
                }

                if (clsTimers.GameElapsedTime.intPowerFlashCount ==
                    clsTimers.GameElapsedTime.intPowerFlashTotal)
                {
                    // Clear timer
                    clsCommon.GameGhostCommon.bolPowerTimerFlash = false;
                    clsTimerEvents.StopTimerPowerFlash();

                    // Set ghosts to normal
                    clsSetAfraid.SetGhostState(GhostState.Alive);
                }
            }
        }

        internal void StartAfraid()
        {
            // Reset options
            clsCommon.GameGhostCommon.bolPowerPelletEaten = false;
            clsCommon.GameGhostCommon.bolPowerTimerStart = true;

            // Reverse all ghosts
            clsSetReverse clsReverse = new clsSetReverse();
            clsReverse.ReverseGhosts();

            // Set ghosts to afraid
            clsSetAfraid clsSetAfraid = new clsSetAfraid();
            clsSetAfraid.SetGhostState(GhostState.Afraid);
        }

        internal void CancelAfraid()
        {
            if (clsCommon.GameGhostCommon != null)
            {
                // Clear global  values
                clsCommon.GameGhostCommon.bolPowerTimerStart = false;
                clsCommon.GameGhostCommon.bolPowerTimerFlash = false;
            }

            if (clsTimers.GameElapsedTime != null)
                clsTimers.GameElapsedTime.intPowerFlashCount = 0;

            // Disable timers
            clsTimerEvents clsTimerEvents = new clsTimerEvents();
            clsTimerEvents.StopTimerPowerStart();
            clsTimerEvents.StopTimerPowerFlash();

            // Set ghosts to normal
            clsSetAfraid clsSetAfraid = new clsSetAfraid();
            clsSetAfraid.SetGhostState(GhostState.Alive);
        }
    }
}