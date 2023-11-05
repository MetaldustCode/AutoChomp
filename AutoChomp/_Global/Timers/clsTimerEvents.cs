using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp
{
    internal class clsTimerEvents
    {
        internal static double dblAverage = 0;

        internal static List<double> lstAverage = new List<double>();

        internal double PrintTime(double dblCurrentTime)
        {
            if (lstAverage.Count > 120)
                lstAverage.RemoveAt(0);

            lstAverage.Add(dblCurrentTime);

            dblAverage = lstAverage.Count > 0 ? lstAverage.Average() : 0.0;

            dblAverage = Math.Round(dblAverage, 0);

            return dblAverage;
        }

        internal int GetTimerPellet()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchPellet.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPellet.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchPellet.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerPellet()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPellet.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPellet.Restart();
        }

        internal void StopTimerPellet()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPellet.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPellet.Stop();
        }

        internal void RestartTimerEatGhost()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchEatGhost.IsRunning)
                    clsTimers.GameStopWatch.StopWatchEatGhost.Restart();
        }

        internal void StopTimerEatGhost()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchEatGhost.IsRunning)
                    clsTimers.GameStopWatch.StopWatchEatGhost.Stop();
        }

        internal int GetTimerSquiggle()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchSquiggle.IsRunning)
                    clsTimers.GameStopWatch.StopWatchSquiggle.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchSquiggle.ElapsedMilliseconds);
            }
            return 0;
        }

        internal int GetTimerEatGhost()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchEatGhost.IsRunning)
                    clsTimers.GameStopWatch.StopWatchEatGhost.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchEatGhost.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerSquiggle()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchSquiggle.IsRunning)
                    clsTimers.GameStopWatch.StopWatchSquiggle.Restart();
        }

        internal int GetTimerReverse()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchReverse.IsRunning)
                    clsTimers.GameStopWatch.StopWatchReverse.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchReverse.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerReverse()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchReverse.IsRunning)
                    clsTimers.GameStopWatch.StopWatchReverse.Restart();
        }

        internal int GetTimerPowerStart()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchPowerStart.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerStart.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchPowerStart.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerPowerStart()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPowerStart.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerStart.Restart();
        }

        internal void StopTimerPowerStart()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPowerStart.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerStart.Stop();
        }

        internal int GetTimerPowerFlash()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchPowerFlash.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerFlash.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchPowerFlash.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerPowerFlash()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPowerFlash.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerFlash.Restart();
        }

        internal void StopTimerPowerFlash()
        {
            if (clsTimers.GameStopWatch != null)
                if (clsTimers.GameStopWatch.StopWatchPowerFlash.IsRunning)
                    clsTimers.GameStopWatch.StopWatchPowerFlash.Stop();
        }

        internal int GetTimerExit()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (!clsTimers.GameStopWatch.StopWatchExit.IsRunning)
                    clsTimers.GameStopWatch.StopWatchExit.Restart();

                return Convert.ToInt32(clsTimers.GameStopWatch.StopWatchExit.ElapsedMilliseconds);
            }
            return 0;
        }

        internal void RestartTimerExit()
        {
            if (clsTimers.GameStopWatch.StopWatchExit.IsRunning)
                clsTimers.GameStopWatch.StopWatchExit.Restart();
        }

        internal void StopTimerExit()
        {
            if (clsTimers.GameStopWatch != null)
            {
                if (clsTimers.GameStopWatch.StopWatchExit.IsRunning)
                    clsTimers.GameStopWatch.StopWatchExit.Stop();
            }
        }
    }
}