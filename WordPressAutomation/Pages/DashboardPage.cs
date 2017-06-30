using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation
{
    public class DashboardPage
    {
        private static string DashboardCode = "//h1[text() = 'Dashboard']";
        private static string DashboardH2Text = "//div/h2[contains(text(), 'Welcome to WordPress!')]";
        private static string DashboardPageLink = Driver.BaseAddress + "wp-admin/";

        public static bool IsOnPage
        {
            get
            {
                if (!HtmlElements.IsOnPage(Driver.Instance, DashboardPageLink,
                    TimeSpan.FromSeconds(5), 500))
                    return false;

                if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath(DashboardCode),
                    TimeSpan.FromSeconds(5), 500))
                    return false;

                if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath(DashboardH2Text),
                    TimeSpan.FromSeconds(5), 500))
                    return false;


                return true;

            }
        }

        public static void GoTo()
        {
            Driver.Wait(TimeSpan.FromSeconds(4));
            Driver.Instance.Navigate().GoToUrl(DashboardPageLink);

            if (!IsOnPage)
                Driver.IsStepFailed = true;
        }

        //public static bool IsAt
        //{
        //    get
        //    {
        //        //Thread.Sleep(2000);
        //        //if (!HtmlElements.IsElementExists(Driver.Instance, new List<By>() { By.CssSelector(".wrap>h1") }, TimeSpan.FromSeconds(5), 500))
        //        //    return false;
        //        if (!HtmlElements.IsOnPage(Driver.Instance, Driver.BaseAddress + "wp-admin/",
        //            TimeSpan.FromSeconds(5), 500))
        //            return false;

        //        try
        //        {
        //            IWebElement element = HtmlElements.FindElementByMultipleCriteria(Driver.Instance, new List<By>() { By.CssSelector(".wrap>h1"), By.XPath("//h1[. = 'Dashboard']") });
        //            return element.Text == "Dashboard";
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //            return false;
        //        }
        //    }
        //}
    }
}
