using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Data
{
    internal class clsDataPacmanMove
    {
        public void SetDataPacmanMove()
        {
            clsGetDirection clsGetDirection = new clsGetDirection();

            GamePacman Pacman = clsCommon.GamePacman;
            clsInput clsInput = new clsInput();

            clsReg clsReg = new clsReg();

            string strValue = clsReg.GetPacmanInputMode();

            Direction direction = Pacman.Direction;

            if (Pacman.Reset_Update)
            {
                Pacman.ptOrigin = Pacman.Reset_ptOrigin;
                Pacman.Direction = Pacman.Reset_Direction;
                Pacman.FacingDirection = Pacman.Reset_FacingDirection;
                Pacman.Reset_Update = false;
            }

            if (strValue == "Keyboard")
            {
                clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();

                clsCalcGlobalAStar.GetNextDirection(ref direction);

                direction = clsInput.GetDirection();

                if (direction != Direction.None)
                {
                    clsGluttony clsGluttony = new clsGluttony();
                    clsGluttony.GetGluttony(Pacman.ptOrigin, Pacman.Direction,
                                            ref Pacman.GameLoop.arrHistory,
                                            ref Pacman.GameLoop.bolHistoryUpdate);
                }
            }

            if (strValue == "Gluttony")
            {
                clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();

                if (!clsCalcGlobalAStar.GetNextDirection(ref direction))
                {
                    clsGluttony clsGluttony = new clsGluttony();
                    direction = clsGluttony.GetGluttony(Pacman.ptOrigin, Pacman.Direction,
                                                        ref Pacman.GameLoop.arrHistory,
                                                        ref Pacman.GameLoop.bolHistoryUpdate);
                }
            }

            if (strValue == "Random")
            {
                direction = clsGetDirection.GetRandomDirection(Pacman.ptOrigin, Pacman.Direction, 6);

                clsGluttony clsGluttony = new clsGluttony();
                clsGluttony.GetGluttony(Pacman.ptOrigin, Pacman.Direction,
                                        ref Pacman.GameLoop.arrHistory,
                                        ref Pacman.GameLoop.bolHistoryUpdate);
            }

            if (strValue == "A-Star")
            {
            }

            List<Position> lstNextCell = new List<Position>();

            if (direction != Direction.None)
            {
                if (clsGetDirection.CanCharacterMove(Pacman.ptOrigin, direction, ref lstNextCell))
                {
                    UpdatePacmanPosition(ref Pacman, direction);
                    Pacman.FacingDirection = direction;
                    Pacman.Direction = direction;
                    clsCommon.GamePosition.posCurrentPacman = Pacman.ptOrigin;
                    Pacman.bolGraphicsRequired = true;
                }
                else
                {
                    if (clsGetDirection.CanCharacterMove(Pacman.ptOrigin, Pacman.FacingDirection, ref lstNextCell))
                    {
                        UpdatePacmanPosition(ref Pacman, Pacman.FacingDirection);
                        clsCommon.GamePosition.posCurrentPacman = Pacman.ptOrigin;
                        Pacman.bolGraphicsRequired = true;
                    }
                }
            }
            else
            {
                if (clsGetDirection.CanCharacterMove(Pacman.ptOrigin, Pacman.Direction, ref lstNextCell))
                {
                    UpdatePacmanPosition(ref Pacman, Pacman.Direction);
                    clsCommon.GamePosition.posCurrentPacman = Pacman.ptOrigin;
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
        }


        internal Boolean IsAtGrid(GamePacman Pacman)
        {
            Boolean[,] arrXDots = clsClassTables.arrXDots;
            arrXDots.GetSize(out int col, out int row);

            List<String> lstXGridOriginString = clsClassTables.lstXGridOriginString;

            if (col > 0 && lstXGridOriginString.Count > 0)
            {
                string strValue = String.Format("{0},{1}", Pacman.ptOrigin.X, Pacman.ptOrigin.Y);
                if (lstXGridOriginString.Contains(strValue))
                    return true;
            }

            return false;
        }

    }
}