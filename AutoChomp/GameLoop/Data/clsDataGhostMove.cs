//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Internal;
//using NAudio.Gui;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

//namespace AutoChomp.Gameloop.Data
//{
//    internal class clsDataGhostMove
//    {
//        internal void SetAStarData()
//        {
//            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;
//            GamePacman Pacman = clsCommon.GamePacman;
//            List<int> lstMatched = new List<int>();
//            for (int i = 0; i < lstGhosts.Count; i++)
//            {
//                GameGhost Ghost = lstGhosts[i];
//                if (Ghost.InputMode == InputMode.AStar)
//                {
//                    if (IsAtGrid(Ghost))
//                    {
//                        clsBuildAndSolve clsBuildAndSolve = new clsBuildAndSolve();
//                        clsBuildAndSolve.BuildToPacman(ref Ghost);
//                        lstMatched.Add(i);
//                        lstGhosts[i] = Ghost;
//                    }
//                }
//            }

//            if (lstMatched.Count > 0)
//                clsCommon.lstGameGhost = lstGhosts;
//        }

//        internal Boolean IsAtGrid(GameGhost Ghost)
//        {
//            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
//            //Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

//            //Ghost.ptPosition = posGhost;

//            Boolean[,] arrXDots = clsClassTables.arrXDots;
//            arrXDots.GetSize(out int col, out int row);

//            List<String> lstXGridOriginString = clsClassTables.lstXGridOriginString;

//            if (col > 0 && lstXGridOriginString.Count > 0)
//            {
//                string strValue = String.Format("{0},{1}", Ghost.ptOrigin.X, Ghost.ptOrigin.Y);
//                if (lstXGridOriginString.Contains(strValue))
//                    return true;
//            }

//            return false;
//        }

//        internal Boolean IsAtGrid(GamePacman Ghost)
//        {
//            clsGetCurrentCell clsGetCurrentCell = new clsGetCurrentCell();
//            //Position posGhost = clsGetCurrentCell.GetCell(Ghost.ptOrigin, Ghost.Direction);

//            //Ghost.ptPosition = posGhost;

//            Boolean[,] arrXDots = clsClassTables.arrXDots;
//            arrXDots.GetSize(out int col, out int row);

//            List<String> lstXGridOriginString = clsClassTables.lstXGridOriginString;

//            if (col > 0 && lstXGridOriginString.Count > 0)
//            {
//                string strValue = String.Format("{0},{1}", Ghost.ptOrigin.X, Ghost.ptOrigin.Y);
//                if (lstXGridOriginString.Contains(strValue))
//                    return true;
//            }

//            return false;
//        }

//        internal void SetDataGhostMove()
//        {
//            // Maximum Frame Skip is 50%
//            // every other frame is skipped

//            clsFrame clsFrame = new clsFrame();

//            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

//            // Get Freme Skip for Ghost Speed

//            List<Double> lstFrameSkip = GetGhostSpeed(out List<Double> lstSpeed, out List<int> lstMultiplier);

//            for (int i = 0; i < lstMultiplier.Count; i++)
//            {
//                for (int j = 0; j < lstMultiplier[i]; j++)
//                {
//                    int intFrameCounter = clsGridValues.lstFrameCounter[i];
//                    ProcessFrame(lstGhost[i], ref intFrameCounter, i, lstFrameSkip[i], lstSpeed[i]);
//                    clsGridValues.lstFrameCounter[i] = intFrameCounter;
//                }
//            }

//            clsCommon.lstGameGhost = lstGhost;
//        }

//        internal void ProcessFrame(GameGhost Ghost, ref int intFrameCounter, int i, Double dblFrameSkip, Double dblSpeed)
//        {
//            clsFrame clsFrame = new clsFrame();

//            Ghost.dblFrameDelay = dblFrameSkip;
//            Ghost.dblSpeed = dblSpeed;
//            {
//                if (intFrameCounter == 0)
//                    clsFrame.UpdateFrame(Ghost, i);
//                else
//                {
//                    // Skip frame to set the correct speed
//                    if (intFrameCounter <= dblFrameSkip - 1)
//                        clsFrame.UpdateFrame(Ghost, i);
//                    else
//                        intFrameCounter = 0;
//                }
//            }
//        }

//        // Get default speed and limit speed to 50 percent in different modes
//        internal List<Double> GetGhostSpeed(out List<Double> lstSpeed, out List<int> lstMultiplier)
//        {
//            lstMultiplier = new List<int>() { 1, 1, 1, 1 }.Multiply();

//            clsTunnel clsIsInTunnel = new clsTunnel();
//            clsIsInTunnel.IsInTunnel(out List<Boolean> lstInTunnel);

//            lstSpeed = new List<Double> { 100, 90, 80, 70 }.Multiply();
//            //lstSpeed = new List<Double> { 50, 50, 50, 50 }.Multiply();
//            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

//            //for (int i = 0; i < lstGhosts.Count; i++)
//            //{
//            //    if (lstInTunnel[i] ||
//            //        lstGhosts[i].GhostState == GhostState.Afraid ||
//            //        lstGhosts[i].HouseState == HouseState.InHouse)
//            //    {
//            //        lstSpeed[i] = 50;
//            //    }

//            //    if (lstGhosts[i].GhostState == GhostState.Dead)
//            //    {
//            //        lstSpeed[i] = 100;
//            //        lstMultiplier[i] = 2;
//            //    }
//            //}

//            List<Double> lstFrameSkip = lstSpeed.ToList();

