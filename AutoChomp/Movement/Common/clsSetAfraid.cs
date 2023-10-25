using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp
{
    internal class clsSetAfraid
    {
        internal void SetAfraidGhosts(Boolean bolIsAfraid, Boolean bolToggle = false)
        {
            if (clsCommon.lstGameGhost != null)
            {
                List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

                for (int i = 0; i < lstGhosts.Count; i++)
                {
                    lstGhosts[i].bolIsAfraid = bolIsAfraid;

                    if (bolIsAfraid)
                    {
                        lstGhosts[i].State = GhostState.Afraid;

                        if (bolToggle)
                        {
                            if (lstGhosts[i].Color == GhostColor.Default)
                                lstGhosts[i].Color = GhostColor.White;
                            else
                                lstGhosts[i].Color = GhostColor.Default;
                        }
                    }
                    else
                    {
                        lstGhosts[i].State = GhostState.Alive;
                        lstGhosts[i].Color = GhostColor.Default;
                    }
                }

                clsCommon.lstGameGhost = lstGhosts;
            }
        }
    }
}
