using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsValidDirection
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void DeleteValid()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    if (clsCommon.GameDebug == null)
                        clsCommon.GameDebug = new GameDebug();

                    EraseDirection(acTrans, clsCommon.GameDebug.lstObjDirection);
                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
        }

        internal void DrawValid()
        {
            int intWidth = -1; int intHeight = -1;

            clsGetGrid clsGetGrid = new clsGetGrid();
            Boolean bolCellSize = clsGetGrid.GetCellSize(ref intWidth, ref intHeight);

            if (bolCellSize)
            {
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Database acDb = acDoc.Database;

                // Start a transaction
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    using (DocumentLock @lock = acDoc.LockDocument())
                    {
                        if (clsCommon.GameDebug == null)
                            clsCommon.GameDebug = new GameDebug();

                        EraseDirection(acTrans, clsCommon.GameDebug.lstObjDirection);

                        List<Direction>[,] arr = clsClassTables.arrXDirection;

                        List<ObjectId> lstObjectIds = new List<ObjectId>();

                        for (int x = 0; x < intWidth; x++)
                        {
                            for (int y = 0; y < intHeight; y++)
                            {
                                if (arr[x, y] != null)
                                {
                                    if (arr[x, y].Count > 0)
                                    {
                                        Point2d ptMid = new Point2d(x * Cell + Middle, y * Cell + Middle);

                                        clsCircle clsCircle = new clsCircle();
                                        lstObjectIds.Add(clsCircle.AddCircle(acTrans, acDb, ptMid.ToPoint3d(), 9, 20).ObjectId);

                                        List<Direction> lstDirections = arr[x, y];

                                        for (int d = 0; d < lstDirections.Count; d++)
                                        {
                                            if (lstDirections[d] == Direction.Up)
                                                lstObjectIds.Add(AddUp(acTrans, acDb, ptMid).ObjectId);

                                            if (lstDirections[d] == Direction.Down)
                                                lstObjectIds.Add(AddDown(acTrans, acDb, ptMid).ObjectId);

                                            if (lstDirections[d] == Direction.Right)
                                                lstObjectIds.Add(AddRight(acTrans, acDb, ptMid).ObjectId);

                                            if (lstDirections[d] == Direction.Left)
                                                lstObjectIds.Add(AddLeft(acTrans, acDb, ptMid).ObjectId);
                                        }
                                    }
                                }
                            }
                        }

                        acTrans.Commit();

                        clsCommon.GameDebug.lstObjDirection = lstObjectIds;
                    }
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
        }

        internal void EraseDirection(Transaction acTrans, List<ObjectId> lstObjectIds)
        {
            for (int i = 0; i < lstObjectIds.Count; i++)
            {
                if (lstObjectIds[i].IsValid && !lstObjectIds[i].IsErased)
                {
                    Entity acEntity = acTrans.GetObject(lstObjectIds[i], OpenMode.ForWrite) as Entity;
                    acEntity.Erase();
                }
            }

            lstObjectIds.Clear();
        }

        internal Line AddUp(Transaction acTrans, Database acDb, Point2d pt)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            Point2d ptEnd = new Point2d(pt.X, pt.Y + Middle);
            return clsPolylineAdd.AddLine(acTrans, acDb, new List<Point2d>() { pt, ptEnd }, 7);
        }

        internal Line AddDown(Transaction acTrans, Database acDb, Point2d pt)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            Point2d ptEnd = new Point2d(pt.X, pt.Y - Middle);
            return clsPolylineAdd.AddLine(acTrans, acDb, new List<Point2d>() { pt, ptEnd }, 7);
        }

        internal Line AddRight(Transaction acTrans, Database acDb, Point2d pt)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            Point2d ptEnd = new Point2d(pt.X + Middle, pt.Y);
            return clsPolylineAdd.AddLine(acTrans, acDb, new List<Point2d>() { pt, ptEnd }, 7);
        }

        internal Line AddLeft(Transaction acTrans, Database acDb, Point2d pt)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            Point2d ptEnd = new Point2d(pt.X - Middle, pt.Y);
            return clsPolylineAdd.AddLine(acTrans, acDb, new List<Point2d>() { pt, ptEnd }, 7);
        }
    }
}