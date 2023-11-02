using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class Position
    {
        internal int X;
        internal int Y;

        internal Position()
        {
            this.X = 0;
            this.Y = 0;
        }

        internal Position(int intX, int intY)
        {
            this.X = intX;
            this.Y = intY;
        }
    }

    internal class GamePolyline
    {
        internal Entity Ent { get; private set; }
        internal Point3d StartPoint { get; private set; }
        internal Point3d EndPoint { get; private set; }

        internal GamePolyline(Entity Ent,
                              Point3d StartPoint,
                              Point3d EndPoint)
        {
            this.Ent = Ent;
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }
    }

    internal class GameObjectId
    {
        internal List<ObjectId> lstObjPacman = new List<ObjectId>();
        internal List<ObjectId> lstObjGhosts = new List<ObjectId>();
        internal List<ObjectId> lstObjMaze = new List<ObjectId>();
        internal List<ObjectId> lstObjDots = new List<ObjectId>();
        internal List<ObjectId> lstObjPower = new List<ObjectId>();
        internal List<ObjectId> lstObjBoxes = new List<ObjectId>();
        internal List<ObjectId> lstObjDirectionBoxes = new List<ObjectId>();
        internal List<ObjectId> lstObjHistoryBoxes = new List<ObjectId>();
        internal List<ObjectId> lstObjAStar = new List<ObjectId>();
        internal List<ObjectId> lstObjPosition = new List<ObjectId>();
    }

    internal class GameDebug
    {
        internal List<ObjectId> lstObjDirection = new List<ObjectId>();
    }
}