using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoChomp
{
    internal static class clsCommon
    {
        internal static bool bolInit = false;

        internal static GameForm GameForm;
        internal static DGVForm DGVForm;
        internal static GameObjectId GameObjectId;
        internal static GamePacman GamePacman;
        internal static List<GameGhost> lstGameGhost;
        internal static List<GameMaze> lstGameMaze;

        //Movement
        internal static GamePosition GamePosition;

        internal static GameDots GameDots;
        internal static GamePower GamePower;
        internal static GameGhostCommon GameGhostCommon;

        internal static GameDebug GameDebug;
    }

    internal static class clsGridValues
    {
        internal static int intMouth = 100;
        internal static double Cell = 100;
        internal static double Middle = Cell / 2;
        internal static double OutSide = Cell / 2;
        internal static double Inside = Cell / 4;
        internal static int intGroup = 1;

        internal static List<int> lstFrameCounter =
            new List<int> { 0, 0, 0, 0 }.Multiply();


    }

    internal static class clsScoreValues
    {
        internal static List<DBText> lstElements;

        internal static int intPacmanScore;
        internal static int intRedScore;
        internal static int intBlueScore;
        internal static int intPinkScore;
        internal static int intOrangeScore;
    }

    internal class GameGhostCommon
    {
        internal bool bolGraphicsRequired;

        internal bool bolPowerPelletEaten;
        internal bool bolPowerTimerStart;
        internal bool bolPowerTimerFlash;

        internal List<GameGhost> lstInHouse;

        internal GameGhostCommon()
        {
            this.bolGraphicsRequired = false;

            this.bolPowerPelletEaten = false;
            this.bolPowerTimerStart = false;
            this.bolPowerTimerFlash = false;

            this.lstInHouse = new List<GameGhost>();
        }
    }
}