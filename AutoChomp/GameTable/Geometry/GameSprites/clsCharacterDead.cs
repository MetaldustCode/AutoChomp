using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsCharacterDead
    {
        private readonly clsCharacterGhost clsCharacterGhost = new clsCharacterGhost();

        internal BlockReference AddGhostDead(String strBlockName, Direction Direction)
        {
            BlockReference rtnValue = null;
            // Get the current document and database

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId,
                                             OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                if (!acBlkTbl.Has(strBlockName))
                {
                    double dblRadius = 80.0;

                    double dblStart = 12.5;
                    double dblEnd = 180 - 12.5;

                    dblStart = dblStart.Deg2Rad();
                    dblEnd = dblEnd.Deg2Rad();

                    List<ObjectId> lstObjId = new List<ObjectId>();
                    List<ObjectId> lstEyeObjectId = new List<ObjectId>();

                    using (Arc acArc = new Arc(new Point3d(0, 0, 0),
                                          dblRadius, dblStart, dblEnd))
                    {
                        acArc.SetDatabaseDefaults();

                        double dblWidth = acArc.StartPoint.X - acArc.EndPoint.X;
                        double dblBase = dblWidth / 5;
                        double dblSide = (dblRadius) - dblBase + (dblBase / 2);

                        List<Ellipse> lstEye = new List<Ellipse>
                        {
                            clsCharacterGhost.AddEllipse(24, 0.75),
                            clsCharacterGhost.AddEllipse(24, 0.75)
                        };

                        List<Ellipse> lstPupil = new List<Ellipse>
                        {
                            clsCharacterGhost.AddEllipse(10, 1.0),
                            clsCharacterGhost.AddEllipse(10, 1.0)
                        };

                        Point2d ptEye = new Point2d();

                        clsCharacterGhost.GetEyeLocation(ref ptEye, Direction, dblWidth);

                        Point2d ptPupil = new Point2d();

                        clsCharacterGhost.GetPupleLocation(ref ptPupil, Direction, dblWidth);

                        lstEye[0].MoveEntityXY(ptEye.X, ptEye.Y);
                        lstEye[1].MoveEntityXY(ptEye.X * -1, ptEye.Y);

                        lstPupil[0].MoveEntityXY((ptEye.X + ptPupil.X), ptEye.Y + ptPupil.Y);
                        lstPupil[1].MoveEntityXY(((ptEye.X * -1) + ptPupil.X), ptEye.Y + ptPupil.Y);

                        for (int i = 0; i < lstEye.Count; i++)
                        {
                            acBlkTblRec.AppendEntity(lstEye[i]);
                            acTrans.AddNewlyCreatedDBObject(lstEye[i], true);
                        }

                        for (int i = 0; i < lstPupil.Count; i++)
                        {
                            acBlkTblRec.AppendEntity(lstPupil[i]);
                            acTrans.AddNewlyCreatedDBObject(lstPupil[i], true);
                        }

                        lstObjId.Add(lstEye[0].ObjectId);
                        lstObjId.Add(lstEye[1].ObjectId);
                        lstObjId.Add(lstPupil[0].ObjectId);
                        lstObjId.Add(lstPupil[1].ObjectId);

                        clsHatch clsHatch = new clsHatch();
                        List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstObjId, "Solid", 1);

                        List<Entity> lstEntity = new List<Entity>();

                        for (int i = 0; i < lstHatch.Count; i++)
                        {
                            if (i >= 0 && i <= 1)
                                lstHatch[i].ColorIndex = 7;

                            if (i >= 2 && i <= 3)
                                lstHatch[i].ColorIndex = 5;

                            lstEntity.Add(lstHatch[i]);
                        }

                        for (int i = 0; i < lstEye.Count; i++)
                            lstEntity.Add(lstEye[i]);

                        for (int i = 0; i < lstPupil.Count; i++)
                            lstEntity.Add(lstPupil[i]);

                        clsBlock clsBlock = new clsBlock();

                        BlockReference acBlkRef = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
                        rtnValue = acBlkRef;
                    }
                }
                else
                {
                    clsInsertBlock clsInsertBlock = new clsInsertBlock();

                    BlockReference acBlkRef = clsInsertBlock.InsertBlock(strBlockName, "0", 1,  acTrans, acDb);

                    rtnValue = acBlkRef;
                }
                acTrans.Commit();
            }

            return rtnValue;
        }
    }
}