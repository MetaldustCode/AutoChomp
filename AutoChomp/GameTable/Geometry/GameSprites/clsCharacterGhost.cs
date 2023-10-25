using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsCharacterGhost
    {
        internal BlockReference AddGhost(GameGhost StructGhost, String strBlockName, Direction Direction, Squiggle Toggle)
        {
            clsHatch clsHatch = new clsHatch();

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
                    List<Entity> lstEntity = new List<Entity>();

                    // Ghost Dome Size
                    double dblRadius = 80.0;

                    // Ghost Dome Angle
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

                        // Set Default Ghost Size
                        double dblWidth = acArc.StartPoint.X - acArc.EndPoint.X;
                        double dblBase = dblWidth / 5;
                        double dblSide = (dblRadius) - dblBase + (dblBase / 2);

                        // Add Side Wall
                        List<Polyline> lstSideWallPline = new List<Polyline>
                        {
                            AddPolyline(acArc.StartPoint.ToPoint2d(), dblSide),
                            AddPolyline(acArc.EndPoint.ToPoint2d(), dblSide)
                        };

                        // Add Eye Opening
                        List<Ellipse> lstEye = new List<Ellipse>
                        {
                            AddEllipse(24, 0.75),
                            AddEllipse(24, 0.75)
                        };

                        // Add Pupil
                        List<Ellipse> lstPupil = new List<Ellipse>
                        {
                            AddEllipse(10, 1.0),
                            AddEllipse(10, 1.0)
                        };

                        // Get Eye Location
                        Point2d ptEye = new Point2d();
                        GetEyeLocation(ref ptEye, Direction, dblWidth);

                        // Move Puple Location
                        Point2d ptPupil = new Point2d();
                        GetPupleLocation(ref ptPupil, Direction, dblWidth);

                        // Move Eye into Position
                        lstEye[0].MoveEntityXY(ptEye.X, ptEye.Y);
                        lstEye[1].MoveEntityXY(ptEye.X * -1, ptEye.Y);

                        // Move Pupil relative to EYe
                        lstPupil[0].MoveEntityXY((ptEye.X + ptPupil.X), ptEye.Y + ptPupil.Y);
                        lstPupil[1].MoveEntityXY(((ptEye.X * -1) + ptPupil.X), ptEye.Y + ptPupil.Y);

                        lstEye.AppendEntity(acTrans, acBlkTblRec);
                        lstPupil.AppendEntity(acTrans, acBlkTblRec);

                        lstObjId.Add(lstEye[0].ObjectId);
                        lstObjId.Add(lstEye[1].ObjectId);
                        lstObjId.Add(lstPupil[0].ObjectId);
                        lstObjId.Add(lstPupil[1].ObjectId);

                        // Add Bottom Squiggle
                        List<Polyline> lstArc = DrawSquiggle(dblWidth, dblSide, Toggle);

                        List<Entity> lstPline = new List<Entity>();

                        lstPline.AddEntity(lstSideWallPline);
                        lstPline.AddEntity(lstArc);

                        Polyline acPline = ArcToPolyline(acArc);
                        // Set Outline Color
                        acPline.ColorIndex = StructGhost.intColorIndex;

                        acPline.AppendEntity(acTrans, acBlkTblRec);

                        // Combine Ghost Outline
                        acPline.JoinEntities(lstPline.ToArray());

                        lstObjId.Insert(0, acPline.ObjectId);

                        // Fill in Hatch and Set Color
                        List<Hatch> lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstObjId, "Solid", 1);
                        SetHatchColor(StructGhost, lstHatch);

                        lstEntity.Add(acPline);
                        lstEntity.AddEntity(lstHatch);
                        lstEntity.AddEntity(lstEye);
                        lstEntity.AddEntity(lstPupil);

                        clsBlock clsBlock = new clsBlock();

                        // Save Block
                        BlockReference acBlkRef = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
                        rtnValue = acBlkRef;
                    }
                }
                else
                {
                    clsInsertBlock clsInsertBlock = new clsInsertBlock();

                    BlockReference acBlkRef = clsInsertBlock.InsertBlock(strBlockName, "0", acTrans, acDb);

                    rtnValue = acBlkRef;
                }
                acTrans.Commit();
            }

            return rtnValue;
        }

        internal void SetHatchColor(GameGhost StructGhost, List<Hatch> lstHatch)
        {
            for (int i = 0; i < lstHatch.Count; i++)
            {
                if (i == 0)
                    lstHatch[i].ColorIndex = StructGhost.intColorIndex;
                if (i >= 1 && i <= 2)
                    lstHatch[i].ColorIndex = 7;
                if (i >= 3 && i <= 4)
                    lstHatch[i].ColorIndex = 5;
            }
        }

        internal void GetEyeLocation(ref Point2d pt1, Direction Direction, double dblWidth)
        {
            if (Direction == Direction.Right || Direction == Direction.Left || Direction == Direction.Down || Direction == Direction.None)
            {
                double dblX = (dblWidth * 0.25);
                double dblY = dblWidth * 0.10;

                pt1 = new Point2d(dblX, dblY);
            }

            if (Direction == Direction.Up)
            {
                double dblX = (dblWidth * 0.25);
                double dblY = dblWidth * 0.10 + 0;

                pt1 = new Point2d(dblX, dblY);
            }
        }

        internal void GetPupleLocation(ref Point2d pt1, Direction Direction, double dblWidth)
        {
            if (Direction == Direction.None)
            {
                double dbl1X = (dblWidth * 0.00);
                double dbl1Y = dblWidth * 0.00;

                pt1 = new Point2d(dbl1X, dbl1Y);
            }

            if (Direction == Direction.Right)
            {
                double dbl1X = (dblWidth * 0.05);
                double dbl1Y = dblWidth * 0.00;

                pt1 = new Point2d(dbl1X, dbl1Y);
            }

            if (Direction == Direction.Left)
            {
                double dbl1X = (dblWidth * -0.05);
                double dbl1Y = dblWidth * 0.00;

                pt1 = new Point2d(dbl1X, dbl1Y);
            }

            if (Direction == Direction.Up)
            {
                double dbl1X = (dblWidth * 0.0);
                double dbl1Y = dblWidth * 0.085;

                pt1 = new Point2d(dbl1X, dbl1Y);
            }

            if (Direction == Direction.Down)
            {
                double dbl1X = (dblWidth * -0.0);
                double dbl1Y = dblWidth * -0.085;

                pt1 = new Point2d(dbl1X, dbl1Y);
            }
        }

        internal Polyline AddPolyline(Point2d ptStartPoint, double dblLength)
        {
            Point2d ptEndPoint = new Point2d(ptStartPoint.X, 0 - dblLength);
            Polyline acPoly = new Polyline();
            {
                acPoly.SetDatabaseDefaults();
                acPoly.AddVertexAt(0, ptStartPoint, 0, 0, 0);
                acPoly.AddVertexAt(1, ptEndPoint, 0, 0, 0);
            }

            return acPoly;
        }

        internal List<Polyline> DrawSquiggle(double dblWidth, double dblHeight,
                                             Squiggle Toggle)
        {
            dblWidth /= 2;
            dblWidth /= 5;

            List<Polyline> lstArc = new List<Polyline>();

            if (Toggle == Squiggle.Standard)
            {
                lstArc.Add(AddArc(dblWidth, true));
                lstArc.Add(AddArc(dblWidth, false));
                lstArc.Add(AddArc(dblWidth, true));
                lstArc.Add(AddArc(dblWidth, false));
                lstArc.Add(AddArc(dblWidth, true));

                for (int i = 0; i < lstArc.Count; i++)
                    lstArc[i].MoveEntityXY(((dblWidth * 2) * i) - (dblWidth * 4), dblHeight * -1);
            }
            else
            {
                lstArc.Add(AddArc(dblWidth, false));
                lstArc.Add(AddArc(dblWidth, true));
                lstArc.Add(AddArc(dblWidth, false));
                lstArc.Add(AddArc(dblWidth, true));
                lstArc.Add(AddArc(dblWidth, false));

                for (int i = 0; i < lstArc.Count; i++)
                    lstArc[i].MoveEntityXY(((dblWidth * 2) * i) - (dblWidth * 4), dblHeight * -1);
            }

            return lstArc;
        }

        private Polyline AddArc(double dblWidth, Boolean bolUp)
        {
            double dblStart = 180;
            double dblEnd = 0;

            if (!bolUp)
            {
                dblStart = 0;
                dblEnd = 180;
            }

            dblStart = dblStart.Deg2Rad();
            dblEnd = dblEnd.Deg2Rad();

            Arc acArc = new Arc(new Point3d(0, 0, 0), dblWidth, dblStart, dblEnd);
            acArc.SetDatabaseDefaults();

            Polyline acPlineArc = ArcToPolyline(acArc);

            return acPlineArc;
        }

        internal Polyline ArcToPolyline(Arc arc)
        {
            Polyline poly = new Polyline();
            poly.AddVertexAt(0, new Point2d(arc.StartPoint.X, arc.StartPoint.Y), GetArcBulge(arc), 0, 0);
            poly.AddVertexAt(1, new Point2d(arc.EndPoint.X, arc.EndPoint.Y), 0, 0, 0);

            return poly;
        }

        private double GetArcBulge(Arc arc)
        {
            double deltaAng = arc.EndAngle - arc.StartAngle;
            if (deltaAng < 0)
                deltaAng += 2 * Math.PI;
            return Math.Tan(deltaAng * 0.25);
        }

        internal Ellipse AddEllipse(Double dblSize, double dblRatio)
        {
            Point3d center = Point3d.Origin;

            Vector3d normal = Vector3d.ZAxis;

            Vector3d majorAxis = dblSize * Vector3d.XAxis;

            double radiusRatio = dblRatio;

            double startAng = 0.0;

            double endAng = 360 * Math.Atan(1.0) / 45.0;
            Ellipse ellipse = new Ellipse(center, normal,
                                          majorAxis, radiusRatio,
                                          startAng, endAng);
            rotateEntity(ellipse, 90);

            return ellipse;
        }

        internal Entity rotateEntity(Entity acEnt, double dblOffset)
        {
            Matrix3d matrix = Matrix3d.Rotation(dblOffset.ToRadians(), Vector3d.ZAxis, Point3d.Origin);
            acEnt.TransformBy(matrix);

            return acEnt;
        }

        internal List<GameGhost> BuildGhostTable()
        {
            List<String> lstName = new List<string>() { "Red", "Pink", "Blue", "Orange" }.Multiply();

            List<int> lstColor = new List<int>() { 1, 241, 4, 30 }.Multiply();

            List<StartLocation> lstStart = new List<StartLocation>()
            {
                StartLocation.Outside,
                StartLocation.Left,
                StartLocation.Middle,
                StartLocation.Right
            }.Multiply();

            List<GameGhost> rtnValue = new List<GameGhost>();

            for (int i = 0; i < lstName.Count; i++)
                rtnValue.Add(new GameGhost(lstName[i], lstName[i], lstColor[i], lstStart[i]));

            return rtnValue;
        }
    }
}