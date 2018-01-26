using System;
using NUnit.Framework;
using NUnit.Core;
using System.Configuration;
using SeleniumEvent;
namespace SeleniumProject
{
    [SetUpFixture]
    public class OnlineSetUpFixture
    {
        [SetUp]
        public void Init()
        {
            MainTest.fixtureStartTime = DateTime.Now.ToString();            
            string strDate = string.Empty;
            strDate = DateTime.Now.ToString("ddMMMyyyy hhmmss").ToUpper();
            Helper.CreateFolder(ConfigurationManager.AppSettings["ScreenShotPath"], strDate, out TestDataParameter.testDateTimePath);
            SeleniumEvent.EventDataParameter.screenShotParentPath = TestDataParameter.testDateTimePath;
        }
        [TearDown]
        public void End()
        {
            
            //System.Data.DataTable dtTestCaseResults = new System.Data.DataTable("TestResults");
            //EventDataParameter.ExportToExcel( MainTest.totalScriptsCount, MainTest.passedScriptsCount,
            //    MainTest.failedScriptsCount, MainTest.fixtureStartTime, MainTest.fixtureEndTime = DateTime.Now.ToString(), MainTest.ignoredScriptsCount, (MainTest.totalScriptsCount - MainTest.ignoredScriptsCount));

        }

    }

}

