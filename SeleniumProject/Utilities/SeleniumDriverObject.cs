using System;
using System.Configuration;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;


namespace SeleniumProject
{
    public static class SeleniumDriverObject
	{
		private static IWebDriver driver;
		private static string[] browserNames = new string[] { "internet explorer", "firefox", "chrome", "safari" };
		private static bool deviceFlag = false;
		private static string screenShotFlag = ConfigurationManager.AppSettings["SaveScreenShot"];
		public static string currentTestCasePath = string.Empty;
		private static string sessionID = string.Empty;
        private static string folderPath = Directory.GetCurrentDirectory();

        #region Local Browser Initialization

        /// <summary>
		/// Initializes Internet Explorer settings which are executed in Web browser
		/// </summary>
		/// <returns></returns>
        public static IWebDriver GetInternetExplorerDriver()
        {
            try
            {
                var options = new InternetExplorerOptions();
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                options.EnableNativeEvents = true;
                options.IgnoreZoomLevel = true;
                driver = new InternetExplorerDriver(folderPath, options);
                driver = Helper.RegisterEvents(driver);
                deviceFlag = false;
                Helper.driver = driver;
                return driver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
		/// Initializes Chrome settings which are executed in Web browser
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetChromeDriver()
        {
            try
            {
                driver = new ChromeDriver(folderPath);
                driver = Helper.RegisterEvents(driver);
                deviceFlag = false;
                Helper.driver = driver;
                return driver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initializes FireFox settings which are executed in Web browser
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetFireFoxDriver()
        {
            try
            {
                driver = new FirefoxDriver(folderPath);
                driver = Helper.RegisterEvents(driver);
                deviceFlag = false;
                Helper.driver = driver;
                return driver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initializes Safari settings which are executed in Web browser
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetSafariDriver()
        {
            try
            {
                driver = new SafariDriver();
                driver = Helper.RegisterEvents(driver);
                deviceFlag = false;
                Helper.driver = driver;
                return driver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Local Browser Initialization

        #region Browserstack Browser Initialization

        /// <summary>
        /// Initializes Andriod settings which are executed in browser stack
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetSamsungGalaxyTab4BrowserStackDriver(string testCaseName)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);
				capability.SetCapability("browserName", "android");
				capability.SetCapability("platform", "Android");
				capability.SetCapability("realMobile", "true");
				capability.SetCapability("device", "Samsung Galaxy Tab 4");
				capability.SetCapability("browserstack.debug", "True");
				capability.SetCapability("browserstack.video", "True");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        	

		/// <summary>
		/// Initialises IOS settings for Iphone 6S which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetIphone6SBrowserStackDriver(string testCaseName, String name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browserName", "iPhone");
				capability.SetCapability("platform", "Mac");

				//Mobile Capability
				capability.SetCapability("device", "Iphone 6S");
				capability.SetCapability("os", "ios");
				if (name.Contains("Landscape") == true)

					capability.SetCapability("deviceOrientation", "Landscape");

				//Test Configuration Capability
				capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");
				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises IOS settings for Iphone 6 Plus which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetIphone6PlusBrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browserName", "iPhone");
				capability.SetCapability("platform", "Mac");
                
				//Mobile Capability
				capability.SetCapability("device", "Iphone 6S Plus");
				capability.SetCapability("os", "ios");

                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "Landscape");
                }
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises IOS settings for Ipad Mini which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetIpadMini4BrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browserName", "iPhone");
				capability.SetCapability("platform", "Mac");

				//Mobile Capability
				capability.SetCapability("device", "iPad Mini 4");
				capability.SetCapability("os", "ios");
                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "Landscape");
                }
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises IOS settings for Ipad Air which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetIpadAirBrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browserName", "iPhone");
				capability.SetCapability("platform", "Mac");

				//Mobile Capability
				capability.SetCapability("device", "iPad Air");
				capability.SetCapability("os", "ios");
                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "Landscape");
                }
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises Chromeon BrowserStack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver Get_Browserstack_Chrome(string testCaseName, String Devicename)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browser", "Chrome");
				capability.SetCapability("browser_version", "59.0");
				capability.SetCapability("os", "Windows");
				capability.SetCapability("os_version", "10");
				capability.SetCapability("resolution", "1920x1200");

				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");
				capability.SetCapability("resolution", "1920x1200");


				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);

				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises Firefox on BrowserStack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver Get_Browserstack_Firefox(string testCaseName, String deviceName)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browser", "Firefox");
				capability.SetCapability("browser_version", "54.0");
				capability.SetCapability("os", "Windows");
				capability.SetCapability("os_version", "8");
				capability.SetCapability("resolution", "1920x1200");

				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");
				capability.SetCapability("resolution", "1920x1200");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises Safari on BrowserStack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver Get_Browserstack_Safari(string testCaseName, String deviceName)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browser", "Safari");
				capability.SetCapability("browser_version", "10.0");
				capability.SetCapability("os", "OS X");
				capability.SetCapability("os_version", "Sierra");
				capability.SetCapability("resolution", "1920x1080");
				capability.SetCapability("browserstack.autoWait", "0");
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initialises IIE 11 Browserstack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver Get_Browserstack_IE11(string testCaseName, String deviceName)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("browser", "IE");
				capability.SetCapability("browser_version", "11.0");
				capability.SetCapability("os", "Windows");
				capability.SetCapability("os_version", "10");
				capability.SetCapability("resolution", "1920x1200");