//            for (int i = 0; i < lstSpeed.Count; i++)
//            {
//                double frameSkip = 1 / (1 - (lstSpeed[i] / 100));
//                if (frameSkip != double.PositiveInfinity)
//                    lstFrameSkip[i] = (int)Math.Ceiling(frameSkip) - 1;
//                else
//                    lstFrameSkip[i] = 0;
//            }

//            return lstFrameSkip;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataGhostMove
    {
        internal void SetDataGhostMove()
        {
            clsFrame clsFrame = new clsFrame();

            // Maximum Frame Skip is 50%
            // every other frame is skipped
            clsTunnel clsIsInTunnel = new clsTunnel();

            clsIsInTunnel.IsInTunnel(out List<Boolean> lstInTunnelGhost);
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            // Get Freme Skip for Ghost Speed
            List<Double> lstSpeed = new List<double>();
            List<Double> lstFrameSkip = GetGhostSpeed(lstInTunnelGhost, ref lstSpeed);

            int intCount = 0;

            for (int i = 0; i < lstFrameSkip.Count; i++)
            {
                GameGhost Ghost = lstGhost[i];

                if (Ghost.Reset_Update)
                {
                    Ghost.ptOrigin = Ghost.Reset_ptOrigin;
                    Ghost.Direction = Ghost.Reset_Direction;
                    Ghost.Reset_Update = false;
                }

                if (Ghost.InputMode != InputMode.None)
                {
                    //Ghost.dblFrameDelay = lstFrameSkip[i];
                    //Ghost.dblSpeed = lstSpeed[i];

                    if (lstFrameSkip[i] == 0)
                        clsFrame.UpdateFrame(Ghost, i);
                    else
                    {
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
                if (clsClassTables.lstXGridOrigin.Contains(lstGhost[i].ptOrigin))
                {
                    clsCommon.GamePacman.GameLoop.bolBoxDirectionUpdate = true;
                    break;
                }
            }
        }

        //internal void UpdateFrame(GameGhost Ghost, int i)
        //{
        //    clsGetDirection clsGetDirection = new clsGetDirection();
        //    Direction NewDirection = clsGetDirection.GetRandomDirection(Ghost.ptOrigin, Ghost.Direction, 6);

        //    clsDataWrapAround clsWrapAround = new clsDataWrapAround();
        //    clsWrapAround.SetNextGhostPosition(ref Ghost, NewDirection);
        //    clsGridValues.lstFrameCounter[i]++;
        //    clsCommon.GameGhostCommon.bolGraphicsRequired = true;
        //}

        internal List<Double> GetGhostSpeed(List<Boolean> lstInTunnelGhost, ref List<Double> lstSpeed)
        {
            List<GameGhost> lstGhost = clsCommon.lstGameGhost;

            lstSpeed = new List<Double> { 95, 85, 75, 65 }.Multiply();

            List<Double> lstFrameDelay = lstSpeed.ToList();

            for (int i = 0; i < lstFrameDelay.Count; i++)
            {
                double frameSkip = 1 / (1 - (lstFrameDelay[i] / 100));
                if (frameSkip != double.PositiveInfinity)
                    lstFrameDelay[i] = (int)Math.Ceiling(frameSkip) - 1;
                else
                    lstFrameDelay[i] = 0;
            }

            for (int i = 0; i < lstFrameDelay.Count; i++)
            {
                if (lstInTunnelGhost[i] ||
                    lstGhost[i].HouseState == HouseState.InHouse ||
                    lstGhost[i].GhostState == GhostState.Afraid)
                {
                    lstFrameDelay[i] = 1;
                    lstSpeed[i] = 50;
                }

                if (lstGhost[i].HouseState == HouseState.ReturnHouse)
                {
                    lstFrameDelay[i] = 1;
                    lstSpeed[i] = 50;
                }

                if (lstGhost[i].HouseState == HouseState.LeaveHouse)
                {
                    lstFrameDelay[i] = 1;
                    lstSpeed[i] = 50;
                }

                if (lstGhost[i].GhostState == GhostState.Dead)
                {
                    lstFrameDelay[i] = 0;
                    lstSpeed[i] = 100;
                }
            }

            return lstFrameDelay;
        }

        internal void SetAStarData()
        {
            List<GameGhost> lstGhosts = clsCommon.lstGameGhost;
            GamePacman Pacman = clsCommon.GamePacman;
            List<int> lstMatched = new List<int>();
            for (int i = 0; i < lstGhosts.Count; i++)
            {
                GameGhost Ghost = lstGhosts[i];
                if (Ghost.InputMode == InputMode.AStar)
                {
                    if (IsAtGrid(Ghost))
                    {
                        clsBuildAndSolve clsBuildAndSolve = new clsBuildAndSolve();
                        clsBuildAndSolve.BuildToPacman(ref Ghost, i);
                        lstMatched.Add(i);
                        lstGhosts[i] = Ghost;
                    }
                }
            }

            if (lstMatched.Count > 0)
                clsCommon.lstGameGhost = lstGhosts;
        }

        internal Boolean IsAtGrid(GameGhost Ghost)
        {
            Boolean[,] arrXDots = clsClassTables.arrXDots;
            arrXDots.GetSize(out int col, out int row);

            List<String> lstXGridOriginString = clsClassTables.lstXGridOriginString;

            if (col > 0 && lstXGridOriginString.Count > 0)
            {
                string strValue = String.Format("{0},{1}", Ghost.ptOrigin.X, Ghost.ptOrigin.Y);
                if (lstXGridOriginString.Contains(strValue))
                    return true;
            }

            return false;
        }
    }
}