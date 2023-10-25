using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMsPacmanMaze2
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
                "            1HHHHHH2            ",
                "            4HH21HH3            ",
                "               VV               ",
                "    1HHHHH2 12 VV 12 1HHHHH2    ",
                "    V1HHHH3 VV VV VV 4HHHH2V    ",
                "    VV      VV 43 VV      VV    ",
                "    VV 1HH2 VV    VV 1HH2 VV    ",
                "    43 4H2V V4HHHH3V V1H3 43    ",
                "         VV 4HHHHHH3 VV         ",
                "         VV          VV         ",
                "         VV          VV         ",
                "         VV          VV         ",
                "    1HH2 43          43 1HH2    ",
                "    4H2V                V1H3    ",
                "      VV 12          12 VV      ",
                "      VV VV          VV VV      ",
                "      VV V4H2 1HH2 1H3V VV      ",
                "      43 4HH3 V  V 4HH3 43      ",
                "              V  V              ",
                "      1HHHHH2 V  V 1HHHHH2      ",
                "      4HH21H3 4HH3 4H21HH3      ",
                "         VV          VV         ",
                "      12 VV 1HHHHHH2 VV 12      ",
                "      VV 43 4HH21HH3 43 VV      ",
                "      VV       VV       VV      ",
                "    1H3V 1HHH2 VV 1HHH2 V4H2    ",
                "    4HH3 4HHH3 43 4HHH3 4HH3    ",
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
                "  MTTTTTTEFTTTTTTTTTTEFTTTTTTN  ",
                "         VV          VV         ",
                "  AHHHH2 VV          VV 1HHHHS  ",
                "  XHHHH3 43          43 4HHHHJ  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  YHHHH2                1HHHHI  ",
                "  XHHHH3    1BO<>PB2    4HHHHJ  ",
                "  L         R      L         R  ",
                "  L         R      L         R  ",
                "  L         R      L         R  ",
                "  L         4TTTTTT3         R  ",
                "  8B2                      1B7  ",
                "    L                      R    ",
                "    L                      R    ",
                "    L                      R    ",
                "    L                      R    ",
                "  MT3                      4TN  ",
                "                                ",
                "  AH2                      1HS  ",
                "  XH3                      4HJ  ",
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
                ".........  ..........  .........",
                "        .  .1HHHHHH2.  .        ",
                "        .  .4HH21HH3.  .        ",
                "   ............VV............   ",
                "   .1HHHHH2.12.VV.12.1HHHHH2.   ",
                "   .V1HHHH3.VV.VV.VV.4HHHH2V.   ",
                "   .VV......VV.43.VV......VV.   ",
                "   .VV.1HH2.VV....VV.1HH2.VV.   ",
                "   .43.4H2V.V4HHHH3V.V1H3.43.   ",
                "   ......VV.4HHHHHH3.VV......   ",
                "        .VV..........VV.        ",
                "        .VV.        .VV.        ",
                "   ......VV.        .VV......   ",
                "   .1HH2.43.        .43.1HH2.   ",
                "   .4H2V....        ....V1H3.   ",
                "   ...VV.12.        .12.VV...   ",
                "     .VV.VV..........VV.VV.     ",
                "     .VV.V4H2.1HH2.1H3V.VV.     ",
                "     .43.4HH3.V  V.4HH3.43.     ",
                "     .........V  V.........     ",
                "     .1HHHHH2.V  V.1HHHHH2.     ",
                "     .4HH21H3.4HH3.4H21HH3.     ",
                ".........VV..........VV.........",
                "     .12.VV.1HHHHHH2.VV.12.     ",
                "     .VV.43.4HH21HH3.43.VV.     ",
                "   ...VV.......VV.......VV...   ",
                "   .1H3V.1HHH2.VV.1HHH2.V4H2.   ",
                "   .4HH3.4HHH3.43.4HHH3.4HH3.   ",
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
                ".........              .........",
                "        .              .        ",
                "        .              .        ",
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
                "           ..........           ",
                "           .1HHHHHH2.           ",
                "           .4HH21HH3.           ",
                "   *...........VV...........*   ",
                "   .1HHHHH2.12.VV.12.1HHHHH2.   ",
                "   .V1HHHH3.VV.VV.VV.4HHHH2V.   ",
                "   .VV......VV.43.VV......VV.   ",
                "   .VV.1HH2 VV....VV 1HH2.VV.   ",
                "   .43.4H2V V4HHHH3V V1H3.43.   ",
                "   ......VV 4HHHHHH3 VV......   ",
                "        .VV          VV.        ",
                "        .VV          VV.        ",
                "   ......VV          VV......   ",
                "   .1HH2.43          43.1HH2.   ",
                "   .4H2V.              .V1H3.   ",
                "   ...VV.12          12.VV...   ",
                "     .VV.VV          VV.VV.     ",
                "     .VV.V4H2 1HH2 1H3V.VV.     ",
                "     .43.4HH3 V  V 4HH3.43.     ",
                "     .........V  V.........     ",
                "     .1HHHHH2.V  V.1HHHHH2.     ",
                "     .4HH21H3.4HH3.4H21HH3.     ",
                "     ....VV...    ...VV....     ",
                "     .12.VV.1HHHHHH2.VV.12.     ",
                "     .VV.43.4HH21HH3.43.VV.     ",
                "   *..VV.......VV.......VV..*   ",
                "   .1H3V.1HHH2.VV.1HHH2.V4H2.   ",
                "   .4HH3.4HHH3.43.4HHH3.4HH3.   ",
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