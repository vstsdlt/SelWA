using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace SeleniumProject
{
    class OpenGoogleEnglish
    {
        public void OpenGoogleEnglishTest(IWebDriver driver, Dictionary<string, string> inputDataCollection, string testCasePath, string inputDriverName)

        {
            driver.Navigate().GoToUrl("https://www.google.co.in");
            driver.FindElement(By.Id("lst-ib")).SendKeys(inputDataCollection["SearchText"]);
            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Google.jpg");
            Thread.Sleep(5000);

        }
    }
}
