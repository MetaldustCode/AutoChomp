using System.Diagnostics;

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

        internal GameStopWatch()
        {
            this.StopWatchIdle = new Stopwatch();

            this.StopWatchPellet = new Stopwatch();

            this.StopWatchSquiggle = new Stopwatch();
            this.StopWatchReverse = new Stopwatch();

            this.StopWatchPowerStart = new Stopwatch();
            this.StopWatchPowerFlash = new Stopwatch();

            this.StopWatchExit = new Stopwatch();
        }
    }

    internal class GameElapsedTime
    {
        internal int intIntervalSquiggle;
        internal int intIntervalAfraid;
        internal int intIntervalFlash;
        internal int intIntervalReverse;

        internal int intPowerStartTime;
        internal int intPowerFlashTime;
        internal int intPowerFlashCount;
        internal int intPowerFlashTotal;

        internal int intPowerExit;

        internal GameElapsedTime()
        {
            this.intIntervalSquiggle = 500;
            this.intIntervalAfraid = 400;
            this.intIntervalFlash = 480;
            this.intIntervalReverse = 10000;

            this.intPowerStartTime = 3000;
            this.intPowerFlashTime = 400;
            this.intPowerFlashCount = 0;
            this.intPowerFlashTotal = 10;

            this.intPowerExit = 3000;
        }
    }
}