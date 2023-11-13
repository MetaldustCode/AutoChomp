using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;

namespace AutoChomp
{
    internal class clsStartPosition
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void GetStartPosition(ref Point2d ptRed, ref Point2d ptPink, ref Point2d ptBlue, ref Point2d ptOrange)
        {
            GetHousePosition(ref ptRed, ref ptPink,
                             ref ptBlue, ref ptOrange);

            //clsStartRandom clsStartRandom = new clsStartRandom();

            //for (int i = 0; i < 4; i++)
            //{
            //    clsStartRandom.GetRandomPosition(out Position pos, out Point2d pt);

            //    if (i == 0) ptRed = pt;
            //    if (i == 1) ptPink = pt;
            //    if (i == 2) ptBlue = pt;
            //    if (i == 3) ptOrange = pt;

            //}
            //ptPink = new Point2d((3 * Cell) + Middle, (3 * Cell) + Middle); // Outside
            //ptBlue = new Point2d((28 * Cell) + Middle, (3 * Cell) + Middle); // Outside
            //ptOrange = new Point2d((8 * Cell) + Middle, (24 * Cell) + Middle); // Outside

            //ptRed = new Point2d((16 * Cell), (21 * Cell) + Middle);

            // Outside
            //ptPink = new Point2d((14 * Cell), (18 * Cell)); // Left
            //ptBlue = new Point2d((16 * Cell), (18 * Cell)); // middle
            //ptOrange = new Point2d((18 * Cell), (18 * Cell)); // Right
        }

        internal void GetHousePosition(ref Point2d ptRed, ref Point2d ptPink,
                                       ref Point2d ptBlue, ref Point2d ptOrange)
        {
            ptBlue = new Point2d((14 * Cell), (18 * Cell) + Middle); // Left
            ptPink = new Point2d((16 * Cell), (18 * Cell) + Middle); // middle
            ptOrange = new Point2d((18 * Cell), (18 * Cell) + Middle); // Right

            ptRed = new Point2d((16 * Cell), (21 * Cell) + Middle);

            //ptBlue = new Point2d((16 * Cell), (21 * Cell) + Middle);
            //ptPink = new Point2d((16 * Cell), (21 * Cell) + Middle);
            //ptOrange = new Point2d((16 * Cell), (21 * Cell) + Middle);
            //ptRed = new Point2d((20 * Cell) + Middle, (21 * Cell) + Middle);
        }

        internal void SetStartPostitionPacman(Transaction acTrans, Database acDb, ref GamePacman Pacman)
        {
            clsStartRandom clsStartRandom = new clsStartRandom();

            Point2d ptPacman = new Point2d((16 * Cell), (9 * Cell) + Middle);

            Pacman.ptOrigin = ptPacman;
            Pacman.Direction = Direction.Right;
            Pacman.FacingDirection = Direction.Right;

            Pacman.Reset_Update = true;
            Pacman.Reset_Direction = Direction.Right;
            Pacman.Reset_ptOrigin = ptPacman;
            Pacman.Reset_FacingDirection = Direction.Right;

            clsCommon.GamePacman = Pacman;
        }
    }
}