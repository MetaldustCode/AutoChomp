using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsCharacterAfraid
    {
        internal BlockReference AddGhostAfraid(String strBlockName, Direction GhostDirection, Squiggle Toggle)
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
                    List<Entity> lstOutlinePline = new List<Entity>();
                    List<Hatch> lstOutlineHatch = new List<Hatch>();

                    GenerateOutline(acTrans, acDb, acBlkTblRec,
                                    Toggle,
                                    ref lstOutlinePline, ref lstOutlineHatch);

                    List<Ellipse> lstFaceEllipse = new List<Ellipse>();
                    List<Entity> lstFacePline = new List<Entity>();
                    List<Hatch> lstFaceHatch = new List<Hatch>();

                    GenerateFace(acTrans, acDb, acBlkTblRec, GhostDirection, ref lstFaceEllipse, ref lstFacePline, ref lstFaceHatch);

                    List<Entity> lstOutline = new List<Entity>();
                    lstOutline.AddEntity(lstOutlinePline);
                    lstOutline.AddEntity(lstOutlineHatch);

                    SetColorOutline(strBlockName, lstOutline);

                    List<Entity> lstFace = new List<Entity>();
                    lstFace.AddEntity(lstFacePline);
                    lstFace.AddEntity(lstFaceHatch);
                    lstFace.AddEntity(lstFaceEllipse);

                    SetColorFace(strBlockName, lstFace);

                    List<Entity> lstEntity = new List<Entity>();

                    lstEntity.AddEntity(lstOutline);
                    lstEntity.AddEntity(lstFace);

                    clsBlock clsBlock = new clsBlock();
                    rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
                }
                else
                {
                    clsInsertBlock clsInsertBlock = new clsInsertBlock();
                    rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", 1, acTrans, acDb);
                }

                acTrans.Commit();
            }

            return rtnValue;
        }

        internal void GenerateFace(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                   Direction GhostDirection,
                                   ref List<Ellipse> rtnEllipse, ref List<Entity> rtnPline, ref List<Hatch> rtnHatch)
        {
            clsCharacterGhost clsCharacterGhost = new clsCharacterGhost();

            double dblRadius = 0;
            double dblWidth = 0;
            double dblBase = 0;
            double dblSide = 0;

            Arc acArc = GetArc(ref dblRadius, ref dblWidth, ref dblBase, ref dblSide);
            acArc.Dispose();

            List<Ellipse> lstPupil = new List<Ellipse>
            {
                clsCharacterGhost.AddEllipse(10, 1.0),
                clsCharacterGhost.AddEllipse(10, 1.0)
            };

            lstPupil.AppendEntity(acTrans, acBlkTblRec);

            List<ObjectId> lstPupilObjectId = new List<ObjectId>
            {
                lstPupil[0].ObjectId,
                lstPupil[1].ObjectId
            };

            Point2d ptPupil = new Point2d();

            clsCharacterGhost.GetPupleLocation(ref ptPupil, GhostDirection, dblWidth);

            Point2d ptEye = new Point2d();

            clsCharacterGhost.GetEyeLocation(ref ptEye, GhostDirection, dblWidth);

            lstPupil[0].MoveEntityXY(ptEye.X + ptPupil.X, ptEye.Y + ptPupil.Y);
            lstPupil[1].MoveEntityXY((ptEye.X * -1) + ptPupil.X, ptEye.Y + ptPupil.Y);

            clsHatch clsHatch = new clsHatch();
            List<Hatch> lstPupilHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstPupilObjectId, "SOLID", 1);

            for (int i = 0; i < lstPupilHatch.Count; i++)
                rtnHatch.Add(lstPupilHatch[i]);

            for (int i = 0; i < lstPupil.Count; i++)
                rtnEllipse.Add(lstPupil[i]);

            GenerateFrown(acTrans, acDb, acBlkTblRec,
                          dblWidth, ref rtnPline, ref rtnHatch);
        }

        internal Arc GetArc(ref double dblRadius, ref double dblWidth, ref double dblBase, ref double dblSide)
        {
            dblRadius = 80.0;
            double dblStart = 12.5;
            double dblEnd = 180 - 12.5;

            dblStart = dblStart.Deg2Rad();
            dblEnd = dblEnd.Deg2Rad();

            Arc acArc = new Arc(new Point3d(0, 0, 0), dblRadius, dblStart, dblEnd);

            acArc.SetDatabaseDefaults();

            dblWidth = acArc.StartPoint.X - acArc.EndPoint.X;
            dblBase = dblWidth / 5;
            dblSide = (dblRadius) - dblBase + (dblBase / 2);

            return acArc;
        }

        internal void GenerateOutline(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                     Squiggle Toggle,
                                      ref List<Entity> rtnPline, ref List<Hatch> rtnHatch)
        {
            clsCharacterGhost clsCharacterGhost = new clsCharacterGhost();

            double dblRadius = 0;
            double dblWidth = 0;
            double dblBase = 0;
            double dblSide = 0;

            // Top
            using (Arc acArc = GetArc(ref dblRadius, ref dblWidth, ref dblBase, ref dblSide))
            {
                Polyline acOutline = clsCharacterGhost.ArcToPolyline(acArc);

                // Side
                List<Polyline> lstPline = new List<Polyline>
                {
                    clsCharacterGhost.AddPolyline(acArc.StartPoint.ToPoint2d(), dblSide),
                    clsCharacterGhost.AddPolyline(acArc.EndPoint.ToPoint2d(), dblSide)
                };

                // Bottom Squiggle
                List<Polyline> lstArc = clsCharacterGhost.DrawSquiggle(dblWidth, dblSide, Toggle);

                List<Entity> lstEntity = new List<Entity>();

                lstEntity.AddEntity(lstPline);
                lstEntity.AddEntity(lstArc);

                lstEntity.Add(acOutline);

                // Combine Lines
                acOutline.JoinEntities(lstEntity.ToArray());
                acOutline.Closed = true;

                acOutline.AppendEntity(acTrans, acBlkTblRec);

                clsHatch clsHatch = new clsHatch();
                rtnHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, new List<ObjectId> { acOutline.ObjectId }, "SOLID", 1);

                rtnPline.Add(acOutline);
            }
        }

        internal void SetColorOutline(String strBlockName, List<Entity> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
            {
                if (strBlockName.EndsWith("B", StringComparison.CurrentCultureIgnoreCase))
                    lstEntity[i].Color = Color.FromRgb(35, 62, 139);
                else
                    lstEntity[i].ColorIndex = 7;
            }
        }

        internal void SetColorFace(String strBlockName, List<Entity> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
            {
                if (strBlockName.EndsWith("B", StringComparison.CurrentCultureIgnoreCase))
                    lstEntity[i].Color = Color.FromRgb(250, 190, 153);
                else
                    lstEntity[i].ColorIndex = 1;
            }
        }

        internal void GenerateFrown(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                     double dblWidth, ref List<Entity> rtnPline, ref List<Hatch> rtnHatch)
        {
            List<ObjectId> lstAfraidObjId = new List<ObjectId>();

            List<Polyline> lstAfraid = DrawAfraid(dblWidth);

            for (int i = 0; i < lstAfraid.Count; i++)
            {
                acBlkTblRec.AppendEntity(lstAfraid[i]);
                acTrans.AddNewlyCreatedDBObject(lstAfraid[i], true);

                lstAfraidObjId.Add(lstAfraid[i].ObjectId);
            }

            clsHatch clsHatch = new clsHatch();
            List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstAfraidObjId, "SOLID", 1);

            for (int i = 0; i < lstHatch.Count; i++)
                rtnHatch.Add(lstHatch[i]);

            rtnPline.AddEntity(lstAfraid);
        }

        private List<Polyline> DrawAfraid(double dblWidth)
        {
            Point2d pt = new Point2d(0, 0);

            dblWidth /= 6.5;
            double dblDown = dblWidth / 3.0;

            List<Polyline> lstArc = new List<Polyline>();

            Polyline Pline1 = new Polyline();
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i.IsEven())
                        Pline1.AddVertexAt(i, pt, 0.5, 0.0, 0.0);
                    else
                        Pline1.AddVertexAt(i, pt, -0.5, 0.0, 0.0);

                    pt = new Point2d((i * dblWidth), pt.Y);
                }
            }

            double dblLength = Pline1.EndPoint.X - Pline1.StartPoint.X;

            pt = new Point2d(0, dblDown);

            Polyline Pline2 = new Polyline();
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i.IsEven())
                        Pline2.AddVertexAt(i, pt, 0.5, 0.0, 0.0);
                    else
                        Pline2.AddVertexAt(i, pt, -0.5, 0.0, 0.0);

                    pt = new Point2d((i * dblWidth), pt.Y);
                }
            }

            double dblX = dblWidth * 0.32;

            Pline1.RemoveVertexAt(0);
            Pline2.RemoveVertexAt(0);

            Pline2.SetPointAt(0, new Point2d(Pline2.StartPoint.X - dblX, Pline1.StartPoint.Y + 5));
            Pline2.SetPointAt(Pline2.NumberOfVertices - 1, new Point2d(Pline2.EndPoint.X + dblX, Pline1.EndPoint.Y + 5));

            Polyline Pline3 = new Polyline();
            {
                Pline3.AddVertexAt(0, Pline1.StartPoint.ToPoint2d(), -0.8, 0.0, 0.0);
                Pline3.AddVertexAt(1, Pline2.StartPoint.ToPoint2d(), -0.8, 0.0, 0.0);
            }

            Polyline Pline4 = new Polyline();
            {
                Pline4.AddVertexAt(0, Pline1.EndPoint.ToPoint2d(), 0.8, 0.0, 0.0);
                Pline4.AddVertexAt(1, Pline2.EndPoint.ToPoint2d(), 0.8, 0.0, 0.0);
            }

            Entity[] objIdColl = new Entity[3];
            objIdColl[0] = Pline2;
            objIdColl[1] = Pline3;
            objIdColl[2] = Pline4;

            Pline1.JoinEntities(objIdColl);

            Pline1.Closed = true;

            lstArc.Add(Pline1);

            for (int i = 0; i < lstArc.Count; i++)
                lstArc[i].MoveEntityXY((dblLength / 2) * -1, -36);

            Pline2.Dispose();
            Pline3.Dispose();
            Pline4.Dispose();

            return lstArc;
        }
    }
}