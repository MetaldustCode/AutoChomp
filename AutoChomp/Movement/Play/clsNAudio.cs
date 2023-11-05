using NAudio.Wave;
using System;
using System.IO;
using System.Reflection;

namespace AutoChomp
{
    internal class clsNAudio
    {
        internal static WaveOut waveOutPowerPellet;
        internal static WaveOut waveOutEatGhost;

        internal static string GetPath(string strValue)
        {
            string Folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return string.Format("{0}\\Audio\\{1}", Folder, strValue);
        }

        internal static void PlayPowerPellet()
        {
            if (waveOutPowerPellet == null)
            {
                string strFullPath = GetPath("power_pellet.wav");

                WaveFileReader reader = new WaveFileReader(strFullPath);
                LoopStream loop = new LoopStream(reader);
                waveOutPowerPellet = new WaveOut();
                waveOutPowerPellet.Init(loop);
                waveOutPowerPellet.Play();
            }
        }

        internal static void StopPowerPellet()
        {
            if (waveOutPowerPellet != null)
            {
                waveOutPowerPellet.Stop();
                waveOutPowerPellet.Dispose();
                waveOutPowerPellet = null;
            }
        }

        public static void Init()
        {
            if (stream1 == null)
            {
                string AudioFolder1 = GetPath("munch_1.wav");
                string AudioFolder2 = GetPath("munch_2.wav");
                stream1 = new WaveFileReader(AudioFolder1);
                stream2 = new WaveFileReader(AudioFolder2);
                out1 = new WaveOut();
                out2 = new WaveOut();
                out1.Init(stream1);
                out2.Init(stream2);
            }
        }

        public static void PlayMunch()
        {
            if (stream1 == null)
            {
                Init();
            }
            else
            {
                stream1.Position = 0;
                stream2.Position = 0;
            }


            PlaySoundsClick();
        }

        public static void PlayEatGhost()
        {
            if (stream3 == null)
            {
                string AudioFolder1 = GetPath("eat_ghost.wav");
                stream3 = new WaveFileReader(AudioFolder1);
                out3 = new WaveOut();
                out3.Init(stream3);
            }
            else
                stream3.Position = 0;

            if (out3.PlaybackState is PlaybackState.Stopped)
                out3.Play();

            // stream1.CurrentTime = new TimeSpan(0L);
        }

        internal static Boolean bolToggle = false;

        internal static void PlaySoundsClick()
        {
            if (bolToggle)
            {
                if (out1.PlaybackState is PlaybackState.Stopped)
                    out1.Play();
            }
            else
            {
                if (out2.PlaybackState is PlaybackState.Stopped)
                    out2.Play();
            }

            bolToggle = !bolToggle;
            //stream1.CurrentTime = new TimeSpan(0L);
            //stream2.CurrentTime = new TimeSpan(0L);
        }

        internal static WaveStream stream1;
        internal static WaveOut out1;
        internal static WaveStream stream2;
        internal static WaveOut out2;
        internal static WaveStream stream3;
        internal static WaveOut out3;
    }
}