using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation.Pages
{
    public class PagesPage
    {
        private static string PagesH1Code = "//h1[text() = 'Pages']";
        private static string PagesSearchPagesCode = "//*[@id='search-submit']";
        private static string PagesPageLink = Driver.BaseAddress + "wp-admin/edit.php?post_type=page";

        public static bool IsOnPage
        {
            get
            {
                if (!HtmlElements.IsOnPage(Driver.Instance, PagesPageLink,
                    TimeSpan.FromSeconds(5), 500))
                {
                    Driver.IsStepFailed = true;
                    return false;
                }

                if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath(PagesH1Code),
                    TimeSpan.FromSeconds(5), 500))
                {
                    Driver.IsStepFailed = true;
                    return false;
                }

                if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath(PagesSearchPagesCode),
                    TimeSpan.FromSeconds(5), 500))
                {
                    Driver.IsStepFailed = true;
                    return false;
                }

                return true;

            }
        }

        public static void GoTo()
        {
            Driver.Wait(TimeSpan.FromSeconds(4));
            Driver.Instance.Navigate().GoToUrl(PagesPageLink);

            if (!IsOnPage)
                Driver.IsStepFailed = true;
        }
    }
}
