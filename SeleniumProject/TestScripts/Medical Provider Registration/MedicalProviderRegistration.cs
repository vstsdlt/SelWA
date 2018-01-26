using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Threading;

namespace SeleniumProject
{
    class MedicalProviderRegistration
    {
        public void MedicalProviderRegistrationTest(IWebDriver driver, Dictionary<string, string> inputDataCollection, string testCasePath, string inputDriverName)
        {

            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            
            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            //-----------------Registration Option(s)---------------------------


            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Registration Option.jpg");
            //Click Physician/Practitioner Registration
            driver.FindElement(By.Id("btn_MPRegistration")).Click();

            //-----------------Physician / Practitioner: Account Verification Information Personal Info-----------------------------
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_FirstNa"), TimeSpan.FromSeconds(30));

            //Enter FirstName
            driver.FindElement(By.Id("txt_FirstNa")).Clear();
            driver.FindElement(By.Id("txt_FirstNa")).SendKeys(inputDataCollection["FirstName"]);

            //Enter LastName
            driver.FindElement(By.Id("txt_LastNa")).Clear();
            driver.FindElement(By.Id("txt_LastNa")).SendKeys(inputDataCollection["LastName"]);
            Thread.Sleep(3000);
            //Enter DateOfBirth
            driver.FindElement(By.Id("txt_BirthDt")).Clear();
            driver.FindElement(By.Id("txt_BirthDt")).SendKeys(inputDataCollection["DateOfBirth"]);
            Thread.Sleep(3000);
            //Enter Last Four Digits of Social Security Number
            driver.FindElement(By.Id("txt_SsnLast4")).Clear();
            driver.FindElement(By.Id("txt_SsnLast4")).SendKeys(inputDataCollection["Last Four Digits of SSN"]);

            //Enter Driver License Number
            driver.FindElement(By.Id("txt_DmvNu")).Clear();
            driver.FindElement(By.Id("txt_DmvNu")).SendKeys(inputDataCollection["Driver License Number"]);

            //Enter Re-type Driver License Number
            driver.FindElement(By.Id("txt_RetypeDmvNu")).Clear();
            driver.FindElement(By.Id("txt_RetypeDmvNu")).SendKeys(inputDataCollection["Driver License Number"]);

            //Select Issued By State
            new SelectElement(driver.FindElement(By.Id("slct_DmvStateCd"))).SelectByText(inputDataCollection["Issued By State"]);

            //Select License Type
            new SelectElement(driver.FindElement(By.Id("slct_LicTypeCd"))).SelectByText(inputDataCollection["License Type"]);


            //Enter License Expiration Date
            driver.FindElement(By.Id("txt_LicExpDt")).Clear();
            driver.FindElement(By.Id("txt_LicExpDt")).SendKeys(inputDataCollection["License Expiration Date"]);

            //Enter Physician/Practitioner License Number
            driver.FindElement(By.Id("txt_LicNu")).Clear();
            driver.FindElement(By.Id("txt_LicNu")).SendKeys(inputDataCollection["Physician/Practitioner License Number"]);

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Ref1 Physician Practitioner Account Verification Information.jpg");

            //Click Next
            driver.FindElement(By.Id("btn_Next")).Click();

            //-----------------Physician / Practitioner: Account Verification Information Contact Info-----------------------------
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_mlAddr1"), TimeSpan.FromSeconds(30));


            //Enter Address Line 1
            driver.FindElement(By.Id("txt_mlAddr1")).Clear();
            driver.FindElement(By.Id("txt_mlAddr1")).SendKeys(inputDataCollection["Address Line 1"]);

            //Enter City
            driver.FindElement(By.Id("txt_mlCity")).Clear();
            driver.FindElement(By.Id("txt_mlCity")).SendKeys(inputDataCollection["City"]);

            //Select State
            new SelectElement(driver.FindElement(By.Id("slct_mlStateCd"))).SelectByText(inputDataCollection["State"]);

            //Enter Zip/Postal Code
            driver.FindElement(By.Id("txt_mlZip")).Clear();
            driver.FindElement(By.Id("txt_mlZip")).SendKeys(inputDataCollection["Zip/Postal Code"]);

            //Select Country
            //new SelectElement(driver.FindElement(By.Id("C_N"))).SelectByText(inputDataCollection["Country"]);

            //Enter Phone
            driver.FindElement(By.Id("txt_telNu")).Clear();
            driver.FindElement(By.Id("txt_telNu")).SendKeys(inputDataCollection["Phone"]);

            //Select Yes - When possible, would you like to receive message from us electronically?
            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/form/div/div/div/div/div[2]/div[3]/div/div/div/div[2]/div[1]/div/div/div/div/div[2]/label/span")).Click();

            //Enter E-mail Address
            driver.FindElement(By.Id("txt_Email")).Clear();
            driver.FindElement(By.Id("txt_Email")).SendKeys(inputDataCollection["E-mail Address"]);

            //Enter Re-type E-mail Address
            driver.FindElement(By.Id("txt_RetypeEmail")).Clear();
            driver.FindElement(By.Id("txt_RetypeEmail")).SendKeys(inputDataCollection["E-mail Address"]);

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Ref2 Physician Practitioner Account Verification Information.jpg");

            //Click Next
            driver.FindElement(By.Id("btn_Next")).Click();

            //------------------------Set Password-------------------------------------
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("txt_Password"), TimeSpan.FromSeconds(30));

            //Enter New Password
            driver.FindElement(By.Id("txt_Password")).Clear();
            driver.FindElement(By.Id("txt_Password")).SendKeys(inputDataCollection["New Password"]);

            //Enter Confirm Password
            driver.FindElement(By.Id("txt_RetypePassword")).Clear();
            driver.FindElement(By.Id("txt_RetypePassword")).SendKeys(inputDataCollection["New Password"]);

            //Select Security Question
            new SelectElement(driver.FindElement(By.Id("txt_SecurityQuestion"))).SelectByText(inputDataCollection["Security Question"]);

            //Enter Security Answer
            driver.FindElement(By.Id("txt_SecurityAnswer")).Clear();
            driver.FindElement(By.Id("txt_SecurityAnswer")).SendKeys(inputDataCollection["Security Answer"]);

            //Enter Confirm Security Answer
            driver.FindElement(By.Id("txt_RetypeSecurityAnswer")).Clear();
            driver.FindElement(By.Id("txt_RetypeSecurityAnswer")).SendKeys(inputDataCollection["Security Answer"]);

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Ref3 Set Password.jpg");

            //Click Submit
            driver.FindElement(By.Id("btn_Next")).Click();

            //-------------------------Account Setup Confirmation---------------------------------
            SeleniumDriverObject.ExplicitWaitForPageLoad(driver, By.Id("btn_Login"), TimeSpan.FromSeconds(30));

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "Ref4 Account Setup Confirmation.jpg");
            //Click Login
            driver.FindElement(By.Id("btn_Login")).Click();

            //Login Screen

            //Enter Username
            driver.FindElement(By.Id("txt_UserName")).Clear();
            driver.FindElement(By.Id("txt_UserName")).SendKeys(inputDataCollection["Physician/Practitioner License Number"]);

            //Enter Password
            driver.FindElement(By.Id("txt_Password")).Clear();
            driver.FindElement(By.Id("txt_Password")).SendKeys(inputDataCollection["New Password"]);

            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "MP Login.jpg");
            
            //Click Login
            driver.FindElement(By.Id("btn_Login")).Click();
            Thread.Sleep(3000);
            //Home
            SeleniumDriverObject.SaveScreenShot(inputDriverName, driver, testCasePath, "MP Home.jpg");

        }
    }
}
