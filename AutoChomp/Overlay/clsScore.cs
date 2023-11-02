using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsScore
    {
        private static readonly double Cell = clsGridValues.Cell;
        private static readonly double Middle = clsGridValues.Middle;

        internal void ClearScore()
        {
            clsScoreValues.lstElements = new List<DBText>();
            clsScoreValues.intPacmanScore = 0;
            clsScoreValues.intRedScore = 0;
            clsScoreValues.intBlueScore = 0;
            clsScoreValues.intPinkScore = 0;
            clsScoreValues.intOrangeScore = 0;
        }

        internal void ShowValue(Transaction acTrans, Database acDb)
        {
            clsReg clsReg = new clsReg();

            ClearText(acTrans, acDb);

            if (clsReg.GetShowHighScore())
            {
                // Convert position to cell
                Boolean[,] arrXDots = clsClassTables.arrXDots;
                arrXDots.GetSize(out int col, out int row);

                double X = 100;
                UpdateHighScore(clsScoreValues.intPacmanScore);

                //AddText(acTrans, acDb, "HI-SCORE", 2, (col * Cell) + Y, (row * Cell)   -600 + X);
                string strValue = String.Format("{0} {1} / {2}", "", clsScoreValues.intPacmanScore, _intHighScore);

                double dblWidth = (col * Cell) / 2;
                double dblHight = (row * Cell) - 160;

                clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
                List<Point2d> lstPoints = new List<Point2d>() { new Point2d(), new Point2d(0, dblWidth) };
                //clsPolylineAdd.AddLine(acTrans, acDb, lstPoints, 3);
                AddText(acTrans, acDb, strValue, 4, dblWidth, dblHight);
            }
        }

        internal static int _intHighScore;

        internal void UpdateHighScore(int intScore)
        {
            clsReg clsReg = new clsReg();
            clsReg.GetHighScore(out int intHighScore);

            if (intHighScore < intScore)
            {
                clsReg.SetHighScore(intScore.ToString());
                _intHighScore = intScore;
            }
            else
                _intHighScore = intHighScore;
        }

        internal void ClearText(Transaction acTrans, Database acDb)
        {
            if (clsScoreValues.lstElements == null)
                ClearScore();
            else
            {
                List<DBText> lstText = clsScoreValues.lstElements;

                for (int i = 0; i < lstText.Count; i++)
                {
                    if (lstText[i].IsObjectIdValid(acDb))
                    {
                        DBText dbText = acTrans.GetObject(lstText[i].ObjectId, OpenMode.ForWrite) as DBText;

                        lstText[i].Erase();
                    }
                }
                clsScoreValues.lstElements.Clear();
            }
        }

        internal DBText AddText(Transaction acTrans, Database acDb,
                                string strValue, int intColor,
                                double X, double Y)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            DBText acText = clsPolylineAdd.AddText(acTrans, acDb, intColor, strValue);
            acText.Justify = AttachmentPoint.BaseCenter;
            clsScoreValues.lstElements.Add(acText);
            acText.Height = 100;
            acText.MoveEntityXY(X, Y);

            return acText;
        }
    }
}