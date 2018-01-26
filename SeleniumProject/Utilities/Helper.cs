using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SeleniumProject
{
    class Helper
    {
        public static IWebDriver driver;
        private static string[] browserNames = new string[] { "internet explorer", "firefox", "chrome", "safari" };
        private static bool deviceFlag = false;
        private static string screenShotFlag = ConfigurationManager.AppSettings["SaveScreenShot"];
        public static string currentTestCasePath = string.Empty;
        private static string sessionID = string.Empty;
        private static string folderPath = Directory.GetCurrentDirectory();

        #region User-defined Methods

        /// <summary>
        /// Goes back to previous page
        /// </summary>
        /// <param name="inputDriver"></param>
        public static void BrowserBack(IWebDriver inputDriver)
        {
            try
            {
                IJavaScriptExecutor js = inputDriver as IJavaScriptExecutor;
                string title = (string)js.ExecuteScript("history.go(-1)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Create folder at specified path and name
        /// </summary>
        /// <param name="parentPath"></param>
        /// <param name="folderName"></param>
        /// <param name="outputPath"></param>
        public static void CreateFolder(string parentPath, string folderName, out string outputPath)
        {
            if (screenShotFlag == "Y")
            {
                outputPath = parentPath + "/" + folderName;
                if (!Directory.Exists(outputPath))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(outputPath);
                }
            }
            else
            {
                outputPath = string.Empty;
            }
        }

        public static bool ValidateFixtureTestData(Dictionary<string, string> inputData, string driver)
        {
            if (inputData.ContainsKey("Fixture") && inputData["Fixture"] != driver)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Wait till element is clickable
        /// </summary>
        /// <param name="inputDriver"></param>
        /// <param name="byElement"></param>
        /// <param name="time"></param>
		public static void ExplicitWaitForPageLoad(IWebDriver inputDriver, By byElement, TimeSpan time)
        {
            try
            {


                WebDriverWait wait = new WebDriverWait(inputDriver, time);

                wait.Until(ExpectedConditions.ElementToBeClickable(byElement));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Wait till element is visible
        /// </summary>
        /// <param name="inputDriver"></param>
        /// <param name="byElement"></param>
        /// <param name="time"></param>
        public static void ExplicitWaitForElementDisplay(IWebDriver inputDriver, By byElement, TimeSpan time)
        {
            try
            {


                WebDriverWait wait = new WebDriverWait(inputDriver, time);

                wait.Until(ExpectedConditions.ElementIsVisible(byElement));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resize the browser window
        /// </summary>
        /// <param name="driver"></param>
		public static void ResizeTest(IWebDriver driver)
        {
            if (!deviceFlag)
            {
                driver.Manage().Window.Size = new Size(800, 1200);
            }
        }

        /// <summary>
        /// Highlights the specified element
        /// </summary>
        /// <param name="inputDriver"></param>
        /// <param name="element"></param>
		public static void FocusElement(IWebDriver inputDriver, IWebElement element)
        {
            try
            {
                var jsDriver = (IJavaScriptExecutor)driver;
                string highlightJavascript = @"arguments[0].style.cssText = ""background-Color: yellow"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
                Thread.Sleep(TimeSpan.FromSeconds(0.2));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UnFocusElement(IWebDriver inputDriver, IWebElement element)
        {
            try
            {
                var jsDriver = (IJavaScriptExecutor)driver;
                string highlightJavascript = @"arguments[0].style.cssText = ""background-Color: none"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
                Thread.Sleep(TimeSpan.FromSeconds(0.2));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Event handler method
        /// </summary>
        /// <param name="inputdriver"></param>
        /// <returns></returns>
		public static IWebDriver RegisterEvents(IWebDriver inputdriver)
        {
            EventFiringWebDriver driver = new EventFiringWebDriver(inputdriver);
            driver.ElementClicked += new EventHandler<WebElementEventArgs>(driver_ElementClicked);
            driver.ExceptionThrown += new EventHandler<WebDriverExceptionEventArgs>(driver_ExceptionThrown);
            return driver;
        }

        /// <summary>
        /// Displays the specified window
        /// </summary>
        /// <param name="originalTab"></param>
		public static void GetOriginalWindowBack(String originalTab)
        {

            List<String> lstWindow = driver.WindowHandles.ToList();


            foreach (var handle in lstWindow)
            {
                driver.SwitchTo().Window(handle);
                Thread.Sleep(5000);

            }
            driver.SwitchTo().Window(originalTab);

        }

        /// <summary>
        /// Takes the currently visible display screenshot
        /// </summary>
        /// <param name="inputDriverName"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void SaveScreenShot(string inputDriverName, string path, string fileName)
        {
            if (screenShotFlag == "Y")
            {
                Thread.Sleep(3000);
                if (inputDriverName.Contains("Browser") == true)
                {
                    Thread.Sleep(6000);
                }
                String strDateOne = DateTime.Now.ToString("ddMMMyyyy hhmmss.fff").ToUpper();
                Screenshot screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenPath = string.Format("{0}/" + strDateOne + "_" + fileName, path);
                screenShot.SaveAsFile(screenPath, ScreenshotImageFormat.Jpeg);
            }

        }

        /// <summary>
        /// Scrolls the current window and takes screenshot
        /// </summary>
        /// <param name="inputDriverName"></param>
        /// <param name="inputDriver"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
		public static void SaveScreenShot(string inputDriverName, IWebDriver inputDriver, string path, string fileName)
        {
            if (screenShotFlag == "Y")
            {
                IJavaScriptExecutor js1 = (IJavaScriptExecutor)inputDriver;
                js1.ExecuteScript("window.scrollTo(0,0)");


                IJavaScriptExecutor js2 = (IJavaScriptExecutor)inputDriver;

                Int64 VPHeight = (Int64)js2.ExecuteScript("return window.innerHeight");

                Int64 PageAHeight = (Int64)js2.ExecuteScript("return document.documentElement.clientHeight");

                Int64 PageBHeight = (Int64)js2.ExecuteScript("return document.body.clientHeight");
                Int64 PageHeight;

                if (PageAHeight < PageBHeight)
                    PageHeight = PageBHeight;
                else
                    PageHeight = PageAHeight;

                //Top Screenshot
                String strDate = DateTime.Now.ToString("ddMMMyyyy hhmmss.fff").ToUpper();
                if (inputDriverName.Contains("Browser") == true)
                {
                    Thread.Sleep(6000);
                }
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenPath = string.Format("{0}/" + strDate + "_Screenshot_1_" + fileName, path);
                screenshot.SaveAsFile(screenPath, ScreenshotImageFormat.Jpeg);
                if (inputDriverName.Contains("Browser") == true)
                {
                    Thread.Sleep(6000);
                }
                Thread.Sleep(3000);
                decimal Iteration = PageHeight / (VPHeight - 50);

                int Iterator = (int)Math.Ceiling(Iteration);

                int i;
                for (i = 2; i <= Iterator + 1; i = i + 1)
                {
                    strDate = DateTime.Now.ToString("ddMMMyyyy hhmmss.fff").ToUpper();
                    if (inputDriverName.Contains("Browser") == true)
                    {
                        Thread.Sleep(6000);
                    }
                    IJavaScriptExecutor js3 = (IJavaScriptExecutor)inputDriver;
                    js3.ExecuteScript("window.scrollBy(0,arguments[0])", VPHeight - 50);
                    Thread.Sleep(2000);
                    Screenshot screenshotRep = ((ITakesScreenshot)driver).GetScreenshot();
                    string screenPathRep = string.Format("{0}/" + strDate + "_Screenshot_" + i + "_" + fileName, path);
                    screenshotRep.SaveAsFile(screenPathRep, ScreenshotImageFormat.Jpeg);
                    Thread.Sleep(3000);

                }
            }
        }


        /// <summary>
        /// Scrolls gridview and Takes Screenshots
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="gridOrXpath"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="countOfColumns"></param>
        public static void ScrollGrid(IWebDriver driver, By gridOrXpath, string path, string fileName, int countOfColumns, string inputDriverName)
        {

            if (inputDriverName.Contains("Browser") == true)
            {
                Helper.SaveScreenShot(inputDriverName, driver, path, fileName);
            }

            if (inputDriverName.Contains("Mobile") == true || inputDriverName.Contains("Tab") == true)
            {
                IWebElement ele = driver.FindElement(gridOrXpath);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript("document.querySelector('table th:nth-child(2)').scrollIntoView();", ele);
                Thread.Sleep(3000);
                Helper.SaveScreenShot(inputDriverName, driver, path, "Scroll1_" + fileName);

                js.ExecuteScript("document.querySelector('table th:nth-child(4)').scrollIntoView();", ele);
                Thread.Sleep(3000);
                Helper.SaveScreenShot(inputDriverName, driver, path, "Scroll2_" + fileName);

                if (countOfColumns == 6)
                {
                    js.ExecuteScript("document.querySelector('table th:nth-child(6)').scrollIntoView();", ele);
                    Thread.Sleep(3000);
                    Helper.SaveScreenShot(inputDriverName, driver, path, "Scroll3_" + fileName);

                }

                if (countOfColumns == 5 || countOfColumns > 6)
                {
                    js.ExecuteScript("document.querySelector('table th:last-child').scrollIntoView();", ele);
                    Thread.Sleep(3000);
                    Helper.SaveScreenShot(inputDriverName, driver, path, "Scroll4_" + fileName);
                }


            }
        }

        /// <summary>
        /// This function Clicks the PDF link and Takes screenshots, then take back to Application. Currently supports Iphone, Ipad devices
        /// </summary>
        /// <param name="inputDriverName"></param>
        /// <param name="idOrXpath"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void ClickPDF(string inputDriverName, By idOrXpath, string path, string fileName)
        {
            if (IsElementPresent(idOrXpath) == true)
            {
                //Below code is specific to Apple devices
                if (inputDriverName.Contains("Iphone6SPlus_Mobile") || inputDriverName.Contains("Iphone6S_Mobile") ||
                    inputDriverName.Contains("IpadMini4_Tab") || inputDriverName.Contains("Iphone6SPlus_Mobile_Portrait") ||
                    inputDriverName.Contains("IpadAir_Tab"))
                {
                    Helper.ExplicitWaitForPageLoad(driver, idOrXpath, TimeSpan.FromSeconds(20));
                    driver.FindElement(idOrXpath).Click();
                    Thread.Sleep(60000);
                    Helper.SaveScreenShot(inputDriverName, path, "LinkClicked_Pdf_" + fileName);//Take screenshot of PDF

                    driver.Navigate().Back(); //Navigate Back
                    Thread.Sleep(30000);
                    Helper.SaveScreenShot(inputDriverName, path, "BacktoApp_" + fileName);//Take screenshot after navigating back

                }

            }
        }

        /// <summary>
        /// The method clicks link that opens in a new tab, takes screenshot and return back to original window
        /// </summary>
        /// <param name="inputDriverName"></param>
        /// <param name="idOrXpath"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void ClickLink(string inputDriverName, By idOrXpath, string path, string fileName)
        {
            if (!(inputDriverName.Contains("Iphone6SPlus_Mobile") || inputDriverName.Contains("Iphone6S_Mobile") ||
                inputDriverName.Contains("IpadMini4_Tab") || inputDriverName.Contains("IpadAir_Tab") || inputDriverName.Contains("IE11_Browser") || inputDriverName.Contains("HTCOneM8_Mobile"))) ///This code doesn't work for IOS Devices,HTC One M8 and IE11.
            {
                if (IsElementPresent(idOrXpath) == true)
                {
                    Helper.ExplicitWaitForPageLoad(driver, idOrXpath, TimeSpan.FromSeconds(20));
                    string currentwindow = driver.CurrentWindowHandle;
                    driver.FindElement(idOrXpath).Click();
                    Thread.Sleep(3000);
                    var tabs = driver.WindowHandles;

                    driver.SwitchTo().Window(tabs[tabs.Count - 1]);
                    if (inputDriverName.Contains("Browser") == true)
                        driver.Manage().Window.Maximize();

                    if (inputDriverName.Contains("Resize") == true)
                        Helper.ResizeTest(driver);
                    Thread.Sleep(3000);
                    Helper.SaveScreenShot(inputDriverName, path, "LinkClicked_" + fileName);//Take screenshot of Opened Link
                    Thread.Sleep(3000);
                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);
                    Thread.Sleep(10000);
                    Helper.SaveScreenShot(inputDriverName, path, "LinkClosed_" + fileName);//Take screenshot of Opened Link
                }
            }
        }


        /// <summary>
        /// Checks if the element is present on the page
        /// </summary>
        /// <param name="idOrXpath"></param>
        /// <returns></returns>
        public static bool IsElementPresent(By idOrXpath)
        {
            try
            {
                driver.FindElement(idOrXpath);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        /// <summary>
        /// Waits 5 seconds when an element is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void driver_ElementClicked(object sender, WebElementEventArgs e)
        {
            if (((OpenQA.Selenium.Support.Events.EventFiringWebDriver)(driver)).WrappedDriver.ToString() == "OpenQA.Selenium.Safari.SafariDriver")
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
            else
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.75));
            }
        }

        /// <summary>
        /// Takes screenshot of the exception thrown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private static void driver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1.5));
            string exceptionPath = string.Empty;
            CreateFolder(currentTestCasePath, "Error", out exceptionPath);

            String strDate = DateTime.Now.ToString("ddMMMyyyy hhmmss.fff").ToUpper();
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string screenPath = string.Format("{0}/" + strDate + "Error.jpeg", exceptionPath);
            screenshot.SaveAsFile(screenPath, ScreenshotImageFormat.Jpeg);

        }

        #endregion User-defined Methods
    }
}
