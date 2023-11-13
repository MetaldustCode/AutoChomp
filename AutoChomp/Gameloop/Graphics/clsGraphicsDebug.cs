using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp.Gameloop.Graphics
{
    internal class clsGraphicsDebug
    {
        internal void DrawCircle()
        {
            List<Point2d> lstPoints = clsCommon.GameDebug.lstCircleOrigin;
            lstPoints = lstPoints.Distinct().ToList();  

            for (int i = 0; i < lstPoints.Count; i++)
            {
                AddCircle(lstPoints[i]);
            }
        }

        internal void AddCircle(Point2d pt)
        {

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {

                    BlockTable acBlkTbl = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForWrite);

                    // Open the Block table record Model space for read
                    BlockTableRecord acBlkTblRec = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    clsCharacterPacman clsCharacterPacman = new clsCharacterPacman();

                    Circle acCircle = clsCharacterPacman.AddCircle(acTrans, acBlkTblRec);
                    acCircle = acTrans.GetObject(acCircle.ObjectId, OpenMode.ForWrite) as Circle;
                    acCircle.MoveEntityXY(pt.X, pt.Y);

                    clsCommon.GameObjectId.lstObjCircle.Add(acCircle.ObjectId);
                    acTrans.Commit();
                }
            }
        }

        internal void DeleteCircle()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    clsEntityDelete clsEntityDelete = new clsEntityDelete();

                    clsEntityDelete.DeleteObjectId(acTrans, acDb, clsCommon.GameObjectId.lstObjCircle);

                    acTrans.Commit();
                }
            }
        }
    }
}
