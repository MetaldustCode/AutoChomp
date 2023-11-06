using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp
{
    internal class clsGluttony
    {
        // Get Suggestion direction from the dot count
        // Check front, right, left and get largest count
        internal Direction GetSuggestion(List<Direction> lstDirection, Point2d Origin, Direction direction,
                                         List<List<Position>> lstLstCellPos, ref List<Position> lstPos,
                                         ref Direction[,] arrHistory, ref Boolean bolUpdateHistory)
        {
            GamePacman Pacman = clsCommon.GamePacman;
            Pacman.GameLoop.bolBoxSuggestionUpdate = true;

            // Store original values
            List<Direction> _lstDirection = lstDirection.ToList();
            List<List<Position>> _lstLstCellPos = lstLstCellPos.ToList();

            // Copy to not be immutable
            clsDotCount clsDotCount = new clsDotCount();
            lstDirection = lstDirection.ToList();

            // Convert position to cell
            Boolean[,] arrXDots = clsClassTables.arrXDots;
            arrXDots.GetSize(out int col, out int row);
            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();
            Position curPos = clsGetCell.GetCell(Origin, direction);

            List<int> lstCount = new List<int>();

            // Get dot count in each direction
            List<List<Position>> lstLstCount = new List<List<Position>>();
            for (int i = 0; i < lstDirection.Count; i++)
            {
                if (lstDirection[i] == Direction.Up)
                {
                    lstCount.Add(clsDotCount.GetUpCount(arrXDots, col, row, curPos, out List<Position> lstUp));
                    lstLstCount.Add(lstUp);
                }
                if (lstDirection[i] == Direction.Down)
                {
                    lstCount.Add(clsDotCount.GetDownCount(arrXDots, col, row, curPos, out List<Position> lstDown));
                    lstLstCount.Add(lstDown);
                }
                if (lstDirection[i] == Direction.Right)
                {
                    lstCount.Add(clsDotCount.GetRightCount(arrXDots, col, row, curPos, out List<Position> lstRight));
                    lstLstCount.Add(lstRight);
                }
                if (lstDirection[i] == Direction.Left)
                {
                    lstCount.Add(clsDotCount.GetLeftCount(arrXDots, col, row, curPos, out List<Position> lstLeft));
                    lstLstCount.Add(lstLeft);
                }
            }

            // Find largest dot count direction
            int intMax = lstCount.Max();

            for (int i = lstCount.Count - 1; i >= 0; i--)
            {
                if (lstCount[i] != intMax)
                {
                    lstCount.RemoveAt(i);
                    lstDirection.RemoveAt(i);
                    lstLstCellPos.RemoveAt(i);
                    lstLstCount.RemoveAt(i);
                }
            }

            // Get random direction if there are matching counts
            int index = clsRandomizer.RandomInteger(0, lstDirection.Count - 1);

            direction = lstDirection[index];
            Pacman.GameLoop.lstBoxSuggestion = lstLstCount[index];

            clsReg clsReg = new clsReg();

            if (clsReg.GetUseHistory())
            {
                // Prevent sprite from running in circles
                RunningInLoop(ref Pacman, ref arrHistory, ref lstPos, ref lstDirection,
                              _lstLstCellPos, _lstDirection,
                              lstLstCellPos, curPos,
                              ref direction, ref bolUpdateHistory);
            }

            clsCommon.GamePacman = Pacman;

            return direction;
        }

        internal void RunningInLoop(ref GamePacman Pacman, ref Direction[,] arrHistory,
                                    ref List<Position> lstPos, ref List<Direction> lstDirection,
                                    List<List<Position>> _lstLstCellPos, List<Direction> _lstDirection,
                                    List<List<Position>> lstLstCellPos, Position curPos,
                                    ref Direction direction, ref Boolean bolUpdateHistory)
        {
            // _lstLstCellPos = Original Position
            // _lstDirection = Original Direction

            // Prevent Runnning in a loop
            if (arrHistory[curPos.X, curPos.Y] != Direction.None && lstDirection.Count > 1)
            {
                Direction dirPrevious = arrHistory[curPos.X, curPos.Y];

                // Remove previous direction
                if (dirPrevious != Direction.None)
                {
                    for (int i = lstDirection.Count - 1; i >= 0; i--)
                    {
                        if (lstDirection[i] == dirPrevious)
                        {
                            lstDirection.RemoveAt(i);
                            lstLstCellPos.RemoveAt(i);
                        }
                    }
                }

                int rnd = clsRandomizer.RandomInteger(0, lstDirection.Count - 1);
                direction = lstDirection[rnd];
                lstPos = lstLstCellPos[rnd];

                if (arrHistory[curPos.X, curPos.Y] != Direction.None)
                {
                    arrHistory[curPos.X, curPos.Y] = Direction.None;
                    bolUpdateHistory = true;
                }
            }
            else
            {
                // If there is only one direction and it is already in history
                // recover the original options list and
                // pick a new direction

                Direction dirPrevious = arrHistory[curPos.X, curPos.Y];

                if (dirPrevious != Direction.None)
                {
                    if (lstDirection[0] == dirPrevious)
                    {
                        // Recover list
                        lstDirection = _lstDirection.ToList();
                        lstLstCellPos = _lstLstCellPos.ToList();

                        for (int i = lstDirection.Count - 1; i >= 0; i--)
                        {
                            // Remove history
                            if (lstDirection[i] == dirPrevious)
                            {
                                lstDirection.RemoveAt(i);
                                lstLstCellPos.RemoveAt(i);
                            }
                        }
                    }
                }

                int rnd = clsRandomizer.RandomInteger(0, lstDirection.Count - 1);
                direction = lstDirection[rnd];
                lstPos = lstLstCellPos[rnd];

                if (arrHistory[curPos.X, curPos.Y] != direction)
                {
                    arrHistory[curPos.X, curPos.Y] = direction;
                    bolUpdateHistory = true;

                    Pacman.GameLoop.lstLoopDirection.Add(direction);
                    Pacman.GameLoop.lstLoopPosition.Add(curPos);
                }
            }
        }

        internal Direction GetGluttony(Point2d Origin, Direction curDirection,
                                       ref Direction[,] arrHistory,
                                       ref Boolean bolUpdateHistory)
        {
            if (clsClassTables.lstXGridOrigin.Contains(Origin))
            {
                GamePacman Pacman = clsCommon.GamePacman;

                // Clear Existing Position
                Pacman.GameLoop.lstBoxSuggestion.Clear();
                Pacman.GameLoop.lstBoxDirection.Clear();

                Pacman.GameLoop.bolBoxSuggestionUpdate = true;
                Pacman.GameLoop.bolBoxDirectionUpdate = true;

                // Get Current Position from Origin and Direction
                clsGetCurrentCell clsGetCell = new clsGetCurrentCell();
                Position pos = clsGetCell.GetCell(Origin, curDirection);

                // clsCommon.bolDirectionBoxUpdate = true;
                // Get the Cell Count for each Direction that is available
                // lstLstPos = List of Cell position in matching Directions
                GetNextDirection(Origin, curDirection,
                                 out List<Direction> lstDirection,
                                 out List<int> lstDotCount,
                                 out List<List<Position>> lstLstCellPos);

                // Does lstDirection contains the current direction
                // Maintain current direction if there are dots
                // Moves character in straight line
                if (HasCurrentDirection(lstDirection, lstDotCount, curDirection))
                {
                    // Strip out any other directions except for current
                    GetCurrentDirection(curDirection, ref lstDotCount, ref lstDirection, ref lstLstCellPos);

                    // Only 1 direction should be left
                    if (lstDotCount.Count == 1)
                    {
                        // Remove Empty Cells from list
                        // Store Value in Reference
                        if (lstDotCount[0] > 0)
                            Pacman.GameLoop.lstBoxDirection = StripEmpty(lstLstCellPos[0]);
                    }

                    // Clear History
                    arrHistory[pos.X, pos.Y] = Direction.None;
                    bolUpdateHistory = true;
                }
                else
                {
                    // if Not Moving in a stright line check for other directions with Dots to Eat
                    Pacman.GameLoop.lstBoxDirection.Clear();
                    if (GetMaxDirection(ref lstDotCount, ref lstDirection, ref lstLstCellPos))
                    {
                        int index = clsRandomizer.RandomInteger(0, lstDotCount.Count - 1);
                        clsCommon.GamePacman.GameLoop.lstBoxDirection = StripEmpty(lstLstCellPos[index]);
                        curDirection = lstDirection[index];

                        if (arrHistory[pos.X, pos.Y] != Direction.None)
                        {
                            arrHistory[pos.X, pos.Y] = Direction.None;
                            bolUpdateHistory = true;
                        }
                    }
                    else
                    {
                        // If There are no dots in line of site
                        // Set Direction towards the most dots
                        List<Position> lstPos = new List<Position>();

                        curDirection = GetSuggestion(lstDirection, Origin, curDirection, lstLstCellPos,
                                                     ref lstPos, ref arrHistory, ref bolUpdateHistory);

                        if (lstPos.Count > 0)
                            Pacman.GameLoop.lstBoxDirection = StripEmpty(lstPos);
                    }
                }

                clsCommon.GamePacman = Pacman;
            }

            return curDirection;
        }

        internal Boolean HasCurrentDirection(List<Direction> lstDirection,
                                             List<int> lstCount,
                                             Direction direction)
        {
            // Sprite will keep moving forward, if there is a Dot in front
            clsReg clsReg = new clsReg();
            if (clsReg.GetUseForward())
            {
                if (lstDirection.Contains(direction))
                {
                    int intPos = lstDirection.IndexOf(direction);
                    if (lstCount[intPos] > 0)
                        return true;
                }
            }

            return false;
        }

        internal Boolean GetMaxDirection(ref List<int> lstDotCount,
                                         ref List<Direction> lstDirection,
                                         ref List<List<Position>> lstLstCellPos)
        {
            int intMax = lstDotCount.Max();

            for (int i = lstDotCount.Count - 1; i >= 0; i--)
            {
                if (lstDotCount[i] != intMax)
                {
                    lstLstCellPos.RemoveAt(i);
                    lstDirection.RemoveAt(i);
                    lstDotCount.RemoveAt(i);
                }
            }

            if (lstDotCount.Count == 1)
                return true;

            return false;
        }

        internal List<Position> StripEmpty(List<Position> lstPosition)
        {
            Boolean[,] arrXDots = clsClassTables.arrXDots;

            for (int i = lstPosition.Count - 1; i >= 0; i--)
            {
                if (!arrXDots[lstPosition[i].X, lstPosition[i].Y])
                {
                    lstPosition.RemoveAt(i);
                }
            }
            return lstPosition;
        }

        internal void GetCurrentDirection(Direction Direction,
                                          ref List<int> lstDotCount,
                                          ref List<Direction> lstDirection,
                                          ref List<List<Position>> lstLstCellPos)
        {
            for (int i = lstDirection.Count - 1; i >= 0; i--)
            {
                if (lstDirection[i] != Direction)
                {
                    lstLstCellPos.RemoveAt(i);
                    lstDirection.RemoveAt(i);
                    lstDotCount.RemoveAt(i);
                }
            }
        }

        internal void GetNextDirection(Point2d Origin, Direction direction,
                                       out List<Direction> lstDirection,
                                       out List<int> lstCount,
                                       out List<List<Position>> lstLstPos)
        {
            lstCount = new List<int>();
            lstLstPos = new List<List<Position>>();

            if (direction == Direction.None)
                direction = Direction.Right;

            clsGetCurrentCell clsGetCell = new clsGetCurrentCell();

            Position pos = clsGetCell.GetCell(Origin, direction);

            clsGetDirection clsGetDirection = new clsGetDirection();
            lstDirection = clsGetDirection.GetValidDirection(pos, direction, false);

            Boolean[,] arrXDots = clsClassTables.arrXDots;
            arrXDots.GetSize(out int col, out int row);

            for (int i = 0; i < lstDirection.Count; i++)
            {
                Position NewPos = pos;

                List<Position> lstPos = new List<Position>();

                while (HasDirection(NewPos, lstDirection[i]))
                {
                    NewPos = GetNextCell(NewPos, lstDirection[i]);

                    if (col.IsInGrid(row, NewPos.X, NewPos.Y))
                        lstPos.Add(NewPos);
                }

                lstLstPos.Add(new List<Position>(lstPos));
            }

            for (int i = 0; i < lstLstPos.Count; i++)
            {
                lstCount.Add(GetDotCount(lstLstPos[i]));
            }
        }

        internal int GetDotCount(List<Position> lstPos)
        {
            Boolean[,] arrXDots = clsClassTables.arrXDots;
            arrXDots.GetSize(out int col, out int row);

            int intCount = 0;

            for (int i = 0; i < lstPos.Count; i++)
            {
                if (col.IsInGrid(row, lstPos[i].X, lstPos[i].Y))
                {
                    if (arrXDots[lstPos[i].X, lstPos[i].Y])
                        intCount++;
                }
            }
            return intCount;
        }

        internal void DrawingDirectionBoxes(Transaction acTrans, Database acDb,
                                            List<Position> lstPt, int intColor)
        {
            clsEntityDelete clsEntityDelete = new clsEntityDelete();
            clsEntityDelete.DeleteObjectId(acTrans, acDb, clsCommon.GameObjectId.lstObjBoxes);
            clsCommon.GameObjectId.lstObjBoxes.Clear();

            for (int i = 0; i < lstPt.Count; i++)
            {
                clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
                BlockReference acBlkRef = clsPolylineAdd.AddBoxBlock(acTrans, acDb, intColor, "GluttonyBox");

                acBlkRef.Position = lstPt[i].GetOrigin().ToPoint3d();
                clsCommon.GameObjectId.lstObjBoxes.Add(acBlkRef.ObjectId);
            }
        }

        internal void DrawingSuggestionBox(Transaction acTrans, Database acDb,
                                           List<Position> lstPt, int intColor)
        {
            clsEntityDelete clsEntityDelete = new clsEntityDelete();
            clsEntityDelete.DeleteObjectId(acTrans, acDb, clsCommon.GameObjectId.lstObjDirectionBoxes);
            clsCommon.GameObjectId.lstObjDirectionBoxes.Clear();

            for (int i = 0; i < lstPt.Count; i++)
            {
                clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
                BlockReference acBlkRef = clsPolylineAdd.AddBoxBlock(acTrans, acDb, intColor, "DirectionBox");

                acBlkRef.Position = lstPt[i].GetOrigin().ToPoint3d();

                clsCommon.GameObjectId.lstObjDirectionBoxes.Add(acBlkRef.ObjectId);
            }
        }

        internal void DrawingHistoryBox(Transaction acTrans, Database acDb,
                                        List<Position> lstPt, List<Direction> lstDirection, int intColor)
        {
            clsEntityDelete clsEntityDelete = new clsEntityDelete();
            clsEntityDelete.DeleteObjectId(acTrans, acDb, clsCommon.GameObjectId.lstObjHistoryBoxes);
            clsCommon.GameObjectId.lstObjHistoryBoxes.Clear();

            for (int i = 0; i < lstPt.Count; i++)
            {
                String strValue = GetLetter(lstDirection[i]);

                BlockReference acBlkRef = AddBoxTextBlock(acTrans, acDb, lstDirection[i], strValue, intColor, "TextBox_" + strValue);

                Point2d pt = lstPt[i].GetOrigin();
                acBlkRef.MoveEntityXY(acTrans, acDb, pt.X, pt.Y);
                clsCommon.GameObjectId.lstObjHistoryBoxes.Add(acBlkRef.ObjectId);
            }
        }

        internal DBText AddText(Transaction acTrans, Database acDb,
                                string strValue, int intColor)
        {
            clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
            DBText acText = clsPolylineAdd.AddText(acTrans, acDb, intColor, strValue);
            acText.Justify = AttachmentPoint.MiddleCenter;
            acText.Height = 40;

            return acText;
        }

        internal string GetLetter(Direction direction)
        {
            string strValue = String.Empty;

            if (direction == Direction.Up) strValue = "U";
            if (direction == Direction.Down) strValue = "D";
            if (direction == Direction.Right) strValue = "R";
            if (direction == Direction.Left) strValue = "L";

            return strValue;
        }

        internal BlockReference AddBoxTextBlock(Transaction acTrans, Database acDb, Direction direction, string strValue, int intColor, string strBlockName)
        {
            BlockReference rtnValue = null;

            // Open the Block table for read
            BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            if (!acBlkTbl.Has(strBlockName))
            {
                DBText acText = AddText(acTrans, acDb, strValue, intColor);

                clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
                Polyline acPline = clsPolylineAdd.AddBox(acTrans, acDb, new Point2d(), intColor);

                List<Entity> lstEntity = new List<Entity>() { acPline, acText };

                clsBlock clsBlock = new clsBlock();
                rtnValue = clsBlock.CreateBlock(acTrans, acDb, lstEntity, strBlockName, acBlkTbl);
            }
            else
            {
                clsInsertBlock clsInsertBlock = new clsInsertBlock();
                rtnValue = clsInsertBlock.InsertBlock(strBlockName, "0", intColor, acTrans, acDb);
            }

            return rtnValue;
        }

        internal Boolean HasDirection(Position position, Direction direction)
        {
            clsGetDirection clsGetDirection = new clsGetDirection();
            List<Direction> lstDirection = clsGetDirection.GetValidDirection(position, direction, true);

            if (lstDirection.Contains(direction))
                return true;
            return false;
        }

        internal Position GetNextCell(Position pos, Direction direction)
        {
            clsDotCount clsDotCount = new clsDotCount();
            if (direction == Direction.Up) return clsDotCount.GetUp(pos);
            if (direction == Direction.Down) return clsDotCount.GetDown(pos);
            if (direction == Direction.Left) return clsDotCount.GetLeft(pos);
            if (direction == Direction.Right) return clsDotCount.GetRight(pos);
            return null;
        }

        internal int GetColor(Direction direction)
        {
            if (direction == Direction.Up) return 1;
            if (direction == Direction.Down) return 2;
            if (direction == Direction.Left) return 3;
            if (direction == Direction.Right) return 4;
            return 1;
        }
    }
}