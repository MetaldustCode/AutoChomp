using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GamePacman
    {
        // GameGrid
        internal List<string> lstPacmanBlockName;

        internal List<BlockReference> lstPacmanBlockReference;

        internal List<string> lstDeathName;
        internal List<BlockReference> lstDeathBlockReference;

        internal PacmanState PacmanState;
        internal Direction Direction;
        internal Point2d Origin;
        internal int intMouth;

        // Movement
        internal Direction FacingDirection;

        internal Boolean bolGraphicsRequired;

        internal Boolean bolCellChanged;
        internal Position posCurrent;

        internal int[,] arrAStar;
        internal List<Position> lstPosition;

        internal GameLoop GameLoop;

        internal GamePacman(string strBlockName, int intColorIndex)
        {
            // GameGrid
            this.lstPacmanBlockName = new List<string>()
            {
             strBlockName + "_Close",
             strBlockName + "_Mid",
             strBlockName + "_Open"
            };

            this.lstPacmanBlockReference = new List<BlockReference>();
            this.lstDeathName = new List<string>();
            this.Direction = Direction.None;
            this.PacmanState = PacmanState.Mid;

            this.Origin = new Point2d(0, 0);
            this.intMouth = intColorIndex;

            // Movement
            this.FacingDirection = Direction.None;
            this.bolGraphicsRequired = true;

            this.posCurrent = new Position(0, 0);
            this.bolCellChanged = false;

            this.GameLoop = new GameLoop();

            this.arrAStar = new int[0, 0];

            this.lstPosition = new List<Position>();    
        }
    }
}