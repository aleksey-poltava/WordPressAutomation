using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation.Pages
{
    public class PagePage
    {
        private static string contentId = "content";
        private static string pageHeaderId = "//header/h1";

        public static string GetTitle()
        {
            if (!IsOn())
                return string.Empty;
            var title = Driver.Instance.FindElement(By.XPath(pageHeaderId));
            if (title != null)
                return title.Text;
            return string.Empty;
        }

        public static bool IsOn()
        {
            if (HtmlElements.IsElementExists(Driver.Instance,
                By.XPath(pageHeaderId),
                TimeSpan.FromSeconds(10),
                500))
                return true;
            return false;
        }
    }
}
