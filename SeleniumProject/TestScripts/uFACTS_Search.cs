using System.Collections.Generic;
using OpenQA.Selenium;
using System.Configuration;
using System;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProject
{
    class UFACTS_Search
    {
        public void UFACTS_SearchTest(IWebDriver driver, Dictionary<string, string> inputDataCollection, string testCasePath, string inputDriverName)

        {
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

                driver.Navigate().GoToUrl(baseUrl);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                //-----------------Search Screen---------------------------

                //Select Source Type
                new SelectElement(driver.FindElement(By.Id("B_D"))).SelectByText(inputDataCollection["Source Type"]);

                //Enter Source Name
                driver.FindElement(By.Id("B_F")).Clear();
                driver.FindElement(By.Id("B_F")).SendKeys(inputDataCollection["Source Name"]);

                //Select Target Type
                new SelectElement(driver.FindElement(By.Id("B_G"))).SelectByText(inputDataCollection["Source Type"]);

                //Enter Target Name
                driver.FindElement(By.Id("B_H")).Clear();
                driver.FindElement(By.Id("B_H")).SendKeys(inputDataCollection["Source Name"]);

                //Select Access Type
                new SelectElement(driver.FindElement(By.Id("B_J"))).SelectByText(inputDataCollection["Access Type"]);

                //Click Search
                driver.FindElement(By.Id("B_L")).Click();

                Helper.SaveScreenShot(inputDriverName, driver, testCasePath, "uFACTS_Search.jpg");
            }             
            catch(Exception ex)
            {
                MainTest.failedScriptsCount++;
            }

}
    }
}
