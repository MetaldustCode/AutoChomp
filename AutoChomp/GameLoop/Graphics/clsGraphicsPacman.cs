using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Graphics
{
    internal class clsGraphicsPacman
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void UpdatePacmanPositionAndVisibility(Transaction acTrans, Database acDb, ref GamePacman Pacman)
        {
            if (Pacman.lstPacmanBlockReference == null)
                return;

            for (int i = 0; i < Pacman.lstPacmanBlockReference.Count; i++)
            {
                if (Pacman.lstPacmanBlockReference[i].IsObjectIdValid(acDb))
                {
                    BlockReference acBlkRef = acTrans.GetObject(Pacman.lstPacmanBlockReference[i].ObjectId, OpenMode.ForWrite) as BlockReference;

                    UpdateRotation(acBlkRef, Pacman);

                    Data.clsDataMouth clsDataMouth = new Data.clsDataMouth();
                    string strBlockOpen = clsDataMouth.Mouth()[Pacman.intMouth];

                    UpdateVisibility(acBlkRef, strBlockOpen);

                    UpdatePosition(acBlkRef, ref Pacman, strBlockOpen);
                }
            }
        }

        internal void UpdateVisibility(BlockReference acBlkRef, string strBlockOpen)
        {
            if (clsCommon.GameGhostCommon.bolEatGhost)
                acBlkRef.Visible = false;
            else
            {
                if (acBlkRef.Name.Contains(strBlockOpen))
                    acBlkRef.Visible = true;
                else
                    acBlkRef.Visible = false;
            }
        }

        internal void UpdateRotation(BlockReference acBlkRef, GamePacman Pacman)
        {
            double dblDirection = 0;

            if (Pacman.Direction == Direction.Down)
                dblDirection = 270;

            if (Pacman.Direction == Direction.Left)
                dblDirection = 180;

            if (Pacman.Direction == Direction.Right)
                dblDirection = 0;

            if (Pacman.Direction == Direction.Up)
                dblDirection = 90;

            acBlkRef.Rotation = dblDirection.ToRadians();
        }

        internal void UpdatePosition(BlockReference acBlkRef, ref GamePacman Pacman, string strBlockOpen)
        {
            if (acBlkRef.Name.Contains(strBlockOpen))
                acBlkRef.Position = Pacman.ptOrigin.ToPoint3d();

           // clsZoomToPoint clsZoomToPoint = new clsZoomToPoint();
            //clsZoomToPoint.ZoomToPoint(Pacman.ptOrigin.ToPoint3d());
        }

        internal void SetVisibilityToFalse(Transaction acTrans, List<BlockReference> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
            {
                BlockReference acBlkRef = acTrans.GetObject(lstEntity[i].ObjectId, OpenMode.ForWrite) as BlockReference;
                acBlkRef.Visible = false;
            }
        }
    }
}