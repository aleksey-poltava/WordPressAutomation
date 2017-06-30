using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation.Pages
{
    public class PostsPage
    {
        public static string Title
        {
            get
            {
                if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("entry-title"), TimeSpan.FromSeconds(10), 500))
                    return string.Empty;
                var title = Driver.Instance.FindElement(By.ClassName("entry-title"));
                if (title != null)
                    return title.Text;
                return string.Empty;
            }
        }

        public static bool IsOnPage
        {
            get
            {
                if (!HtmlElements.IsOnPage(Driver.Instance, Driver.BaseAddress + "wp-admin/edit.php",
                    TimeSpan.FromSeconds(5), 500))
                    return false;
                return true;

            }
        }

        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress + "wp-admin/edit.php");
        }
    }
}
