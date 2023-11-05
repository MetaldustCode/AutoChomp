using System;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataMouth
    {
        internal void UpdateMouthState()
        {
            GamePacman Pacman = clsCommon.GamePacman;

            string strBlockOpen = Mouth()[Pacman.intMouth];

            if (strBlockOpen == "Mid")
                clsCommon.GamePacman.PacmanState = PacmanState.Mid;
            if (strBlockOpen == "Open")
                clsCommon.GamePacman.PacmanState = PacmanState.Open;
            if (strBlockOpen == "Close")
                clsCommon.GamePacman.PacmanState = PacmanState.Close;
        }

        internal List<String> Mouth()
        {
            int intCount = clsGridValues.intMouth;

            List<String> lstMouth = new List<String>();

            for (int i = 0; i < intCount; i++)
                lstMouth.Add("Mid");

            for (int i = 0; i < intCount; i++)
                lstMouth.Add("Open");

            for (int i = 0; i < intCount; i++)
                lstMouth.Add("Mid");

            for (int i = 0; i < intCount; i++)
                lstMouth.Add("Close");

            return lstMouth;
        }

        public Boolean UpdateMouth(ref GamePacman Pacman)
        {
            clsGetDirection clsGetDirection = new clsGetDirection();
            List<Position> lstNextCell = new List<Position>();
            if (clsGetDirection.CanCharacterMove(Pacman.ptOrigin, Pacman.Direction, ref lstNextCell))
            {
                List<String> lstMouth = Mouth();

                Pacman.intMouth++;

                if (Pacman.intMouth > lstMouth.Count - 1)
                    Pacman.intMouth = 0;

                return true;
            }

            return false;
        }
    }
}