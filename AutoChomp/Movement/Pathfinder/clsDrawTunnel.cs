using AutoChomp;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;
using System.Windows.Documents;

namespace AutoPAC
{
    internal class clsDrawTunnel
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void DrawTunnel(Transaction acTrans, Database acDb)
        {
            bool[,] arrXGrid = clsClassTables.arrXTunnel;

            for (int x = 0; x < arrXGrid.GetLength(0); x++)
            {
                for (int y = 0; y < arrXGrid.GetLength(1); y++)
                {
                    if (arrXGrid[x, y])
                    {
                        Point3d pt3Mid = new Point3d(x * Cell + Middle, y * Cell + Middle, 0);

                        clsCircle clsCircle = new clsCircle();
                        clsCircle.AddCircle(acTrans, acDb, pt3Mid, 3, 30);
                    }
                }
            }
        }

        internal List<Point2d> GetMidPoint()
        {
            List<Point2d> rtnValue = new List<Point2d>();

            bool[,] arrXGrid = clsClassTables.arrXGridPath;

            for (int x = 0; x < arrXGrid.GetLength(0); x++)
            {
                for (int y = 0; y < arrXGrid.GetLength(1); y++)
                {
                    if (arrXGrid[x, y])
                    {
                        Point3d pt3Mid = new Point3d(x * Cell + Middle, y * Cell + Middle, 0);

                        rtnValue.Add(pt3Mid.ToPoint2d());
                    }
                }
            }

            return rtnValue;
        }


    }
}