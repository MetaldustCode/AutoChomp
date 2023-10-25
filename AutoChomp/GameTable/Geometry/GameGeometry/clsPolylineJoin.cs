using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsPolylineJoin
    {
        /// <summary>
        /// Gets a layerName and tries to join all polylines on the given layer
        /// sends back a little log message to display
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        internal List<Polyline> JoinPolyline(Transaction acTrans, List<Polyline> lstPline)
        {
            List<Polyline> rtnValue = new List<Polyline>();

            //Editor acEd = acDoc.Editor;

            //LayerTable layerTable = (LayerTable)acTrans.GetObject(acDb.LayerTableId, OpenMode.ForRead);

            // create a list of the entities
            List<GamePolyline> entities = FillListOfEntities(lstPline, acTrans);

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    try
                    {
                        // check if start/endpoints are the same
                        // if they are join them and reset the loops and start again
                        if ((entities[i].StartPoint == entities[j].StartPoint) ||
                            (entities[i].StartPoint == entities[j].EndPoint) ||
                            (entities[i].EndPoint == entities[j].StartPoint) ||
                            (entities[i].EndPoint == entities[j].EndPoint))
                        {
                            Entity srcPLine = entities[i].Ent;
                            Entity addPLine = entities[j].Ent;

                            // join both entities
                            srcPLine.UpgradeOpen();
                            srcPLine.JoinEntity(addPLine);

                            // delete the joined entity
                            addPLine.UpgradeOpen();
                            entities.RemoveAt(j);
                            addPLine.Erase();

                            // set new start and end point of the joined polyline
                            entities[i - 1] = new GamePolyline(srcPLine, GetStartPointData(srcPLine), GetEndPointData(srcPLine));

                            // reset i to the start (as it has changed)
                            i = entities.Count;
                            j = 0;
                        }
                    }
                    catch (System.Exception)
                    {
                        // ed.WriteMessage("\nError: n{0}", ex.Message);
                    }
                }
            }

            for (int i = 0; i < entities.Count; i++)
                rtnValue.Add((Polyline)entities[i].Ent);

            return rtnValue;
        }

        /// <summary>
        /// Function to fill the entities list with a give TypedValue
        /// </summary>
        /// <param name="tvs"></param>
        /// <param name="tr"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        private static List<GamePolyline> FillListOfEntities(List<Polyline> lstPline, Transaction tr)
        {
            // declare a list and fill it with all elements from our selectionfilter
            List<GamePolyline> entities = new List<GamePolyline>();

            foreach (Polyline acPline in lstPline)
            {
                Entity ent = tr.GetObject(acPline.ObjectId, OpenMode.ForRead) as Entity;
                entities.Add(new GamePolyline(ent, GetStartPointData(ent), GetEndPointData(ent)));
            }

            return entities;
        }

        /// <summary>
        /// Function to get the startpoint coordinates of a polyline
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Point3d GetStartPointData(Entity obj)
        {
            // If a "lightweight" (or optimized) polyline
            Polyline acPline = obj as Polyline;
            if (acPline != null)
            {
                return new Point3d(acPline.GetPoint2dAt(0).X, acPline.GetPoint2dAt(0).Y, acPline.Elevation);
            }
            else
            {
                // If an old-style, 2D polyline
                Polyline2d p2d = obj as Polyline2d;
                if (p2d != null)
                {
                    return new Point3d(p2d.StartPoint.X, p2d.StartPoint.Y, p2d.Elevation);
                }
                else
                {
                    // If an old-style, 3D polyline
                    Polyline3d p3d = obj as Polyline3d;
                    if (p3d != null)
                    {
                        return p3d.StartPoint;
                    }
                }
            }
            return new Point3d(0, 0, 0);
        }

        /// <summary>
        /// Function to get the endpoint coordinates of a polyline
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Point3d GetEndPointData(Entity obj)
        {
            // If a "lightweight" (or optimized) polyline
            Polyline acPline = obj as Polyline;
            if (acPline != null)
            {
                return new Point3d(acPline.GetPoint2dAt(acPline.NumberOfVertices - 1).X, acPline.GetPoint2dAt(acPline.NumberOfVertices - 1).Y, acPline.Elevation);
            }
            else
            {
                // If an old-style, 2D polyline
                Polyline2d p2d = obj as Polyline2d;
                if (p2d != null)
                {
                    return new Point3d(p2d.EndPoint.X, p2d.EndPoint.Y, p2d.Elevation);
                }
                else
                {
                    // If an old-style, 3D polyline
                    Polyline3d p3d = obj as Polyline3d;
                    if (p3d != null)
                    {
                        return p3d.EndPoint;
                    }
                }
            }
            return new Point3d(0, 0, 0);
        }
    }
}