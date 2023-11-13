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
        internal Point2d ptOrigin;
        internal Direction FacingDirection;
        //internal Position ptPosition;
        internal int intMouth;

        // Movement


        internal Boolean bolGraphicsRequired;

        internal Boolean bolCellChanged;

        internal int[,] arrAStar;
        internal List<Position> lstAStarPosition;

        internal GameLoop GameLoop;

        internal InputMode InputMode;
        internal Boolean bolSearchMode;

        // Chase Mode
        internal int[,] arrCurrentAStar;
        internal Position CurrentGhostPosition;
        internal Position CurrentPacmanPosition;
        internal GameGhost GhostCurrentChase;
        internal List<Position> lstCurrentChase;

        // Set Cell Position
        internal Boolean bolResetOrigin;
        internal Direction dirReset;
        internal Point2d ptResetOrigin;

        internal Boolean Reset_Update;
        internal Direction Reset_Direction;
        internal Point2d Reset_ptOrigin;
        internal Direction Reset_FacingDirection;


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

            this.ptOrigin = new Point2d(0, 0);
            this.intMouth = intColorIndex;

            // Movement
            this.FacingDirection = Direction.None;

            this.bolGraphicsRequired = true;

            //this.ptPosition = new Position(0, 0);
            this.bolCellChanged = false;

            this.GameLoop = new GameLoop();

            this.arrAStar = new int[0, 0];

            this.lstAStarPosition = new List<Position>();

            this.InputMode = InputMode.None;

            this.bolSearchMode = false;
            this.arrCurrentAStar = new int[0, 0];
            this.GhostCurrentChase = null;
            this.lstCurrentChase = new List<Position>();
            this.CurrentPacmanPosition = new Position(0, 0);
            this.CurrentGhostPosition = new Position(0, 0);

            this.Reset_Update = false;
            this.Reset_Direction = Direction.None;
            this.Reset_ptOrigin = new Point2d();
            this.Reset_FacingDirection   = Direction.None;
        }
    }
}