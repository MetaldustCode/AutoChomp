using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GameGhost
    {
        internal string strName;

        internal List<string> lstBlockNameStandard;
        internal List<string> lstBlockNameAlternate;
        internal List<string> lstBlockNameAfraid;
        internal List<string> lstBlockNameDead;

        internal List<BlockReference> lstStandard;
        internal List<BlockReference> lstAlternate;
        internal List<BlockReference> lstAfraid;
        internal List<BlockReference> lstDead;

        internal Direction Direction;
        internal Squiggle Squiggle;
        internal Point2d ptOrigin;
        internal Position ptPosition;
        internal GhostColor Color;
        internal StartLocation StartLocation;
        internal GhostState GhostState;
        internal int intColorIndex;

        //internal double dblFrameDelay;
        //internal double dblSpeed;

        internal Boolean bolUpdateHistory;
        internal HouseState HouseState;
        //internal Boolean bolExitHouse;

        internal Direction[,] arrHistory;

        internal Boolean bolCellChanged;

        internal int[,] arrAStarGrid;
        internal List<Position> lstAStarPosition;
        internal List<int> lstAStarNumber;
        internal Boolean bolAStarShowLine;

        internal Boolean bolIsEaten;

        // Form Values
        internal InputMode InputMode;

        //Red
        //Pink
        //Blue
        //Orange

        internal GameGhost(string strName, string strBlockName, int intColorIndex, StartLocation StartLocation)
        {
            this.lstBlockNameStandard = new List<string>()
            {
                strBlockName + "_SU",
                strBlockName + "_SD",
                strBlockName + "_SL",
                strBlockName + "_SR"
            };

            this.lstBlockNameAlternate = new List<string>()
            {
                strBlockName + "_AU",
                strBlockName + "_AD",
                strBlockName + "_AL",
                strBlockName + "_AR"
            };

            this.lstBlockNameAfraid = new List<string>()
            {
                strBlockName + "_SB",
                strBlockName + "_SW",
                strBlockName + "_AB",
                strBlockName + "_AW"
            };

            this.lstBlockNameDead = new List<string>()
            {
                strBlockName + "_DU",
                strBlockName + "_DD",
                strBlockName + "_DL",
                strBlockName + "_DR"
            };

            this.lstStandard = new List<BlockReference>();
            this.lstAlternate = new List<BlockReference>();
            this.lstAfraid = new List<BlockReference>();
            this.lstDead = new List<BlockReference>();

            this.Direction = Direction.None;
            this.Squiggle = Squiggle.Standard;
            this.ptOrigin = new Point2d(0, 0);
            this.Color = GhostColor.Default;
            this.StartLocation = StartLocation;

            this.GhostState = GhostState.Alive;
            this.intColorIndex = intColorIndex;

            // History
            this.bolUpdateHistory = false;
            this.HouseState = HouseState.InHouse;
            //this.bolExitHouse = false;

            clsGenerateTables clsGenerateTables = new clsGenerateTables();
            clsGenerateTables.GetSize(out int col, out int row);
            this.arrHistory = new Direction[col, row];

            this.strName = strName;

            this.ptPosition = new Position(0, 0);

            this.bolCellChanged = false;

            this.arrAStarGrid = new int[0, 0];
            this.lstAStarPosition = new List<Position>();
            this.lstAStarNumber = new List<int>();

            this.InputMode = InputMode.None;
            this.bolAStarShowLine = false;
            this.bolIsEaten = false;
        }
    }
}