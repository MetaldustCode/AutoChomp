using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal static class clsClassTables
    {
        // Grid Path
        internal static Boolean[,] arrXGridPath = null;

        // Directions Path
        internal static List<Direction>[,] arrXDirection = null;

        internal static List<List<Direction>> lstLstXDirection = null;

        // Position Path
        internal static List<Position> lstXGridPosition = null;

        internal static List<Point2d> lstXGridOrigin = null;

        // Tunnel Path
        internal static Boolean[,] arrXTunnel = null;

        // Position String
        internal static List<String> lstXTunnelString = null;

        internal static List<Position> lstXTunnelPosition = null;
        internal static List<Point2d> lstXTunnelOrigin = null;

        // A-Star
        internal static int[,] arrAStarNum = null;

        internal static DBText[,] arrAStarText = null;

        // Dots Path
        internal static Boolean[,] arrXDots = null;

        internal static BlockReference[,] arrXBlkRefDots = null;

        // Power Pellet Path
        internal static Boolean[,] arrXPower = null;

        internal static BlockReference[,] arrXBlkRefPower = null;

        // Startup Locations
        internal static Boolean[,] arrXStartUp = null;

        internal static List<Point2d> lstXStartupOrigin = null;
        internal static List<Position> lstXStartupPosition = null;
    }
}