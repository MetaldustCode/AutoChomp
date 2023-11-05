using System.Collections.Generic;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataSquiggle
    {
        internal void SetDataSquiggle()
        {
            clsTimerEvents clsTimerEvents = new clsTimerEvents();
            int intElapsed = clsTimerEvents.GetTimerSquiggle();

            if (intElapsed > clsTimers.GameElapsedTime.intIntervalSquiggle)
            {
                List<GameGhost> lstGhost = clsCommon.lstGameGhost;
                for (int i = 0; i < lstGhost.Count; i++)
                {
                    GameGhost gameGhost = lstGhost[i];
                    if (gameGhost.Squiggle == Squiggle.Standard)
                        gameGhost.Squiggle = Squiggle.Alternate;
                    else
                        gameGhost.Squiggle = Squiggle.Standard;
                }
                clsTimerEvents.RestartTimerSquiggle();

                clsCommon.GameGhostCommon.bolGraphicsRequired = true;
            }
        }
    }
}