using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace AutoChomp
{
    internal class clsHouse
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        // Move up and down until bolExitHouse is true
        internal Direction MoveUpandDown(GameGhost Ghost)
        {
            double YTop = (19 * Cell);
            double YBottom = (18 * Cell);

            if (Ghost.HouseState == HouseState.InHouse)
            {
                Point2d Origin = Ghost.ptOrigin;
                Direction direction = Ghost.Direction;

                if (direction == Direction.Up)
                    if (Origin.Y > YTop)
                        return Direction.Down;

                if (direction == Direction.Down)
                    if (Origin.Y < YBottom)
                        return Direction.Up;
            }

            return Ghost.Direction;
        }

        // Move up and down until bolExitHouse is true
        internal Boolean MoveDown(ref GameGhost Ghost, ref Direction direction)
        {

            double YBottom = (18 * Cell) + 50;

            if (Ghost.HouseState == HouseState.ReturnHouse)
            {
                Point2d Origin = Ghost.ptOrigin;

                if (Origin.Y > YBottom)
                    direction = Direction.Down;
                else
                {
                    direction = Direction.None;
                    return true;
                }
            }

            return false;
        }

        // Move up and down until bolExitHouse is true
        internal Boolean MoveUp(ref GameGhost Ghost, ref Direction direction)
        {

            double YTop = (21 * Cell) +50;

            if (Ghost.HouseState == HouseState.LeaveHouse)
            {
                Point2d Origin = Ghost.ptOrigin;

                if (Origin.Y < YTop)
                    direction = Direction.Up;
                else
                {
                    direction = Direction.None;
                    return true;
                }
            }

            return false;
        }



        // Move up and down until bolExitHouse is true
        internal Boolean MoveRight(ref GameGhost Ghost, ref Direction direction)
        {
            Point2d pt = new Point2d((18 * Cell), (21 * Cell) + Middle);

            if (Ghost.HouseState == HouseState.ReturnHouse)
            {
                Point2d Origin = Ghost.ptOrigin;


                if (Origin.X < pt.X)
                    direction = Direction.Right;
                else
                {
                    direction = Direction.None;
                    return true;
                }
            }


            return false;
        }

        // Move up and down until bolExitHouse is true
        internal Boolean MoveLeft(ref GameGhost Ghost, ref Direction direction)
        {

            Point2d pt = new Point2d((14 * Cell), (21 * Cell) + Middle);

            if (Ghost.HouseState == HouseState.ReturnHouse)
            {
                Point2d Origin = Ghost.ptOrigin;
                direction = Ghost.Direction;

                if (Origin.X > pt.X)
                    direction = Direction.Left;
                else
                {
                    direction = Direction.None;
                    return true;
                }
            }

            return false;
        }



        internal Boolean MoveMiddle(ref GameGhost Ghost, ref Direction direction)
        {
            double X = (16 * Cell);

            Point2d Origin = Ghost.ptOrigin;

            // Check if to the ghost has escaped
            Boolean bolCheck = false;
            // Move towards the center
            if (Origin.X < X)
            {
                direction = Direction.Right;
                bolCheck = true;
            }
            if (Origin.X > X)
            {
                direction = Direction.Left;
                bolCheck = true;
            }

            if (!bolCheck)
            {
                direction = Direction.None;
                return true;
            }
            return false;
        }








        internal Direction ExitHouse(GameGhost Ghost, out bool bolEscaped)
        {
            bolEscaped = false;
            double Y = (21 * Cell) + Middle;
            double X = (16 * Cell);

            Point2d Origin = Ghost.ptOrigin;

            // Check if to the ghost has escaped
            if (Origin.Y < Y)
            {
                // Move towards the center
                if (Origin.X < X)
                    return Direction.Right;

                if (Origin.X > X)
                    return Direction.Left;
            }
            else
            {
                // Set escaped and pick direction
                Ghost.HouseState = HouseState.OutHouse;

                bolEscaped = true;
                return Direction.None;
            }

            // move up towards the exit
            return Direction.Up;
        }

        // Set the exit order of the ghosts
        internal List<GameGhost> ReOrder(List<GameGhost> lstGhost)
        {
            // Get ghost list to exit
            List<String> lstExit = new List<String>() { "Red", "Pink", "Blue", "Orange" };
            lstExit.Reverse();

            List<GameGhost> lstExitOrder = new List<GameGhost>();

            // Add Ghost to list
            for (int i = 0; i < lstExit.Count; i++)
            {
                for (int k = 0; k < lstGhost.Count; k++)
                {
                    if (lstGhost[k].strName == lstExit[i])
                        lstExitOrder.Add(lstGhost[k]);
                }
            }

            return lstExitOrder.ToList();
        }
    }
}