using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class GameMaze
    {
        internal List<String> lstGridIsland;
        internal List<String> lstGridBorder;
        internal List<String> lstGridPath;
        internal List<String> lstTunnelPath;
        internal List<String> lstDots;

        internal GameMaze(List<String> lstGridIsland, List<String> lstGridBorder,
                          List<String> lstGridPath, List<String> lstTunnelPath, List<string> lstDots)
        {
            this.lstGridIsland = lstGridIsland;
            this.lstGridBorder = lstGridBorder;
            this.lstGridPath = lstGridPath;
            this.lstTunnelPath = lstTunnelPath;
            this.lstDots = lstDots;
        }
    }
}