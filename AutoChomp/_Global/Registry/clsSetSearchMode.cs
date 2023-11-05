using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSetSearchMode
    {
        // "Red", "Pink", "Blue", "Orange"

        internal void SetSearchMode()
        {
            clsReg clsReg = new clsReg();

            Boolean bolRed = clsReg.GetRedSearchVisible();
            Boolean bolPink = clsReg.GetPinkSearchVisible();
            Boolean bolBlue = clsReg.GetBlueSearchVisible();
            Boolean bolOrange = clsReg.GetOrangeSearchVisible();

            AssignGhostSearchMode("Red", bolRed);
            AssignGhostSearchMode("Pink", bolPink);
            AssignGhostSearchMode("Blue", bolBlue);
            AssignGhostSearchMode("Orange", bolOrange);

            Boolean bolPacman = clsReg.GetPacmanSearchVisible();

            AssignPacmanSearchMode(bolPacman);
        }

        internal void AssignGhostSearchMode(string strName, Boolean bolSearch)
        {
            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            if (lstGhosts != null)
            {
                for (int i = 0; i < lstGhosts.Count; i++)
                {
                    if (lstGhosts[i].strName == strName)
                        lstGhosts[i].bolAStarShowLine = bolSearch;
                }

                clsCommon.lstGameGhost = lstGhosts;
            }
        }

        internal void AssignPacmanSearchMode(Boolean bolSearch)
        {
            if (clsCommon.GamePacman != null)
                clsCommon.GamePacman.bolSearchMode = bolSearch;
        }
    }
}