using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsDrawDots
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void PlaceBlocks(BlockReference[,] arrBlkRef)
        {
            List<BlockReference> lstBlockRef = arrBlkRef.ToList();
            List<Point2d> lstOrigin = arrBlkRef.GetOrigin();

            for (int i = 0; i < lstBlockRef.Count; i++)
            {
                BlockReference acBlkRef = lstBlockRef[i];

                acBlkRef.Position = lstOrigin[i].ToPoint3d();
            }
        }

        internal BlockReference[,] DrawDots(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec, Boolean[,] arrXGrid)
        {
            return AddDots(acTrans, acDb, acBlkTbl, acBlkTblRec, arrXGrid, "Game_Dot", 10, 7);
        }

        internal BlockReference[,] DrawPower(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec, Boolean[,] arrXGrid)
        {
            return AddDots(acTrans, acDb, acBlkTbl, acBlkTblRec, arrXGrid, "Game_Power", 30, 7);
        }

        internal BlockReference[,] AddDots(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec,
                                           Boolean[,] arrXGrid, string strBlockName, double dblSize, int intColor)
        {
            arrXGrid.GetSize(out int col, out int row);

            BlockReference[,] rtnValue = new BlockReference[col, row];

            clsDrawDots clsGameDots = new clsDrawDots();

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arrXGrid[c, r])
                        rtnValue[c, r] = clsGameDots.CreateDot(acTrans, acDb, acBlkTblRec, acBlkTbl,
                                                               strBlockName, intColor, dblSize);
                }
            }

            return rtnValue;
        }

        internal BlockReference CreateDot(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec, BlockTable acBlkTbl,
                                       string strBlockName, int intColor, double dblRadius)
        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                Point3d pt3Mid = new Point3d(0, 0, 0);

                clsCircle clsCircle = new clsCircle();
                Circle acCircle = clsCircle.AddCircle(acTrans, acDb, pt3Mid, intColor, dblRadius);

                List<ObjectId> lstObjectId = new List<ObjectId> { acCircle.ObjectId };

                clsHatch clsHatch = new clsHatch();
                List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstObjectId, "Solid", 1);

                List<Entity> lstEntity = new List<Entity> { acCircle };

                for (int i = 0; i < lstHatch.Count; i++)
                    lstEntity.Add(lstHatch[i]);

                clsBlock clsBlock = new clsBlock();

                BlockReference acBlkRef = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);

                rtnValue = acBlkRef;
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();

                BlockReference acBlkRef = clsInsertBlock.InsertBlock(strBlockName, "0", acTrans, acDb);

                rtnValue = acBlkRef;
            }

            return rtnValue;
        }
    }
}