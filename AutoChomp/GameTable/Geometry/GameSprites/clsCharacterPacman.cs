using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsCharacterPacman
    {
        internal BlockReference AddPacman(Transaction acTrans, Database acDb,
                                        BlockTable acBlkTbl, BlockTableRecord acBlkTblRec,
                                        string strBlockName, double dblRadius,
                                        double dblStart, double dblEnd)
        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                dblStart = dblStart.Deg2Rad();
                dblEnd = dblEnd.Deg2Rad();

                Polyline acPline = new Polyline();

                using (Arc acArc = new Arc(new Point3d(0, 0, 0),
                                      dblRadius, dblStart, dblEnd))
                {
                    acArc.SetDatabaseDefaults();
                    acPline = AddPolyline(acTrans, acBlkTblRec, acArc, dblRadius);
                    acPline.ColorIndex = 2;
                }

                List<ObjectId> lstHandle = new List<ObjectId> { acPline.ObjectId };

                clsHatch clsHatch = new clsHatch();
                List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstHandle, "Solid", 2);

                List<Entity> lstEntity = new List<Entity> { acPline };

                for (int i = 0; i < lstHatch.Count; i++)
                    lstEntity.Add(lstHatch[i]);

                clsBlock clsBlock = new clsBlock();

                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", 1, acTrans, acDb);
            }

            return rtnValue;
        }

        internal BlockReference AddPacman(Transaction acTrans, Database acDb,
                                        BlockTable acBlkTbl, BlockTableRecord acBlkTblRec,
                                        string strBlockName, double dblRadius)
        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                Circle acCircle = AddCircle(acTrans, acBlkTblRec);
                acCircle.ColorIndex = 2;
                acCircle.Radius = dblRadius;
                List<ObjectId> lstObjectId = new List<ObjectId> { acCircle.ObjectId };

                clsHatch clsHatch = new clsHatch();
                List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstObjectId, "Solid", 1);

                List<Entity> lstEntity = new List<Entity>
                {
                    acCircle
                };

                for (int i = 0; i < lstHatch.Count; i++)
                    lstEntity.Add(lstHatch[i]);

                clsBlock clsBlock = new clsBlock();

                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", 1, acTrans, acDb);
            }

            return rtnValue;
        }

        private Polyline AddPolyline(Transaction acTrans, BlockTableRecord acBlkTblRec, Arc acArc, double dblRadius)
        {
            double dblOffset = (dblRadius * 0.3) * -1;

            Point2d ptStartPoint = acArc.StartPoint.ToPoint2d();
            Point2d ptMidPoint = new Point2d(dblOffset, 0);
            Point2d ptEndPoint = acArc.EndPoint.ToPoint2d();
            Polyline acPoly = new Polyline();
            {
                acPoly.ColorIndex = 2;
                acPoly.SetDatabaseDefaults();
                acPoly.AddVertexAt(0, ptStartPoint, 0, 0, 0);
                acPoly.AddVertexAt(1, ptMidPoint, 0, 0, 0);
                acPoly.AddVertexAt(2, ptEndPoint, 0, 0, 0);

                // Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acPoly);
                acTrans.AddNewlyCreatedDBObject(acPoly, true);

                acPoly.JoinEntity(acArc);
            }

            return acPoly;
        }

        internal Circle AddCircle(Transaction acTrans, BlockTableRecord acBlkTblRec)
        {
            Circle acCircle = new Circle();
            acCircle.SetDatabaseDefaults();
            acCircle.Center = new Point3d(0, 0, 0);
            acCircle.Radius = 80;

            acBlkTblRec.AppendEntity(acCircle);
            acTrans.AddNewlyCreatedDBObject(acCircle, true);
            return acCircle;
        }

        internal BlockReference AddExplode(Transaction acTrans, Database acDb,
                                         BlockTable acBlkTbl, BlockTableRecord acBlkTblRec,
                                         string strBlockName,
                                         double dblInsideRadius, double dblOutsideRadius)

        {
            BlockReference rtnValue = null;

            if (!acBlkTbl.Has(strBlockName))
            {
                double dblCount = 10.0;
                double dblAngle = 360.0 / dblCount;

                List<double> lstAngle = new List<double>();

                for (int i = 0; i < dblCount; i++)
                    lstAngle.Add((dblAngle * i));

                List<Entity> lstEntity = new List<Entity>();

                for (int i = 0; i < lstAngle.Count; i++)
                {
                    Point3d pt1 = new Point3d(dblInsideRadius, 0, 0);
                    Point3d pt2 = new Point3d(dblOutsideRadius, 0, 0);

                    pt1 = RotatePoint(pt1, new Point3d(), lstAngle[i]);
                    pt2 = RotatePoint(pt2, new Point3d(), lstAngle[i]);

                    Polyline acPoly = new Polyline();
                    {
                        acPoly.ColorIndex = 2;
                        acPoly.SetDatabaseDefaults();
                        acPoly.AddVertexAt(0, pt1.ToPoint2d(), 0, 0, 0);
                        acPoly.AddVertexAt(1, pt2.ToPoint2d(), 0, 0, 0);
                        acPoly.ConstantWidth = 2.0;
                    }

                    acPoly.SetDatabaseDefaults();
                    acPoly.ColorIndex = 2;

                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);

                    lstEntity.Add((Entity)acPoly);
                }

                clsBlock clsBlock = new clsBlock();

                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", 1,  acTrans, acDb);
            }

            return rtnValue;
        }

        private Point3d RotatePoint(Point3d pointToRotate, Point3d centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            double pt1 = (cosTheta * (pointToRotate.X - centerPoint.X) -
                          sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X);

            double pt2 = (sinTheta * (pointToRotate.X - centerPoint.X) +
                         cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y);

            return new Point3d(pt1, pt2, 0);
        }
    }
}