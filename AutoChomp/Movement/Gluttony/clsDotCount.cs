using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsDotCount
    {
        // Get next cell
        internal Position GetUp(Position pos)
        {
            return new Position(pos.X, pos.Y + 1);
        }

        internal Position GetDown(Position pos)
        {
            return new Position(pos.X, pos.Y - 1);
        }

        internal Position GetRight(Position pos)
        {
            return new Position(pos.X + 1, pos.Y);
        }

        internal Position GetLeft(Position pos)
        {
            return new Position(pos.X - 1, pos.Y);
        }

        // Get dot count in each direction
        internal int GetUpCount(Boolean[,] arrXDots, int col, int row, Position pos, out List<Position> lstPos)
        {
            lstPos = new List<Position>();
            int rtnValue = 0;

            for (int r = 0; r < row; r++)
            {
                if (r > pos.Y)
                {
                    for (int c = 0; c < col; c++)
                    {
                        if (arrXDots[c, r])
                        {
                            rtnValue++;
                            lstPos.Add(new Position(c, r));
                        }
                    }
                }
            }
            return rtnValue;
        }

        internal int GetDownCount(Boolean[,] arrXDots, int col, int row, Position pos, out List<Position> lstPos)
        {
            lstPos = new List<Position>();
            int rtnValue = 0;

            for (int r = 0; r < row; r++)
            {
                if (r < pos.Y)
                {
                    for (int c = 0; c < col; c++)
                    {
                        if (arrXDots[c, r])
                        {
                            rtnValue++;
                            lstPos.Add(new Position(c, r));
                        }
                    }
                }
            }
            return rtnValue;
        }

        internal int GetRightCount(Boolean[,] arrXDots, int col, int row, Position pos, out List<Position> lstPos)
        {
            lstPos = new List<Position>();
            int rtnValue = 0;

            for (int c = 0; c < col; c++)
            {
                if (c > pos.X)
                {
                    for (int r = 0; r < row; r++)
                    {
                        if (arrXDots[c, r])
                        {
                            rtnValue++;
                            lstPos.Add(new Position(c, r));
                        }
                    }
                }
            }
            return rtnValue;
        }

        internal int GetLeftCount(Boolean[,] arrXDots, int col, int row, Position pos, out List<Position> lstPos)
        {
            lstPos = new List<Position>();
            int rtnValue = 0;

            for (int c = 0; c < col; c++)
            {
                if (c < pos.X)
                {
                    for (int r = 0; r < row; r++)
                    {
                        if (arrXDots[c, r])
                        {
                            rtnValue++;
                            lstPos.Add(new Position(c, r));
                        }
                    }
                }
            }
            return rtnValue;
        }
    }
}