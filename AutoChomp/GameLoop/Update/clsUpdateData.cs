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

                // Check Ghost Collision
                clsDataEatGhost.EatGhosts();

                // Move Pacman
                clsPacmanMove.SetDataPacmanMove();

                // Update Mouth
                clsPacmanMove.SetDataUpdateMouth();

                // Calculate AStar
                clsDataGhostMove.SetAStarData();

                // Move Ghost
                clsDataGhostMove.SetDataGhostMove();

                // Set Squiggle
                clsSquiggleData.SetDataSquiggle();
            }

            // Hide Dots, Flash Power Pellets
            Gameloop.Data.clsDataDot clsDotData = new Gameloop.Data.clsDataDot();
            clsDotData.SetDataDots();
        }
    }
}