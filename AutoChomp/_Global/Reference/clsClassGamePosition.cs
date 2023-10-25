using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GameLoop
    {
        internal List<Position> lstLoopPosition;
        internal List<Direction> lstLoopDirection;

        internal Boolean bolHistoryUpdate;
        internal Direction[,] arrHistory;

        internal bool bolBoxDirectionUpdate;
        internal List<Position> lstBoxDirection;

        internal bool bolBoxSuggestionUpdate;
        internal List<Position> lstBoxSuggestion;

        internal GameLoop()
        {
            this.lstLoopPosition = new List<Position>();
            this.lstLoopDirection = new List<Direction>();

            // History
            this.bolHistoryUpdate = false;
            clsGenerateTables clsGenerateTables = new clsGenerateTables();
            clsGenerateTables.GetSize(out int col, out int row);

            this.arrHistory = new Direction[col, row];

            this.bolBoxDirectionUpdate = false;
            this.lstBoxDirection = new List<Position>();

            this.bolBoxSuggestionUpdate = false;
            this.lstBoxSuggestion = new List<Position>();
        }
    }

    internal class GamePosition
    {
        internal Point2d posCurrentPacman;
        internal Point2d posCurrentRed;
        internal Point2d posCurrentPink;
        internal Point2d posCurrentBlue;
        internal Point2d posCurrentOrange;

        internal Point2d posLastPacman;
        internal Point2d posLastRed;
        internal Point2d posLastPink;
        internal Point2d posLastBlue;
        internal Point2d posLastOrange;

        internal Boolean bolUpdatePacman;
        internal Boolean bolUpdateRed;
        internal Boolean bolUpdatePink;
        internal Boolean bolUpdateBlue;
        internal Boolean bolUpdateOrange;

        internal Boolean bolUpdateDataGridPacman;
        internal Boolean bolUpdateDataDotsPacman;

        internal Boolean bolUpdateGraphicsGridPacman;
        internal Boolean bolUpdateGraphicsDotsPacman;

        internal Boolean bolUpdateGridCleared;

        internal GamePosition()
        {
            this.posCurrentPacman = new Point2d();
            this.posCurrentRed = new Point2d();
            this.posCurrentPink = new Point2d();
            this.posCurrentBlue = new Point2d();
            this.posCurrentOrange = new Point2d();

            this.posLastPacman = new Point2d();
            this.posLastRed = new Point2d();
            this.posLastPink = new Point2d();
            this.posLastBlue = new Point2d();
            this.posLastOrange = new Point2d();

            this.bolUpdatePacman = false;
            this.bolUpdateRed = false;
            this.bolUpdatePink = false;
            this.bolUpdateBlue = false;
            this.bolUpdateOrange = false;

            this.bolUpdateDataGridPacman = false;
            this.bolUpdateDataDotsPacman = false;
            this.bolUpdateGraphicsGridPacman = false;
            this.bolUpdateGraphicsDotsPacman = false;
            this.bolUpdateGridCleared = false;
        }
    }
}