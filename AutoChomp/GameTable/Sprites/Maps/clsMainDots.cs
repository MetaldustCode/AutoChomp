using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMainDots
    {
        internal BlockReference[,] CreateDots(Transaction acTrans, Database acDb,
                                              BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            Boolean[,] arrXGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstDots, '.');

            clsDrawDots clsGameDots = new clsDrawDots();
            BlockReference[,] arrBlkRef = clsGameDots.DrawDots(acTrans, acDb, acBlkTbl, acBlkTblRec, arrXGrid);

            clsGameDots.PlaceBlocks(arrBlkRef);
            List<BlockReference> lstBlock = arrBlkRef.ToList();

            clsCommon.GameDots.lstBlockReference = lstBlock;
            clsCommon.GameDots.lstOrigin = arrBlkRef.GetOrigin();

            List<Boolean> lstIsEaten = new List<bool>();
            List<Boolean> lstIsBlockVisible = new List<bool>();

            for (int i = 0; i < lstBlock.Count; i++)
            {
                lstIsEaten.Add(false);
                lstIsBlockVisible.Add(true);
            }

            clsCommon.GameDots.lstIsEaten = lstIsEaten;
            clsCommon.GameDots.lstIsBlockVisible = lstIsBlockVisible;

            return arrBlkRef;
        }

        internal BlockReference[,] CreatePower(Transaction acTrans, Database acDb,
                                  BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            Boolean[,] arrXGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstDots, '*');

            clsDrawDots clsGameDots = new clsDrawDots();
            BlockReference[,] arrBlkRef = clsGameDots.DrawPower(acTrans, acDb, acBlkTbl, acBlkTblRec, arrXGrid);

            clsGameDots.PlaceBlocks(arrBlkRef);

            List<BlockReference> lstBlock = arrBlkRef.ToList();
            clsCommon.GamePower.lstBlockReference = lstBlock;
            clsCommon.GamePower.lstOrigin = arrBlkRef.GetOrigin();

            List<Boolean> lstIsEaten = new List<bool>();
            List<Boolean> lstIsBlockVisible = new List<bool>();

            for (int i = 0; i < lstBlock.Count; i++)
            {
                lstIsEaten.Add(false);
                lstIsBlockVisible.Add(true);
            }

            clsCommon.GamePower.lstIsEaten = lstIsEaten;
            clsCommon.GamePower.lstIsBlockVisible = lstIsBlockVisible;

            return arrBlkRef;
        }
    }
}