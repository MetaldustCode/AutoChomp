using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AutoChomp
{
    internal class clsInsertBlock
    {
        internal BlockReference InsertBlock(string strBlockname, string strLayerName,
                                          Transaction acTrans, Database acDb)
        {
            BlockReference rtnValue = null;
            {
                // Open the Block table for read
                BlockTable acBlkTbl = default;

                try
                {
                    acBlkTbl = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead);
                }
                catch (System.Exception)
                {
                    return null;
                }

                ObjectId blkRecId = ObjectId.Null;
                if (acBlkTbl.Has(strBlockname))
                {
                    blkRecId = acBlkTbl[strBlockname];
                }
                // Insert the block into the current space
                if (blkRecId != ObjectId.Null)
                {
                    BlockReference acBlkRef = new BlockReference(Point3d.Origin, blkRecId) { Layer = strLayerName };
                    BlockTableRecord btr = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    btr.AppendEntity(acBlkRef);
                    acTrans.AddNewlyCreatedDBObject(acBlkRef, true);
                    rtnValue = acBlkRef;
                }
            }

            return rtnValue;
        }
    }
}