				//Test Configuration Capability

                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "3.4.0");
				capability.SetCapability("resolution", "1920x1200");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


        /// <summary>
        /// Initializes Andriod settings for HTC One M8 which are executed in browser stack
        /// </summary>
        /// 
        /// <returns></returns>
        public static IWebDriver GetHTCOneM8BrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["BrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["BrowserStackKey"]);

				//Selenium Capability
               capability.SetCapability("platform", "Android");

				//BrowserStack Capability
               capability.SetCapability("browser", "Chrome");
               capability.SetCapability("browser_version", "59.0");

				//Mobile Capability
				capability.SetCapability("device", "HTC One M8");
				capability.SetCapability("realMobile", "false");
				capability.SetCapability("os", "android");
                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "landscape");
                }
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "2.45.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);                
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initializes Andriod settings for Samsung Galaxy S7 which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetSamsungGalaxyS7BrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["RealDeviceBrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["RealDeviceBrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("platform", "Android");

				//BrowserStack Capability
                capability.SetCapability("browser", "Chrome");
                capability.SetCapability("browser_version", "59.0");

				//Mobile Capability
				capability.SetCapability("device", "Samsung Galaxy S7");
				capability.SetCapability("realMobile", "true");
				capability.SetCapability("os", "android");

                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "landscape");
                }
				capability.SetCapability("browserstack.console", "disable");
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "2.45.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		
		/// <summary>
		/// Initializes Andriod settings for Samsung Galaxy Note 4 which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetSamsungGalaxyNote4BrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["RealDeviceBrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["RealDeviceBrowserStackKey"]);
                capability.SetCapability("browser", "Chrome");
                capability.SetCapability("browser_version", "59.0");
				capability.SetCapability("platform", "Android");
				capability.SetCapability("realMobile", "true");
				capability.SetCapability("device", "Samsung Galaxy Note 4");
				if (name.Contains("Landscape") == true)
					capability.SetCapability("deviceOrientation", "landscape");
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        /// <summary>
        /// Initializes Andriod settings for Google Nexus 9 which are executed in browser stack
        /// </summary>
        /// <returns></returns>
        public static IWebDriver GetGoogleNexus9BrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["RealDeviceBrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["RealDeviceBrowserStackKey"]);
                capability.SetCapability("browser", "Chrome");
                capability.SetCapability("browser_version", "59.0");
				capability.SetCapability("platform", "Android");
				capability.SetCapability("realMobile", "true");
				capability.SetCapability("device", "Google Nexus 9");
                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "landscape");
                }
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Initializes Andriod settings for Google Pixel which are executed in browser stack
		/// </summary>
		/// <returns></returns>
		public static IWebDriver GetGooglePixelBrowserStackDriver(string testCaseName, string name)
		{
			try
			{
				DesiredCapabilities capability = new DesiredCapabilities();

				capability.SetCapability("browserstack.user", ConfigurationManager.AppSettings["RealDeviceBrowserStackUser"]);
				capability.SetCapability("browserstack.key", ConfigurationManager.AppSettings["RealDeviceBrowserStackKey"]);

				//Selenium Capability
				capability.SetCapability("platform", "Android");

				//BrowserStack Capability
                capability.SetCapability("browser", "Chrome");
                capability.SetCapability("browser_version", "59.0");

				//Mobile Capability
				capability.SetCapability("device", "Google Pixel");
				capability.SetCapability("realMobile", "true");
				capability.SetCapability("os", "android");
                if (name.Contains("Landscape") == true)
                {
                    capability.SetCapability("deviceOrientation", "landscape");
                }
				//Test Configuration Capability
                capability.SetCapability("browserstack.debug", "false");
                capability.SetCapability("browserstack.video", "false");
				capability.SetCapability("acceptSslCerts", "true");
				capability.SetCapability("browserConnectionEnabled", "true");
				capability.SetCapability("browserstack.local", "true");
				capability.SetCapability("browserstack.selenium_version", "2.45.0");

				driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), capability);
				sessionID = ((RemoteWebDriver)driver).SessionId.ToString();
				driver = Helper.RegisterEvents(driver);
				deviceFlag = true;
				return driver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        #endregion Browserstack Browser Initialization
   

    }
}