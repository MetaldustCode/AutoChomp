using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp
{
    internal class clsPolylineCombine
    {
        internal List<List<Polyline>> Combine(List<Polyline> lstPline)
        {
            List<Point2d> lstStartPoint = new List<Point2d>();
            List<Point2d> lstEndPoint = new List<Point2d>();

            for (int i = 0; i < lstPline.Count; i++)
            {
                List<Point2d> lstPoint = lstPline[i].GetPoint();
                lstStartPoint.Add(lstPoint[0]);
                lstEndPoint.Add(lstPoint[1]);
            }

            List<List<int>> lstLstMatch = new List<List<int>>();

            for (int i = 0; i < lstStartPoint.Count; i++)
            {
                List<int> lstMatch = new List<int> { i };

                for (int k = 0; k < lstStartPoint.Count; k++)
                {
                    if (i != k)
                    {
                        if (IsMatched(lstStartPoint[i], lstStartPoint[k], lstEndPoint[k]) ||
                            IsMatched(lstEndPoint[i], lstStartPoint[k], lstEndPoint[k]))
                            lstMatch.Add(k);
                    }
                }

                lstLstMatch.Add(lstMatch);
            }

            List<List<int>> lstLstFound = new List<List<int>>();

            while (true)
            {
                if (lstLstMatch.Count == 0) break;

                List<int> lstMatchRow = new List<int>();
                List<int> lstCheckedRow = new List<int>();

                for (int i = 0; i < lstLstMatch.Count; i++)
                {
                    if (i == 0)
                    {
                        lstMatchRow = new List<int>(lstLstMatch[i]);
                        lstCheckedRow.Add(i);
                    }
                    else
                    {
                        List<int> lstRow = new List<int>();
                        if (HasValue(lstMatchRow, lstLstMatch, lstRow))
                        {
                            for (int m = 0; m < lstRow.Count; m++)
                            {
                                for (int j = 0; j < lstLstMatch[lstRow[m]].Count; j++)
                                {
                                    int intValue = lstLstMatch[lstRow[m]][j];
                                    lstMatchRow.Add(intValue);
                                }
                                lstCheckedRow.Add(lstRow[m]);
                            }

                            lstMatchRow = lstMatchRow.Distinct().ToList();
                            lstRow.Add(i);
                        }
                    }
                }

                for (int i = lstLstMatch.Count - 1; i >= 0; i--)
                {
                    if (lstCheckedRow.Contains(i))
                        lstLstMatch.RemoveAt(i);
                }

                lstLstFound.Add(lstMatchRow);
            }

            List<List<Polyline>> lstLstPline = GroupPline(lstLstFound, lstPline);

            return lstLstPline;
        }

        internal List<List<Polyline>> GroupPline(List<List<int>> lstLstFound, List<Polyline> lstPline)
        {
            List<List<Polyline>> lstLstPline = new List<List<Polyline>>();

            for (int i = 0; i < lstLstFound.Count; i++)
            {
                List<int> lstFound = new List<int>(lstLstFound[i]);

                List<Polyline> lstTemp = new List<Polyline>();

                for (int j = 0; j < lstFound.Count; j++)
                {
                    int intPline = lstFound[j];

                    Polyline pLine = lstPline[intPline];

                    lstTemp.Add(pLine);
                }

                lstLstPline.Add(lstTemp);
            }

            return lstLstPline;
        }

        internal void FindMatch(int intStart, List<List<int>> lstLstMatch, List<int> rtnValue)
        {
            if (!rtnValue.Contains(intStart))
            {
                for (int i = 0; i < lstLstMatch.Count; i++)
                {
                    if (lstLstMatch[i].Contains(intStart))
                    {
                        for (int j = 0; j < lstLstMatch[i].Count; j++)
                            rtnValue.Add(lstLstMatch[i][j]);

                        for (int k = 0; k < lstLstMatch[i].Count; k++)
                        {
                            FindMatch(lstLstMatch[i][k], lstLstMatch, rtnValue);
                        }
                    }
                }
            }
        }

        internal Boolean HasValue(List<int> lstValue1, List<int> lstValue2)
        {
            for (int i = 0; i < lstValue1.Count; i++)
            {
                if (lstValue2.Contains(lstValue1[i]))
                    return true;
            }
            return false;
        }

        internal Boolean HasValue(List<int> lstValue1, List<List<int>> lstLstValue2, List<int> lstRow)
        {
            Boolean bolFound = false;
            for (int i = 0; i < lstLstValue2.Count; i++)
            {
                if (HasValue(lstLstValue2[i], lstValue1))
                {
                    lstRow.Add(i);
                    bolFound = true;
                }
            }

            return bolFound;
        }

        internal Boolean IsMatched(Point2d ptMatch, Point2d ptStartPoint, Point2d ptEndPoint)
        {
            if (ptMatch == ptStartPoint || ptMatch == ptEndPoint)
            {
                return true;
            }
            return false;
        }

        internal List<Polyline> CombineLines(Transaction acTrans, List<ObjectId> lstHandle)
        {
            List<Polyline> lstPline = new List<Polyline>();

            for (int i = 0; i < lstHandle.Count; i++)
            {
                Polyline acPline = acTrans.GetObject(lstHandle[i], OpenMode.ForWrite) as Polyline;
                lstPline.Add(acPline);
            }

            clsPolylineCombine clsJoin = new clsPolylineCombine();
            List<List<Polyline>> lstLstPolyline = clsJoin.Combine(lstPline);

            List<Polyline> lstCombined = new List<Polyline>();

            for (int i = 0; i < lstLstPolyline.Count; i++)
            {
                clsPolylineJoin clsJoinPolyline = new clsPolylineJoin();

                List<Polyline> lstTemp = (clsJoinPolyline.JoinPolyline(acTrans, lstLstPolyline[i]));

                for (int k = 0; k < lstTemp.Count; k++)
                    lstCombined.Add(lstTemp[k]);
            }

            return lstCombined;
        }

        internal List<Polyline> HandleToPolyline(Transaction acTrans, List<ObjectId> lstHandle)
        {
            List<Polyline> lstPline = new List<Polyline>();

            for (int i = 0; i < lstHandle.Count; i++)
            {
                Polyline acPline = acTrans.GetObject(lstHandle[i], OpenMode.ForWrite) as Polyline;
                lstPline.Add(acPline);
            }

            return lstPline;
        }
    }
}