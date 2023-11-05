using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsGenerateTables
    {
        private readonly double Cell = clsGridValues.Cell;
        private readonly double Middle = clsGridValues.Middle;

        internal void GenerateTable()
        {
            GenerateGridList();
            GenerateXTunnelList();
            GenerateXGrid();
            GenerateXTunnel();
            GenerateStartPositon();
            GenerateDirection();
            GenerateGridListAndDirectionList();
            GenerateXDots();
            GenerateXPower();
            GenerateAStarNumber();
        }

        internal void GenerateXDots()
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            clsClassTables.arrXDots = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstDots, new List<String> { "." });
        }

        internal void GenerateAStarNumber()
        {
            if (clsClassTables.arrXDots != null)
            {
                clsClassTables.arrXDots.GetSize(out int col, out int row);

                clsClassTables.arrAStarNum = new int[col, row];
                clsClassTables.arrAStarText = new DBText[col, row];
            }
        }

        internal void GenerateHistory()
        {
            if (clsClassTables.arrXDots != null)
            {
                clsClassTables.arrXDots.GetSize(out int col, out int row);

                clsCommon.GamePacman.GameLoop.arrHistory = new Direction[col, row];

                List<GameGhost> lstGhosts = clsCommon.lstGameGhost;

                for (int i = 0; i < lstGhosts.Count; i++)
                {
                    lstGhosts[i].arrHistory = new Direction[col, row];
                    lstGhosts[i].bolUpdateHistory = false;
                }
            }
        }

        internal void GenerateXPower()
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            clsClassTables.arrXPower = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstDots, new List<String> { "*" });
        }

        internal void GenerateXGrid()
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            clsClassTables.arrXGridPath = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstGridPath, new List<String> { "." });
        }

        internal void GetSize(out int col, out int row)
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            bool[,] arrXGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstGridPath, new List<String> { "." });

            arrXGrid.GetSize(out col, out row);
        }

        internal void GenerateGridList()
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            clsFilter clsFilter = new clsFilter();

            Boolean[,] arrGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstGridPath, new List<String> { "." });

            int col = arrGrid.GetLength(0);
            int row = arrGrid.GetLength(1);

            List<Point2d> lstMiddle = new List<Point2d>();

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arrGrid[c, r] == true)
                        lstMiddle.Add(new Point2d((c * Cell) + Middle, (r * Cell) + Middle));
                }
            }

            clsClassTables.lstXGridOrigin = lstMiddle;
        }

        internal void GenerateXTunnelList()
        {
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();
            clsFilter clsFilter = new clsFilter();

            Boolean[,] arrGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstTunnelPath, new List<String> { "." });

            int col = arrGrid.GetLength(0);
            int row = arrGrid.GetLength(1);

            List<Position> lstTunnelPosition = new List<Position>();

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arrGrid[c, r] == true)
                        lstTunnelPosition.Add(new Position(c, r));
                }
            }

            List<String> lstString = new List<string>();
            List<Point2d> lstPoint = new List<Point2d>();

            for (int i = 0; i < lstTunnelPosition.Count; i++)
            {
                string strPosition = String.Format("{0},{1}", lstTunnelPosition[i].X, lstTunnelPosition[i].Y);
                lstString.Add(strPosition);
                lstPoint.Add(lstTunnelPosition[i].GetOrigin());
            }

            clsClassTables.lstXTunnelString = lstString;
            clsClassTables.lstXTunnelPosition = lstTunnelPosition;
            clsClassTables.lstXTunnelOrigin = lstPoint;
        }

        internal void GenerateXTunnel()
        {
            clsFilter clsFilter = new clsFilter();
            clsReg clsReg = new clsReg();
            int index = clsReg.GetMazeIndex();

            Boolean[,] arrXGrid = clsFilter.ConvertToBooleanGrid(clsCommon.lstGameMaze[index].lstTunnelPath, new List<String> { "." });

            clsClassTables.arrXTunnel = arrXGrid;
        }

        internal void GenerateDirection()
        {
            clsGetDirection clsCharacterDirection = new clsGetDirection();
            Boolean[,] arrXGrid = clsClassTables.arrXGridPath;

            arrXGrid.GetSize(out int col, out int row);

            List<Direction>[,] arrXDirection = new List<Direction>[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                    arrXDirection[x, y] = clsCharacterDirection.GetPossibleDirection(new Position(x, y));
            }

            clsClassTables.arrXDirection = arrXDirection;
        }

        internal void GenerateGridListAndDirectionList()
        {
            bool[,] arrXGrid = clsClassTables.arrXGridPath;
            List<Direction>[,] arrXDir = clsClassTables.arrXDirection;
            List<Point2d> lstOrigin = clsClassTables.lstXGridOrigin;

            arrXGrid.GetSize(out int col, out int row);

            List<Position> lstPosition = new List<Position>();
            List<List<Direction>> lstLstDirection = new List<List<Direction>>();

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arrXGrid[c, r])
                    {
                        lstPosition.Add(new Position(c, r));
                        lstLstDirection.Add(arrXDir[c, r]);
                    }
                }
            }

            clsClassTables.lstXGridPosition = lstPosition;
            clsClassTables.lstLstXDirection = lstLstDirection;

            List<String> lstXGridPositionString = new List<string>();
            for (int i = 0; i < lstPosition.Count; i++)
                lstXGridPositionString.Add(string.Format("{0},{1}", lstPosition[i].X, lstPosition[i].Y));

            List<String> lstXGridOriginString = new List<string>();
            for (int i = 0; i < lstOrigin.Count; i++)
                lstXGridOriginString.Add(string.Format("{0},{1}", lstOrigin[i].X, lstOrigin[i].Y));

            clsClassTables.lstXGridPositionString = lstXGridPositionString;
            clsClassTables.lstXGridOriginString = lstXGridOriginString;
        }

        internal void GenerateStartPositon()
        {
            Boolean[,] arrXGrid = clsClassTables.arrXGridPath.CloneArray();
            Boolean[,] arrXTunnel = clsClassTables.arrXTunnel.CloneArray();

            arrXGrid.GetSize(out int col, out int row);

            List<Point2d> lstXStartupOrigin = new List<Point2d>();
            List<Position> lstXStartupPosition = new List<Position>();

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (arrXTunnel[x, y])
                        arrXGrid[x, y] = false;
                }
            }

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (arrXGrid[x, y])
                    {
                        Position pos1 = new Position(x, y);
                        Point2d pt2d = pos1.GetOrigin();
                        lstXStartupPosition.Add(pos1);
                        lstXStartupOrigin.Add(pt2d);
                    }
                }
            }

            clsClassTables.arrXStartUp = arrXGrid;
            clsClassTables.lstXStartupOrigin = lstXStartupOrigin;
            clsClassTables.lstXStartupPosition = lstXStartupPosition;
        }
    }
}