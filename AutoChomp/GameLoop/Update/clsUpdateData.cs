namespace AutoChomp.Gameloop.Update
{
    internal class clsUpdateData
    {
        internal void UpdateData()
        {
            clsAfraid clsAfraid = new clsAfraid();

            if (!clsAfraid.EatGhost())
            {
                Gameloop.Data.clsDataPacmanMove clsPacmanMove = new Gameloop.Data.clsDataPacmanMove();
                Gameloop.Data.clsDataGhostMove clsDataGhostMove = new Gameloop.Data.clsDataGhostMove();
                Gameloop.Data.clsDataEatGhost clsDataEatGhost = new Gameloop.Data.clsDataEatGhost();
                Gameloop.Data.clsDataSquiggle clsSquiggleData = new Gameloop.Data.clsDataSquiggle();
                
                clsCommon.GameDebug.lstCircleOrigin.Clear();

                // Check Ghost Collision
                clsDataEatGhost.EatGhosts();

                // Move Pacman
                clsPacmanMove.SetDataPacmanMove();

                // Update Mouth
                clsPacmanMove.SetDataUpdateMouth();

                // Move Ghost
                clsDataGhostMove.SetDataGhostMove();

                // Calculate AStar
                clsDataGhostMove.SetAStarData();

                // Set Squiggle
                clsSquiggleData.SetDataSquiggle();

                clsCalcGlobalAStar clsCalcGlobalAStar = new clsCalcGlobalAStar();
                clsCalcGlobalAStar.clsAStar();

      

                Data.clsDataAlignToGrid clsDataAlignToGrid = new Data.clsDataAlignToGrid();
                clsDataAlignToGrid.AlignToGrid(true);
            }

            // Hide Dots, Flash Power Pellets
            Gameloop.Data.clsDataDot clsDotData = new Gameloop.Data.clsDataDot();
            clsDotData.SetDataDots();
        }
    }
}