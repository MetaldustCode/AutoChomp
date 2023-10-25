using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsBlock
    {
        internal BlockReference CreateBlock(Transaction acTrans, Database acDb, List<Entity> lstEntity, string strBlockname, BlockTable acBlkTbl)
        {
            if (!acBlkTbl.Has(strBlockname))
            {
                BlockTableRecord btr = new BlockTableRecord { Name = strBlockname };

                ObjectId objId = acBlkTbl.ObjectId;

                if (objId.IsObjectIdValid(acDb))
                {
                    acBlkTbl = acTrans.GetObject(acBlkTbl.ObjectId, OpenMode.ForWrite) as BlockTable;

                    ObjectId btrId = acBlkTbl.Add(btr);
                    acTrans.AddNewlyCreatedDBObject(btr, true);

                    ObjectIdCollection objIdColl = new ObjectIdCollection();

                    for (int i = 0; i < lstEntity.Count; i++)
                        objIdColl.Add(lstEntity[i].ObjectId);

                    btr.AssumeOwnershipOf(objIdColl);

                    BlockTableRecord ms = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    BlockReference br = new BlockReference(Point3d.Origin, btrId);

                    ms.AppendEntity(br);

                    acTrans.AddNewlyCreatedDBObject(br, true);

                    return br;
                }
            }

            return null;
        }
    }
}