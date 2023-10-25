using System;
using System.IO;
using System.Reflection;

namespace AutoChomp
{
    internal static class clsPlaySounds
    {
        internal static System.Media.SoundPlayer player1 = null;
        internal static System.Media.SoundPlayer player2 = null;

        internal static System.Media.SoundPlayer Palette = null;

        internal static Boolean bolFirst = true;

        internal static void PlayPalette()
        {
            string AudioFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (Palette == null)
            {
                Palette = new System.Media.SoundPlayer(AudioFolder + @"\Audio\power_pellet2.wav");
                Palette.LoadAsync();
            }

            Palette.PlayLooping();
        }

        internal static  void StopPalette()
        {           
            Palette.Stop();
        }

        internal static void PlayMP3()
        {
            string AudioFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (player1 == null)
            {
                player1 = new System.Media.SoundPlayer(AudioFolder + @"\Audio\munch_1.wav");
                player1.LoadAsync();

                player2 = new System.Media.SoundPlayer(AudioFolder + @"\Audio\munch_2.wav");
                player2.LoadAsync();
            }

            if (bolFirst)
            {
                player1.Play();
                bolFirst = false;
            }
            else
            {
                player2.Play();
                bolFirst = true;
            }
        }
    }
}