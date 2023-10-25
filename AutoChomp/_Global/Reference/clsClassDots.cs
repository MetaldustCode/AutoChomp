using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GameDots
    {
        internal List<BlockReference> lstBlockReference;

        internal List<Point2d> lstOrigin;

        internal List<bool> lstIsEaten;
        internal List<bool> lstIsBlockVisible;

        //internal bool[,] arrXGrid;
        internal bool bolGraphicsRequired;

        internal GameDots()
        {
            this.lstBlockReference = new List<BlockReference>();

            this.lstOrigin = new List<Point2d>();
            //this.lstPosition = new List<Position>();

            this.lstIsEaten = new List<bool>();
            //this.lstIsBlockVisible = new List<bool>();

            //this.arrXGrid = null;
            this.bolGraphicsRequired = false;
        }
    }
}