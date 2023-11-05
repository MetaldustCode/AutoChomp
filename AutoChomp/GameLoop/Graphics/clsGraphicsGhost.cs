using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoChomp.Gameloop.Graphics
{
    internal class clsGraphicsGhost
    {
        // "Red", "Pink", "Blue", "Orange"

        internal void UpdateGhostGraphics(Transaction acTrans, Database acDb, ref List<GameGhost> lstGhost)
        {
            UpdateGhostPosition(acTrans, acDb, ref lstGhost);
            UpdateGhostVisibility(acTrans, acDb, ref lstGhost);
        }

        internal void SetStartPostitionGhost(Transaction acTrans, Database acDb, ref List<GameGhost> lstGhost)
        {
            List<GameGhost> lstInHouse = new List<GameGhost>();

            Point2d ptRed = new Point2d();
            Point2d ptPink = new Point2d();
            Point2d ptBlue = new Point2d();
            Point2d ptOrange = new Point2d();

            clsStartPosition clsStartPosition = new clsStartPosition();
            clsStartPosition.GetStartPosition(ref ptRed, ref ptPink, ref ptBlue, ref ptOrange);

            for (int i = 0; i < lstGhost.Count; i++)
            {
                GameGhost Ghost = lstGhost[i];
                //Ghost.StartLocation = StartLocation.Outside;
                //Ghost.Direction = Direction.Right;
                if (Ghost.StartLocation == StartLocation.Outside)
                {
                    Ghost.ptOrigin = ptRed;
                    Ghost.HouseState = HouseState.OutHouse;
                }

                if (Ghost.StartLocation == StartLocation.Left)
                {
                    Ghost.ptOrigin = ptPink;
                    Ghost.HouseState = HouseState.InHouse;
                    lstInHouse.Add(Ghost);
                }

                if (Ghost.StartLocation == StartLocation.Middle)
                {
                    Ghost.ptOrigin = ptBlue;
                    Ghost.HouseState = HouseState.InHouse;
                    lstInHouse.Add(Ghost);
                }

                if (Ghost.StartLocation == StartLocation.Right)
                {
                    Ghost.ptOrigin = ptOrange;
                    Ghost.HouseState = HouseState.InHouse;
                    lstInHouse.Add(Ghost);
                }

                lstGhost[i] = Ghost;

                for (int k = 0; k < lstGhost[i].lstStandard.Count; k++)
                {
                    if (lstGhost[i].lstStandard[k] != null)
                    {
                        if (lstGhost[i].lstStandard[k].IsObjectIdValid(acDb))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(lstGhost[i].lstStandard[k].ObjectId, OpenMode.ForWrite) as BlockReference;
                            Update(acBlkRef, lstGhost[i].ptOrigin.ToPoint3d());
                        }
                    }
                }

                for (int k = 0; k < lstGhost[i].lstAfraid.Count; k++)
                {
                    if (lstGhost[i].lstAfraid[k] != null)
                    {
                        if (lstGhost[i].lstAfraid[k].IsObjectIdValid(acDb))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(lstGhost[i].lstAfraid[k].ObjectId, OpenMode.ForWrite) as BlockReference;
                            Update(acBlkRef, lstGhost[i].ptOrigin.ToPoint3d());
                        }
                    }
                }

                for (int k = 0; k < lstGhost[i].lstDead.Count; k++)
                {
                    if (lstGhost[i].lstDead[k] != null)
                    {
                        if (lstGhost[i].lstDead[k].IsObjectIdValid(acDb))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(lstGhost[i].lstDead[k].ObjectId, OpenMode.ForWrite) as BlockReference;
                            Update(acBlkRef, lstGhost[i].ptOrigin.ToPoint3d());
                        }
                    }
                }

                for (int k = 0; k < lstGhost[i].lstAlternate.Count; k++)
                {
                    if (lstGhost[i].lstAlternate[k] != null)
                    {
                        if (lstGhost[i].lstAlternate[k].IsObjectIdValid(acDb))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(lstGhost[i].lstAlternate[k].ObjectId, OpenMode.ForWrite) as BlockReference;
                            Update(acBlkRef, lstGhost[i].ptOrigin.ToPoint3d());
                        }
                    }
                }
            }

            // Store Ghose In House
            clsCommon.GameGhostCommon.lstInHouse = lstInHouse;
        }

        internal Boolean UpdateGhostPosition(Transaction acTrans, Database acDb, ref List<GameGhost> lstGhost)
        {
            Boolean rtnValue = false;

            for (int i = 0; i < lstGhost.Count; i++)
            {
                string strSuffix = "";
                if (GetGhostStatus(lstGhost[i], ref strSuffix))
                {
                    if (UpdateGhostPosition(acTrans, acDb, lstGhost[i].lstStandard, lstGhost[i].ptOrigin, strSuffix)) rtnValue = true;
                    if (UpdateGhostPosition(acTrans, acDb, lstGhost[i].lstAlternate, lstGhost[i].ptOrigin, strSuffix)) rtnValue = true;
                    if (UpdateGhostPosition(acTrans, acDb, lstGhost[i].lstAfraid, lstGhost[i].ptOrigin, strSuffix)) rtnValue = true;
                    if (UpdateGhostPosition(acTrans, acDb, lstGhost[i].lstDead, lstGhost[i].ptOrigin, strSuffix)) rtnValue = true;
                }
            }

            return rtnValue;
        }

        internal void UpdateGhostVisibility(Transaction acTrans, Database acDb, ref List<GameGhost> lstGhost)
        {
            for (int i = 0; i < lstGhost.Count; i++)
            {
                string strSuffix = "";
                if (GetGhostStatus(lstGhost[i], ref strSuffix))
                    UpdateGhostVisibity(acTrans, acDb, lstGhost[i], strSuffix);
            }
        }

        internal Boolean GetGhostStatus(GameGhost Ghost, ref string strSuffix)
        {
            Squiggle squiggle = Ghost.Squiggle;
            GhostState state = Ghost.GhostState;
            GhostColor GhostColor = Ghost.Color;
            Direction direction = Ghost.Direction;

            List<Tuple<Squiggle, GhostState, GhostColor, Direction, string>> lstTuple = GetTuple();

            for (int i = 0; i < lstTuple.Count; i++)
            {
                if (lstTuple[i].Item1 == squiggle &&
                    lstTuple[i].Item2 == state &&
                    lstTuple[i].Item3 == GhostColor &&
                    lstTuple[i].Item4 == direction)
                {
                    strSuffix = lstTuple[i].Item5;
                    return true;
                }
            }

            return false;
        }

        // Standard "_SU"  Alternate "_AU"  Afraid "_SB"  Dead "_DU"
        // Standard "_SD"  Alternate "_AD"  Afraid "_SW"  Dead "_DD"
        // Standard "_SL"  Alternate "_AL"  Afraid "_AB"  Dead "_DL"
        // Standard "_SR"  Alternate "_AR"  Afraid "_AW"  Dead "_DR"

        internal List<Tuple<Squiggle, GhostState, GhostColor, Direction, string>> GetTuple()
        {
            List<Tuple<Squiggle, GhostState, GhostColor, Direction, string>> lstTuple = new List<Tuple<Squiggle, GhostState, GhostColor, Direction, string>>
            {
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Alive, GhostColor.Default, Direction.None, "_SU"), // Standard
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Alive, GhostColor.Default, Direction.Up, "_SU"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Alive, GhostColor.Default, Direction.Down, "_SD"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Alive, GhostColor.Default, Direction.Left, "_SL"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Alive, GhostColor.Default, Direction.Right, "_SR"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Alive, GhostColor.Default, Direction.None, "_AU"), // Alternate
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Alive, GhostColor.Default, Direction.Up, "_AU"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Alive, GhostColor.Default, Direction.Down, "_AD"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Alive, GhostColor.Default, Direction.Left, "_AL"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Alive, GhostColor.Default, Direction.Right, "_AR"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.Default, Direction.None, "_SB"), // Standard Afraid Black
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.Default, Direction.Up, "_SB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.Default, Direction.Down, "_SB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.Default, Direction.Left, "_SB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.Default, Direction.Right, "_SB"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.Default, Direction.None, "_AB"), // Alternate Afraid Black
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.Default, Direction.Up, "_AB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.Default, Direction.Down, "_AB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.Default, Direction.Left, "_AB"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.Default, Direction.Right, "_AB"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.White, Direction.None, "_SW"), // Standard Afraid White
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.White, Direction.Up, "_SW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.White, Direction.Down, "_SW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.White, Direction.Left, "_SW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Afraid, GhostColor.White, Direction.Right, "_SW"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.White, Direction.None, "_AW"), // Alternate Afraid White
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.White, Direction.Up, "_AW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.White, Direction.Down, "_AW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.White, Direction.Left, "_AW"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Afraid, GhostColor.White, Direction.Right, "_AW"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Dead, GhostColor.Default, Direction.None, "_DU"), // Standard Dead
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Dead, GhostColor.Default, Direction.Up, "_DU"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Dead, GhostColor.Default, Direction.Down, "_DD"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Dead, GhostColor.Default, Direction.Left, "_DL"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Standard, GhostState.Dead, GhostColor.Default, Direction.Right, "_DR"),

                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Dead, GhostColor.Default, Direction.None, "_DU"), // Alternate Dead
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Dead, GhostColor.Default, Direction.Up, "_DU"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Dead, GhostColor.Default, Direction.Down, "_DD"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Dead, GhostColor.Default, Direction.Left, "_DL"),
                new Tuple<Squiggle, GhostState, GhostColor, Direction, string>(Squiggle.Alternate, GhostState.Dead, GhostColor.Default, Direction.Right, "_DR")
            };

            return lstTuple;
        }

        internal Boolean UpdateGhostPosition(Transaction acTrans, Database acDb, List<BlockReference> lstBlockReferences, Point2d ptPosition, string strSuffix)
        {
            Boolean rtnValue = false;

            for (int k = 0; k < lstBlockReferences.Count; k++)
            {
                if (lstBlockReferences[k] != null)
                {
                    lstBlockReferences[k].ObjectId.IsObjectIdValid(acDb);
                    {
                        ObjectId objId = lstBlockReferences[k].ObjectId;

                        if (objId.IsObjectIdValid(acDb))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(objId, OpenMode.ForWrite) as BlockReference;

                            if (acBlkRef.Name.EndsWith(strSuffix, StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (ptPosition.ToPoint3d() != acBlkRef.Position)
                                {
                                    Update(acBlkRef, ptPosition.ToPoint3d());
                                    rtnValue = true;
                                }
                            }
                        }
                    }
                }
            }
            return rtnValue;
        }

        internal void UpdateGhostVisibity(Transaction acTrans, Database acDb, GameGhost Ghost, string strSuffix)
        {
            UpdateGhostVisibity(acTrans, acDb, Ghost.lstStandard, strSuffix);
            UpdateGhostVisibity(acTrans, acDb, Ghost.lstAlternate, strSuffix);
            UpdateGhostVisibity(acTrans, acDb, Ghost.lstAfraid, strSuffix);
            UpdateGhostVisibity(acTrans, acDb, Ghost.lstDead, strSuffix);
        }

        internal void UpdateGhostVisibity(Transaction acTrans, Database acDb, List<BlockReference> lstBlockReferences, string strSuffix)
        {
            for (int k = 0; k < lstBlockReferences.Count; k++)
            {
                if (lstBlockReferences[k].IsObjectIdValid(acDb))
                {
                    BlockReference acBlkRef = acTrans.GetObject(lstBlockReferences[k].ObjectId, OpenMode.ForWrite) as BlockReference;

                    if (acBlkRef.Name.EndsWith(strSuffix, StringComparison.CurrentCultureIgnoreCase))
                        Update(acBlkRef, true);
                    else
                        Update(acBlkRef, false);
                }
            }
        }

        internal void Update(BlockReference acBlkRef, Boolean bolVisible)
        {
            if (acBlkRef.Visible != bolVisible)
                acBlkRef.Visible = !acBlkRef.Visible;
        }

        internal void Update(BlockReference acBlkRef, Point3d Position)
        {
            if (acBlkRef.Position != Position)
                acBlkRef.Position = Position;
        }
    }
}