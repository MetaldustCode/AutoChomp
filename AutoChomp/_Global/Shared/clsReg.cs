using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutoChomp
{
    internal class clsReg
    {
        internal static string strBase = "SOFTWARE";
        internal static string strGPKey = "AutoChomp";
        internal static string strGPProgramName = "AutoChomp";

        internal String GetAutoChomp()
        {
            return getGuid("AutoChompPalette");
        }

        internal void SetHighScore(string strValue)
        {
            SaveData("HighScore", strValue);
        }

        internal string GetHighScore(out int intSpeed)
        {
            string strValue = GetData("HighScore");

            if (int.TryParse(strValue, out intSpeed))
                return strValue;
            else
            {
                intSpeed = 0;
                return "0";
            }
        }

        internal void SetSpeed(string strValue)
        {
            SaveData("StartSpeed", strValue);
        }

        internal string GetSpeed(ref double dblSpeed)
        {
            string strValue = GetData("StartSpeed");

            if (double.TryParse(strValue, out dblSpeed))
                return strValue;
            else
            {
                dblSpeed = 6;
                return "6";
            }
        }

        internal void SetSpacing(string strValue)
        {
            SaveData("Spacing", strValue);
        }

        internal string GetSpacing(ref double dblSpacing)
        {
            string strValue = GetData("Spacing");

            if (double.TryParse(strValue, out dblSpacing))
                return strValue;
            else
            {
                dblSpacing = 1.0;
                return "1.0";
            }
        }

        internal void SetMazeIndex(int strValue)
        {
            SaveData("ShowMazeIndex", strValue.ToString());
        }

        internal int GetMazeIndex()
        {
            string strValue = GetData("ShowMazeIndex");

            if (strValue.Length == 0)
                return 0;

            if (int.TryParse(strValue, out int intValue))
            {
                return intValue;
            }
            else
                return 0;
        }

        internal void SetShowDirection(Boolean strValue)
        {
            SaveData("ShowDirection", strValue.ToString());
        }

        internal Boolean GetShowDirection()
        {
            string strValue = GetData("ShowDirection");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetShowHighScore(Boolean strValue)
        {
            SaveData("ShowHighScore", strValue.ToString());
        }

        internal Boolean GetShowHighScore()
        {
            string strValue = GetData("ShowHighScore");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetIncrementLevel(Boolean strValue)
        {
            SaveData("IncrementLevel", strValue.ToString());
        }
        internal Boolean GetIncrementLevel()
        {
            string strValue = GetData("IncrementLevel");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetShowSuggestion(Boolean strValue)
        {
            SaveData("ShowSuggestion", strValue.ToString());
        }

        internal Boolean GetShowSuggestion()
        {
            string strValue = GetData("ShowSuggestion");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }


        internal void SetDebugDirection(Boolean strValue)
        {
            SaveData("ShowDebugDirection", strValue.ToString());
        }

        internal Boolean GetDebugDirection()
        {
            string strValue = GetData("ShowDebugDirection");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }


        internal void SetGhostEatDots(Boolean strValue)
        {
            SaveData("ShowGhostEatDots", strValue.ToString());
        }

        internal Boolean GetGhostEatDots()
        {
            string strValue = GetData("ShowGhostEatDots");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }


        internal void SetPacmanEatDots(Boolean strValue)
        {
            SaveData("ShowPacmanEatDots", strValue.ToString());
        }

        internal Boolean GetPacmanEatDots()
        {
            string strValue = GetData("ShowPacmanEatDots");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }





        internal void SetShowHistory(Boolean strValue)
        {
            SaveData("ShowHistory", strValue.ToString());
        }

        internal Boolean GetShowHistory()
        {
            string strValue = GetData("ShowHistory");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetUseHistory(Boolean strValue)
        {
            SaveData("UseHistory", strValue.ToString());
        }

        internal Boolean GetUseHistory()
        {
            string strValue = GetData("UseHistory");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetUseForward(Boolean strValue)
        {
            SaveData("UseForward", strValue.ToString());
        }

        internal Boolean GetUseForward()
        {
            string strValue = GetData("UseForward");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetShowGridNumbers(String strValue)
        {
            SaveData("ShowGridNumbers", strValue);
        }

        internal String GetShowGridNumbers()
        {
            string strValue = GetData("ShowGridNumbers");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetShowGhostPath(String strValue)
        {
            SaveData("ShowGhostPath", strValue);
        }

        internal String GetShowGhostPath()
        {
            string strValue = GetData("ShowGhostPath");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        // "Red", "Pink", "Blue", "Orange"

        internal void SetPacmanSearchMode(String strValue)
        {
            SaveData("PacmanSearchMode", strValue);
        }

        internal String GetPacmanSearchMode()
        {
            string strValue = GetData("PacmanSearchMode");

            if (strValue.Length == 0)
                return "Keyboard";
            else
                return strValue;
        }

        internal void SetRedSearchMode(String strValue)
        {
            SaveData("RedSearchMode", strValue);
        }

        internal String GetRedSearchMode()
        {
            string strValue = GetData("RedSearchMode");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetPinkSearchMode(String strValue)
        {
            SaveData("PinkSearchMode", strValue);
        }

        internal String GetPinkSearchMode()
        {
            string strValue = GetData("PinkSearchMode");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetBlueSearchMode(String strValue)
        {
            SaveData("BlueSearchMode", strValue);
        }

        internal String GetBlueSearchMode()
        {
            string strValue = GetData("BlueSearchMode");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetOrangeSearchMode(String strValue)
        {
            SaveData("OrangeSearchMode", strValue);
        }

        internal String GetOrangeSearchMode()
        {
            string strValue = GetData("OrangeSearchMode");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        // "Red", "Pink", "Blue", "Orange"

        internal void SetPacmanSearchVisible(Boolean strValue)
        {
            SaveData("PacmanSearchVisible", strValue.ToString());
        }

        internal Boolean GetPacmanSearchVisible()
        {
            string strValue = GetData("PacmanSearchVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        internal void SetRedSearchVisible(Boolean strValue)
        {
            SaveData("RedSearchVisible", strValue.ToString());
        }

        internal Boolean GetRedSearchVisible()
        {
            string strValue = GetData("RedSearchVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        internal void SetPinkSearchVisible(Boolean strValue)
        {
            SaveData("PinkSearchVisible", strValue.ToString());
        }

        internal Boolean GetPinkSearchVisible()
        {
            string strValue = GetData("PinkSearchVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        internal void SetBlueSearchVisible(Boolean strValue)
        {
            SaveData("BlueSearchVisible", strValue.ToString());
        }

        internal Boolean GetBlueSearchVisible()
        {
            string strValue = GetData("BlueSearchVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        internal void SetOrangeSearchVisible(Boolean strValue)
        {
            SaveData("OrangeSearchVisible", strValue.ToString());
        }

        internal Boolean GetOrangeSearchVisible()
        {
            string strValue = GetData("OrangeSearchVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        // "Red", "Pink", "Blue", "Orange"

        internal void SetRedSpeed(String strValue)
        {
            SaveData("RedSpeed", strValue);
        }

        internal String GetRedSpeed()
        {
            string strValue = GetData("RedSpeed");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetPinkSpeed(String strValue)
        {
            SaveData("PinkSpeed", strValue);
        }

        internal String GetPinkSpeed()
        {
            string strValue = GetData("PinkSpeed");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetBlueSpeed(String strValue)
        {
            SaveData("BlueSpeed", strValue);
        }

        internal String GetBlueSpeed()
        {
            string strValue = GetData("BlueSpeed");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        internal void SetOrangeSpeed(String strValue)
        {
            SaveData("OrangeSpeed", strValue);
        }

        internal String GetOrangSpeed()
        {
            string strValue = GetData("OrangeSpeed");

            if (strValue.Length == 0)
                return "None";
            else
                return strValue;
        }

        // -------------

        internal void SetDotsVisible(Boolean strValue)
        {
            SaveData("DotsVisible", strValue.ToString());
        }

        internal Boolean GetDotsVisible()
        {
            string strValue = GetData("DotsVisible");

            if (strValue.Length == 0 || strValue == "True")
                return true;
            else
                return false;
        }

        internal void SetNumbersVisible(Boolean strValue)
        {
            SaveData("NumbersVisible", strValue.ToString());
        }

        internal Boolean GetNumbersVisible()
        {
            string strValue = GetData("NumbersVisible");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        internal void SetPlaySound(Boolean strValue)
        {
            SaveData("PlaySound", strValue.ToString());
        }

        internal Boolean GetPlaySound()
        {
            string strValue = GetData("PlaySound");

            if (strValue.Length == 0 || strValue == "False")
                return false;
            else
                return true;
        }

        //======================

        internal void SetTab(String strValue)
        {
            SaveData("Tab", strValue);
        }

        internal int GetTab()
        {
            string strValue = GetData("Tab");

            if (strValue.Length == 0)
                return 0;
            else
                return strValue.ToInt();
        }

        #region "Basic Code"

        private bool getGPkey(ref RegistryKey gpKey)
        {
            bool rtnValue = false;
            RegistryKey regBaseKey = null;
            try
            {
                // get the base software key
                regBaseKey = Registry.CurrentUser.OpenSubKey(strBase, true);
                if (getSubKey(ref regBaseKey, strGPKey, ref gpKey))
                {
                    rtnValue = true;
                }
                else
                {
                    rtnValue = false;
                }
            }
            catch (System.Exception)
            {
                rtnValue = false;
            }
            finally
            {
                regBaseKey.Close();
            }

            return rtnValue;
        }

        // Get GUID of palette or create a new one
        private string getGuid(string strKey)
        {
            string strGuid = "";
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    RegistryKey guidKey = null;
                    // Get sub key
                    if (getSubKey(ref gpKey, strKey, ref guidKey))
                    {
                        // Get Guid
                        if (hasValue(ref guidKey, "GUID"))
                        {
                            strGuid = guidKey.GetValue("GUID", "").ToString();
                        }
                        else
                        {
                            // Create new Guid
                            strGuid = "{" + System.Guid.NewGuid().ToString() + "}";
                            guidKey.SetValue("GUID", strGuid);
                        }
                        guidKey.Close();
                    }
                }
                gpKey.Close();
                return strGuid;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving GUID" + "\n" + ex.ToString());
            }
            return "";
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                lstKeys.Add(item);
            }

            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyValueList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    lstKeys.Add(item);
                }
            }
            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool hasValue(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        //// Delete Key
        //private bool DeleteSubKey(string strKey)
        //{
        //    try
        //    {
        //        RegistryKey gpKey = null;
        //        // get Default key
        //        if (getGPkey(ref gpKey))
        //        {
        //            if (hasSubKey(ref gpKey, strKey))
        //            {
        //                //RegistryKey subkey = null;
        //                // Delete Sub Key
        //                gpKey.DeleteSubKey(strKey, true);
        //            }
        //            gpKey.Close();
        //        }

        //        return true;
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //}

        // Delete the name in the SubKey Given
        private bool DeleteSubKeyName(string strKey, string name)
        {
            bool rtnValue = false;
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    if (hasSubKey(ref gpKey, strKey))
                    {
                        RegistryKey subkey = null;
                        // get subkey
                        if (getSubKey(ref gpKey, strKey, ref subkey))
                        {
                            // Delete the name of the Key
                            foreach (string item in subkey.GetSubKeyNames())
                            {
                                if (item == name)
                                {
                                    subkey.DeleteValue(name);
                                    rtnValue = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            subkey.DeleteValue(name);
                            subkey.Close();
                        }
                    }
                    gpKey.Close();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return rtnValue;
        }

        // Check if the Registry Key contains a subkey
        private bool hasSubKey(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        // get SubKey and or create a new one and return the new key
        private bool getSubKey(ref RegistryKey key, string strkey, ref RegistryKey rtnKey)
        {
            try
            {
                if (!hasSubKey(ref key, strkey))
                {
                    key.CreateSubKey(strkey);
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
                else
                {
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        internal void setproject(string Value)
        {
            SaveValue(strGPProgramName, "LSTPROJECT", Value);
        }

        internal string getproject()
        {
            string strValue = GetValue(strGPProgramName, "LSTPROJECT");
            return strValue;
        }

        internal void SaveData(string strName, string Value)
        {
            SaveValue(strGPProgramName, strName, Value);
        }

        internal string GetData(string strName)
        {
            string strValue = GetValue(strGPProgramName, strName);
            return strValue;
        }

        internal void SaveValue(string strKey, string myName, string myData)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    subkey.SetValue(myName, myData);
                }
            }
        }

        internal string GetValue(string strKey, string myName)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    return (string)subkey.GetValue(myName, "");
                }
            }
            return "";
        }

        internal string getLastProject()
        {
            string strValue = GetValue(strGPProgramName, "LASTPROJECT");
            return strValue;
        }

        internal void setLastProject(string Value)
        {
            SaveValue(strGPProgramName, "LASTPROJECT", Value.ToString());
        }

        internal void SaveProjectDefaults(List<string> lstProjects, List<string> lstDefault)
        {
            string strCombined = "";
            for (int i = 0; i <= lstProjects.Count - 1; i++)
            {
                string strValues = "";
                for (int k = 0; k <= lstDefault.Count - 1; k++)
                {
                    if (lstDefault.Count - 1 == k)
                    {
                        strValues += lstDefault[k];
                    }
                    else
                    {
                        strValues += lstDefault[k] + "|";
                    }
                }
                strCombined += lstProjects[i] + "|" + strValues + "$";
            }
        }

        #endregion "Basic Code"
    }
}