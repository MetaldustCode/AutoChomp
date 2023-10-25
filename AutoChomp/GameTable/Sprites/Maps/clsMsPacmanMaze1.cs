using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMsPacmanMaze1
    {
        internal List<String> GetGridIsland()
        {
            List<String> a = new List<String>
            {
              // 01234567890123456789012345678901 32
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "    1HH2    1HHHHHH2    1HH2    ",
                "    4HH3    4HHHHHH3    4HH3    ",
                "                                ",
                "      12 1HHH2 12 1HHH2 12      ",
                "      VV V   V VV V   V VV      ",
                "      VV 4HHH3 VV 4HHH3 VV      ",
                "      VV       VV       VV      ",
                "      V4HH2 1HH34HH2 1HH3V      ",
                "      4HHH3 4HHHHHH3 4HHH3      ",
                "                                ",
                "      1HHH2          1HHH2      ",
                "      V1HH3          4HH2V      ",
                "      VV                VV      ",
                "      VV 12          12 VV      ",
                "      43 VV          VV 43      ",
                "         VV          VV         ",
                "      1HH34HH2 12 1HH34HH2      ",
                "      4HHHHHH3 VV 4HHHHHH3      ",
                "               VV               ",
                "      1HHH2 1HH34HH2 1HHH2      ",
                "      4HHH3 4HHHHHH3 4HHH3      ",
                "                                ",
                "    1HH2 1HHH2 12 1HHH2 1HH2    ",
                "    V  V V1HH3 VV 4HH2V V  V    ",
                "    V  V VV    VV    VV V  V    ",
                "    V  V VV 1HH34HH2 VV V  V    ",
                "    4HH3 43 4HHHHHH3 43 4HH3    ",
                "                                ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }

        internal List<String> GetGridBorder()
        {
            List<String> a = new List<String>
            {
              // 01234567890123456789012345678901 32
                "                                ",
                "                                ",
                "  5TTTTTTEFTTTTTTTTTTEFTTTTTT6  ",
                "  L      VV          VV      R  ",
                "  L      VV          VV      R  ",
                "  L      43          43      R  ",
                "  L                          R  ",
                "  8B2                      1B7  ",
                "    L                      R    ",
                "  MT3                      4TN  ",
                "                                ",
                "  PB2                      1BO  ",
                "    L                      R    ",
                "    L                      R    ",
                "    L       1BO<>PB2       R    ",
                "    L       R      L       R    ",
                "    L       R      L       R    ",
                "    L       R      L       R    ",
                "  MT3       4TTTTTT3       4TN  ",
                "                                ",
                "  PB2                      1BO  ",
                "    L                      R    ",
                "    L                      R    ",
                "    L                      R    ",
                "  5T3                      4T6  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  8BBBBBBBBBBBBBBBBBBBBBBBBBB7  ",
                "                                ",
                "                                "
            };

            a.Reverse();
            return a;
        }

        internal List<String> GetGridPath()
        {
            List<String> a = new List<String>
            {
              // 01234567890123456789012345678901 32
                "                                ",
                "                                ",
                "                                ",
                "   ......  ..........  ......   ",
                "   .1HH2.  .1HHHHHH2.  .1HH2.   ",
                "   .4HH3.  .4HHHHHH3.  .4HH3.   ",
                "   ..........................   ",
                "     .12.1HHH2.12.1HHH2.12.     ",
                "     .VV.V   V.VV.V   V.VV.     ",
                "     .VV.4HHH3.VV.4HHH3.VV.     ",
                "......VV.......VV.......VV......",
                "     .V4HH2.1HH34HH2.1HH3V.     ",
                "     .4HHH3.4HHHHHH3.4HHH3.     ",
                "     ......................     ",
                "     .1HHH2.        .1HHH2.     ",
                "     .V1HH3.        .4HH2V.     ",
                "     .VV....        ....VV.     ",
                "     .VV.12.        .12.VV.     ",
                "     .43.VV.        .VV.43.     ",
                ".........VV..........VV.........",
                "     .1HH34HH2.12.1HH34HH2.     ",
                "     .4HHHHHH3.VV.4HHHHHH3.     ",
                "     ..........VV..........     ",
                "     .1HHH2.1HH34HH2.1HHH2.     ",
                "     .4HHH3.4HHHHHH3.4HHH3.     ",
                "   ..........................   ",
                "   .1HH2.1HHH2.12.1HHH2.1HH2.   ",
                "   .V  V.V1HH3.VV.4HH2V.V  V.   ",
                "   .V  V.VV....VV....VV.V  V.   ",
                "   .V  V.VV.1HH34HH2.VV.V  V.   ",
                "   .4HH3.43.4HHHHHH3.43.4HH3.   ",
                "   ..........................   ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }

        internal List<String> GetTunnelPath()
        {
            List<String> a = new List<String>
            {
              // 01234567890123456789012345678901 32
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "....                        ....",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "....                        ....",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }

        internal List<String> GetDots()
        {
            List<String> a = new List<String>
            {
              // 01234567890123456789012345678901 32
                "                                ",
                "                                ",
                "                                ",
                "   ......  ..........  ......   ",
                "   *1HH2.  .1HHHHHH2.  .1HH2*   ",
                "   .4HH3.  .4HHHHHH3.  .4HH3.   ",
                "   ..........................   ",
                "     .12.1HHH2.12.1HHH2.12.     ",
                "     .VV.V   V.VV.V   V.VV.     ",
                "     .VV.4HHH3.VV.4HHH3.VV.     ",
                "     .VV.......VV.......VV.     ",
                "     .V4HH2 1HH34HH2 1HH3V.     ",
                "     .4HHH3 4HHHHHH3 4HHH3.     ",
                "     .                    .     ",
                "     .1HHH2          1HHH2.     ",
                "     .V1HH3          4HH2V.     ",
                "     .VV                VV.     ",
                "     .VV 12          12 VV.     ",
                "     .43 VV          VV 43.     ",
                "     .   VV          VV   .     ",
                "     .1HH34HH2 12 1HH34HH2.     ",
                "     .4HHHHHH3 VV 4HHHHHH3.     ",
                "     .......   VV   .......     ",
                "     .1HHH2.1HH34HH2.1HHH2.     ",
                "     .4HHH3.4HHHHHH3.4HHH3.     ",
                "   ..........................   ",
                "   .1HH2.1HHH2.12.1HHH2.1HH2.   ",
                "   .V  V.V1HH3.VV.4HH2V.V  V.   ",
                "   .V  V.VV....VV....VV.V  V.   ",
                "   *V  V.VV.1HH34HH2.VV.V  V*   ",
                "   .4HH3.43.4HHHHHH3.43.4HH3.   ",
                "   ..........................   ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }
    }
}