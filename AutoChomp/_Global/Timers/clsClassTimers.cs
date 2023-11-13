using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace AutoChomp
{
    internal static class clsTimers
    {
        internal static GameStopWatch GameStopWatch;
        internal static GameElapsedTime GameElapsedTime;
    }

    internal class GameStopWatch
    {
        internal Stopwatch StopWatchIdle;

        internal Stopwatch StopWatchPellet;
        internal Stopwatch StopWatchSquiggle;
        internal Stopwatch StopWatchReverse;

        internal Stopwatch StopWatchPowerStart;
        internal Stopwatch StopWatchPowerFlash;

        internal Stopwatch StopWatchExit;

        internal Stopwatch StopWatchEatGhost;

        internal GameStopWatch()
        {
            this.StopWatchIdle = new Stopwatch();

            this.StopWatchPellet = new Stopwatch();

            this.StopWatchSquiggle = new Stopwatch();
            this.StopWatchReverse = new Stopwatch();

            this.StopWatchPowerStart = new Stopwatch();
            this.StopWatchPowerFlash = new Stopwatch();

            this.StopWatchExit = new Stopwatch();

            this.StopWatchEatGhost = new Stopwatch();
        }
    }

    internal class GameElapsedTime
    {
        internal int intIntervalSquiggle; 
        internal int intIntervalPellettFlash;
        internal int intIntervalReverse;

        internal int intPowerStartTime;
        internal int intPowerFlashTime;
        internal int intPowerFlashCount;
        internal int intPowerFlashTotal;

        internal int intHouseExit;

        internal int intEatGhost;

        internal List<ChaseMode> lstChaseModeCategory;
        internal List<int> lstChaseModeTime;

        internal GameElapsedTime()
        {
            this.intIntervalSquiggle = 500;      
            this.intIntervalPellettFlash = 480;
            this.intIntervalReverse = 10000;

            this.intPowerStartTime = 5000;
            this.intPowerFlashTime = 200;
            this.intPowerFlashCount = 0;
            this.intPowerFlashTotal = 12;

            this.intHouseExit = 3000;

            this.intEatGhost = 550;

            //Scatter for 7 seconds, then Chase for 20 seconds.
            //Scatter for 7 seconds, then Chase for 20 seconds.
            //Scatter for 5 seconds, then Chase for 20 seconds.
            //Scatter for 5 seconds, then switch to Chase mode permanently.

            this.lstChaseModeCategory =
                new List<ChaseMode>(){ChaseMode.Scatter, ChaseMode.Chase,
                                      ChaseMode.Scatter, ChaseMode.Chase,
                                      ChaseMode.Scatter, ChaseMode.Chase,
                                      ChaseMode.Scatter, ChaseMode.Chase};


            this.lstChaseModeTime =
                new List<int>() { 7000, 20000,
                                  7000, 20000,
                                  50000, 20000,
                                  5000, 0 };
        }
    }
}