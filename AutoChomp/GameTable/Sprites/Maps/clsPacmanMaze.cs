using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsPacmanMaze
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
                "    1HH2 1HHH2    1HHH2 1HH2    ",
                "    V  V V   V    V   V V  V    ",
                "    4HH3 4HHH3    4HHH3 4HH3    ",
                "                                ",
                "    1HH2 12 1HHHHHH2 12 1HH2    ",
                "    4HH3 VV 4HH21HH3 VV 4HH3    ",
                "         VV    VV    VV         ",
                "         V4HH2 VV 1HH3V         ",
                "         V1HH3 43 4HH2V         ",
                "         VV          VV         ",
                "         VV          VV         ",
                "         43          43         ",
                "                                ",
                "         12          12         ",
                "         VV          VV         ",
                "         VV          VV         ",
                "         VV 1HHHHHH2 VV         ",
                "         43 4HH21HH3 43         ",
                "               VV               ",
                "    1HH2 1HHH2 VV 1HHH2 1HH2    ",
                "    4H2V 4HHH3 43 4HHH3 V1H3    ",
                "      VV                VV      ",
                "      VV 12 1HHHHHH2 12 VV      ",
                "      43 VV 4HH21HH3 VV 43      ",
                "         VV    VV    VV         ",
                "    1HHHH34HH2 VV 1HH34HHHH2    ",
                "    4HHHHHHHH3 43 4HHHHHHHH3    ",
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
                "  5TTTTTTTTTTTTEFTTTTTTTTTTTT6  ",
                "  L            VV            R  ",
                "  L            VV            R  ",
                "  L            VV            R  ",
                "  L            43            R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  8BBBB2                1BBBB7  ",
                "       L                R       ",
                "       L                R       ",
                "       L    1BO<>PB2    R       ",
                "  MTTTT3    R      L    4TTTTN  ",
                "            R      L            ",
                "  PBBBB2    R      L    1BBBBO  ",
                "       L    4TTTTTT3    R       ",
                "       L                R       ",
                "       L                R       ",
                "  5TTTT3                4TTTT6  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  L                          R  ",
                "  YH2                      1HI  ",
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
                "   ............  ............   ",
                "   .    .     .  .     .    .   ",
                "   .    .     .  .     .    .   ",
                "   .    .     .  .     .    .   ",
                "   ..........................   ",
                "   .    .  .        .  .    .   ",
                "   .    .  .        .  .    .   ",
                "   ......  ....  ....  ......   ",
                "        .     .  .     .        ",
                "        .     .  .     .        ",
                "        .  ..........  .        ",
                "        .  .        .  .        ",
                "        .  .        .  .        ",
                "............        ............",
                "        .  .        .  .        ",
                "        .  .        .  .        ",
                "        .  ..........  .        ",
                "        .  .        .  .        ",
                "        .  .        .  .        ",
                "   ............  ............   ",
                "   .    .     .  .     .    .   ",
                "   .    .     .  .     .    .   ",
                "   ...  ................  ...   ",
                "     .  .  .        .  .  .     ",
                "     .  .  .        .  .  .     ",
                "   ......  ....  ....  ......   ",
                "   .          .  .          .   ",
                "   .          .  .          .   ",
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
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "........                ........",
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
                "   ............  ............   ",
                "   .    .     .  .     .    .   ",
                "   *    .     .  .     .    *   ",
                "   .    .     .  .     .    .   ",
                "   ..........................   ",
                "   .    .  .        .  .    .   ",
                "   .    .  .        .  .    .   ",
                "   ......  ....  ....  ......   ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "        .              .        ",
                "   ............  ............   ",
                "   .    .     .  .     .    .   ",
                "   .    .     .  .     .    .   ",
                "   *..  .......  .......  ..*   ",
                "     .  .  .        .  .  .     ",
                "     .  .  .        .  .  .     ",
                "   ......  ....  ....  ......   ",
                "   .          .  .          .   ",
                "   .          .  .          .   ",
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