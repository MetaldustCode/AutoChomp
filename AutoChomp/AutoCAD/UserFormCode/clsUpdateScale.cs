using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp 
{
    internal class clsUpdateScale
    {
        internal void UpdateSpacing()
        {
            clsReg clsReg = new clsReg();

            string strCurrent = clsReg.GetSpacing(out double dblCurrent);

            string strValue =clsCommon.GameForm.cboSpacing.Text;

            if (strValue != strCurrent)
            {
                if (double.TryParse(strValue, out _))
                    clsReg.SetSpacing(strValue);
            }

            // Align to the Current Center Point
            Gameloop.Data.clsDataAlignToGrid clsAlignToGrid = new Gameloop.Data.clsDataAlignToGrid();
            clsAlignToGrid.AlignToGrid(false);
        }
    }
}
