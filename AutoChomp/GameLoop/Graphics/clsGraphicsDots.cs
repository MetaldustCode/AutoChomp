using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Graphics
{
    internal class clsGraphicsDots
    {
        internal void DisplayDots(Transaction acTrans)
        {
            clsReg clsReg = new clsReg();

            Boolean bolShowDots = clsReg.GetDotsVisible();

            if (clsCommon.GameDots != null)
            {
                GameDots GameDots = clsCommon.GameDots;

                List<bool> lstEatenDots = GameDots.lstIsEaten;

                for (int i = 0; i < lstEatenDots.Count; i++)
                {
                    if (lstEatenDots[i] && GameDots.lstIsBlockVisible[i])
                    {
                        BlockReference acBlkRef = GameDots.lstBlockReference[i];
                        if (acBlkRef.ObjectId.IsValid && !acBlkRef.ObjectId.IsErased)
                        {
                            acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                            if (acBlkRef.Visible == true)
                                acBlkRef.Visible = false;
                        }
                        GameDots.lstIsBlockVisible[i] = false;
                    }
                }

                for (int i = 0; i < lstEatenDots.Count; i++)
                {
                    BlockReference acBlkRef = GameDots.lstBlockReference[i];
                    if (acBlkRef.ObjectId.IsValid && !acBlkRef.ObjectId.IsErased)
                    {
                        acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                        if (bolShowDots)
                        {
                            if (GameDots.lstIsBlockVisible[i])
                            {
                                if (acBlkRef.Visible == false)
                                    acBlkRef.Visible = true;
                            }
                        }
                        else
                        {
                            if (acBlkRef.Visible == true)
                                acBlkRef.Visible = false;
                        }
                    }
                }

                clsCommon.GameDots = GameDots;
            }
        }

        internal void DisplayPower(Transaction acTrans)
        {
            if (clsCommon.GamePower != null)
            {
                GamePower GamePower = clsCommon.GamePower;

                List<bool> lstEatenPower = GamePower.lstIsEaten;

                for (int i = 0; i < lstEatenPower.Count; i++)
                {
                    if (lstEatenPower[i])
                        DisplayPower(acTrans, GamePower, false, i);
                    else
                    {
                        DisplayPower(acTrans, GamePower, true, i);

                        if (GamePower.lstIsBlockVisible[i] != GamePower.bolIsFlashOn)
                        {
                            DisplayPower(acTrans, GamePower, GamePower.bolIsFlashOn, i);
                            GamePower.lstIsBlockVisible[i] = GamePower.bolIsFlashOn;
                        }
                    }
                }
            }
        }

        internal void DisplayPower(Transaction acTrans, GamePower GamePower, Boolean bolIsVisible, int i)
        {
            BlockReference acBlkRef = GamePower.lstBlockReference[i];
            if (acBlkRef.ObjectId.IsValid & !acBlkRef.ObjectId.IsErased)
            {
                acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                if (acBlkRef.Visible != bolIsVisible)
                    acBlkRef.Visible = bolIsVisible;
            }
        }
    }
}