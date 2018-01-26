using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Office.Interop.Excel;

namespace SeleniumEvent
{

    public static class EventDataParameter
    {
        #region variables
        
        private static string folderPath = Directory.GetCurrentDirectory();
        private static string excelDataSourcePath = folderPath + ConfigurationManager.AppSettings["ExcelDataSourcePath"];
        private static string xMLTestResultsPath = folderPath + ConfigurationManager.AppSettings["XMLTestResultsPath"];
        private static string excelTestResultsPath = folderPath + ConfigurationManager.AppSettings["ExcelTestResultsPath"];

        private static List<string> localBrowserList = new List<string>() { "IE", "Chrome", "Firefox", "Safari" };
        public static string testDateTimePath = string.Empty;
        public static string screenShotPath = string.Empty;
        public static string screenShotParentPath = ConfigurationManager.AppSettings["ScreenShotParentPath"];
        public static string deviceOS = string.Empty;
        public static string deviceTestSessionID = string.Empty;

        #endregion variables

        #region methods

        public static void GetDataTabletFromXMLFile(List<TestCaseDetails> listTestDetails, string runStartTime, string runEndTime)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            XmlNodeList xmlnodeTestResult;
            int i, j, k, l = 0;
            string strAtrbName, strAtrbExecuted, strAtrbResult = null;
            string strAtrbSuccess, strAtrbTime, strAtrbAsserts = null;
            string strErrMessage, strErrStackTrace, strRsnMessage = null;
            string strDateTime = DateTime.Now.ToFileTime().ToString();
            int totalScripts = 0;
            int failScripts = 0;
            int ignoredScripts = 0;
            DateTime endTime;
            System.Data.DataTable dtTestCaseResults = new System.Data.DataTable("TestResults");
            dtTestCaseResults.Columns.Add("#");
            dtTestCaseResults.Columns.Add("Test Case Name");
            dtTestCaseResults.Columns.Add("Test Case Begin Time");
            dtTestCaseResults.Columns.Add("Test Case End Time");
            dtTestCaseResults.Columns.Add("Test Case execution Duration");
            dtTestCaseResults.Columns.Add("Language");
            dtTestCaseResults.Columns.Add("Local-BrowserStack");
            dtTestCaseResults.Columns.Add("Browser");
            dtTestCaseResults.Columns.Add("Device");
            dtTestCaseResults.Columns.Add("OS");
            dtTestCaseResults.Columns.Add("SessionId");
            dtTestCaseResults.Columns.Add("Result");
            dtTestCaseResults.Columns.Add("Notes");

