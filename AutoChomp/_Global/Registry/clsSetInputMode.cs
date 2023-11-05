using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsSetInputMode
    {     // "Red", "Pink", "Blue", "Orange"
        internal void SetInputMode()
        {
            clsReg clsReg = new clsReg();

            string strRed = clsReg.GetRedInputMode();
            string strPink = clsReg.GetPinkInputMode();
            string strBlue = clsReg.GetBlueInputMode();
            string strOrange = clsReg.GetOrangeInputMode();

            AssignGhostInputMode("Red", strRed);
            AssignGhostInputMode("Pink", strPink);
            AssignGhostInputMode("Blue", strBlue);
            AssignGhostInputMode("Orange", strOrange);

            string strPacman = clsReg.GetPacmanInputMode();

            AssignPacmanInputMode(strPacman);
        }

        internal void AssignGhostInputMode(string strName, string strInputMode)
        {
            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            if (lstGhosts != null)
            {
                for (int i = 0; i < lstGhosts.Count; i++)
                {
                    if (lstGhosts[i].strName == strName)
                        lstGhosts[i].InputMode = GetInputMode(strInputMode);
                }

                clsCommon.lstGameGhost = lstGhosts;
            }
        }

        internal void AssignPacmanInputMode(string strInputMode)
        {
            if (clsCommon.GamePacman != null)
                clsCommon.GamePacman.InputMode = GetInputMode(strInputMode);
        }

        internal InputMode GetInputMode(string strValue)
        {
            List<String> lstValue =
                new List<String>(){"None",
                                   "Keyboard",
                                   "Random",
                                   "Gluttany",
                                   "A-Star"};

            List<InputMode> lstMode =
                new List<InputMode>(){InputMode.None,
                                      InputMode.Keyboard,
                                      InputMode.Random,
                                      InputMode.Gluttany,
                                      InputMode.AStar};

            int intPos = lstValue.IndexOf(strValue);

            if (intPos > -1)
                return lstMode[intPos];

            return InputMode.None;
        }
    }
}