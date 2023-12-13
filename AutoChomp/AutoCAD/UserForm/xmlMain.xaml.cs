using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AutoChomp
{
    public partial class xmlMain : UserControl
    {
        private Boolean bolInit = false;

        public xmlMain()
        {
            bolInit = true;
            {
                InitializeComponent();
                InitReference();

                clsInit clsInit = new clsInit();
                clsInit.Init(this);

                StartDispatch();

                Application.Idle += new System.EventHandler(OnIdle);
            }
            bolInit = false;
        }

        private void InitReference()
        {
            if (clsCommon.GameForm == null)
                clsCommon.GameForm = new GameForm();

            clsCommon.GameForm.txtInfo = this.txtInfo;
            clsCommon.GameForm.cboSpacing = this.cboSpacing;
        }

        private void cboMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void cboSpacing_DropDownClosed(object sender, EventArgs e)
        {
            clsUpdateScale clsUpdateScale = new clsUpdateScale();
            clsUpdateScale.UpdateSpacing();       
        }



        private void cboSpacing_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = !((System.Windows.Controls.ComboBox)sender).IsDropDownOpen;
        }

        private void cboSpacing_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private Boolean bolInLoop = false;

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bolInLoop || bolInit) return;
            clsReg clsReg = new clsReg();
            clsReg.SetSpeed(this.txtNum.Text);
        }

        private void btnNumUp_Click(object sender, RoutedEventArgs e)
        {
            bolInLoop = true;
            {
                if (double.TryParse(txtNum.Text, out double dblText))
                {
                    dblText += 1.0;
                    dblText = Math.Round(dblText, 0);
                    txtNum.Text = dblText.ToString();
                }
                else
                    txtNum.Text = "1";

                clsReg clsReg = new clsReg();
                clsReg.SetSpeed(this.txtNum.Text);
            }
            bolInLoop = false;
        }

        private void btnNumDown_Click(object sender, RoutedEventArgs e)
        {
            bolInLoop = true;
            {
                if (double.TryParse(txtNum.Text, out double dblText))
                {
                    if (dblText > 0)
                    {
                        dblText -= 1.0;
                        dblText = Math.Round(dblText, 0);
                        txtNum.Text = dblText.ToString();
                    }
                }
                else
                    txtNum.Text = "1";

                clsReg clsReg = new clsReg();
                clsReg.SetSpeed(this.txtNum.Text);
            }
            bolInLoop = false;
        }

        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb?.SelectAll();
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clsReg clsReg = new clsReg();

            clsReg.SetTab(this.TabMain.SelectedIndex.ToString());
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnStop.Content.ToString() == "Pause")
                this.btnStop.Content = "Continue";
            else
                this.btnStop.Content = "Pause";

            clsNAudio.StopPowerPellet();
        }

        private void btnHouse_Click(object sender, RoutedEventArgs e)
        {
            clsUpdateScale clsUpdateScale = new clsUpdateScale();
            clsUpdateScale.UpdateSpacing();
            //clsSolvePath clsSolvePath = new clsSolvePath();

            //clsSolvePath.Solve();

            //clsNAudio.PlayPowerPellet();

            //clsAStar clsAStar = new clsAStar();

            //clsAStar.CreateTextData();
            // clsPlaySounds.PlayPalette();

            //clsValidDirection clsValidDirection = new clsValidDirection();
            //clsValidDirection.DrawValid();
        }

        private void btnPacmanDeath_Click(object sender, RoutedEventArgs e)
        {
            //clsNAudio clsNAudio = new clsNAudio();
            //clsNAudio.StopPowerPellet();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            clsScoreValues.intPacmanScore = 0;
            Reset();
        }

        private void Reset()
        {
            this.IsEnabled = false;
            //if (clsCommon.GamePacman != null)
            //{
            //    List<Direction> lstDirection = clsCommon.GamePacman.GameLoop.lstLoopDirection;
            //    List<Position> lstPosition = clsCommon.GamePacman.GameLoop.lstLoopPosition;
            //}

            clsResetGame clsCreateGrid = new clsResetGame();

            clsCreateGrid.ResetGame();

            clsZoomToPoint clsZoomToPoint = new clsZoomToPoint();
            //clsZoomToPoint.ZoomScale();

            clsReg clsReg = new clsReg();
            if (clsReg.GetIncrementLevel())
                IncrementMaze();

            bolInit = true;
            this.IsEnabled = true;
        }

        internal void ResetIdle()
        {
            clsTimers.GameStopWatch.StopWatchIdle.Restart();
        }

        internal int DisplayIdle()
        {
            int intElapsed = Convert.ToInt32(clsTimers.GameStopWatch.StopWatchIdle.ElapsedMilliseconds);

            if (intElapsed > 50) intElapsed = 50;

            ResetIdle();

            clsTimerEvents clsTimerEvents = new clsTimerEvents();

            string strValue = clsTimerEvents.PrintTime(Convert.ToDouble(intElapsed)).ToString();

            double dblValue = strValue.ToDouble();
            if (dblValue > 0)
            {
                dblValue = 1000 / dblValue;
                dblValue = Math.Round(dblValue, 0);
            }

            strValue = string.Format("{0} - {1} FPS", strValue.PadLeft(2, '0'), dblValue);
            this.txtInfo.Text = strValue;

            return intElapsed;
        }

        private void OnIdle(object sender, System.EventArgs e)
        {
            if (this.btnStop.Content.ToString() == "Pause")
            {
                if (bolInit)
                {
                    if (clsCommon.GameDots.lstIsEaten.Contains(false))
                    {
                        int intMax = this.txtNum.Text.ToInt();
                        int intElapsed = DisplayIdle();

                        if (intElapsed > intMax)
                            intElapsed = intMax;

                        clsAfraid clsAfraid = new clsAfraid();
                        clsAfraid.EatPellet();

                        Gameloop.Update.clsUpdateData clsUpdateData = new Gameloop.Update.clsUpdateData();
                        for (int i = 0; i < intElapsed; i++)
                            clsUpdateData.UpdateData();

                        if (intElapsed > 0)
                        {
                            Gameloop.Update.clsUpdateGraphics clsDisplayGraphics = new Gameloop.Update.clsUpdateGraphics();
                            clsDisplayGraphics.UpdateGraphics();
                        }
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        internal void IncrementMaze()
        {
            ComboBox cboMaze = clsCommon.GameForm.cboMaze;

            int intIndex = cboMaze.SelectedIndex;

            intIndex++;

            if (intIndex > cboMaze.Items.Count - 1)
                intIndex = 0;

            cboMaze.SelectedIndex = intIndex;

            clsReg clsReg = new clsReg();
            clsReg.SetMazeIndex(cboMaze.SelectedIndex);
        }

        internal void ForceMessage()
        {
            // Set the cursor without ectually moving it - enough to

            // generate a Windows message

            System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;

            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(pt.X, pt.Y);
        }
        public void StartDispatch()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(15);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            ForceMessage();
        }

    }
}