            FileStream fs = new FileStream(xMLTestResultsPath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnodeTestResult = xmldoc.GetElementsByTagName("test-results");
            for (l = 0; l <= xmlnodeTestResult[0].Attributes.Count - 1; l++)
            {
                switch (xmlnodeTestResult[0].Attributes.Item(l).Name.ToString())
                {
                    case "total":
                        totalScripts += Convert.ToInt32(xmlnodeTestResult[0].Attributes.Item(l).Value);
                        break;

                    case "errors":
                        failScripts += Convert.ToInt32(xmlnodeTestResult[0].Attributes.Item(l).Value);
                        break;

                    case "failures":
                        failScripts += Convert.ToInt32(xmlnodeTestResult[0].Attributes.Item(l).Value);
                        break;

                    case "ignored":
                        totalScripts += Convert.ToInt32(xmlnodeTestResult[0].Attributes.Item(l).Value);
                        ignoredScripts = Convert.ToInt32(xmlnodeTestResult[0].Attributes.Item(l).Value);
                        break;

                    case "time":
                        TimeSpan time = TimeSpan.Parse(xmlnodeTestResult[0].Attributes.Item(l).Value.ToString());
                        endTime = DateTime.Now;
                        break;
                }
            }
            xmlnode = xmldoc.GetElementsByTagName("test-case");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                strAtrbName = null;
                strAtrbExecuted = null;
                strAtrbResult = null;
                strAtrbSuccess = null;
                strAtrbTime = null;
                strAtrbAsserts = null;

                for (j = 0; j <= xmlnode[i].Attributes.Count - 1; j++)
                {
                    switch (xmlnode[i].Attributes.Item(j).Name.ToString())
                    {
                        case "name":
                            strAtrbName = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;

                        case "executed":
                            strAtrbExecuted = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;

                        case "result":
                            strAtrbResult = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;

                        case "success":
                            strAtrbSuccess = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;

                        case "time":
                            strAtrbTime = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;

                        case "asserts":
                            strAtrbAsserts = xmlnode[i].Attributes.Item(j).Value.ToString();
                            break;
                    }
                }

                strErrMessage = null;
                strErrStackTrace = null;
                strRsnMessage = null;

                if (xmlnode[i].ChildNodes.Count > 0)
                {
                    if (xmlnode[i].ChildNodes.Item(0).Name.ToString() == "failure")
                    {
                        for (k = 0; k <= xmlnode[i].ChildNodes.Item(0).ChildNodes.Count - 1; k++)
                        {
                            switch (xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(k).Name.ToString())
                            {
                                case "message":
                                    strErrMessage = xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(k).InnerText.Trim();
                                    break;

                                case "stack-trace":
                                    strErrStackTrace = xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(k).InnerText.Trim();
                                    break;
                            }
                        }
                    }
                    else if (xmlnode[i].ChildNodes.Item(0).Name.ToString() == "reason")
                    {
                        if (xmlnode[i].ChildNodes.Item(0).ChildNodes.Count > 0)
                        {
                            if (xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(0).Name != null)
                            {
                                if (xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(0).Name.ToString() == "message")
                                {
                                    strRsnMessage = xmlnode[i].ChildNodes.Item(0).ChildNodes.Item(0).InnerText.Trim();
                                }
                            }
                        }
                    }
                }
                string[] strAtrbNameArray = new string[3];
                string language = string.Empty;
                string testDriver = string.Empty;
                if (!string.IsNullOrEmpty(strAtrbName))
                {
                    strAtrbNameArray = strAtrbName.Split('.');
                    if (strAtrbNameArray[1] != null)
                    {
                        testDriver = strAtrbNameArray[1].Replace("\"", "");
                        if (strAtrbNameArray[2].Contains("English"))
                        {
                            language = "English";
                        }
                        else
                        {
                            language = "Spanish";
                        }
                    }
                }

                var timeline = from test in listTestDetails
                               where test.FixtureName == testDriver && test.TestCaseName.Contains(strAtrbNameArray[2])
                               select test;

                dtTestCaseResults.Rows.Add(i + 1, strAtrbNameArray[2].Split('(')[0], timeline.FirstOrDefault().StartTime, timeline.FirstOrDefault().EndTime, strAtrbTime, language, VerifyLocalExecution(SplitString(strAtrbNameArray[1])), VerifyBrowser(SplitString(strAtrbNameArray[1])), VerifyDevice(SplitString(strAtrbNameArray[1])), timeline.FirstOrDefault().OS, timeline.FirstOrDefault().SessionId, strAtrbResult, strErrMessage + strErrStackTrace + strRsnMessage);
            }

            //ExportToExcel(dtTestCaseResults, totalScripts, totalScripts - failScripts - ignoredScripts, failScripts, runStartTime, runEndTime, ignoredScripts, totalScripts - ignoredScripts);
        }

        public static string VerifyLocalExecution(string input)
        {
            if (localBrowserList.Contains(input, StringComparer.OrdinalIgnoreCase))
            {
                return "Local";
            }
            else
            {
                return "BrowserStack";
            }
        }

        public static string VerifyDevice(string input)
        {
            if (localBrowserList.Contains(input, StringComparer.OrdinalIgnoreCase))
            {
                return "N/A";
            }
            else
            {
                return input;
            }
        }

        public static string SplitString(string input)
        {
            input = input.Replace("\"", "");
            input = input.Split('(', ')')[1];
            return input;
        }

        public static string GetDriverName(string input)
        {
            string driver = string.Empty;
            string[] inputArray = input.Split('.');
            if (inputArray.Length > 1)
            {
                driver = inputArray[1].Replace("\"", "");
            }
            return driver;
        }

