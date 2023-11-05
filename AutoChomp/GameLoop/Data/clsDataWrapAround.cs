using Autodesk.AutoCAD.Geometry;
using System;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataWrapAround
    {
        private readonly double Cell = clsGridValues.Cell;

        internal void WrapCharacterPacman(ref GamePacman Pacman)
        {
            Point2d ptPosition = Pacman.ptOrigin;

            UpdatePosition(ref ptPosition);

            Pacman.ptOrigin = ptPosition;
        }

        internal void WrapCharacterGhost(ref GameGhost Ghost)
        {
            Point2d ptPosition = Ghost.ptOrigin;

            UpdatePosition(ref ptPosition);

            Ghost.ptOrigin = ptPosition;
        }

        internal void UpdatePosition(ref Point2d ptPosition)
        {
            clsGetGrid clsGetGrid = new clsGetGrid();
            double dblWidth = 0; double dblHeight = 0;
            clsGetGrid.GetGridSize(ref dblWidth, ref dblHeight);

            if (ptPosition.X < 0 + Cell)
                ptPosition = new Point2d(dblWidth - Cell, ptPosition.Y);

            if (ptPosition.X > dblWidth - Cell)
                ptPosition = new Point2d(0 + Cell, ptPosition.Y);

            if (ptPosition.Y < 0 + Cell)
                ptPosition = new Point2d(ptPosition.X, dblHeight - Cell);

            if (ptPosition.Y > dblHeight - Cell)
                ptPosition = new Point2d(ptPosition.X, 0 + Cell);
        }

        public void SetNextGhostPosition(ref GameGhost Ghost, Direction direction)
        {
            clsDataWrapAround clsWrapAround = new clsDataWrapAround();
            clsWrapAround.WrapCharacterGhost(ref Ghost);

            Point2d ptOrigin = Ghost.ptOrigin;

            double dblOffset = Convert.ToDouble(clsCommon.GameForm.cboSpacing.Text);

            if (direction == Direction.Up)
                ptOrigin = new Point2d(ptOrigin.X, ptOrigin.Y + dblOffset);

            if (direction == Direction.Down)
                ptOrigin = new Point2d(ptOrigin.X, ptOrigin.Y - dblOffset);

            if (direction == Direction.Left)
                ptOrigin = new Point2d(ptOrigin.X - dblOffset, ptOrigin.Y);

            if (direction == Direction.Right)
                ptOrigin = new Point2d(ptOrigin.X + dblOffset, ptOrigin.Y);

            Ghost.ptOrigin = ptOrigin;
            Ghost.Direction = direction;
        }

        public void UpdatePacmanPosition(ref GamePacman Pacman, Direction direction)
        {
            clsDataWrapAround clsWrapAround = new clsDataWrapAround();
            clsWrapAround.WrapCharacterPacman(ref Pacman);

            Point2d ptPosition = Pacman.ptOrigin;

            double dblOffset = Convert.ToDouble(clsCommon.GameForm.cboSpacing.Text);

            if (direction == Direction.Up)
                ptPosition = new Point2d(ptPosition.X, ptPosition.Y + dblOffset);

            if (direction == Direction.Down)
                ptPosition = new Point2d(ptPosition.X, ptPosition.Y - dblOffset);

            if (direction == Direction.Left)
                ptPosition = new Point2d(ptPosition.X - dblOffset, ptPosition.Y);

            if (direction == Direction.Right)
                ptPosition = new Point2d(ptPosition.X + dblOffset, ptPosition.Y);

            Pacman.ptOrigin = ptPosition;
            Pacman.Direction = direction;
        }
    }
}