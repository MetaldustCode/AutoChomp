namespace AutoChomp.Update
{
    internal class clsUpdateData
    {
        internal void UpdateData()
        {
            // Move Pacman
            Data.clsDataPacmanMove clsPacmanMove = new Data.clsDataPacmanMove();
            clsPacmanMove.SetDataPacmanMove();

            // Update Mouth
            clsPacmanMove.SetDataUpdateMouth();

            // Hide Dots, Flash Power Pellets
            Data.clsDataDot clsDotData = new Data.clsDataDot();
            clsDotData.SetDataDots();

            // Set Squiggle
            clsDataSquiggle clsSquiggleData = new clsDataSquiggle();
            clsSquiggleData.SetDataSquiggle();

            // Move Ghost
            Data.clsDataGhostMove clsMoveGhost = new Data.clsDataGhostMove();
            clsMoveGhost.SetDataGhostMove();
        }
    }
}