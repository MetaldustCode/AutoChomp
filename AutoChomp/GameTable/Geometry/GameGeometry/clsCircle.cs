using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;

namespace AutoChomp
{
    internal class clsCircle
    {
        internal Circle AddCircle(Transaction acTrans, Database acDb, Point3d pt1, int intColor, double dblRadius)
        {
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
            // Open the Block table record Model space for write
            BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;

            Circle acCirc = new Circle();
            {
                acCirc.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

                acCirc.SetDatabaseDefaults();
                acCirc.Center = pt1;
                acCirc.Radius = dblRadius;

                // Add the new object to the block table record and the transaction
                acBlkTblRec.AppendEntity(acCirc);
                acTrans.AddNewlyCreatedDBObject(acCirc, true);
            }

            return acCirc;
        }
    }
}