        public static string VerifyBrowser(string input)
        {
            string output = string.Empty;
            switch (input)
            {
                case "SamsungGalaxyS5":
                    output = "Chrome";
                    break;

                case "SamsungGalaxyTab4":
                    output = "Chrome";
                    break;

                case "Iphone5S":
                    output = "Safari";
                    break;

                case "Iphone6":
                    output = "Safari";
                    break;

                case "IpadMini":
                    output = "Safari";
                    break;

                default:
                    output = input;
                    break;
            }
            return output;
        }
        //
        public static void ExportToExcel(/*this System.Data.DataTable dtTestCaseResults,*/ int totalScripts, int passScripts, int failScripts, string startTime, string endTime, int ignoredScripts, int executedScriptsCount)
        {
            string excelFormatName = excelTestResultsPath + "MMDDYYYY_AutomationRunResult.xlsx";
            //int rowcount = dtTestCaseResults.Rows.Count;
            string strFilePath = screenShotParentPath;
            string strDateTime = DateTime.Now.ToFileTime().ToString();
            Application excelApplication = new Application();
            excelApplication.Workbooks.Open(excelFormatName);
            _Worksheet excelResultSheet = excelApplication.ActiveSheet;
            Range excelCellrange;
            excelResultSheet.Name = "Result";  //dtTestCaseResults.TableName;

            try
            {
                //if (rowcount >= 0)
                {
                    excelResultSheet.Cells[3, 2] = totalScripts.ToString();
                    excelResultSheet.Cells[4, 2] = executedScriptsCount.ToString();
                    excelResultSheet.Cells[5, 2] = ignoredScripts.ToString();
                    excelResultSheet.Cells[6, 2] = passScripts.ToString();
                    excelResultSheet.Cells[7, 2] = failScripts.ToString();
                    excelResultSheet.Cells[8, 2] = startTime;
                    excelResultSheet.Cells[9, 2] = endTime;
                    /*
                    for (int j = 0; j < dtTestCaseResults.Rows.Count; j++)
                    {
                        for (int k = 0; k < dtTestCaseResults.Columns.Count; k++)
                        {
                            excelResultSheet.Cells[j + 15, k + 1] = dtTestCaseResults.Rows[j].ItemArray[k].ToString().Trim();
                        }
                    }
                    
                    excelCellrange = excelResultSheet.Range[excelResultSheet.Cells[1, 1], excelResultSheet.Cells[dtTestCaseResults.Rows.Count + 1, dtTestCaseResults.Columns.Count]];
                    excelCellrange.EntireColumn.AutoFit();

                    Range formatHeader = excelResultSheet.Range[excelResultSheet.Cells[1, 1], excelResultSheet.Cells[1, dtTestCaseResults.Columns.Count]];
                    formatHeader.EntireRow.Font.Bold = true;
                    formatHeader.EntireRow.Font.Color = Color.White;
                    formatHeader.Interior.Color = Color.SteelBlue;

                    Range formatMessageCells = excelResultSheet.Range[excelResultSheet.Cells[1, 8], excelResultSheet.Cells[dtTestCaseResults.Rows.Count + 1, 9]];
                    formatMessageCells.RowHeight = 15;
                    formatMessageCells.EntireColumn.AutoFit();
                    formatMessageCells.ColumnWidth = 70;
                    */
                }
                /*
                else
                {
                    excelResultSheet.Cells[1, 1] = "No results found!";
                }*/
            }
            catch
            {
            }
            finally
            {
                excelResultSheet.SaveAs(@"C:\AutomatedRunResults\Results.xlsx");  //Path.Combine(strFilePath, @"\TestResult " + screenShotParentPath.Split('/')[1] + ".xlsx"));
                excelApplication.Workbooks.Close();
                excelApplication.Quit();
            }
        }

        public static void ArchiveFiles()
        {
            //Copy TestResults.xml to screen shot path
            string sourceFile = Path.Combine(excelTestResultsPath, "TestResult.xml");
            string destFile = Path.Combine(screenShotParentPath, string.Format("TestResult {0}.xml", screenShotParentPath.Split('/')[1]));
            File.Copy(sourceFile, destFile);

            //Copy Input data file TestData.xlsx  to screen shot path
            sourceFile = Path.Combine(excelTestResultsPath, "DataSources\\TestData.xlsx");
            destFile = Path.Combine(screenShotParentPath, string.Format("TestResult {0}.xlsx", screenShotParentPath.Split('/')[1]));
            File.Copy(sourceFile, destFile);
        }

        #endregion methods
    }
}