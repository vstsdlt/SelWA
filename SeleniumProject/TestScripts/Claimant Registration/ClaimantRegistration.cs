using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace SeleniumProject
{
    class ClaimantRegistration
    {
        public void ClaimantRegistrationTest(IWebDriver driver, Dictionary<string, string> inputDataCollection, string testCasePath, string inputDriverName)
        {

            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            //************************************************Start Flow************************************************//

            //Navigate to Claimant Registration
            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Registration Option.jpg");
            driver.FindElement(By.Id("btn_ClaimantRegistration")).Click();

            //Claimant 
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_Ssn"), TimeSpan.FromSeconds(30));
            driver.FindElement(By.Id("txt_Ssn")).Clear();
            driver.FindElement(By.Id("txt_Ssn")).SendKeys(inputDataCollection["SSN"]);

            driver.FindElement(By.Id("txt_RetypeSsn")).Clear();
            driver.FindElement(By.Id("txt_RetypeSsn")).SendKeys(inputDataCollection["SSN"]);

            driver.FindElement(By.Id("txt_BirthDt")).Clear();
            driver.FindElement(By.Id("txt_BirthDt")).SendKeys(inputDataCollection["DOB"]);
                        
            if (inputDataCollection["Gender"].ToLower() == "male")
                driver.FindElement(By.XPath("//div[@id='radio_Gender']/div[2]/label")).Click();
            else
                driver.FindElement(By.XPath("//div[@id='radio_Gender']/div[1]/label")).Click();

            driver.FindElement(By.Id("txt_FirstNa")).Clear();
            driver.FindElement(By.Id("txt_FirstNa")).SendKeys(inputDataCollection["FirstName"]);

            driver.FindElement(By.Id("txt_MddlInitNa")).Clear();
            driver.FindElement(By.Id("txt_MddlInitNa")).SendKeys(inputDataCollection["MiddleInitial"]);

            driver.FindElement(By.Id("txt_LastNa")).Clear();
            driver.FindElement(By.Id("txt_LastNa")).SendKeys(inputDataCollection["LastName"]);

            driver.FindElement(By.Id("txt_NameSuffix")).Clear();
            driver.FindElement(By.Id("txt_NameSuffix")).SendKeys(inputDataCollection["Suffix"]);

            driver.FindElement(By.Id("txt_DmvNu")).Clear();
            driver.FindElement(By.Id("txt_DmvNu")).SendKeys(inputDataCollection["DLNumber"]);

            new SelectElement(driver.FindElement(By.Id("slct_DmvStateCd"))).SelectByText("IssuedByState");      //District of Columbia
            new SelectElement(driver.FindElement(By.Id("slct_EthnCd"))).SelectByText("EthnicHeritage");     //
            new SelectElement(driver.FindElement(By.Id("slct_RaceCd"))).SelectByText("Race");               //American Indian/Alaskan Native	

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Claimant Authentication.jpg");
            driver.FindElement(By.Id("btn_Next")).Click();

            //Mailing Address
            //driver.FindElement(By.Id("B_F")).SendKeys("");
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_Mailing_Addr1"), TimeSpan.FromSeconds(30));
            driver.FindElement(By.Id("txt_Mailing_Addr1")).Clear();
            driver.FindElement(By.Id("txt_Mailing_Addr1")).SendKeys(inputDataCollection["AddressLine1"]);

            driver.FindElement(By.Id("txt_Mailing_Addr2")).Clear();
            driver.FindElement(By.Id("txt_Mailing_Addr2")).SendKeys(inputDataCollection["AddressLine2"]);

            driver.FindElement(By.Id("txt_Mailing_City")).Clear();
            driver.FindElement(By.Id("txt_Mailing_City")).SendKeys(inputDataCollection["City"]);

            new SelectElement(driver.FindElement(By.Id("slct_Mailing_StateCd"))).SelectByText("IssuedByState");      //Washington

            driver.FindElement(By.Id("txt_Mailing_Zip")).Clear();
            driver.FindElement(By.Id("txt_Mailing_Zip")).SendKeys(inputDataCollection["ZipCode"]);

            new SelectElement(driver.FindElement(By.Id("slct_Mailing_CountryCd"))).SelectByText("Country");        //United States of America

            //Residential Address
            driver.FindElement(By.Id("chk_SameasResidential")).Click();

            //Telephone Numbers
            driver.FindElement(By.Id("txt_TelNu")).SendKeys(inputDataCollection["HomeTelephoneNumber"]);

            //Correspondence Preference
            driver.FindElement(By.XPath("//div[@id='rd_EmailPrefIn']/div[2]/label")).Click();
            driver.FindElement(By.Id("txt_Email")).SendKeys(inputDataCollection["EmailAddress"]);
            driver.FindElement(By.Id("txt_RetypeEmail")).SendKeys(inputDataCollection["EmailAddress"]);
            new SelectElement(driver.FindElement(By.Id("slct_PrefLanguage1Cd"))).SelectByText("PreferredLanguage");      //English	

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "General Information.jpg");
            driver.FindElement(By.Id("btn_Next")).Click();

            //Set Password
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_Password"), TimeSpan.FromSeconds(30));
            driver.FindElement(By.Id("txt_Password")).SendKeys(inputDataCollection["Password"]);
            driver.FindElement(By.Id("txt_RetypePassword")).SendKeys(inputDataCollection["Password"]);
            new SelectElement(driver.FindElement(By.Id("txt_SecurityQuestion"))).SelectByText("What was the name of your first pet?");
            driver.FindElement(By.Id("txt_SecurityAnswer")).SendKeys("Flipper");
            driver.FindElement(By.Id("txt_RetypeSecurityAnswer")).SendKeys("Flipper");

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Set Password.jpg");
            driver.FindElement(By.Id("btn_Submit")).Click();

            //Click on Login button
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("btn_Login"), TimeSpan.FromSeconds(30));
            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Confirmation.jpg");
            driver.FindElement(By.Id("btn_Login")).Click();

            //Enter User id and Password
            driver.FindElement(By.Id("txt_userName")).SendKeys(inputDataCollection["UserID"]);
            driver.FindElement(By.Id("txt_password")).SendKeys(inputDataCollection["Password"]);

            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("btn_Login"), TimeSpan.FromSeconds(30));
            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Claimant Login.jpg");
            driver.FindElement(By.Id("btn_Login")).Click();

        }

    }
}
