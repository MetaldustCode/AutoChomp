using System;
using System.Windows.Controls;

namespace AutoChomp
{
    /// <summary>
    /// Interaction logic for xmlDebug.xaml
    /// </summary>
    public partial class xmlDebug : UserControl
    {
        public xmlDebug()
        {
            InitializeComponent();
            InitReference();
            LoadRegistry();
        }

        private void InitReference()
        {
            if (clsCommon.GameForm == null)
                clsCommon.GameForm = new GameForm();

            clsCommon.GameForm.chkDebugDirection = this.chkDebugDirection;
        }

        private void LoadRegistry()
        {
            bolInit = true;
            {
                clsReg clsReg = new clsReg();

                this.chkDebugDirection.IsChecked = clsReg.GetDebugDirection();
                this.chkGhostEatDots.IsChecked = clsReg.GetGhostEatDots();
                this.chkPacmanEatDots.IsChecked = clsReg.GetPacmanEatDots();
                this.chkShowCurrentCell.IsChecked = clsReg.GetCurrentCell();
            }
            bolInit = false;
        }

        internal Boolean bolInit = false;

        private void chkPath_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (bolInit) return;

            clsReg clsReg = new clsReg();

            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkDebugDirection")
                clsReg.SetDebugDirection((bool)chk.IsChecked);

            if (chk.Name == "chkGhostEatDots")
                clsReg.SetGhostEatDots((bool)chk.IsChecked);

            if (chk.Name == "chkPacmanEatDots")
                clsReg.SetPacmanEatDots((bool)chk.IsChecked);

            if (chk.Name == "chkShowCurrentCell")
                clsReg.SetCurrentCell((bool)chk.IsChecked);


            clsValidDirection clsValidDirection = new clsValidDirection();

            if (clsReg.GetDebugDirection())
                clsValidDirection.DrawValid();
            else
                clsValidDirection.DeleteValid();
        }
    }
}