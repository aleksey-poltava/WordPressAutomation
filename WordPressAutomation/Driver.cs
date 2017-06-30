using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace WordPressAutomation
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static bool IsStepFailed;

        public static string BaseAddress
        {
            get { return "http://192.168.99.1:8181/wordpress/"; }
        }

        public static void Initialize()
        {
            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability(CapabilityType.BrowserName, "firefox");
            capability.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Linux));
            var commandTimeout = TimeSpan.FromSeconds(5);
            var uri = "http://192.168.99.100:4444/wd/hub/";

            try
            {
                Instance = new RemoteWebDriver(new Uri(uri), DesiredCapabilities.Firefox(), commandTimeout);
                //Instance = new FirefoxDriver();
                //Instance = new ChromeDriver();

                Instance.Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static void Close()
        {
            Instance.Quit();
            Instance = null;
        }

        public static void Wait(TimeSpan seconds)
        {
            Thread.Sleep(seconds);
        }
    }
}