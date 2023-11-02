using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp.Data
{
    internal class clsDataPacmanMove
    {
        public void SetDataPacmanMove()
        {
            clsGetDirection clsGetDirection = new clsGetDirection();

            GamePacman Pacman = clsCommon.GamePacman;
            clsInput clsInput = new clsInput();

            clsReg clsReg = new clsReg();

            string strValue = clsReg.GetPacmanSearchMode();

            Direction direction = Pacman.Direction;

            if (strValue == "Keyboard")
            {
                direction = clsInput.GetDirection();

                //if (direction != Direction.None)
                {
                    clsGluttony clsGluttony = new clsGluttony();
                    clsGluttony.GetGluttony(Pacman.Origin, Pacman.Direction,
                                            ref Pacman.GameLoop.arrHistory,
                                            ref Pacman.GameLoop.bolHistoryUpdate);
                }
            }

            if (strValue == "Gluttony")
            {
                clsGluttony clsGluttony = new clsGluttony();
                direction = clsGluttony.GetGluttony(Pacman.Origin, Pacman.Direction,
                                                    ref Pacman.GameLoop.arrHistory,
                                                    ref Pacman.GameLoop.bolHistoryUpdate);
            }

            if (strValue == "Random")
            {
                direction = clsGetDirection.GetRandomDirection(Pacman.Origin, Pacman.Direction, 6);

                clsGluttony clsGluttony = new clsGluttony();
                clsGluttony.GetGluttony(Pacman.Origin, Pacman.Direction,
                                        ref Pacman.GameLoop.arrHistory,
                                        ref Pacman.GameLoop.bolHistoryUpdate);
            }

            List<Position> lstNextCell = new List<Position>();

            if (direction != Direction.None)
            {
                if (clsGetDirection.CanCharacterMove(Pacman.Origin, direction, ref lstNextCell))
                {
                    UpdatePacmanPosition(ref Pacman, direction);
                    Pacman.FacingDirection = direction;
                    Pacman.Direction = direction;
                    clsCommon.GamePosition.posCurrentPacman = Pacman.Origin;
                    Pacman.bolGraphicsRequired = true;
                }
                else
                {
                    if (clsGetDirection.CanCharacterMove(Pacman.Origin, Pacman.FacingDirection, ref lstNextCell))
                    {
                        UpdatePacmanPosition(ref Pacman, Pacman.FacingDirection);
                        clsCommon.GamePosition.posCurrentPacman = Pacman.Origin;
                        Pacman.bolGraphicsRequired = true;
                    }
                }
            }
            else
            {
                if (clsGetDirection.CanCharacterMove(Pacman.Origin, Pacman.Direction, ref lstNextCell))
                {
                    UpdatePacmanPosition(ref Pacman, Pacman.Direction);
                    clsCommon.GamePosition.posCurrentPacman = Pacman.Origin;
                    Pacman.bolGraphicsRequired = true;
                }
            }

            if (Pacman.bolGraphicsRequired)
            {
                //clsAStar clsAStar = new clsAStar();
                //clsAStar.UpdatePacmanAStar(ref Pacman);
            }

            clsCommon.GamePacman = Pacman;
        }

        internal void SetDataUpdateMouth()
        {
            GamePacman Pacman = clsCommon.GamePacman;
            clsDataMouth clsDataPacmanMouth = new clsDataMouth();
            if (clsDataPacmanMouth.UpdateMouth(ref Pacman))
            {
                // Update Pacman State
                clsDataPacmanMouth.UpdateMouthState();
            }
            clsCommon.GamePacman = Pacman;
        }

        public void UpdatePacmanPosition(ref GamePacman Pacman, Direction direction)
        {
            clsDataWrapAround clsWrapAround = new clsDataWrapAround();

            clsWrapAround.WrapCharacterPacman(ref Pacman);

            Point2d ptPosition = Pacman.Origin;

            double dblOffset = Convert.ToDouble(clsCommon.GameForm.cboSpacing.Text);

            if (direction == Direction.Up)
                ptPosition = new Point2d(ptPosition.X, ptPosition.Y + dblOffset);

            if (direction == Direction.Down)
                ptPosition = new Point2d(ptPosition.X, ptPosition.Y - dblOffset);

            if (direction == Direction.Left)
                ptPosition = new Point2d(ptPosition.X - dblOffset, ptPosition.Y);

            if (direction == Direction.Right)
                ptPosition = new Point2d(ptPosition.X + dblOffset, ptPosition.Y);

            Pacman.Origin = ptPosition;
        }
    }
}