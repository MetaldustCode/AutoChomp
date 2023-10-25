using System;

namespace AutoChomp
{
    public static class clsRandomizer
    {
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);

        public static int RandomInteger(int minimum, int maximum)
        {
            return random.Next(minimum, maximum + 1);
        }
    }
}