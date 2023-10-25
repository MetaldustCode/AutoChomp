using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal static class clsEntityAppend
    {
        internal static void AppendEntity(this Polyline acPline, Transaction acTrans, BlockTableRecord acBlkTblRec)
        {
            acBlkTblRec.AppendEntity(acPline);
            acTrans.AddNewlyCreatedDBObject(acPline, true);
        }

        internal static void AppendEntity(this List<Ellipse> lstEllipse, Transaction acTrans, BlockTableRecord acBlkTblRec)
        {
            for (int i = 0; i < lstEllipse.Count; i++)
            {
                acBlkTblRec.AppendEntity(lstEllipse[i]);
                acTrans.AddNewlyCreatedDBObject(lstEllipse[i], true);
            }
        }

        internal static void AddEntity(this List<Entity> rtnValue, List<Entity> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                rtnValue.Add(lstEntity[i]);
        }

        internal static void AddEntity(this List<Entity> rtnValue, List<Polyline> lstPolyline)
        {
            for (int i = 0; i < lstPolyline.Count; i++)
                rtnValue.Add(lstPolyline[i]);
        }

        internal static void AddEntity(this List<Entity> rtnValue, List<Ellipse> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                rtnValue.Add(lstEntity[i]);
        }

        internal static void AddEntity(this List<Entity> rtnValue, List<Hatch> lsthatch)
        {
            for (int i = 0; i < lsthatch.Count; i++)
                rtnValue.Add(lsthatch[i]);
        }
    }
}