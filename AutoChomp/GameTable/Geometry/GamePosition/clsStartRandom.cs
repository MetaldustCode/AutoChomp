using Autodesk.AutoCAD.Geometry;

namespace AutoChomp
{
    internal class clsStartRandom
    {
        // Create Start Point in Cell, Exclude the Tunnels.
        internal void GetRandomPosition(out Position pos, out Point2d pt)
        {
            int index = clsRandomizer.RandomInteger(0, clsClassTables.lstXStartupPosition.Count - 1);

            pos = clsClassTables.lstXStartupPosition[index];
            pt = clsClassTables.lstXStartupOrigin[index];
        }
    }
}