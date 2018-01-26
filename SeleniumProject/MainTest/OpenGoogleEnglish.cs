using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace SeleniumProject
{
    class OpenGoogleEnglish
    {
        public void OpenGoogleEnglishTest(IWebDriver driver, Dictionary<string, string> inputDataCollection, string testCasePath, string inputDriverName)

        {
            try
            {
                driver.Navigate().GoToUrl("https://www.google.co.in");
                driver.FindElement(By.Id("lst-ib")).SendKeys(inputDataCollection["SearchText"]);
                Helper.SaveScreenShot(inputDriverName, driver, testCasePath, "Google.jpg");
                MainTest.passedScriptsCount++;
                Thread.Sleep(1000);
            }
            catch(Exception ex)
            {
                MainTest.failedScriptsCount++;
            }
        }
    }
}
