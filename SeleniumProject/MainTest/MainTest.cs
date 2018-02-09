using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace SeleniumProject
{
    //Local Browsers
    [TestFixture("Chrome_Local")]
    [TestFixture("IE_Local")]
    //[TestFixture("Firefox_Local")]

    /*
    [TestFixture("IE11_Browser")]
    [TestFixture("Chrome_Browser")]
    [TestFixture("Firefox_Browser")]
    [TestFixture("Safari_Browser")]
    
    //Iphone and Ipad
    [TestFixture("Iphone6SPlus_Mobile_Portrait")]
    [TestFixture("Iphone6SPlus_Mobile_Landscape")]
    [TestFixture("Iphone6S_Mobile_Portrait")]
    [TestFixture("Iphone6S_Mobile_Landscape")]
    [TestFixture("IpadMini4_Tab_Portrait")]
    [TestFixture("IpadMini4_Tab_Landscape")]
    [TestFixture("IpadAir_Tab_Portrait")]
    [TestFixture("IpadAir_Tab_Landscape")]

    //Android Devices
    [TestFixture("HTCOneM8_Mobile_Portrait")]
    [TestFixture("HTCOneM8_Mobile_Landscape")]
    [TestFixture("GooglePixel_Mobile_Portrait")]
    [TestFixture("GooglePixel_Mobile_Landscape")]
    [TestFixture("SamsungGalaxyS7_Mobile_Portrait")]
    [TestFixture("SamsungGalaxyS7_Mobile_Landscape")]
    [TestFixture("SamsungGalaxyNote4_Tab_Portrait")]
    [TestFixture("SamsungGalaxyNote4_Tab_Landscape")]
    [TestFixture("GoogleNexus9_Tab_Portrait")]
    [TestFixture("GoogleNexus9_Tab_Landscape")]
    */

        //Demo
    public class MainTest
    {
        #region Private variables

        private IWebDriver driver;
        public int countDataSet;
        
        public string inputDriverName = string.Empty;
        private string screenShotPath = string.Empty;
        private string loggedInUserName = string.Empty;

        public static string fixtureStartTime, fixtureEndTime = String.Empty;
        public static int totalScriptsCount, ignoredScriptsCount, passedScriptsCount, failedScriptsCount = 0;
        public static bool ignoredScriptsFlag;
        public static bool includeTestCase;

        #endregion Private variables


        #region Test Settings

        public MainTest(string inputDriver)
        {
            inputDriverName = inputDriver;
        }


        [TestFixtureSetUp]
        public void Init()
        {
            countDataSet = 0;
            
            screenShotPath = TestDataParameter.testDateTimePath;
            SeleniumEvent.EventDataParameter.deviceOS = string.Empty;
            SeleniumEvent.EventDataParameter.deviceTestSessionID = string.Empty;
        }

        [TestFixtureTearDown]
        public void TeardownFixture()
        {
           
        }

        [SetUp]
        public void SetupTest()
        {
            totalScriptsCount++;
            countDataSet++;
            ignoredScriptsFlag = true;
            if (inputDriverName != string.Empty)
            {
                driver = TestDataParameter.GetDriverNameFromXML(inputDriverName, TestContext.CurrentContext.Test.Name);

                if (driver == null)
                {
                    Assert.Ignore("Fixture doesn't exist in the list");
                }
            }
            else
            {
                Assert.Ignore("Parameters are not provided for Fixture");
            }

            includeTestCase = TestDataParameter.IncludeTestCase(TestContext.CurrentContext.Test.Name.Substring(0, TestContext.CurrentContext.Test.Name.IndexOf("(")));
        }

        [TearDown]
        public void TeardownTest()
        {
            if (ignoredScriptsFlag == true)
            {
                ignoredScriptsCount++;
            }

            driver.Quit();
            
        }

        #endregion Test Settings

        #region Test methods

        private void ValidateFixtureTestData(Dictionary<string, string> inputDataCollection, string inputDriverName)
        {
            if (!Helper.ValidateFixtureTestData(inputDataCollection, inputDriverName))
            {
                Assert.Ignore(string.Format("Fixture:{0}  does not match input data", inputDriverName));
            }

        }

        [Test(Description = "OpenGoogleEnglish"), TestCaseSource(typeof(TestDataConstructors), "GetOpenGoogleEnglishData")]
        public void OpenGoogleEnglish(Dictionary<string, string> inputDataCollection)
        {
            if (includeTestCase)
            {
                ignoredScriptsFlag = false;
                string testCasePath = string.Empty;
                Helper.CreateFolder(screenShotPath, "OpenGoogleEnglish/" + inputDriverName + "_DataSet_" + countDataSet, out testCasePath);
                SeleniumDriverObject.currentTestCasePath = testCasePath;
                OpenGoogleEnglish ObjOpenGoogleEnglish = new OpenGoogleEnglish();

                ObjOpenGoogleEnglish.OpenGoogleEnglishTest(driver, inputDataCollection, testCasePath, inputDriverName);
            }
        
        }

        //[Test(Description = "UFACTS_Search"), TestCaseSource(typeof(TestDataConstructors), "GetuFACTS_SearchData")]
        //public void UFACTS_Search(Dictionary<string, string> inputDataCollection)
        //{
        //    if (includeTestCase)
        //    {
        //        ignoredScriptsFlag = false;
        //        string testCasePath = string.Empty;
        //        Helper.CreateFolder(screenShotPath, "uFACTS_Search/" + inputDriverName + "_DataSet_" + countDataSet, out testCasePath);
        //        SeleniumDriverObject.currentTestCasePath = testCasePath;
        //        UFACTS_Search ObjuFACTS_Search = new UFACTS_Search();

        //        ObjuFACTS_Search.UFACTS_SearchTest(driver, inputDataCollection, testCasePath, inputDriverName);
        //    }
        //}

        #endregion Test methods
    }
}