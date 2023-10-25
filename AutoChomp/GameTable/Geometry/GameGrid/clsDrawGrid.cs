using Autodesk.AutoCAD.DatabaseServices;

using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsDrawGrid
    {
        internal void DrawGrid(Transaction acTrans, Database acDb,
                               String[,] a, int intColor, ref List<ObjectId> lstObjId)
        {
            clsDrawGridLines clsLine = new clsDrawGridLines();

            int row = a.GetLength(0);
            int col = a.GetLength(1);

            List<Polyline> lstPline = new List<Polyline>();

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    String strValue = a[r, c];

                    if (strValue == " ")
                        continue;

                    if (strValue == "H")
                        clsLine.DrawHorizontal(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "V")
                        clsLine.DrawVertical(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "1")
                        clsLine.DrawMidTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "2")
                        clsLine.DrawMidTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "3")
                        clsLine.DrawMidBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "4")
                        clsLine.DrawMidBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // -----------------------------

                    if (strValue == "A")
                        clsLine.DrawCornerSharpTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "S")
                        clsLine.DrawCornerSharpTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "Q")
                        clsLine.DrawCornerSharpBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "W")
                        clsLine.DrawCornerSharpBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "5")
                        clsLine.DrawCornerTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "6")
                        clsLine.DrawCornerTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "7")
                        clsLine.DrawCornerBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "8")
                        clsLine.DrawCornerBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // -----------------------------

                    if (strValue == "T")
                        clsLine.DrawDoubleTop(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "B")
                        clsLine.DrawDoubleBottom(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "R")
                        clsLine.DrawDoubleRight(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "L")
                        clsLine.DrawDoubleLeft(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // -----------------------------

                    if (strValue == "M")
                        clsLine.DrawEndCapTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "N")
                        clsLine.DrawEndCapTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "O")
                        clsLine.DrawEndCapBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "P")
                        clsLine.DrawEndCapBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "]")
                        clsLine.DrawMiddleCapR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "[")
                        clsLine.DrawMiddleCapL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // -----------------------------

                    if (strValue == ">")
                        clsLine.DrawOpeningR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    if (strValue == "<")
                        clsLine.DrawOpeningL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // -----------------------------

                    // TRFlat
                    if (strValue == "E")
                        clsLine.DrawJunctionTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // TLFlat
                    if (strValue == "F")
                        clsLine.DrawJunctionTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // TRFlat
                    if (strValue == "C")
                        clsLine.DrawJunctionBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // TLFlat
                    if (strValue == "D")
                        clsLine.DrawJunctionBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // TRSide
                    if (strValue == "I")
                        clsLine.DrawJunctionSideBR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // BRSide
                    if (strValue == "J")
                        clsLine.DrawJunctionSideTR(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // TLSide
                    if (strValue == "X")
                        clsLine.DrawJunctionSideTL(acTrans, acDb, r, c, intColor).Add(ref lstPline);

                    // BLSide
                    if (strValue == "Y")
                        clsLine.DrawJunctionSideBL(acTrans, acDb, r, c, intColor).Add(ref lstPline);
                }
            }

            for (int i = 0; i < lstPline.Count; i++)
                lstObjId.Add(lstPline[i].ObjectId);
        }
    }
}