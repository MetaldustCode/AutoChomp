using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSetAfraid
    {
        internal void SetGhostState(GhostState ghostState, Boolean bolToggle = false)
        {
            if (clsCommon.lstGameGhost != null)
            {
                clsReg clsReg = new clsReg();

                if (clsReg.GetPlaySound())
                {
                    if (ghostState == GhostState.Afraid)
                        clsNAudio.PlayPowerPellet();
                    else
                        clsNAudio.StopPowerPellet();
                }

                List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

                if (clsCommon.GameGhostCommon.bolPowerTimerFlash == false)
                {
                    for (int i = 0; i < lstGhosts.Count; i++)
                        if (lstGhosts[i].bolIsEaten == true)
                            lstGhosts[i].bolIsEaten = false;
                }

                for (int i = 0; i < lstGhosts.Count; i++)
                {
                    if (!lstGhosts[i].bolIsEaten)
                    {
                        if (lstGhosts[i].GhostState != GhostState.Dead)
                        {
                            if (ghostState == GhostState.Afraid)
                            {
                                lstGhosts[i].GhostState = GhostState.Afraid;

                                if (bolToggle)
                                {
                                    if (lstGhosts[i].Color == GhostColor.Default)
                                        lstGhosts[i].Color = GhostColor.White;
                                    else
                                        lstGhosts[i].Color = GhostColor.Default;
                                }
                            }
                            if (ghostState == GhostState.Alive)
                            {
                                lstGhosts[i].GhostState = GhostState.Alive;
                                lstGhosts[i].Color = GhostColor.Default;
                            }
                        }
                    }
                }

                clsCommon.lstGameGhost = lstGhosts;
            }
        }
    }
}