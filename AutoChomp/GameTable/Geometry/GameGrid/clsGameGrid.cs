using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGameGrid
    {
        internal void AddPolyline(List<Polyline> lstPolyline, ref List<Polyline> rtnValue)
        {
            for (int i = 0; i < lstPolyline.Count; i++)
                rtnValue.Add(lstPolyline[i]);
        }

        internal void DrawGrid(Transaction acTrans, Database acDb,
                               ref List<Polyline> lstGridBorder, ref List<Polyline> lstGridIsland)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();
            String[,] arrBorder = clsFilter.ConvertToGrid(clsCommon.lstGameMaze[index].lstGridBorder);
            String[,] arrIsland = clsFilter.ConvertToGrid(clsCommon.lstGameMaze[index].lstGridIsland);

            clsLetters clsLetters = new clsLetters();
            List<String> lstBorderLetters = clsLetters.GetGridLetters();

            lstGridBorder = DrawGrid2(acTrans, acDb, lstBorderLetters, arrBorder).ToPolyline();
            lstGridIsland = DrawGrid2(acTrans, acDb, lstBorderLetters, arrIsland).ToPolyline();

            lstGridBorder.SetPolylineColor(3);
            lstGridIsland.SetPolylineColor(new List<int> { 1, 2, 4, 5, 6, 30 });

            lstGridBorder.SetPlineWidth(1.5);
            lstGridIsland.SetPlineWidth(2.0);
        }

        internal List<Entity> DrawGrid2(Transaction acTrans, Database acDb,
                                        List<String> lstGridLetters, String[,] arrGrid)
        {
            List<Entity> rtnValue = new List<Entity>();

            int intColor = 5;

            clsDrawGrid clsDrawGrid = new clsDrawGrid();

            List<ObjectId> lstObjectId = new List<ObjectId>();
            clsFilter clsFilter = new clsFilter();
            arrGrid = clsFilter.Filter(arrGrid, lstGridLetters);

            clsDrawGrid.DrawGrid(acTrans, acDb,
                                 arrGrid, intColor, ref lstObjectId);

            clsPolylineCombine clsPolylineCombine = new clsPolylineCombine();

            {
                List<Polyline> lstCombined = clsPolylineCombine.CombineLines(acTrans, lstObjectId);

                clsPolylineAdd clsLineGridShape = new clsPolylineAdd();
                clsLineGridShape.RemoveLines(lstCombined);

                for (int i = 0; i < lstCombined.Count; i++)
                    rtnValue.Add(lstCombined[i]);

                return rtnValue;
            }
        }

        internal void DrawOpening(Transaction acTrans, Database acDb, BlockTableRecord acBlkTblRec,
                                     ref List<Polyline> lstPolyline, ref List<Hatch> lstHatch)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();
            String[,] arrBorder = clsFilter.ConvertToGrid(clsCommon.lstGameMaze[index].lstGridBorder);

            lstPolyline = DrawGrid2(acTrans, acDb, new List<String> { "<", ">" }, arrBorder).ToPolyline();

            List<ObjectId> lstObjectId = new List<ObjectId>();

            for (int i = 0; i < lstPolyline.Count; i++)
                lstObjectId.Add(lstPolyline[i].ObjectId);

            clsHatch clsHatch = new clsHatch();

            lstHatch = clsHatch.FillInPolyline(acTrans, acDb, acBlkTblRec, lstObjectId, "Solid", 1);

            for (int i = 0; i < lstHatch.Count; i++)
            {
                Hatch acHatch = acTrans.GetObject(lstHatch[i].ObjectId, OpenMode.ForWrite) as Hatch;
                acHatch.ColorIndex = 7;
            }

            for (int i = 0; i < lstPolyline.Count; i++)
            {
                lstPolyline[i].ColorIndex = 7;
                lstPolyline[i].ConstantWidth = 0;
            }
        }
    }
}