using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsHatch
    {
        internal List<Hatch> FillInPolyline(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                            List<ObjectId> lstPolyline,
                                            string strPattern, double dblScale)
        {
            List<Hatch> rtnValue = new List<Hatch>();

            for (int i = 0; i < lstPolyline.Count; i++)
            {
                ObjectId objid = lstPolyline[i];

                if (objid.IsObjectIdValid(acDb))
                {
                    Entity acEntity = acTrans.GetObject(lstPolyline[i], OpenMode.ForWrite) as Entity;

                    if (acEntity is Polyline acPline)
                    {
                        if (acPline.Closed)
                            rtnValue.Add(ProcessHatch(acTrans, acDb, acBlkTblRec, strPattern, dblScale, acEntity));
                    }
                    else
                        rtnValue.Add(ProcessHatch(acTrans, acDb, acBlkTblRec, strPattern, dblScale, acEntity));
                }
            }

            return rtnValue;
        }

        internal List<Hatch> FillInMaskPolyline(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                         List<ObjectId> lstPolyline,
                                         string strPattern, double dblScale)
        {
            List<Hatch> rtnValue = new List<Hatch>();

            ObjectIdCollection lstObjectIdColl = new ObjectIdCollection(); ;

            for (int i = 0; i < lstPolyline.Count; i++)
            {
                ObjectId objid = lstPolyline[i];

                if (objid.IsObjectIdValid(acDb))
                {
                    Entity acEntity = acTrans.GetObject(lstPolyline[i], OpenMode.ForWrite) as Entity;

                    lstObjectIdColl.Add(acEntity.ObjectId);
                }
            }

            rtnValue.Add(ProcessHatch(acTrans, acDb, acBlkTblRec, strPattern, dblScale, lstObjectIdColl));

            return rtnValue;
        }

        internal Hatch ProcessHatch(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                    string strPattern, double dblScale, Entity acEntity)
        {
            ObjectIdCollection objIdColl = new ObjectIdCollection();

            ObjectId objId = acEntity.ObjectId;

            Hatch acHatch = new Hatch();

            if (acEntity.IsObjectIdValid(acDb))
            {
                objIdColl.Add(objId);

                try
                {
                    acHatch.PatternScale = dblScale;
                    acHatch.ColorIndex = acEntity.ColorIndex;
                    acBlkTblRec.AppendEntity(acHatch);
                    acTrans.AddNewlyCreatedDBObject(acHatch, true);

                    acHatch.SetHatchPattern(HatchPatternType.PreDefined, strPattern);
                    acHatch.Associative = true;
                    acHatch.AppendLoop(HatchLoopTypes.External, objIdColl);
                    acHatch.EvaluateHatch(true);
                }
                catch (System.Exception)
                {
                    return null;
                }
            }

            return acHatch;
        }

        internal Hatch ProcessHatch(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                    string strPattern, double dblScale, ObjectIdCollection objIdColl)
        {
            if (objIdColl.Count == 2)
            {
                for (int i = 0; i < objIdColl.Count; i++)
                {
                    if (!objIdColl[i].IsObjectIdValid(acDb))
                        return null;
                }

                Hatch acHatch = new Hatch();

                try
                {
                    acHatch.PatternScale = dblScale;
                    acBlkTblRec.AppendEntity(acHatch);
                    acTrans.AddNewlyCreatedDBObject(acHatch, true);

                    acHatch.SetHatchPattern(HatchPatternType.PreDefined, strPattern);
                    acHatch.Associative = true;

                    ObjectIdCollection objIdColl1 = new ObjectIdCollection();
                    ObjectIdCollection objIdColl2 = new ObjectIdCollection();
                    for (int i = 0; i < objIdColl.Count; i++)
                    {
                        if (i == 0)
                            objIdColl1.Add(objIdColl[i]);
                        if (i == 1)
                            objIdColl2.Add(objIdColl[i]);
                    }

                    acHatch.AppendLoop(HatchLoopTypes.External, objIdColl1);
                    acHatch.AppendLoop(HatchLoopTypes.External, objIdColl2);
                    acHatch.EvaluateHatch(true);
                }
                catch (System.Exception)
                {
                    return null;
                }

                return acHatch;
            }
            return null;
        }
    }
}