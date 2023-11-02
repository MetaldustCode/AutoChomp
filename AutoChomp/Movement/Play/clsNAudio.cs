using NAudio.Wave;
using System;
using System.IO;
using System.Reflection;

namespace AutoChomp
{
    internal class clsNAudio
    {
        private static WaveOut waveOutPowerPellet;

        internal static string GetPath(string strValue)
        {
            string Folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return string.Format("{0}\\Audio\\{1}", Folder, strValue);
        }

        internal static void PlayPowerPellet()
        {
            string strFullPath = GetPath("power_pellet.wav");

            if (waveOutPowerPellet == null)
            {
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

        internal static Boolean bolinit = false;

        public static void PlayMunch()
        {
            if (!bolinit)
            {
                string AudioFolder1 = GetPath("munch_1.wav");
                string AudioFolder2 = GetPath("munch_2.wav");

                stream1 = new WaveFileReader(AudioFolder1);
                out1 = new WaveOut();
                out1.Init(stream1);
                stream2 = new WaveFileReader(AudioFolder2);
                out2 = new WaveOut();
                out2.Init(stream2);
                bolinit = true;
            }

            PlaySoundsClick();
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
                if (out1.PlaybackState is PlaybackState.Stopped)
                    out2.Play();
            }

            bolToggle = !bolToggle;
            stream1.CurrentTime = new TimeSpan(0L);
            stream2.CurrentTime = new TimeSpan(0L);
        }

        internal static WaveStream stream1;
        internal static WaveOut out1;
        internal static WaveStream stream2;
        internal static WaveOut out2;
    }
}