using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsMsPacmanMaze3
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
                "    1HHHHH2    12    1HHHHH2    ",
                "    V1HHHH3    VV    4HHHH2V    ",
                "    VV         VV         VV    ",
                "    43 12 1HH2 VV 1HH2 12 43    ",
                "       VV V  V VV V  V VV       ",
                "       VV 4HH3 43 4HH3 VV       ",
                "       VV              VV       ",
                "       V4H2 1HHHHHH2 1H3V       ",
                "    12 4HH3 4HHHHHH3 4HH3 12    ",
                "    VV                    VV    ",
                "    V4H2 12          12 1H3V    ",
                "    4HH3 VV          VV 4HH3    ",
                "         VV          VV         ",
                "    12 1H3V          V4H2 12    ",
                "    VV 4HH3          4HH3 VV    ",
                "    VV                    VV    ",
                "    V4H2 1HHH2 12 1HHH2 1H3V    ",
                "    4HH3 V1HH3 VV 4HH2V 4HH3    ",
                "         VV    VV    VV         ",
                "      12 VV 1HH34HH2 VV 12      ",
                "      VV 43 4HHHHHH3 43 VV      ",
                "      VV                VV      ",
                "    1H3V       12       V4H2    ",
                "    4HH3       VV       4HH3    ",
                "               VV               ",
                "    1HH2    1HH34HH2    1HH2    ",
                "    4HH3    4HHHHHH3    4HH3    ",
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
                "  5TTTTTTTTTEFTTTTEFTTTTTTTTT6  ",
                "  L         VV    VV         R  ",
                "  L         VV    VV         R  ",
                "  L         43    43         R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  YHH2                    1HHI  ",
                "  QHH3                    4HHW  ",
                "                                ",
                "  [                          ]  ",
                "  L                          R  ",
                "  L         1BO<>PB2         R  ",
                "  L         R      L         R  ",
                "  L         R      L         R  ",
                "  L         R      L         R  ",
                "  L         4TTTTTT3         R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  YH2                      1HI  ",
                "  XH3                      4HJ  ",
                "  L                          R  ",
                "  L      1HHH2    1HHH2      R  ",
                "  L      V1HH3    4HH2V      R  ",
                "  L      VV          VV      R  ",
                "  L      VV          VV      R  ",
                "  L      VV          VV      R  ",
                "  L      VV          VV      R  ",
                "  8BBBBBBCDBBBBBBBBBBCDBBBBBB7  ",
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
                "   .........  ....  .........   ",
                "   .1HHHHH2.  .12.  .1HHHHH2.   ",
                "   .V1HHHH3.  .VV.  .4HHHH2V.   ",
                "   .VV.........VV.........VV.   ",
                "   .43.12.1HH2.VV.1HH2.12.43.   ",
                "   ....VV.V  V.VV.V  V.VV....   ",
                "      .VV.4HH3.43.4HH3.VV.      ",
                "      .VV..............VV.      ",
                ".......V4H2.1HHHHHH2.1H3V.......",
                "   .12.4HH3.4HHHHHH3.4HH3.12.   ",
                "   .VV....................VV.   ",
                "   .V4H2.12.        .12.1H3V.   ",
                "   .4HH3.VV.        .VV.4HH3.   ",
                "   ......VV.        .VV......   ",
                "   .12.1H3V.        .V4H2.12.   ",
                "   .VV.4HH3.        .4HH3.VV.   ",
                "   .VV....................VV.   ",
                "   .V4H2.1HHH2.12.1HHH2.1H3V.   ",
                "   .4HH3.V1HH3.VV.4HH2V.4HH3.   ",
                "   ......VV....VV....VV......   ",
                "     .12.VV.1HH34HH2.VV.12.     ",
                "     .VV.43.4HHHHHH3.43.VV.     ",
                "   ...VV................VV...   ",
                "   .1H3V.     .12.     .V4H2.   ",
                "   .4HH3.     .VV.     .4HH3.   ",
                "   ......  ....VV....  ......   ",
                "   .1HH2.  .1HH34HH2.  .1HH2.   ",
                "   .4HH3.  .4HHHHHH3.  .4HH3.   ",
                "   ......  ..........  ......   ",
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
                "...                          ...",
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
                "   .........  ....  .........   ",
                "   .1HHHHH2.  .12.  .1HHHHH2.   ",
                "   *V1HHHH3.  .VV.  .4HHHH2V*   ",
                "   .VV.........VV.........VV.   ",
                "   .43.12.1HH2.VV.1HH2.12.43.   ",
                "   ....VV.V  V.VV.V  V.VV....   ",
                "      .VV.4HH3.43.4HH3.VV.      ",
                "      .VV..............VV.      ",
                "   ....V4H2 1HHHHHH2 1H3V....   ",
                "   .12 4HH3 4HHHHHH3 4HH3 12.   ",
                "   .VV                    VV.   ",
                "   .V4H2 12          12 1H3V.   ",
                "   .4HH3 VV          VV 4HH3.   ",
                "   .     VV          VV     .   ",
                "   .12 1H3V          V4H2 12.   ",
                "   .VV 4HH3          4HH3 VV.   ",
                "   .VV                    VV.   ",
                "   .V4H2 1HHH2 12 1HHH2 1H3V.   ",
                "   .4HH3 V1HH3 VV 4HH2V 4HH3.   ",
                "   ......VV....VV....VV......   ",
                "     .12.VV.1HH34HH2.VV.12.     ",
                "     .VV.43.4HHHHHH3.43.VV.     ",
                "   *..VV................VV..*   ",
                "   .1H3V.     .12.     .V4H2.   ",
                "   .4HH3.     .VV.     .4HH3.   ",
                "   ......  ....VV....  ......   ",
                "   .1HH2.  .1HH34HH2.  .1HH2.   ",
                "   .4HH3.  .4HHHHHH3.  .4HH3.   ",
                "   ......  ..........  ......   ",
                "                                ",
                "                                ",
                "                                "
            };
            a.Reverse();
            return a;
        }
    }
}