using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoChomp.Data
{
    internal class clsData
    {
    }
}


//      < DataGrid x: Name = "dgvCharacter" ItemsSource = "{Binding}"
//          GridLinesVisibility = "Vertical"
//          IsReadOnly = "True"
//          CanUserReorderColumns = "False"
//          CanUserResizeColumns = "False"
//          CanUserSortColumns = "False"
//          CanUserDeleteRows = "False"
//          CanUserResizeRows = "False"
//          SelectionMode = "Single"
//          CanUserAddRows = "False"
//          AutoGenerateColumns = "True"
//          VirtualizingStackPanel.IsVirtualizing = "True"
//          AutomationProperties.IsOffscreenBehavior = "Default" >

//          < DataGrid.CellStyle >
//              < Style TargetType = "DataGridCell" >

//                  < Style.Triggers >
//                      < Trigger Property = "IsSelected" Value = "True" >
//                          < Setter Property = "Background" Value = "#dddddd" />
//                          < Setter Property = "Foreground" Value = "Black" />
//                      </ Trigger >
//                  </ Style.Triggers >

//                  < Setter Property = "BorderBrush" Value = "Black" />
//                  < Setter Property = "BorderThickness" Value = "0,0,0,1" />
//              </ Style >
//          </ DataGrid.CellStyle >
//      </ DataGrid >



//        internal void PopulateDataGridLevel(DataGrid dgv)
//{
//    List<List<String>> lstLstLines = ProcessLevel();
//    if (lstLstLines.Count > 0)
//    {
//        if (dgv.DataContext == null)
//        {
//            dgv.DataContext = null;
//            System.Data.DataTable dt = new System.Data.DataTable("Level");

//            List<string> lstProperties = GetLevelHeader();
//            for (int i = 0; i < lstProperties.Count; i++)
//                dt.Columns.Add(lstProperties[i].ToString());

//            for (int i = 0; i < lstLstLines.Count; i++)
//            {
//                DataRow DataRow = dt.NewRow();
//                dt.Rows.Add(DataRow);
//            }

//            dgv.DataContext = dt.DefaultView;
//        }

//        UpdateValues(dgv, lstLstLines);
//    }
//}



//internal void UpdateValues(DataGrid dgv, List<List<String>> lstLstLines)
//{
//    System.Data.DataView dt = (System.Data.DataView)dgv.DataContext;

//    System.Data.DataTable dt2 = dt.Table;
//    for (int i = 0; i < dt2.Rows.Count; i++)
//        dt2.Rows[i].ItemArray = lstLstLines[i].ToArray();
//}

