using System;
using System.Windows.Controls;

namespace AutoChomp
{
    public partial class xmlOptions : UserControl
    {
        public xmlOptions()
        {
            InitializeComponent();
            InitReference();
            LoadRegistry();
        }

        private void InitReference()
        {
            if (clsCommon.GameForm == null)
                clsCommon.GameForm = new GameForm();

            clsCommon.GameForm.chkSound = this.chkSound;
            clsCommon.GameForm.chkDot = this.chkDot;
            clsCommon.GameForm.chkNumber = this.chkNumber;
        }

        internal Boolean bolInit = false;

        private void LoadRegistry()
        {
            bolInit = true;
            {
                clsReg clsReg = new clsReg();

                this.chkDot.IsChecked = clsReg.GetDotsVisible();
                this.chkNumber.IsChecked = clsReg.GetNumbersVisible();
                this.chkSound.IsChecked = clsReg.GetPlaySound();

                this.chkDirection.IsChecked = clsReg.GetShowDirection();
                this.chkSuggestion.IsChecked = clsReg.GetShowSuggestion();
                this.chkHistory.IsChecked = clsReg.GetShowHistory();

                this.chkUseForward.IsChecked = clsReg.GetUseForward();
                this.chkUseHistory.IsChecked = clsReg.GetUseHistory();

                this.chkHighScore.IsChecked = clsReg.GetShowHighScore();

                this.chkIncrementLevel.IsChecked = clsReg.GetIncrementLevel();

                this.chkPlaySound.IsChecked = clsReg.GetPlaySound();
            }
            bolInit = false;
        }

        private void chkPath_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (bolInit) return;

            clsReg clsReg = new clsReg();

            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkDot")
                clsReg.SetDotsVisible((bool)chk.IsChecked);

            if (chk.Name == "chkNumber")
                clsReg.SetNumbersVisible((bool)chk.IsChecked);

            if (chk.Name == "chkDirection")
                clsReg.SetShowDirection((bool)chk.IsChecked);

            if (chk.Name == "chkSuggestion")
                clsReg.SetShowSuggestion((bool)chk.IsChecked);

            if (chk.Name == "chkHistory")
                clsReg.SetShowHistory((bool)chk.IsChecked);

            if (chk.Name == "chkUseForward")
                clsReg.SetUseForward((bool)chk.IsChecked);

            if (chk.Name == "chkUseHistory")
                clsReg.SetUseHistory((bool)chk.IsChecked);

            if (chk.Name == "chkHighScore")
                clsReg.SetShowHighScore((bool)chk.IsChecked);

            if (chk.Name == "chkIncrementLevel")
                clsReg.SetIncrementLevel((bool)chk.IsChecked);

            if (chk.Name == "chkPlaySound")
            {
                clsReg.SetPlaySound((bool)chk.IsChecked);

                if (!(bool)chk.IsChecked)
                    clsNAudio.StopPowerPellet();
            }
        }
    }
}