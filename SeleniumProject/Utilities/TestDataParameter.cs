using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OpenQA.Selenium;
using OfficeOpenXml;
using System.Xml;
using System;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;
namespace SeleniumProject
{
    public static class TestDataParameter
	{
		private static string folderPath = Directory.GetCurrentDirectory();
		private static string excelDataSourcePath = folderPath + ConfigurationManager.AppSettings["ExcelDataSourcePath"];
		private static string xMLTestResultsPath = folderPath + ConfigurationManager.AppSettings["XMLTestResultsPath"];
		private static string excelTestResultsPath = folderPath + ConfigurationManager.AppSettings["ExcelTestResultsPath"];
		private static string xmlTestCaseDataPath = folderPath + ConfigurationManager.AppSettings["TestDataPath"];
		public static string testDateTimePath = string.Empty;
		public static string screenShotPath = string.Empty;
        public static Dictionary<string, List<Dictionary<string, string>>> parentDictionary;


		#region methods

		/// <summary>
		/// This method loads the xml on which the browser values are set for running the test
		/// </summary>
		public static XDocument ReadMobileTestCaseDatafromXML()
		{
			XDocument xmlTestCaseData = XDocument.Load(xmlTestCaseDataPath);
			return xmlTestCaseData;
		}

		/// <summary>
		///  Get Driver Name From XML
		/// </summary>
		/// <param name="name"></param>
		/// <param name="testCaseName"></param>
		/// <returns></returns>
		public static IWebDriver GetDriverNameFromXML(string name, string testCaseName)
		{
			IWebDriver driver;
			string selectedBrowser = string.Empty;
			//Read driver from XML file
            
			XDocument xmlTestCaseData = TestDataParameter.ReadMobileTestCaseDatafromXML();
			var browsers = from browser in xmlTestCaseData.Descendants("TestFixtures").Descendants("add")
						   select new { Include = browser.Attribute("Include").Value, Name = browser.Attribute("name").Value };

			browsers = browsers.Where(x => x.Include == "1" && x.Name == name.Trim());
			if (browsers.Count() > 0)
			{
				selectedBrowser = browsers.FirstOrDefault().Name;//read from data xml
			}
			else
			{
				return null;
			}
            

            //selectedBrowser = name;
			switch (selectedBrowser)
			{
                //Browsers

                case Constants.BROWSER_CHROMELOCAL:
                    driver = SeleniumDriverObject.GetChromeDriver();
                    break;

                case Constants.BROWSER_IELOCAL:
                    driver = SeleniumDriverObject.GetInternetExplorerDriver();
                    break;

                case Constants.BROWSER_FIREFOXLOCAL:
                    driver = SeleniumDriverObject.GetFireFoxDriver();
                    break;
                    
                case Constants.BROWSER_IE_11:
					driver = SeleniumDriverObject.Get_Browserstack_IE11(testCaseName, name);
					break;

				case Constants.BROWSER_CHROME:
					driver = SeleniumDriverObject.Get_Browserstack_Chrome(testCaseName, name);
					break;

				case Constants.BROWSER_FIREFOX:
					driver = SeleniumDriverObject.Get_Browserstack_Firefox(testCaseName, name);
					break;

				case Constants.BROWSER_SAFARI:
					driver = SeleniumDriverObject.Get_Browserstack_Safari(testCaseName, name);
					break;

				//Iphone and Ipad Portrait
				case Constants.BROWSERSTACK_IPHONE6S_PORTRAIT:
					driver = SeleniumDriverObject.GetIphone6SBrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPADMINI4_PORTRAIT:
					driver = SeleniumDriverObject.GetIpadMini4BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPHONE6SPLUS_PORTRAIT:
					driver = SeleniumDriverObject.GetIphone6PlusBrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPADAIR_PORTRAIT:
					driver = SeleniumDriverObject.GetIpadAirBrowserStackDriver(testCaseName, name);
					break;

				// Iphone and Ipad Landscape
				case Constants.BROWSERSTACK_IPHONE6S_LANDSCAPE:
					driver = SeleniumDriverObject.GetIphone6SBrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPADMINI4_LANDSCAPE:
					driver = SeleniumDriverObject.GetIpadMini4BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPHONE6SPLUS_LANDSCAPE:
					driver = SeleniumDriverObject.GetIphone6PlusBrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_IPADAIR_LANDSCAPE:
					driver = SeleniumDriverObject.GetIpadAirBrowserStackDriver(testCaseName, name);
					break;

				//ANDROID Portrait
				case Constants.BROWSERSTACK_HTCONEM8_PORTRAIT:
					driver = SeleniumDriverObject.GetHTCOneM8BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_SAMSUNGNOTE4_PORTRAIT:
					driver = SeleniumDriverObject.GetSamsungGalaxyNote4BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_SAMSUNGS7_PORTRAIT:
					driver = SeleniumDriverObject.GetSamsungGalaxyS7BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_GOOGLENEXUS9_PORTRAIT:
					driver = SeleniumDriverObject.GetGoogleNexus9BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_GOOGLEPIXEL_PORTRAIT:
					driver = SeleniumDriverObject.GetGooglePixelBrowserStackDriver(testCaseName, name);
					break;

				//ANDROID LANDSCAPE
				case Constants.BROWSERSTACK_HTCONEM8_LANDSCAPE:
					driver = SeleniumDriverObject.GetHTCOneM8BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_SAMSUNGS7_Landscape:
					driver = SeleniumDriverObject.GetSamsungGalaxyS7BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_SAMSUNGNOTE4_Landscape:
					driver = SeleniumDriverObject.GetSamsungGalaxyNote4BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_GOOGLENEXUS9_LANDSCAPE:
					driver = SeleniumDriverObject.GetGoogleNexus9BrowserStackDriver(testCaseName, name);
					break;

				case Constants.BROWSERSTACK_GOOGLEPIXEL_LANDSCAPE:
					driver = SeleniumDriverObject.GetGooglePixelBrowserStackDriver(testCaseName, name);
					break;
                    
				default:
					driver = null;
					break;
			}
			return driver;
		}

