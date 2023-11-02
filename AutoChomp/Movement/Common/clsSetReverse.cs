using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSetReverse
    {
        internal Boolean UpdateReverse()
        {
            clsTimerEvents clsTimerEvents = new clsTimerEvents();
            int intElapsed = clsTimerEvents.GetTimerReverse();

            if (intElapsed > clsTimers.GameElapsedTime.intIntervalReverse)
            {
                clsTimerEvents.RestartTimerReverse();
                return true;
            }

            return false;
        }

        internal void ReverseGhosts()
        {
            clsGetDirection clsGetDirection = new clsGetDirection();

            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhosts.Count; i++)
            {
                Direction direction = lstGhosts[i].Direction;

                if (!lstGhosts[i].bolInHouse)
                {
                    direction = clsGetDirection.GetReverse(direction);
                    lstGhosts[i].Direction = direction;
                    // lstGhosts[i].bolInHouse = true;
                }
            }

            clsCommon.lstGameGhost = lstGhosts;
        }
    }
}