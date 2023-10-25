using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GamePower
    {
        internal List<BlockReference> lstBlockReference;
        internal List<Point2d> lstOrigin;
        internal List<Position> lstPosition;

        internal List<bool> lstIsEaten;
        internal List<bool> lstIsBlockVisible;

        internal bool bolIsFlashOn;

        internal bool[,] arrEaten;
        internal bool bolGraphicsRequired;

        internal GamePower()
        {
            this.lstBlockReference = new List<BlockReference>();
            this.lstOrigin = new List<Point2d>();
            this.lstPosition = new List<Position>();

            this.lstIsEaten = new List<bool>();
            this.bolIsFlashOn = false;

            this.lstIsBlockVisible = new List<bool>();

            this.arrEaten = null;
            this.bolGraphicsRequired = false;
        }
    }
}