		/// <summary>
		/// Include Test Case
		/// </summary>
		/// <param name="testName"></param>
		/// <returns></returns>
		public static bool IncludeTestCase(string testName)
		{
			XDocument xmlTestCaseData = TestDataParameter.ReadMobileTestCaseDatafromXML();
			var testCases = from testCase in xmlTestCaseData.Descendants("TestCases").Descendants("add")
							select new { Include = testCase.Attribute("Include").Value, Name = testCase.Attribute("name").Value };
			testCases = testCases.Where(x => x.Include == "1" && x.Name == testName);
			if (testCases.Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        private static List<string> GetAllTestCaseName()
        {
            List<string> strarr = new List<string>();
           
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(excelDataSourcePath,0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            Sheets sheets = xlWorkbook.Worksheets;
            Worksheet xlWorksheet = (Worksheet)sheets.get_Item(1);
            Range xlFirstColumn = xlWorksheet.UsedRange.Columns[1];
            
            System.Array myvalues = (System.Array)xlFirstColumn.Cells.Value;
            string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
            foreach (string val in strArray)
           {
               strarr.Add(val);
           }

            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(0);
            xlApp.Quit();            
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlApp);
            return strarr;
        }

        
        /// <summary>
        /// This method loads the browser types for which tests should be run
        /// </summary>
        public static List<Dictionary<string, string>> TestDataExcelDictionary(string testCaseName)
		{
            List<Dictionary<string, string>> testDataDictionary;
            
            if (parentDictionary == null)
            {
                List<string> strarr = GetAllTestCaseName();

                parentDictionary = new Dictionary<string, List<Dictionary<string, string>>>();
               
                if (parentDictionary.Count == 0)
                {
                    var ep = new ExcelPackage(new FileInfo(excelDataSourcePath));
                    foreach (string item in strarr)
                    {
                        
			            ExcelWorksheet ws = ep.Workbook.Worksheets["TestCaseData"];
			            Dictionary<string, object> testData = new Dictionary<string, object>();
			            int startRow = 0, endRow = 0, endColumn = 0, cRow = 0;
			            for (int row = 1; row <= ws.Dimension.End.Row; row++)
			            {
				            if (ws.Cells[row, 1].Value != null && ws.Cells[row, 1].Value.ToString().Trim() == item)
				            {
					            startRow = row;
					            
				            }
				            for (int column = 2; column <= ws.Dimension.End.Column; column++)
				            {
					            if (ws.Cells[row, column].Value != null && ws.Cells[row, column].Value.ToString().Trim() == item)
					            {
						            endRow = row;
						            endColumn = column;
						            break;
					            }
				            }
			            }
			            Collection<string[]> testDataArray = new Collection<string[]>();
			            testDataDictionary = new List<Dictionary<string, string>>();
			            
			            for (int row = startRow + 1; row < endRow; row++, cRow++)
			            {
				            int cColumn = 0;
				            Dictionary<string, string> temp = new Dictionary<string, string>();
                            for (int column = 2; column < endColumn; column++, cColumn++)
                            {
                                if (ws.Cells[row, column].Value == null)
                                {
                                    temp.Add(ws.Cells[startRow, column].Value.ToString().Trim(), string.Empty);
                                }
                                else
                                {
                                    temp.Add(ws.Cells[startRow, column].Value.ToString().Trim(), ws.Cells[row, column].Value.ToString().Trim());
                                }
                            }
				            testDataDictionary.Add(temp);
			            }
                        parentDictionary.Add(item, testDataDictionary);
                    }
                }
            }

            testDataDictionary = parentDictionary[testCaseName];
            
            return testDataDictionary;
		}
        
		/// <summary>
		/// Get Sub Node
		/// </summary>
		/// <param name="vnode"></param>
		/// <param name="outfilewriter"></param>
		private static void GetSubNode(XmlNode vnode, StreamWriter outfilewriter)
		{
			try
			{
				while (vnode.HasChildNodes == true)
				{
					string stringtest = "";
					foreach (XmlAttribute atttest1 in vnode.Attributes)
					{
						if (atttest1.OwnerElement.Name != "test-suite")
						{
							stringtest = stringtest + atttest1.Value + ",";
						}
					}
					if (!string.IsNullOrEmpty(stringtest))
					{
						outfilewriter.WriteLine(stringtest.Replace("\"", ""));
					}

					foreach (XmlNode vchildnode in vnode)
					{
						if (vchildnode.Name != "environment" && vchildnode.Name != "failure" && vchildnode.Name != "culture-info")
						{
							GetSubNode(vchildnode, outfilewriter);
						}
					}
					return;
				}

				while (vnode.HasChildNodes == false)
				{
					string stringtest = "";
					foreach (XmlAttribute atttest1 in vnode.Attributes)
					{
						if (atttest1.OwnerElement.Name != "test-suite")
						{
							stringtest = stringtest + atttest1.Value + ",";
						}
					}
					if (!string.IsNullOrEmpty(stringtest))
					{
						outfilewriter.WriteLine(stringtest.Replace("\"", ""));
					}
					return;
				}
			}
			catch (Exception ex)
			{
			}
		}
        
		#endregion methods
	}
}