using System;
using System.Collections.Generic;

namespace AutoChomp
{
    internal class clsPathfinder
    {
        internal List<Position> GetPath(int[,] attNum, Position ptStart)
        {
            List<Position> rtnValue = new List<Position>();

            int intValue = attNum[ptStart.X, ptStart.Y];

            if (intValue > 0)
            {
            }

            return rtnValue;
        }

        //Sub PathFinder(ByVal arr(,) As String, ByRef frmWidth As Integer, ByVal frmHeight As Integer,
        //               ByRef x As Integer, ByRef y As Integer, ByRef CTR As Integer,
        //               ByRef outX As List(Of Integer), ByRef outY As List(Of Integer))

        //        Dim curNumber As Integer
        //        If IsNumeric(arr(x, y)) Then
        //            curNumber = arr(x, y)

        //            Dim maX As New List(Of Integer)
        //            Dim maY As New List(Of Integer)

        //            Dim maPair As New List(Of String)

        //            GetNextDirection(x, y, frmWidth, frmHeight, curNumber, maPair, arr, maX, maY)

        //            Dim k As Integer
        //            If maX.Count > 0 Then
        //                While (True)
        //                    Dim myRnd As Integer = RandomNumber(0, maX.Count)
        //                    'If isEven(myRnd) Then
        //                    k = myRnd
        //                        Exit While
        //                    'End If
        //                End While

        //                For i As Integer = 0 To maX.Count - 1
        //                    frmPathfinder.dgv(maX(k), maY(k)).Style.BackColor = Color.Orange
        //                    outX.Add(maX(k))
        //                    outY.Add(maY(k))
        //                    CTR += 1
        //                    PathFinder(arr, frmWidth, frmHeight, maX(k), maY(k), CTR, outX, outY)
        //                Next
        //            End If

        //        End If

        //    End Sub

        internal Boolean GetNextDirection(int[,] attNum, List<Position> lstCurrent, Position curPos, ref Position rtnValue)
        {
            clsAStar clsAStar = new clsAStar();
            int intCur = attNum[curPos.X, curPos.Y];

            attNum.GetSize(out int col, out int row);

            clsBuildMap clsBuildMap = new clsBuildMap();

            for (int i = 0; i < 4; i++)
            {
                int aX = curPos.X;
                int aY = curPos.Y;

                clsBuildMap.GetDirection(i, ref aX, ref aY);

                if (col.IsInGrid(row, aX, aY))
                {
                    if (attNum[aX, aY] > 0 && attNum[aX, aY] < intCur)
                    {
                        Position newPos = new Position(aX, aY);
                        if (!clsAStar.HasPosition(lstCurrent, newPos))
                        {
                            rtnValue = newPos;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //    Sub GetNextDirection(ByRef x As Integer, ByRef y As Integer, ByRef frmwidth As Integer, ByRef frmHeight As Integer, _
        //                   ByRef curNumber As Integer, ByRef maPair As List(Of String), ByRef arr(,) As String, _
        //                   ByRef maX As List(Of Integer), ByRef maY As List(Of Integer))

        //        For i As Integer = 0 To 3
        //            Dim ax As Integer = x
        //            Dim ay As Integer = y
        //            getDirection(i, ax, ay)
        //            If isEven(i) Then
        //                If IsInGrid(frmwidth, frmHeight, ax, ay) Then
        //                    If IsNumeric(arr(ax, ay)) Then
        //                        If CInt(arr(ax, ay)) < curNumber Then
        //                            If Not maPair.Contains(ax & "," & ay) Then
        //                                maPair.Add(ax & "," & ay)
        //                                maX.Add(ax)
        //                                maY.Add(ay)
        //                                Exit Sub
        //                            End If
        //                        End If
        //                    End If
        //                End If
        //            End If
        //        Next

        //        If maX.Count = 0 Then

        //            For i As Integer = 0 To 3
        //                Dim ax As Integer = x
        //                Dim ay As Integer = y
        //                getDirection(i, ax, ay)
        //                If IsInGrid(frmwidth, frmHeight, ax, ay) Then
        //                    If IsNumeric(arr(ax, ay)) Then
        //                        If CInt(arr(ax, ay)) < curNumber Then
        //                            If Not maPair.Contains(ax & "," & ay) Then
        //                                maPair.Add(ax & "," & ay)
        //                                maX.Add(ax)
        //                                maY.Add(ay)
        //                                Exit Sub
        //                            End If
        //                        End If
        //                    End If
        //                End If
        //            Next
        //        End If
        //    End Sub
    }
}