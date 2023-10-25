﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoChomp
{
    public partial class xmlGhost : UserControl
    {
        public xmlGhost()
        {
            InitializeComponent();
            InitReference();
            LoadDefault();
            LoadPacman();
            LoadMaze();
        }

        private void InitReference()
        {
            if (clsCommon.GameForm == null)
                clsCommon.GameForm = new GameForm();

            clsCommon.GameGhostCommon = new GameGhostCommon();

            clsCommon.GameForm.chkRed = this.chkRed;
            clsCommon.GameForm.chkPink = this.chkPink;
            clsCommon.GameForm.chkBlue = this.chkBlue;
            clsCommon.GameForm.chkOrange = this.chkOrange;

            clsCommon.GameForm.cboRed = this.cboRed;
            clsCommon.GameForm.cboPink = this.cboPink;
            clsCommon.GameForm.cboBlue = this.cboBlue;
            clsCommon.GameForm.cboOrange = this.cboOrange;

            clsCommon.GameForm.cboMaze = this.cboMaze;
        }

        private void LoadMaze()
        {
            List<String> lstDropDown = new List<String>() { "Pacman Maze", "Ms Pacman Maze 1", "Ms Pacman Maze 2", "Ms Pacman Maze 3", "Ms Pacman Maze 4" };
            SetDropDownMaze(lstDropDown);

            LoadRegistryMaze();
        }

        private void LoadDefault()
        {
            List<String> lstDropDown = new List<String>() { "None", "Random" };
            SetDropDown(lstDropDown);

            LoadRegistry(lstDropDown);
        }

        private void LoadPacman()
        {
            List<String> lstDropDown = new List<String>() { "Keyboard", "Random", "Gluttony" };
            SetDropDownPacman(lstDropDown);

            LoadRegistryPacman(lstDropDown);
        }

        private void LoadRegistry(List<String> lstDropDown)
        {
            clsReg clsReg = new clsReg();

            this.cboRed.SelectedIndex = lstDropDown.IndexOf(clsReg.GetRedSearchMode());
            this.cboPink.SelectedIndex = lstDropDown.IndexOf(clsReg.GetPinkSearchMode());
            this.cboBlue.SelectedIndex = lstDropDown.IndexOf(clsReg.GetBlueSearchMode());
            this.cboOrange.SelectedIndex = lstDropDown.IndexOf(clsReg.GetOrangeSearchMode());

            this.chkRed.IsChecked = clsReg.GetRedSearchVisible();
            this.chkPink.IsChecked = clsReg.GetPinkSearchVisible();
            this.chkBlue.IsChecked = clsReg.GetBlueSearchVisible();
            this.chkOrange.IsChecked = clsReg.GetOrangeSearchVisible();
        }

        private void LoadRegistryPacman(List<String> lstDropDown)
        {
            clsReg clsReg = new clsReg();

            this.cboPacman.SelectedIndex = lstDropDown.IndexOf(clsReg.GetPacmanSearchMode());
            this.chkPacman.IsChecked = clsReg.GetRedSearchVisible();
        }

        private void LoadRegistryMaze()
        {
            clsReg clsReg = new clsReg();

            this.cboMaze.SelectedIndex = clsReg.GetMazeIndex();
        }

        private void SetDropDown(List<String> lstDropDown)
        {
            for (int i = 0; i < lstDropDown.Count; i++)
            {
                this.cboRed.Items.Add(lstDropDown[i]);
                this.cboPink.Items.Add(lstDropDown[i]);
                this.cboBlue.Items.Add(lstDropDown[i]);
                this.cboOrange.Items.Add(lstDropDown[i]);
            }
        }

        private void SetDropDownPacman(List<String> lstDropDown)
        {
            for (int i = 0; i < lstDropDown.Count; i++)
            {
                this.cboPacman.Items.Add(lstDropDown[i]);
            }
        }

        private void SetDropDownMaze(List<String> lstDropDown)
        {
            for (int i = 0; i < lstDropDown.Count; i++)
            {
                this.cboMaze.Items.Add(lstDropDown[i]);
            }
        }

        private void cboPath_DropDownClosed(object sender, EventArgs e)
        {
            clsReg clsReg = new clsReg();

            ComboBox cbo = (ComboBox)sender;

            if (cbo.Name == "cboPacman") clsReg.SetPacmanSearchMode(cbo.Text);
            if (cbo.Name == "cboRed") clsReg.SetRedSearchMode(cbo.Text);
            if (cbo.Name == "cboPink") clsReg.SetPinkSearchMode(cbo.Text);
            if (cbo.Name == "cboBlue") clsReg.SetBlueSearchMode(cbo.Text);
            if (cbo.Name == "cboOrange") clsReg.SetOrangeSearchMode(cbo.Text);
            if (cbo.Name == "cboMaze") clsReg.SetMazeIndex(cbo.SelectedIndex);
        }

        private void chkPath_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            clsReg clsReg = new clsReg();

            CheckBox chk = (CheckBox)sender;
            if (chk.Name == "chkPacman") clsReg.SetPacmanSearchVisible((bool)chk.IsChecked);
            if (chk.Name == "chkRed") clsReg.SetRedSearchVisible((bool)chk.IsChecked);
            if (chk.Name == "chkPink") clsReg.SetPinkSearchVisible((bool)chk.IsChecked);
            if (chk.Name == "chkBlue") clsReg.SetBlueSearchVisible((bool)chk.IsChecked);
            if (chk.Name == "chkOrange") clsReg.SetOrangeSearchVisible((bool)chk.IsChecked);
        }

        private void cbo_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = !((System.Windows.Controls.ComboBox)sender).IsDropDownOpen;
        }

        private void cbo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}