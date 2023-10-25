using System.Windows.Controls;

namespace AutoChomp
{
    internal class GameForm
    {
        internal CheckBox chkSound;
        internal CheckBox chkDot;
        internal CheckBox chkNumber;

        internal CheckBox chkRed;
        internal CheckBox chkPink;
        internal CheckBox chkBlue;
        internal CheckBox chkOrange;

        internal ComboBox cboRed;
        internal ComboBox cboPink;
        internal ComboBox cboBlue;
        internal ComboBox cboOrange;

        internal ComboBox cboSpacing;
        internal TextBox txtInfo;

        internal ComboBox cboMaze;

        internal CheckBox chkDebugDirection;
    }

    internal class DGVForm
    {
        internal DataGrid dgvCharacter;
        internal DataGrid dgvSpeed;
        internal DataGrid dgvLevel;
        internal DataGrid dgvTimer;
        internal TabControl tabControl;
    }
}