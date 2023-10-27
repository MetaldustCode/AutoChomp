using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp.Data
{
    internal class clsDataGhostMove
    {
        internal void SetDataGhostMove()
        {
            // Maximum Frame Skip is 50%
            // every other frame is skipped
            clsTunnel clsIsInTunnel = new clsTunnel();
            clsFrame clsFrame = new clsFrame();
            List<Boolean> lstInTunnelGhost = new List<Boolean>();

            clsIsInTunnel.IsInTunnel(ref lstInTunnelGhost);
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            // Get Freme Skip for Ghost Speed
            List<Double> lstSpeed = new List<double>();
            List<Double> lstFrameSkip = GetGhostSpeed(lstInTunnelGhost, ref lstSpeed);

            int intCount = 0;

            for (int i = 0; i < lstFrameSkip.Count; i++)
            {
                // Is sprite selected to move
                if (IsRandom(intCount))
                {
                    GameGhost Ghost = lstGhost[i];

                    Ghost.dblFrameDelay = lstFrameSkip[i];
                    Ghost.dblSpeed = lstSpeed[i];

                    if (lstFrameSkip[i] == 0)
                        clsFrame.UpdateFrame(Ghost, i);
                    else
                    {
                        // Skip frame to set the correct speed
                        if (clsGridValues.lstFrameCounter[i] <= lstFrameSkip[i] - 1)
                            clsFrame.UpdateFrame(Ghost, i);
                        else
                            clsGridValues.lstFrameCounter[i] = 0;
                    }
                }

                // Loop for Multiple
                if (intCount == 3)
                    intCount = 0;
                else
                    intCount++;
            }

            // Update Pacman Dot if Ghost is at Grid Origin
            for (int i = 0; i < lstGhost.Count; i++)
            {
                if (clsClassTables.lstXGridOrigin.Contains(lstGhost[i].Origin))
                {
                    clsCommon.GamePacman.GameLoop.bolBoxDirectionUpdate = true;
                    break;
                }
            }

            clsAStar clsAStar = new clsAStar();
            for (int i = 0; i < lstGhost.Count; i++)
            {
                GameGhost gameGhost = lstGhost[i];
                if (!gameGhost.bolInHouse)
                    clsAStar.UpdateAStar(ref gameGhost);
                lstGhost[i] = gameGhost;
            }

            clsCommon.lstGameGhost = lstGhost;
        }


        // Is sprite selected to move
        internal Boolean IsRandom(int i)
        {
            string strRed = clsCommon.GameForm.cboRed.Text;
            string strPink = clsCommon.GameForm.cboPink.Text;
            string strBlue = clsCommon.GameForm.cboBlue.Text;
            string strOrange = clsCommon.GameForm.cboOrange.Text;

            if (strRed == "Random" && i == 0) return true;
            if (strPink == "Random" && i == 1) return true;
            if (strBlue == "Random" && i == 2) return true;
            if (strOrange == "Random" && i == 3) return true;

            return false;
        }

        // Get default speed and limit speed to 50 percent in different modes
        internal List<Double> GetGhostSpeed(List<Boolean> lstInTunnel, ref List<Double> lstSpeed)
        {
            lstSpeed = new List<Double> { 100, 90, 80, 50 }.Multiply();

            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

            for (int i = 0; i < lstGhosts.Count; i++)
            {
                if (lstInTunnel[i] ||
                    lstGhosts[i].bolIsAfraid ||
                    lstGhosts[i].bolInHouse)
                    lstSpeed[i] = 50;
            }

            List<Double> lstFrameDelay = lstSpeed.ToList();

            for (int i = 0; i < lstFrameDelay.Count; i++)
            {
                double frameSkip = 1 / (1 - (lstFrameDelay[i] / 100));
                if (frameSkip != double.PositiveInfinity)
                    lstFrameDelay[i] = (int)Math.Ceiling(frameSkip) - 1;
                else
                    lstFrameDelay[i] = 0;
            }

            //for (int i = 0; i < lstFrameDelay.Count; i++)
            //{
            //    if (lstInTunnelGhost[i])
            //    {
            //        lstFrameDelay[i] = 1;
            //        lstSpeed[i] = 50;
            //    }
            //}

            return lstFrameDelay;
        }
    }
}