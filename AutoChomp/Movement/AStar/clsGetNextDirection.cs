using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp.Movement.AStar
{
    internal class clsGetNextDirection
    {
        internal Direction GetNextDirection(GameGhost Ghost, List<Position> lstPos)
        {
            Direction rtnValue = Ghost.Direction;

            Position posCurrent = Ghost.posCurrent;

            for (int i = 0; i < lstPos.Count; i++)
            {
                Position posNext = lstPos[i];

                rtnValue = GetNextDirection(posCurrent, posNext);

                if (rtnValue != Direction.None)
                {
                    Ghost.Direction = rtnValue;
                }
                else
                {
                    // Random
                }

                break;
            }  

            return rtnValue;
        }

        internal Direction GetNextDirection(Position posCurrent, Position posNext)
        {
            if (posNext.X > posCurrent.X)
                return Direction.Right;

            if (posNext.X < posCurrent.X)
                return Direction.Left;

            if (posNext.Y > posCurrent.Y)
                return Direction.Up;

            if (posNext.X < posCurrent.X)
                return Direction.Down;

            return Direction.None;

        }
    }
}
