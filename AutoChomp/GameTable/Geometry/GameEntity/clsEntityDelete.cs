using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsEntityDelete
    {
        internal void DeleteElements(Transaction acTrans, Database acDb)
        {
            GameObjectId gameElements = clsCommon.GameObjectId;
            if (gameElements == null) return;

            DeleteObjectId(acTrans, acDb, gameElements.lstObjPacman);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjGhosts);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjMaze);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjDots);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjPower);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjBoxes);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjDirectionBoxes);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjHistoryBoxes);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjAStar);
            DeleteObjectId(acTrans, acDb, gameElements.lstObjPosition);

            //  clsCommon.bolDirectionBoxUpdate = false;
            //  clsCommon.GamePacman.GameLoop.bolBoxDirectionUpdate = false;
            //DeleteObjectId(acTrans, acDb, clsCommon.GamePacman.lstCellBlkRef);

            //if (clsCommon.GamePacman.lstCellOrigin != null)
            //    clsCommon.GamePacman.lstCellOrigin.Clear();

            gameElements.lstObjPacman.Clear();
            gameElements.lstObjGhosts.Clear();
            gameElements.lstObjMaze.Clear();
            gameElements.lstObjDots.Clear();
            gameElements.lstObjPower.Clear();
            gameElements.lstObjBoxes.Clear();
            gameElements.lstObjHistoryBoxes.Clear();

            clsCommon.GameObjectId = gameElements;
        }

        //internal void DeleteObjectId(Transaction acTrans, Database acDb, List<BlockReference> lstObjectId)
        //{
        //    if (lstObjectId != null)
        //    {
        //        for (int i = 0; i < lstObjectId.Count; i++)
        //            DeleteObjectId(acTrans, acDb, lstObjectId[i].ObjectId);
        //    }
        //}

        internal void DeleteObjectId(Transaction acTrans, Database acDb, List<ObjectId> lstObjectId)
        {
            if (lstObjectId != null)
            {
                for (int i = 0; i < lstObjectId.Count; i++)
                    DeleteObjectId(acTrans, acDb, lstObjectId[i]);
            }
        }

        internal void DeleteObjectId(Transaction acTrans, Database acDb, List<DBText> lstObjectId)
        {
            if (lstObjectId != null)
            {
                for (int i = 0; i < lstObjectId.Count; i++)
                    DeleteObjectId(acTrans, acDb, lstObjectId[i].ObjectId);
            }
        }

        internal void DeleteObjectId(Transaction acTrans, Database acDb, ObjectId ObjectId)
        {
            try
            {
                if (ObjectId.IsObjectIdValid(acDb))
                {
                    Entity acEntity = acTrans.GetObject(ObjectId, OpenMode.ForWrite) as Entity;

                    acEntity?.Erase();
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}