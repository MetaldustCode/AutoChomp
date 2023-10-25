using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (Ghost.bolInHouse && !Ghost.bolExitHouse)
            {
                Point2d Origin = Ghost.Origin;
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

        internal Direction ExitHouse(GameGhost Ghost, out bool bolEscaped)
        {
            bolEscaped = false;
            double Y = (21 * Cell) + Middle;
            double X = (16 * Cell);

            Point2d Origin = Ghost.Origin;

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
                Ghost.bolInHouse = false;
                Ghost.bolExitHouse = false;
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