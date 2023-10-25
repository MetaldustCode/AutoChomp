using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMsPacmanMaze4
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
                "    12 1HH2 1HHHHHH2 1HH2 12    ",
                "    VV V  V V1HHHH2V V  V VV    ",
                "    VV 4HH3 VV    VV 4HH3 VV    ",
                "    VV      VV 12 VV      VV    ",
                "    V4H2 12 VV VV VV 12 1H3V    ",
                "    4HH3 VV 43 VV 43 VV 4HH3    ",
                "         VV    VV    VV         ",
                "      1HH34HH2 VV 1HH34HH2      ",
                "      4HH21HH3 43 4HH21HH3      ",
                "         VV          VV         ",
                "         VV          VV         ",
                "         43          43         ",
                "                                ",
                "         12          12         ",
                "         VV          VV         ",
                "         VV          VV         ",
                "         V4HH2 12 1HH3V         ",
                "      12 4HHH3 VV 4HHH3 12      ",
                "      VV       VV       VV      ",
                "      V4HH2 12 VV 12 1HH3V      ",
                "      4HHH3 VV 43 VV 4HHH3      ",
                "            VV    VV            ",
                "    1HH2 12 V4HHHH3V 12 1HH2    ",
                "    V1H3 VV 4HHHHHH3 VV 4H2V    ",
                "    VV   VV          VV   VV    ",
                "    VV 1H34HH2    1HH34H2 VV    ",
                "    43 4HHHHH3    4HHHHH3 43    ",
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
                "  5TTTTTTTTTTTTTTTTTTTTTTTTTT6  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  8B2                      1B7  ",
                "    L                      R    ",
                "    L                      R    ",
                "  MT3 12    1BO<>PB2    12 4TN  ",
                "      VV    R      L    VV      ",
                "  AHHH3V    R      L    V4HHHS  ",
                "  QHHH2V    R      L    V1HHHW  ",
                "      VV    4TTTTTT3    VV      ",
                "  PB2 43                43 1BO  ",
                "    L                      R    ",
                "    L                      R    ",
                "    L                      R    ",
                "    L                      R    ",
                "  5T3                      4T6  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L            12            R  ",
                "  L            VV            R  ",
                "  L            VV            R  ",
                "  8BBBBBBBBBBBBCDBBBBBBBBBBBB7  ",
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
                "   ..........................   ",
                "   .12.1HH2.1HHHHHH2.1HH2.12.   ",
                "   .VV.V  V.V1HHHH2V.V  V.VV.   ",
                "   .VV.4HH3.VV....VV.4HH3.VV.   ",
                "   .VV......VV.12.VV......VV.   ",
                "   .V4H2.12.VV.VV.VV.12.1H3V.   ",
                "   .4HH3.VV.43.VV.43.VV.4HH3.   ",
                "   ......VV....VV....VV......   ",
                "     .1HH34HH2.VV.1HH34HH2.     ",
                "     .4HH21HH3.43.4HH21HH3.     ",
                "     ....VV..........VV....     ",
                "     .  .VV.        .VV.  .     ",
                "......  .43.        .43.  ......",
                "        ....        ....        ",
                "        .12.        .12.        ",
                "......  .VV.        .VV.  ......",
                "     .  .VV..........VV.  .     ",
                "     ....V4HH2.12.1HH3V....     ",
                "     .12.4HHH3.VV.4HHH3.12.     ",
                "     .VV.......VV.......VV.     ",
                "     .V4HH2.12.VV.12.1HH3V.     ",
                "     .4HHH3.VV.43.VV.4HHH3.     ",
                "   .........VV....VV.........   ",
                "   .1HH2.12.V4HHHH3V.12.1HH2.   ",
                "   .V1H3.VV.4HHHHHH3.VV.4H2V.   ",
                "   .VV...VV..........VV...VV.   ",
                "   .VV.1H34HH2.  .1HH34H2.VV.   ",
                "   .43.4HHHHH3.  .4HHHHH3.43.   ",
                "   ............  ............   ",
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
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "     .                    .     ",
                "......                    ......",
                "                                ",
                "                                ",
                "......                    ......",
                "     .                    .     ",
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
                "   ..........................   ",
                "   .12.1HH2.1HHHHHH2.1HH2.12.   ",
                "   *VV.V  V.V1HHHH2V.V  V.VV*   ",
                "   .VV.4HH3.VV....VV.4HH3.VV.   ",
                "   .VV......VV.12.VV......VV.   ",
                "   .V4H2.12.VV.VV.VV.12.1H3V.   ",
                "   .4HH3.VV.43.VV.43.VV.4HH3.   ",
                "   ......VV....VV....VV......   ",
                "     .1HH34HH2 VV 1HH34HH2.     ",
                "     .4HH21HH3 43 4HH21HH3.     ",
                "     ....VV          VV....     ",
                "        .VV          VV.        ",
                "        .43          43.        ",
                "        .              .        ",
                "        .12          12.        ",
                "        .VV          VV.        ",
                "        .VV          VV.        ",
                "     ....V4HH2 12 1HH3V....     ",
                "     .12.4HHH3 VV 4HHH3.12.     ",
                "     .VV....   VV   ....VV.     ",
                "     .V4HH2.12 VV 12.1HH3V.     ",
                "     .4HHH3.VV 43 VV.4HHH3.     ",
                "   .........VV    VV.........   ",
                "   .1HH2.12.V4HHHH3V.12.1HH2.   ",
                "   .V1H3.VV.4HHHHHH3.VV.4H2V.   ",
                "   .VV...VV..........VV...VV.   ",
                "   *VV.1H34HH2.  .1HH34H2.VV*   ",
                "   .43.4HHHHH3.  .4HHHHH3.43.   ",
                "   ............  ............   ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }
    }
}