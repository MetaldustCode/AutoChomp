using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsPolylineAdd
    {
        internal ObjectId AddPolyLine(Transaction acTrans, Database acDb, List<Point2d> lstPoint, int intColor, double dblWidth = 0)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            Polyline acPoly = new Polyline
            {
                Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor)
            };

            acPoly.SetDatabaseDefaults();
            for (int i = 0; i < lstPoint.Count; i++)
                acPoly.AddVertexAt(i, new Point2d(lstPoint[i].X, lstPoint[i].Y), 0, dblWidth, dblWidth);

            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acPoly);
            acTrans.AddNewlyCreatedDBObject(acPoly, true);

            return acPoly.ObjectId;
        }

        internal Line AddLine(Transaction acTrans, Database acDb, List<Point2d> lstPoint, int intColor)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            Line acPoly = new Line();
            acPoly.SetDatabaseDefaults();

            acPoly.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

            acPoly.StartPoint = lstPoint[0].ToPoint3d();
            acPoly.EndPoint = lstPoint[1].ToPoint3d();

            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acPoly);
            acTrans.AddNewlyCreatedDBObject(acPoly, true);

            return acPoly;
        }

        internal DBText AddText(Transaction acTrans, Database acDb, int intColor, string strText)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            DBText acPoly = new DBText();
            acPoly.SetDatabaseDefaults();

            acPoly.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

            acPoly.Justify = AttachmentPoint.MiddleCenter;

            acPoly.Height = 30;
            acPoly.TextString = strText;
            // Add the new object to the block table record and the transaction
            acBlkTblRec.AppendEntity(acPoly);
            acTrans.AddNewlyCreatedDBObject(acPoly, true);

            return acPoly;
        }

        internal void RemoveLines(List<Polyline> lstCombined)
        {
            for (int i = 0; i < lstCombined.Count; i++)
            {
                List<Boolean> lstDelete = new List<bool>();
                Polyline acPline = lstCombined[i];
                for (int k = 0; k < acPline.NumberOfVertices; k++)
                    lstDelete.Add(GetSegment(k, acPline));

                for (int k = lstDelete.Count - 1; k >= 0; k--)
                {
                    if (lstDelete[k])
                        acPline.RemoveVertexAt(k);
                }
            }
        }

        internal Boolean GetSegment(int index, Polyline acPline)
        {
            int prev = index == 0 && acPline.Closed ? acPline.NumberOfVertices - 1 : index - 1;
            if (acPline.GetSegmentType(prev) != SegmentType.Line ||
                acPline.GetSegmentType(index) != SegmentType.Line)
            {
                return false;
            }

            LineSegment2d seg1 = acPline.GetLineSegment2dAt(prev);
            LineSegment2d seg2 = acPline.GetLineSegment2dAt(index);

            double xDiff = seg1.StartPoint.X - seg1.EndPoint.X;
            double yDiff = seg1.StartPoint.Y - seg1.EndPoint.Y;
            double dblAngle1 = (double)Math.Atan2(yDiff, xDiff) * (double)(180 / Math.PI);

            double xDiff2 = seg2.StartPoint.X - seg2.EndPoint.X;
            double yDiff2 = seg2.StartPoint.Y - seg2.EndPoint.Y;
            double dblAngle2 = (double)Math.Atan2(yDiff2, xDiff2) * (double)(180 / Math.PI);

            if (dblAngle1 == dblAngle2)
                return true;
            return false;
        }

        internal BlockReference AddArrowBlock(Transaction acTrans, Database acDb, int intColor, string strBlockName)
        {
            BlockReference rtnValue = null;
            // Get the current document and database

            // Open the Block table for read
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            if (!acBlkTbl.Has(strBlockName))
            {
                Point2d pt = new Point2d();
                Polyline acPline = AddArrow(acTrans, acDb, pt, intColor);

                List<Entity> lstEntity = new List<Entity>() { acPline };

                clsBlock clsBlock = new clsBlock();
                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", intColor,  acTrans, acDb);
            }

            return rtnValue;
        }

        internal BlockReference AddBoxBlock(Transaction acTrans, Database acDb, int intColor, string strBlockName)
        {
            BlockReference rtnValue = null;

            // Open the Block table for read
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            if (!acBlkTbl.Has(strBlockName))
            {
                Point2d pt = new Point2d();
                Polyline acPline = AddBox(acTrans, acDb, pt, intColor);

                List<Entity> lstEntity = new List<Entity>() { acPline };

                clsBlock clsBlock = new clsBlock();
                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);
            }

            return rtnValue;
        }

        internal Polyline AddArrow(Transaction acTrans, Database acDb, Point2d pt, int intColor)
        {
            double Middle = clsGridValues.Middle;

            Point2d TM = new Point2d(pt.X, pt.Y + Middle);
            Point2d BL = new Point2d(pt.X - Middle, pt.Y - Middle);
            Point2d BR = new Point2d(pt.X + Middle, pt.Y - Middle);

            Polyline acPoly = new Polyline();
            {
                acPoly.ColorIndex = intColor;
                acPoly.SetDatabaseDefaults();
                acPoly.AddVertexAt(0, TM, 0, 2, 2);
                acPoly.AddVertexAt(1, BL, 0, 2, 2);
                acPoly.AddVertexAt(2, BR, 0, 2, 2);
                acPoly.Closed = true;
            }

            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            acBlkTblRec.AppendEntity(acPoly);
            acTrans.AddNewlyCreatedDBObject(acPoly, true);

            return acPoly;
        }

        internal Polyline AddBox(Transaction acTrans, Database acDb, Point2d pt, int intColor)
        {
            double Middle = clsGridValues.Middle;

            Point2d tl = new Point2d(pt.X - Middle, pt.Y + Middle);
            Point2d tr = new Point2d(pt.X + Middle, pt.Y + Middle);
            Point2d br = new Point2d(pt.X + Middle, pt.Y - Middle);
            Point2d bl = new Point2d(pt.X - Middle, pt.Y - Middle);

            Polyline acPoly = new Polyline();
            {
                acPoly.ColorIndex = intColor;
                acPoly.SetDatabaseDefaults();
                acPoly.AddVertexAt(0, tl, 0, 2, 2);
                acPoly.AddVertexAt(1, tr, 0, 2, 2);
                acPoly.AddVertexAt(1, br, 0, 2, 2);
                acPoly.AddVertexAt(1, bl, 0, 2, 2);
                acPoly.Closed = true;
            }

            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            acBlkTblRec.AppendEntity(acPoly);
            acTrans.AddNewlyCreatedDBObject(acPoly, true);

            return acPoly;
        }
    }
}