using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsTunnel
    {
        // Check if sprite in tunnel
        internal void IsInTunnel(out List<Boolean> lstInTunnelGhost)
        {
            lstInTunnelGhost = new List<bool>() { false, false, false, false }.Multiply();

            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

            List<Position> lstPos = new List<Position>();
            Position posPacman = new Position();

            clsGetCell.GetCellPosition(ref lstPos, ref posPacman);

            List<Position> lstTunnel = clsClassTables.lstXTunnelPosition;

            for (int t = 0; t < lstTunnel.Count; t++)
            {
                for (int p = 0; p < lstPos.Count; p++)
                {
                    if (lstTunnel[t].X == lstPos[p].X &&
                        lstTunnel[t].Y == lstPos[p].Y)
                        lstInTunnelGhost[p] = true;
                }
            }
        }
    }
}