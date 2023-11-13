using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsCharacterOther
    {
        internal List<GameGhost> ProcessSprites(Transaction acTrans, Database acDb, BlockTable acBlkTbl,
                                                BlockTableRecord acBlkTblRec, out GamePacman Pacman)
        {
            List<GameGhost> lstGhost = AddCharacterGhost();

            for (int i = 0; i < lstGhost.Count; i++)
                BuildDead(lstGhost[i]);

            for (int i = 0; i < lstGhost.Count; i++)
                BuildAfraid(lstGhost[i]);

            Pacman = CreateCharacterPacman(acTrans, acDb, acBlkTbl, acBlkTblRec);

            return lstGhost;
        }

        internal GamePacman CreateCharacterPacman(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            List<BlockReference> lstPacman = AddPacman(acTrans, acDb, acBlkTbl, acBlkTblRec);

            List<String> lstPacmanDeathName = new List<string>();
            List<BlockReference> lstPacmanDeathBlkRef = AddPacmanDeath(acTrans, acDb, acBlkTbl, acBlkTblRec, ref lstPacmanDeathName);

            GamePacman GamePacman = clsCommon.GamePacman;

            GamePacman.lstPacmanBlockName.Clear();
            GamePacman.lstPacmanBlockReference.Clear();

            GamePacman.lstDeathBlockReference = lstPacmanDeathBlkRef;
            GamePacman.lstDeathName = lstPacmanDeathName;

            for (int i = 0; i < lstPacman.Count; i++)
            {
                GamePacman.lstPacmanBlockName.Add(lstPacman[i].Name);
                GamePacman.lstPacmanBlockReference.Add(lstPacman[i]);
            }

            return GamePacman;
        }

        internal List<BlockReference> AddPacman(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            List<BlockReference> rtnValue = new List<BlockReference>();

            double dblMid = 30;
            double dblOpen = 60;
            double dblRadius = 80;

            clsCharacterPacman clsCharacterPacman = new clsCharacterPacman();
            List<String> lstName = lstPacmanName();

            rtnValue.Add(clsCharacterPacman.AddPacman(acTrans, acDb, acBlkTbl, acBlkTblRec, lstName[0], dblRadius, dblMid, dblMid * -1));
            rtnValue.Add(clsCharacterPacman.AddPacman(acTrans, acDb, acBlkTbl, acBlkTblRec, lstName[1], dblRadius, dblOpen, dblOpen * -1));
            rtnValue.Add(clsCharacterPacman.AddPacman(acTrans, acDb, acBlkTbl, acBlkTblRec, lstName[2], dblRadius));

            return rtnValue;
        }

        internal List<BlockReference> AddPacmanDeath(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec, ref List<String> lstDeathName)
        {
            List<BlockReference> rtnValue = new List<BlockReference>();

            double dblRadius = 80;

            clsCharacterPacman clsCharacterPacman = new clsCharacterPacman();

            double dblAngle = 30;
            while (dblAngle <= 180)
            {
                string strName = "PacmanDeath_" + dblAngle.ToString().PadLeft(3, '0');
                rtnValue.Add(clsCharacterPacman.AddPacman(acTrans, acDb, acBlkTbl, acBlkTblRec, strName, dblRadius, dblAngle, dblAngle * -1));
                lstDeathName.Add(strName);
                dblAngle += 10;
            }

            string strExplode = "PacmanDeath_Explode";
            BlockReference acBlkRefExplode = clsCharacterPacman.AddExplode(acTrans, acDb, acBlkTbl, acBlkTblRec, strExplode, 40, dblRadius);
            lstDeathName.Add(strExplode);
            rtnValue.Add(acBlkRefExplode);

            for (int i = 0; i < rtnValue.Count; i++)
            {
                dblRadius = 90;
                rtnValue[i].Rotation = dblRadius.ToRadians();
            }

            return rtnValue;
        }

        internal List<String> lstPacmanName()
        {
            List<String> rtnValue = new List<String>
            {
                "Pacman_Mid",
                "Pacman_Open",
                "Pacman_Close"
            };

            return rtnValue;
        }

        internal List<GameGhost> AddCharacterGhost()
        {
            //"Red"
            //"Pink"
            //"Blue"
            //"Orange"

            clsCharacterGhost clsCharacterGhost = new clsCharacterGhost();

            List<GameGhost> lstGhost = clsCharacterGhost.BuildGhostTable();

            for (int i = 0; i < lstGhost.Count; i++)
                lstGhost[i] = BuildGhost(lstGhost[i]);

            return lstGhost;
        }

        internal GameGhost BuildGhost(GameGhost Ghost)
        {
            clsCharacterGhost clsCharacterGhost = new clsCharacterGhost();
            List<Direction> lstDirection = GetDirection();

            for (int i = 0; i < Ghost.lstBlockNameStandard.Count; i++)
                Ghost.lstStandard.Add(clsCharacterGhost.AddGhost(Ghost, Ghost.lstBlockNameStandard[i], lstDirection[i], Squiggle.Standard));

            for (int i = 0; i < Ghost.lstBlockNameAlternate.Count; i++)
                Ghost.lstAlternate.Add(clsCharacterGhost.AddGhost(Ghost, Ghost.lstBlockNameAlternate[i], lstDirection[i], Squiggle.Alternate));

            return Ghost;
        }

        internal GameGhost BuildDead(GameGhost Ghost)
        {
            clsCharacterDead clsCharacterDead = new clsCharacterDead();
            List<Direction> lstDirection = GetDirection();

            for (int i = 0; i < Ghost.lstBlockNameDead.Count; i++)
                Ghost.lstDead.Add(clsCharacterDead.AddGhostDead(Ghost.lstBlockNameDead[i], lstDirection[i]));

            return Ghost;
        }

        internal GameGhost BuildAfraid(GameGhost Ghost)
        {
            clsCharacterAfraid clsCharacterAfraid = new clsCharacterAfraid();

            for (int i = 0; i < Ghost.lstBlockNameAfraid.Count; i++)
            {
                if (Ghost.lstBlockNameAfraid[i].EndsWith("SB", StringComparison.CurrentCultureIgnoreCase) ||
                    Ghost.lstBlockNameAfraid[i].EndsWith("SW", StringComparison.CurrentCultureIgnoreCase))
                    Ghost.lstAfraid.Add(clsCharacterAfraid.AddGhostAfraid(Ghost.lstBlockNameAfraid[i], Direction.None, Squiggle.Standard));
                else
                    Ghost.lstAfraid.Add(clsCharacterAfraid.AddGhostAfraid(Ghost.lstBlockNameAfraid[i], Direction.None, Squiggle.Alternate));
            }

            return Ghost;
        }

        internal List<Direction> GetDirection()
        {
            List<Direction> rtnValue = new List<Direction>
            {
                Direction.Up,
                Direction.Down,
                Direction.Left,
                Direction.Right
            };

            return rtnValue;
        }
    }
}