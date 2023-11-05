using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp
{
    public static class clsExtension
    {
        private static readonly double Cell = clsGridValues.Cell;
        private static readonly double Middle = clsGridValues.Middle;

        internal static string[,] ToStringArray(this int[,] arrAStar, Boolean bolExcludeZero = false)
        {
            arrAStar.GetSize(out int col, out int row);

            string[,] rtnValue = new string[col, row];

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    string strValue = arrAStar[i, j].ToString();

                    if (bolExcludeZero)
                    {
                        if (strValue == "0")
                            strValue = "";
                    }

                    rtnValue[i, j] = strValue;
                }
            }

            return rtnValue;
        }

        internal static string[,] ConvertIntToString(this int[,] arrNum)
        {
            arrNum.GetSize(out int col, out int row);

            string[,] arrText = new string[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                    arrText[x, y] = arrNum[x, y].ToString();
            }

            return arrText;
        }

        internal static int[,] ConvertStringToInt(this string[,] arrText)
        {
            arrText.GetSize(out int col, out int row);

            int[,] arrNum = new int[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (arrText[x, y] != null)
                        arrNum[x, y] = arrText[x, y].ToInt();
                }
            }

            return arrNum;
        }

        internal static Point2d GetOrigin(this int x, int y)
        {
            double X = (x * Cell) + Middle;
            double Y = (y * Cell) + Middle;

            return new Point2d(X, Y);
        }

        internal static bool[,] CloneArray(this bool[,] arr)
        {
            arr.GetSize(out int col, out int row);
            bool[,] arrNew = new bool[col, row];

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                    arrNew[x, y] = arr[x, y];
            }

            return arrNew;
        }

        internal static List<Point2d> GetOrigin(this BlockReference[,] arr)
        {
            List<Point2d> rtnValue = new List<Point2d>();
            arr.GetSize(out int col, out int row);

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arr[c, r] != null)
                    {
                        double X = (c * Cell) + Middle;
                        double Y = (r * Cell) + Middle;

                        rtnValue.Add(new Point2d(X, Y));
                    }
                }
            }
            return rtnValue;
        }

        internal static List<ObjectId> GetObjectId(this BlockReference[,] arr)
        {
            List<ObjectId> rtnValue = new List<ObjectId>();
            arr.GetSize(out int col, out int row);

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arr[c, r] != null)
                        rtnValue.Add(arr[c, r].ObjectId);
                }
            }
            return rtnValue;
        }

        internal static Point2d GetOrigin(this List<Position> lstPosition, int i)
        {
            double X = (lstPosition[i].X * Cell) + Middle;
            double Y = (lstPosition[i].Y * Cell) + Middle;

            return new Point2d(X, Y);
        }

        internal static Point2d GetOrigin(this Position Position)
        {
            double X = (Position.X * Cell) + Middle;
            double Y = (Position.Y * Cell) + Middle;

            return new Point2d(X, Y);
        }

        internal static void GetSize(this Direction[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this Position[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this Boolean[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this String[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this DBText[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this int[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this BlockReference[,] arr, out int col, out int row)
        {
            col = arr.GetLength(0);
            row = arr.GetLength(1);
        }

        internal static void GetSize(this List<String> lstString, out int col, out int row)
        {
            col = 0;
            row = lstString.Count;

            for (int i = 0; i < lstString.Count; i++)
            {
                if (lstString[i].Length > col)
                    col = lstString[i].Length;
            }
        }

        internal static List<BlockReference> ToList(this BlockReference[,] arr)
        {
            List<BlockReference> rtnValue = new List<BlockReference>();
            arr.GetSize(out int col, out int row);

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arr[c, r] != null)
                        rtnValue.Add(arr[c, r]);
                }
            }
            return rtnValue;
        }

        internal static List<Direction> ToList(this Direction[,] arr, out List<Position> lstPosition)
        {
            List<Direction> rtnValue = new List<Direction>();
            lstPosition = new List<Position>();
            arr.GetSize(out int col, out int row);

            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (arr[c, r] != Direction.None)
                    {
                        rtnValue.Add(arr[c, r]);
                        lstPosition.Add(new Position(c, r));
                    }
                }
            }
            return rtnValue;
        }

        internal static Point2d GetLast(this List<Point2d> lstValue)
        {
            return lstValue[lstValue.Count - 1];
        }

        internal static Position GetLast(this List<Position> lstValue)
        {
            return lstValue[lstValue.Count - 1];
        }

        internal static List<Boolean> Multiply(this List<Boolean> lstValue)
        {
            List<Boolean> rtnValue = new List<Boolean>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<StartLocation> Multiply(this List<StartLocation> lstValue)
        {
            List<StartLocation> rtnValue = new List<StartLocation>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<Double> Multiply(this List<Double> lstValue)
        {
            List<Double> rtnValue = new List<double>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<Position> Multiply(this List<Position> lstValue)
        {
            List<Position> rtnValue = new List<Position>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<Direction> Multiply(this List<Direction> lstValue)
        {
            List<Direction> rtnValue = new List<Direction>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<String> Multiply(this List<String> lstValue)
        {
            List<String> rtnValue = new List<String>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static List<int> Multiply(this List<int> lstValue)
        {
            List<int> rtnValue = new List<int>();

            for (int j = 0; j < clsGridValues.intGroup; j++)
            {
                for (int i = 0; i < lstValue.Count; i++)
                    rtnValue.Add(lstValue[i]);
            }

            return rtnValue;
        }

        internal static void MakeUnique(ref List<int> lstRow, ref List<int> lstCol)
        {
            List<Tuple<int, int>> lstTuple = new List<Tuple<int, int>>();

            for (int i = 0; i < lstRow.Count; i++)
                lstTuple.Add(new Tuple<int, int>(lstRow[i], lstCol[i]));

            var lstTuple2 = lstTuple.Distinct();

            lstRow.Clear();
            lstCol.Clear();

            foreach (var item in lstTuple2)
            {
                lstRow.Add(item.Item1);
                lstCol.Add(item.Item2);
            }
        }

        internal static void GetDirection(this int intDir, ref int x, ref int y)
        {
            switch (intDir)
            {
                case 0:
                    y -= 1; // Down
                    break;

                case 1:
                    y += 1; // Up
                    break;

                case 2:
                    x += 1; // Right
                    break;

                case 3:
                    x -= 1; // Left
                    break;
            }
        }

        internal static void GetDirection(this Direction direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case Direction.Down:
                    y -= 1; // Down
                    break;

                case Direction.Up:
                    y += 1; // Up
                    break;

                case Direction.Right:
                    x += 1; // Right
                    break;

                case Direction.Left:
                    x -= 1; // Left
                    break;
            }
        }

        internal static Boolean IsInGrid(this int intWidth, int intHeight, int x, int y)
        {
            if ((x >= 0) && (x <= intWidth - 1))
            {
                if ((y >= 0) && (y <= intHeight - 1))
                    return true;
            }

            return false;
        }

        internal static Boolean IsInGrid(this Boolean[,] arr, int x, int y)
        {
            int intWidth = arr.GetLength(0);
            int intHeight = arr.GetLength(1);

            return IsInGrid(intWidth, intHeight, x, y);
        }

        internal static int ToInt(this string strValue)
        {
            if (int.TryParse(strValue, out int intValue))
                return intValue;
            return intValue;
        }

        internal static int ToInt(this double dblValue)
        {
            return Convert.ToInt32(dblValue);
        }

        internal static List<ObjectId> GetObjectId(this List<Hatch> lstEntity)
        {
            List<ObjectId> rtnValue = new List<ObjectId>();

            for (int i = 0; i < lstEntity.Count; i++)
                rtnValue.Add(lstEntity[i].ObjectId);

            return rtnValue;
        }

        internal static List<ObjectId> GetObjectId(this List<Polyline> lstEntity)
        {
            List<ObjectId> rtnValue = new List<ObjectId>();

            for (int i = 0; i < lstEntity.Count; i++)
                rtnValue.Add(lstEntity[i].ObjectId);

            return rtnValue;
        }

        internal static void AddToObjectId(this List<Hatch> lstEntity, ref List<ObjectId> lstObjectId)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                lstObjectId.Add(lstEntity[i].ObjectId);
        }

        internal static void AddToObjectId(this List<Polyline> lstEntity, ref List<ObjectId> lstObjectId)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                lstObjectId.Add(lstEntity[i].ObjectId);
        }

        internal static void AddToMazeElement(this List<ObjectId> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                clsCommon.GameObjectId.lstObjMaze.Add(lstEntity[i]);
        }

        internal static void AddToDotsElement(this List<ObjectId> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                clsCommon.GameObjectId.lstObjDots.Add(lstEntity[i]);
        }

        internal static void AddToPowerElement(this List<ObjectId> lstEntity)
        {
            for (int i = 0; i < lstEntity.Count; i++)
                clsCommon.GameObjectId.lstObjPower.Add(lstEntity[i]);
        }

        public static double Angle(this Point2d start, Point2d end)
        {
            return Math.Atan2(start.Y - end.Y, end.X - start.X);
        }

        public static Boolean IsObjectIdValid(this Entity acEntity, Database acDb)
        {
            if (acEntity.Database == acDb)
            {
                if (acEntity != null)
                {
                    ObjectId objId = acEntity.ObjectId;

                    if (objId.IsValid)
                    {
                        if (!objId.IsErased)
                        {
                            if (objId.Database == acDb)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public static Boolean IsObjectIdValid(this ObjectId objId, Database acDb)
        {
            try
            {
                if (objId.Database == acDb)
                {
                    if (objId.IsValid)
                    {
                        if (!objId.IsErased)
                            return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static double ToDouble(this String strValue)
        {
            double.TryParse(strValue, out double dblValue);

            return dblValue;
        }

        public static Boolean IsPositiveNumber(this String strValue)
        {
            for (int i = 0; i < strValue.Length; i++)
            {
                if (!((strValue[i].IsNumeric() || strValue[i] == '.')))
                    return false;
            }
            return true;
        }

        public static bool IsNumeric(this char Value)
        {
            if (Double.TryParse(Value.ToString(), out _))
                return true;
            return false;
        }

        public static bool IsNumeric(this string Value)
        {
            if (Double.TryParse(Value.ToString(), out _))
                return true;
            return false;
        }

        public static Boolean IsNumeric(this String strValue, out double dblValue)
        {
            String strDegree = "".GetDegreeSymbol();
            strValue = strValue.Replace(strDegree, "");
            if (Double.TryParse(strValue, out dblValue))
                return true;
            return false;
        }

        public static Boolean IsNumeric(this String strValue, out int intValue)
        {
            String strDegree = "".GetDegreeSymbol();
            strValue = strValue.Replace(strDegree, "");
            if (int.TryParse(strValue, out intValue))
                return true;
            return false;
        }

        public static String GetDegreeSymbol(this String strValue)
        {
            return strValue + "°";
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static List<Polyline> ToPolyline(this List<Entity> lstEntity)
        {
            List<Polyline> rtnValue = new List<Polyline>();

            for (int i = 0; i < lstEntity.Count; i++)
                rtnValue.Add((Polyline)lstEntity[i]);

            return rtnValue;
        }

        public static void SetPolylineColor(this List<Polyline> lstPline, List<int> lstColor)
        {
            int intColor = 0;

            for (int j = 0; j < lstPline.Count; j++)
            {
                lstPline[j].ColorIndex = lstColor[intColor];

                intColor++;
                if (intColor == lstColor.Count)
                    intColor = 0;
            }
        }

        public static void SetPolylineColor(this List<Polyline> lstPline, int intColor)
        {
            for (int j = 0; j < lstPline.Count; j++)
                lstPline[j].ColorIndex = intColor;
        }

        public static void SetPolylineColor(this List<Polyline> lstPline, Color acColor)
        {
            for (int j = 0; j < lstPline.Count; j++)
                lstPline[j].Color = acColor;
        }

        public static void SetPlineWidth(this List<Polyline> lstPline, double dblWidth)
        {
            for (int j = 0; j < lstPline.Count; j++)
                lstPline[j].ConstantWidth = dblWidth;
        }

        public static Boolean MoveEntityXY(this Entity Entity, double X, double Y)
        {
            Point3d acPt3d = new Point3d(0, 0, 0);
            Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(X, Y, 0));
            Entity.TransformBy(Matrix3d.Displacement(acVec3d));

            return true;
        }

        public static Boolean MoveEntityXY(this DBText Entity, double X, double Y)
        {
            Point3d acPt3d = new Point3d(0, 0, 0);
            Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(X, Y, 0));
            Entity.TransformBy(Matrix3d.Displacement(acVec3d));

            return true;
        }

        public static Boolean MoveEntityXY(this Polyline Entity, double X, double Y)
        {
            Point3d acPt3d = new Point3d(0, 0, 0);
            Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(X, Y, 0));
            Entity.TransformBy(Matrix3d.Displacement(acVec3d));

            return true;
        }

        public static Boolean MoveEntityXY(this Entity acEntity, Transaction acTrans, Database acDb, double X, double Y)
        {
            if (acEntity.IsObjectIdValid(acDb))
            {
                acEntity = acTrans.GetObject(acEntity.ObjectId, OpenMode.ForWrite) as Entity;

                Point3d acPt3d = new Point3d(0, 0, 0);
                Vector3d acVec3d = acPt3d.GetVectorTo(new Point3d(X, Y, 0));
                acEntity.TransformBy(Matrix3d.Displacement(acVec3d));
                return true;
            }
            return false;
        }

        public static double Deg2Rad(this double Deg)
        {
            return Deg * Math.PI / 180;
        }

        public static double Rad2Deg(this double Rad)
        {
            return Rad * 180 / Math.PI;
        }

        public static Boolean HasMatch(this Polyline acPline1, Polyline acPline2)
        {
            List<Point2d> lst1 = GetPoint(acPline1);
            List<Point2d> lst2 = GetPoint(acPline2);

            for (int i = 0; i < lst1.Count; i++)
            {
                for (int k = 0; k < lst2.Count; k++)
                {
                    if (lst1[i] == lst2[k])
                        return true;
                }
            }
            return false;
        }

        public static List<Point2d> GetPoint(this Polyline acPline)
        {
            List<Point2d> rtnValue = new List<Point2d>
            {
                acPline.StartPoint.ToPoint2d(),

                acPline.EndPoint.ToPoint2d()
            };

            rtnValue = rtnValue.Distinct().ToList();

            return rtnValue;
        }

        public static List<Point2d> GetVertices(this Polyline acPline)
        {
            List<Point2d> rtnValue = new List<Point2d>
            {
                acPline.StartPoint.ToPoint2d()
            };

            int vn = acPline.NumberOfVertices;

            for (int i = 0; i < vn; i++)
                rtnValue.Add(acPline.GetPoint2dAt(i));

            rtnValue.Add(acPline.EndPoint.ToPoint2d());

            rtnValue = rtnValue.Distinct().ToList();

            return rtnValue;
        }

        public static Point2d ToPoint2d(this Point3d pt3)
        {
            return new Point2d(pt3.X, pt3.Y);
        }

        public static Point3d ToPoint3d(this Point2d pt2)
        {
            return new Point3d(pt2.X, pt2.Y, 0);
        }

        public static Point3d ToPoint3d(this Point2d pt2, double dblZ)
        {
            return new Point3d(pt2.X, pt2.Y, dblZ);
        }

        public static void Add(this List<Polyline> lstPolyline, ref List<Polyline> rtnValue)
        {
            for (int i = 0; i < lstPolyline.Count; i++)
                rtnValue.Add(lstPolyline[i]);
        }

        public static void Add(this Polyline acPolyline, ref List<Polyline> rtnValue)
        {
            rtnValue.Add(acPolyline);
        }

        public static string ListToString(this List<String> lstValue, char chrDelim)
        {
            string strBlock = "";
            for (int j = 0; j < lstValue.Count; j++)
            {
                if (j == lstValue.Count - 1)
                    strBlock += lstValue[j];
                else
                    strBlock += lstValue[j] + chrDelim;
            }
            return strBlock;
        }

        public static List<String> StringToList(this string strValue, char chrDelim)
        {
            List<String> rtnValue = new List<String>();
            if (strValue.Length > 0)
            {
                if (strValue.Contains(chrDelim))
                {
                    string[] Arr = strValue.Split(chrDelim);
                    List<String> lstArr = Arr.ToList();
                    for (int i = 0; i < lstArr.Count; i++)
                        rtnValue.Add(lstArr[i]);
                }
                else
                    rtnValue.Add(strValue);
            }

            return rtnValue;
        }

        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double ToDegrees(this double val)
        {
            return val * (180.0 / Math.PI);
        }

        public static double RoundTo(this double dblIn, int intPrecision)
        {
            return Math.Round(dblIn, intPrecision);
        }

        public static double GetAngle(this Point3d pt1, Point3d pt2)
        {
            double xDiff = pt2.X - pt1.X;
            double yDiff = pt2.Y - pt1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        public static ObjectId HandleToObjectId(this Database acDb, String strHandle)
        {
            try
            {
                if (!String.IsNullOrEmpty(strHandle))
                    return acDb.GetObjectId(false, new Handle(Int64.Parse(strHandle, System.Globalization.NumberStyles.AllowHexSpecifier)), 0);
                else
                    return ObjectId.Null;
            }
            catch (Exception)
            {
                return ObjectId.Null;
            }
        }
    }
}