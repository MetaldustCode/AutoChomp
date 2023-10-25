using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace AutoChomp
{
    internal class clsResetGame
    {
        internal void ResetGame()
        {
            clsAfraid clsAfraid = new clsAfraid();
            clsAfraid.CancelAfraid();

            BuildGame();

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
        }

        internal void BuildGame()
        {
            clsGenerateTables clsGenerateTables = new clsGenerateTables();
            clsEntityDelete clsEntityDelete = new clsEntityDelete();

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead);

                    // Open the Block table record Model space for read
                    BlockTableRecord acBlkTblRec = (BlockTableRecord)acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                    // Delete Existing Maze

                    clsEntityDelete.DeleteElements(acTrans, acDb);

                    clsInit clsInit = new clsInit();
                    clsInit.InitReset();

                    // Create Background
                    ProcessMaze(acTrans, acDb, acBlkTblRec);

                    // Create Dots
                    ProcessDots(acTrans, acDb, acBlkTbl, acBlkTblRec);

                    {
                        // Create Pacman and Ghosts
                        ProcessCharacters(acTrans, acDb, acBlkTbl, acBlkTblRec);

                        // Init History for Characters
                        clsGenerateTables.GenerateHistory();
                    }

                    clsScore clsScore = new clsScore();
                    clsScore.ShowValue(acTrans, acDb);

                    clsReg clsReg = new clsReg();
                    clsValidDirection clsValidDirection = new clsValidDirection();

                    if (clsReg.GetDebugDirection())
                        clsValidDirection.DrawValid();
                    else
                        clsValidDirection.DeleteValid();

                    //clsDrawBox clsDrawBox = new clsDrawBox();
                    //clsDrawBox.DrawBox(acTrans, acDb);

                    // Set Default Location
                    //clsHasMoved clsHasMoved = new clsHasMoved();
                    //clsHasMoved.AddCurrentLocation();

                    //clsTimerEvents clsTimerEvents = new clsTimerEvents();
                    //clsTimerEvents.RestartTimerPellet();

                    acTrans.Commit();

                    dynamic acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;

                    acadApp.ZoomExtents();
                }
            }
        }

        internal void ProcessMaze(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec)
        {
            // Create Grid
            clsMainGrid clsCreateGrid = new clsMainGrid();
            List<ObjectId> lstGrid = clsCreateGrid.Main(acTrans, acDb, acBlkTblRec);

            // Store ObjectId
            lstGrid.AddToMazeElement();
        }

        internal void ProcessDots(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            clsMainDots clsMainDots = new clsMainDots();
            BlockReference[,] arrDots = clsMainDots.CreateDots(acTrans, acDb, acBlkTbl, acBlkTblRec);
            BlockReference[,] arrPower = clsMainDots.CreatePower(acTrans, acDb, acBlkTbl, acBlkTblRec);

            clsClassTables.arrXBlkRefDots = arrDots;
            clsClassTables.arrXBlkRefPower = arrPower;

            // Store ObjectId
            arrDots.GetObjectId().AddToDotsElement();
            arrPower.GetObjectId().AddToPowerElement();
        }

        internal void ProcessCharacters(Transaction acTrans, Database acDb, BlockTable acBlkTbl, BlockTableRecord acBlkTblRec)
        {
            GamePacman GamePacman = new GamePacman("Pacman", 1);
            List<GameGhost> lstGameGhost = new List<GameGhost>();

            clsMainCharacters clsCreateCharacters = new clsMainCharacters();
            clsCreateCharacters.CreateCharacters(acTrans, acDb, acBlkTbl, acBlkTblRec, ref GamePacman, ref lstGameGhost);

            // Store BlockReferences
            clsCommon.GamePacman = GamePacman;
            clsCommon.lstGameGhost = lstGameGhost;

            StoreObjectIdInElement(lstGameGhost);
            StoreObjectIdInElement(GamePacman);
        }

        internal void StoreObjectIdInElement(GamePacman Pacman)
        {
            List<ObjectId> lstObjectId = new List<ObjectId>();

            GetObjectId(Pacman.lstPacmanBlockReference, ref lstObjectId);
            GetObjectId(Pacman.lstDeathBlockReference, ref lstObjectId);

            clsCommon.GameObjectId.lstObjPacman = lstObjectId;
        }

        internal void StoreObjectIdInElement(List<GameGhost> lstGameGhost)
        {
            List<ObjectId> lstObjectId = new List<ObjectId>();

            for (int i = 0; i < lstGameGhost.Count; i++)
            {
                GetObjectId(lstGameGhost[i].lstStandard, ref lstObjectId);
                GetObjectId(lstGameGhost[i].lstAlternate, ref lstObjectId);
                GetObjectId(lstGameGhost[i].lstAfraid, ref lstObjectId);
                GetObjectId(lstGameGhost[i].lstDead, ref lstObjectId);
            }

            clsCommon.GameObjectId.lstObjGhosts = lstObjectId;
        }

        internal void GetObjectId(List<BlockReference> lstValue, ref List<ObjectId> lstObjectId)
        {
            for (int i = 0; i < lstValue.Count; i++)
                lstObjectId.Add(lstValue[i].ObjectId);
        }
    }
}