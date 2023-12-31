﻿using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMainCharacters
    {
        internal List<Direction> GetDirections()
        {
            // "Red", "Pink", "Blue", "Orange"

            return new List<Direction>() { Direction.None, Direction.Up, Direction.Up, Direction.Up }.Multiply();
        }

        public void CreateCharacters(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec,
                                     out GamePacman GamePacman, out List<GameGhost> lstGameGhost)
        {
            clsCharacterOther clsCharacterOther = new clsCharacterOther();

            lstGameGhost = clsCharacterOther.ProcessSprites(acTrans, acDb, acBlkTbl, acBlkTblRec, out GamePacman);

            List<Direction> lstDirections = GetDirections();

            // Set Random Direction for Testing
            for (int i = 0; i < lstDirections.Count; i++)
            {
                GameGhost GameGhost = lstGameGhost[i];
                GameGhost.Direction = lstDirections[i];
                lstGameGhost[i] = GameGhost;
            }

            // Set Pacman Position to Start Point
            clsStartPosition clsStartPosition = new clsStartPosition();
            clsStartPosition.SetStartPostitionPacman(acTrans, acDb, ref GamePacman);

            Gameloop.Graphics.clsGraphicsPacman clsGraphicsPacman = new Gameloop.Graphics.clsGraphicsPacman();
            clsGraphicsPacman.UpdatePacmanPositionAndVisibility(acTrans, acDb, ref GamePacman);

            // Hide Pacman Death
            clsGraphicsPacman.SetVisibilityToFalse(acTrans, GamePacman.lstDeathBlockReference);

            // Set Ghost Positions
            Gameloop.Graphics.clsGraphicsGhost clsPositionGhost = new Gameloop.Graphics.clsGraphicsGhost();
            clsPositionGhost.SetStartPostitionGhost(acTrans, acDb, ref lstGameGhost);

            // Update Ghosts
            Gameloop.Graphics.clsGraphicsGhost clsGhostPosition = new Gameloop.Graphics.clsGraphicsGhost();
            clsGhostPosition.UpdateGhostGraphics(acTrans, acDb, ref lstGameGhost);
        }
    }
}