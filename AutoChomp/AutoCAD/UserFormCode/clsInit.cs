using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChomp
{
    internal class clsInit
    {
        internal void Init(xmlMain xmlMain)
        {
            // InitReset();
            // Init global variables

            InitTimers();
            InitKeyBoard();

            InitForm(xmlMain);
            SetComboBox(xmlMain);
            InitDGV(xmlMain);
        }

        internal void InitReset()
        {
            InitMaze();
            InitTables();
            InitReference();
        }

        private void InitKeyBoard()
        {
            // Load Keyboard Monitor
            clsSharpDX.Main();
        }

        private void InitTables()
        {
            clsGenerateTables clsGenerateTables = new clsGenerateTables();
            clsGenerateTables.GenerateTable();
        }

        private void InitDGV(xmlMain xmlMain)
        {
            if (clsCommon.DGVForm == null)
                clsCommon.DGVForm = new DGVForm();

            clsCommon.DGVForm.dgvCharacter = xmlMain.dgvCharacter;
            clsCommon.DGVForm.dgvSpeed = xmlMain.dgvSpeed;
            clsCommon.DGVForm.dgvTimer = xmlMain.dgvTimer;
            clsCommon.DGVForm.dgvLevel = xmlMain.dgvLevel;
            clsCommon.DGVForm.tabControl = xmlMain.TabMain;
        }

        internal void InitForm(xmlMain xmlMain)
        {
            clsReg clsReg = new clsReg();
            xmlMain.TabMain.SelectedIndex = clsReg.GetTab();

            // Fill In UserForm Default Values
            List<String> lstSpacing = SetDropDownSpacing(xmlMain);

            string strSpeed = clsReg.GetSpeed(out double dblSpeed);
            xmlMain.txtNum.Text = strSpeed;

            string strSpacing = clsReg.GetSpacing(out double dblSpacing);
            xmlMain.cboSpacing.SelectedIndex = lstSpacing.IndexOf(strSpacing);
        }

        private void SetComboBox(xmlMain xmlMain)
        {
            List<GameMode> lstGameMode = Enum.GetValues(typeof(GameMode))
                            .Cast<GameMode>()
                            .ToList();

            for (int i = 0; i < lstGameMode.Count; i++)
                xmlMain.cboMode.Items.Add(lstGameMode[i]);

            xmlMain.cboMode.SelectedIndex = 0;
        }

        private List<string> SetDropDownSpacing(xmlMain xmlMain)
        {
            double Middle = clsGridValues.Middle;

            List<string> lstSpacing = new List<string>();

            for (int i = 1; i < Middle; i++)
            {
                double dblValue = Middle / i;
                if (CountDecimalDigits(dblValue) < 8)
                    lstSpacing.Add(dblValue.ToString());
            }

            lstSpacing.Add("1.0");
            lstSpacing.Add("0.5");
            lstSpacing.Add("0.25");

            lstSpacing.Reverse();

            xmlMain.cboSpacing.Items.Clear();

            for (int i = 0; i < lstSpacing.Count; i++)
                xmlMain.cboSpacing.Items.Add(lstSpacing[i]);

            return lstSpacing;
        }

        private int CountDecimalDigits(double n)
        {
            return n.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    .SkipWhile(c => c != '.')
                    .Skip(1)
                    .Count();
        }

        internal void InitReference()
        {
            clsCommon.GameObjectId = new GameObjectId();

            clsCommon.GamePacman = new GamePacman("Pacman", 2);

            clsCommon.GamePosition = new GamePosition();

            clsCommon.GameDots = new GameDots();

            clsCommon.GamePower = new GamePower();

            clsCommon.GameGhostCommon = new GameGhostCommon();

            clsTimers.GameStopWatch = new GameStopWatch();

            clsTimers.GameElapsedTime = new GameElapsedTime();

            // clsTables.GameTracking = new GameTracking();
        }

        internal void InitTimers()
        {
            //if (!clsTimers.GameStopWatch.StopWatchSquiggle.IsRunning)
            //    clsTimers.GameStopWatch.StopWatchSquiggle.Start();
        }

        internal List<List<String>> PacmanMaze()
        {
            List<List<String>> rtnValue = new List<List<string>>();

            clsPacmanMaze clsMaze = new clsPacmanMaze();

            rtnValue.Add(clsMaze.GetGridIsland());
            rtnValue.Add(clsMaze.GetGridBorder());
            rtnValue.Add(clsMaze.GetGridPath());
            rtnValue.Add(clsMaze.GetTunnelPath());
            rtnValue.Add(clsMaze.GetDots());

            return rtnValue;
        }

        internal List<List<String>> MsPacmanMaze1()
        {
            List<List<String>> rtnValue = new List<List<string>>();

            clsMsPacmanMaze1 clsMaze = new clsMsPacmanMaze1();

            rtnValue.Add(clsMaze.GetGridIsland());
            rtnValue.Add(clsMaze.GetGridBorder());
            rtnValue.Add(clsMaze.GetGridPath());
            rtnValue.Add(clsMaze.GetTunnelPath());
            rtnValue.Add(clsMaze.GetDots());

            return rtnValue;
        }

        internal List<List<String>> MsPacmanMaze2()
        {
            List<List<String>> rtnValue = new List<List<string>>();

            clsMsPacmanMaze2 clsMaze = new clsMsPacmanMaze2();

            rtnValue.Add(clsMaze.GetGridIsland());
            rtnValue.Add(clsMaze.GetGridBorder());
            rtnValue.Add(clsMaze.GetGridPath());
            rtnValue.Add(clsMaze.GetTunnelPath());
            rtnValue.Add(clsMaze.GetDots());

            return rtnValue;
        }

        internal List<List<String>> MsPacmanMaze3()
        {
            List<List<String>> rtnValue = new List<List<string>>();

            clsMsPacmanMaze3 clsMaze = new clsMsPacmanMaze3();

            rtnValue.Add(clsMaze.GetGridIsland());
            rtnValue.Add(clsMaze.GetGridBorder());
            rtnValue.Add(clsMaze.GetGridPath());
            rtnValue.Add(clsMaze.GetTunnelPath());
            rtnValue.Add(clsMaze.GetDots());

            return rtnValue;
        }

        internal List<List<String>> MsPacmanMaze4()
        {
            List<List<String>> rtnValue = new List<List<string>>();

            clsMsPacmanMaze4 clsMaze = new clsMsPacmanMaze4();

            rtnValue.Add(clsMaze.GetGridIsland());
            rtnValue.Add(clsMaze.GetGridBorder());
            rtnValue.Add(clsMaze.GetGridPath());
            rtnValue.Add(clsMaze.GetTunnelPath());
            rtnValue.Add(clsMaze.GetDots());

            return rtnValue;
        }

        internal void InitMaze()
        {
            List<GameMaze> lstGameMaze = new List<GameMaze>();

            List<List<String>> Maze = new List<List<string>>();

            for (int i = 0; i < 5; i++)
            {
                if (i == 0) Maze = PacmanMaze();
                if (i == 1) Maze = MsPacmanMaze1();
                if (i == 2) Maze = MsPacmanMaze2();
                if (i == 3) Maze = MsPacmanMaze3();
                if (i == 4) Maze = MsPacmanMaze4();

                lstGameMaze.Add(new GameMaze(Maze[0], Maze[1], Maze[2], Maze[3], Maze[4]));
            }

            clsCommon.lstGameMaze = lstGameMaze;
        }
    }
}