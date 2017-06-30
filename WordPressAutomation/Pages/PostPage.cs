using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation
{
    public class PostPage
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
                if (!HtmlElements.IsOnPage(Driver.Instance, Driver.BaseAddress + "wp-admin/post-new.php",
                    TimeSpan.FromSeconds(5), 500))
                    return false;

                if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath("//h1[text() = 'Add New Post']"),
                    TimeSpan.FromSeconds(5), 500))
                    return false;

                return true;

            }
        }

        public static void GoTo()
        {
            Driver.Instance.Url = Driver.BaseAddress + "wp-admin/edit.php";
        }
    }
}
