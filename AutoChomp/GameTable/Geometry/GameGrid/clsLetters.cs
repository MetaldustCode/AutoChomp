using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsLetters
    {
        internal List<String> GetGridLetters()
        {
            List<String> lstGrid = new List<String>
            {
                "B", "E", "F", "H", "I", "J", "L",
                "M", "N", "O", "P", "R", "T", "V","[","]",
                "X", "Y", "C", "D",
                "1", "2", "3", "4",
                "5", "6", "7", "8",
                "A", "S", "Q", "W" }; // TL TR BL BR Top Sharp Corner
            return lstGrid;
        }
    }
}