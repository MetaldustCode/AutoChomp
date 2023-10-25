using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMainGrid
    {
        internal List<ObjectId> Main(Transaction acTrans, Database acDb,
                                     BlockTableRecord acBlkTblRec)
        {
            List<Polyline> lstPolyline = new List<Polyline>();
            List<Hatch> lstHatch = new List<Hatch>();

            List<Polyline> lstGridBorder = new List<Polyline>();
            List<Polyline> lstGridIsland = new List<Polyline>();

            clsGameGrid clsGameGrid = new clsGameGrid();
            clsGameGrid.DrawGrid(acTrans, acDb, ref lstGridBorder, ref lstGridIsland);

            clsGameGrid.DrawOpening(acTrans, acDb, acBlkTblRec, ref lstPolyline, ref lstHatch);

            clsGameGrid.AddPolyline(lstGridBorder, ref lstPolyline);
            clsGameGrid.AddPolyline(lstGridIsland, ref lstPolyline);

            List<ObjectId> rtnValue = new List<ObjectId>();
            lstPolyline.AddToObjectId(ref rtnValue);
            lstHatch.AddToObjectId(ref rtnValue);
            lstGridBorder.AddToObjectId(ref rtnValue);
            lstGridIsland.AddToObjectId(ref rtnValue);

            return rtnValue;
        }
    }
}