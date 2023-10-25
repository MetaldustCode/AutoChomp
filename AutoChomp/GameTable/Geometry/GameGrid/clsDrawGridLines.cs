using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsDrawGridLines
    {
        private readonly clsElements clsElements = new clsElements();
        private readonly clsPolylineAdd clsShape = new clsPolylineAdd();

        internal Polyline DrawHorizontal(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetHorizontal(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawVertical(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetVertical(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);

            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        // ---------------------------

        internal Polyline DrawMidTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetMiddleTR(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);

            Polyline acPline = acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;

            acPline.FilletAt(1, clsGridValues.Middle);

            return acPline;
        }

        internal Polyline DrawMidTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetMiddleTL(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);

            Polyline acPline = acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;

            acPline.FilletAt(1, clsGridValues.Middle);

            return acPline;
        }

        internal Polyline DrawMidBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetMiddleBR(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);

            Polyline acPline = acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;

            acPline.FilletAt(1, clsGridValues.Middle);

            return acPline;
        }

        internal Polyline DrawMidBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstPoint = clsElements.GetMiddleBL(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstPoint, intColor);

            Polyline acPline = acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;

            acPline.FilletAt(1, clsGridValues.Middle);

            return acPline;
        }

        // ---------------------------

        internal List<Polyline> DrawCornerSharpTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstMiddle = clsElements.GetHorizontal(r, c);
            List<Point2d> lstMiddle2 = clsElements.GetMiddleRB(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle2, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            //acPline1.FilletAt(1, clsGridValues.OutSide);
            //acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerSharpTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstMiddle = clsElements.GetHorizontal(r, c);
            List<Point2d> lstMiddle2 = clsElements.GetMiddleLB(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle2, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            //acPline1.FilletAt(1, clsGridValues.OutSide);
            //acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerSharpBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstMiddle = clsElements.GetHorizontal(r, c);
            List<Point2d> lstMiddle2 = clsElements.GetMiddleRT(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle2, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            //acPline1.FilletAt(1, clsGridValues.OutSide);
            //acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerSharpBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstMiddle = clsElements.GetHorizontal(r, c);
            List<Point2d> lstMiddle2 = clsElements.GetMiddleLT(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle2, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            //acPline1.FilletAt(1, clsGridValues.OutSide);
            //acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // ---------------------------

        internal List<Polyline> DrawCornerTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetCornerTR(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline> { acPline1, acPline2 };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetCornerTL(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetCornerBR(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawCornerBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetCornerBL(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // ---------------------------

        internal List<Polyline> DrawJunctionTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetTop(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawJunctionTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetTop(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // C
        internal List<Polyline> DrawJunctionBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetBottom(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // D
        internal List<Polyline> DrawJunctionBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetBottom(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawJunctionSideBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetRight(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawJunctionSideTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetRight(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTR(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawJunctionSideBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetLeft(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleBL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawJunctionSideTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstCorner = clsElements.GetLeft(r, c);
            List<Point2d> lstMiddle = clsElements.GetMiddleTL(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstCorner, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstMiddle, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            acPline1.FilletAt(1, clsGridValues.OutSide);
            acPline2.FilletAt(1, clsGridValues.Inside);

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // ---------------------------

        internal List<Polyline> DrawDoubleTop(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstTop = clsElements.GetTop(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstHorizontal, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstTop, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawDoubleLeft(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstVertical = clsElements.GetVertical(r, c);
            List<Point2d> lstLeft = clsElements.GetLeft(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstVertical, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstLeft, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawDoubleRight(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstVertical = clsElements.GetVertical(r, c);
            List<Point2d> lstRight = clsElements.GetRight(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstVertical, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstRight, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        internal List<Polyline> DrawDoubleBottom(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstBottom = clsElements.GetBottom(r, c);

            List<ObjectId> lstObjId = new List<ObjectId>
            {
                clsShape.AddPolyLine(acTrans, acDb, lstHorizontal, intColor),
                clsShape.AddPolyLine(acTrans, acDb, lstBottom, intColor)
            };

            Polyline acPline1 = acTrans.GetObject(lstObjId[0], OpenMode.ForWrite) as Polyline;
            Polyline acPline2 = acTrans.GetObject(lstObjId[1], OpenMode.ForWrite) as Polyline;

            List<Polyline> rtnValue = new List<Polyline>
            {
                acPline1,
                acPline2
            };

            return rtnValue;
        }

        // ---------------------------

        internal Polyline DrawEndCapTR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstTop = clsElements.GetTop(r, c);

            List<Point2d> lstLoop = new List<Point2d>(lstHorizontal)
            {
                lstTop[1],
                lstTop[0]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawEndCapTL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstTop = clsElements.GetTop(r, c);

            List<Point2d> lstLoop = new List<Point2d>
            {
                lstHorizontal[1],
                lstHorizontal[0],
                lstTop[0],
                lstTop[1]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawEndCapBR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstTop = clsElements.GetBottom(r, c);

            List<Point2d> lstLoop = new List<Point2d>(lstHorizontal)
            {
                lstTop[1],
                lstTop[0]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawEndCapBL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstHorizontal = clsElements.GetHorizontal(r, c);
            List<Point2d> lstTop = clsElements.GetBottom(r, c);

            List<Point2d> lstLoop = new List<Point2d>
            {
                lstHorizontal[1],
                lstHorizontal[0],
                lstTop[0],
                lstTop[1]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawOpeningL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstTop = clsElements.GetOpeningTop(r, c);
            List<Point2d> lstBottom = clsElements.GetOpeningBottom(r, c);

            lstBottom[0] = new Point2d(lstBottom[0].X + 0.75, lstBottom[0].Y);
            lstTop[0] = new Point2d(lstTop[0].X + 0.75, lstTop[0].Y);

            List<Point2d> lstLoop = new List<Point2d>
            {
                lstTop[1],
                lstTop[0],
                lstBottom[0],
                lstBottom[1]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawOpeningR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstTop = clsElements.GetOpeningTop(r, c);
            List<Point2d> lstBottom = clsElements.GetOpeningBottom(r, c);

            lstBottom[1] = new Point2d(lstBottom[1].X - 0.75, lstBottom[1].Y);
            lstTop[1] = new Point2d(lstTop[1].X - 0.75, lstTop[1].Y);

            List<Point2d> lstLoop = new List<Point2d>
            {
                lstTop[0],
                lstTop[1],
                lstBottom[1],
                lstBottom[0]
            };

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawMiddleCapL(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstLoop = clsElements.GetMiddleCapLeft(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }

        internal Polyline DrawMiddleCapR(Transaction acTrans, Database acDb, int r, int c, int intColor)
        {
            List<Point2d> lstLoop = clsElements.GetMiddleCapRight(r, c);

            ObjectId objId = clsShape.AddPolyLine(acTrans, acDb, lstLoop, intColor);
            return acTrans.GetObject(objId, OpenMode.ForWrite) as Polyline;
        }
    }
}