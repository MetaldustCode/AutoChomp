using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsElements
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        // ---------------------------------------

        internal List<Point2d> GetTop(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r) + Cell;
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetBottom(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r);
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetRight(int r, int c)
        {
            double SX = (Cell * c) + Cell;
            double SY = (Cell * r);
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetLeft(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r);
            double EX = (Cell * c);
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleLB(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r);
            double EX = (Cell * c);
            double EY = (Cell * r) + (Cell / 2);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleRB(int r, int c)
        {
            double SX = (Cell * c) + Cell;
            double SY = (Cell * r);
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r) + (Cell / 2);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleLT(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r) + (Cell / 2);
            double EX = (Cell * c);
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleRT(int r, int c)
        {
            double SX = (Cell * c) + Cell;
            double SY = (Cell * r) + (Cell / 2);
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetHorizontal(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r) + Middle;
            double EX = SX + Cell;
            double EY = SY;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetOpeningTop(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r) + (Middle - (Cell / 10));
            double EX = SX + Cell;
            double EY = SY;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetOpeningBottom(int r, int c)
        {
            double SX = (Cell * c);
            double SY = (Cell * r) + (Cell / 10);
            double EX = SX + Cell;
            double EY = SY;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetVertical(int r, int c)
        {
            double SX = (Cell * c) + Middle;
            double SY = (Cell * r);
            double EX = SX;
            double EY = SY + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMidToRight(int r, int c)
        {
            double SX = (Cell * c) + Middle;
            double SY = (Cell * r) + Middle;
            double EX = (Cell * c) + Cell;
            double EY = (Cell * r) + Middle;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMidToLeft(int r, int c)
        {
            double SX = (Cell * c) + Middle;
            double SY = (Cell * r) + Middle;
            double EX = (Cell * c);
            double EY = (Cell * r) + Middle;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMidToTop(int r, int c)
        {
            double SX = (Cell * c) + Middle;
            double SY = (Cell * r) + Middle;
            double EX = (Cell * c) + Middle;
            double EY = (Cell * r) + Cell;

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMidToBottom(int r, int c)
        {
            double SX = (Cell * c) + Middle;
            double SY = (Cell * r) + Middle;
            double EX = (Cell * c) + Middle;
            double EY = (Cell * r);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                new Point2d(SX, SY),
                new Point2d(EX, EY)
            };

            return rtnvalue;
        }

        // ---------------------------------------------

        internal List<Point2d> GetMiddleTL(int r, int c)
        {
            List<Point2d> lstRight = GetMidToRight(r, c);
            List<Point2d> lstBottom = GetMidToBottom(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstBottom[1],
                lstBottom[0],
                lstRight[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleTR(int r, int c)
        {
            List<Point2d> lstLeft = GetMidToLeft(r, c);
            List<Point2d> lstBottom = GetMidToBottom(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstLeft[1],
                lstLeft[0],
                lstBottom[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleBL(int r, int c)
        {
            List<Point2d> lstRight = GetMidToRight(r, c);
            List<Point2d> lstTop = GetMidToTop(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstRight[1],
                lstRight[0],
                lstTop[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleBR(int r, int c)
        {
            List<Point2d> lstLeft = GetMidToLeft(r, c);
            List<Point2d> lstTop = GetMidToTop(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstTop[1],
                lstLeft[0],
                lstLeft[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetCornerTL(int r, int c)
        {
            List<Point2d> lstLeft = GetLeft(r, c);
            List<Point2d> lstTop = GetTop(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstLeft[0],
                lstLeft[1],
                lstTop[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetCornerTR(int r, int c)
        {
            List<Point2d> lstRight = GetRight(r, c);
            List<Point2d> lstTop = GetTop(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstTop[0],
                lstRight[1],
                lstRight[0]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetCornerBL(int r, int c)
        {
            List<Point2d> lstLeft = GetLeft(r, c);
            List<Point2d> lstBottom = GetBottom(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstBottom[1],
                lstBottom[0],

                lstLeft[1]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetCornerBR(int r, int c)
        {
            List<Point2d> lstRight = GetRight(r, c);
            List<Point2d> lstBottom = GetBottom(r, c);

            List<Point2d> rtnvalue = new List<Point2d>
            {
                lstRight[1],
                lstRight[0],
                lstBottom[0]
            };

            return rtnvalue;
        }

        internal List<Point2d> GetMiddleCapLeft(int r, int c)
        {
            double X1 = (Cell * c);
            double Y1 = (Cell * r);
            double X2 = (Cell * c);
            double Y2 = (Cell * r) + Middle;
            double X3 = (Cell * c) + Middle;
            double Y3 = (Cell * r) + Middle;
            double X4 = (Cell * c) + Middle;
            double Y4 = (Cell * r);

            List<Point2d> rtnValue = new List<Point2d>()
            {
                new Point2d(X1, Y1),
                new Point2d(X2, Y2),
                new Point2d(X3, Y3),
                new Point2d(X4, Y4)
            };

            return rtnValue;
        }

        internal List<Point2d> GetMiddleCapRight(int r, int c)
        {
            double X1 = (Cell * c) + Middle;
            double Y1 = (Cell * r);
            double X2 = (Cell * c) + Middle;
            double Y2 = (Cell * r) + Middle;
            double X3 = (Cell * c) + Cell;
            double Y3 = (Cell * r) + Middle;
            double X4 = (Cell * c) + Cell;
            double Y4 = (Cell * r);

            List<Point2d> rtnValue = new List<Point2d>()
            {
                new Point2d(X1, Y1),
                new Point2d(X2, Y2),
                new Point2d(X3, Y3),
                new Point2d(X4, Y4)
            };

            return rtnValue;
        }
    }
}