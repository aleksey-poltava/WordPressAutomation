using OpenQA.Selenium;
using WordPressAutomation.Pages;

namespace WordPressAutomation
{
    public class LeftNavigation
    {
        const string menuPostsLocator = "wp-menu-name";

        public class Posts
        {
            public static void Select()
            {
                DashboardPage.GoTo();

                if (!DashboardPage.IsOnPage)
                    return;

                var menuPosts = Driver.Instance.FindElements(By.ClassName(menuPostsLocator))[1];
                menuPosts.Click();
            }

            public class AddNew
            {
                public static void Select()
                {
                    if (!PostsPage.IsOnPage)
                        return;

                    var addPostLink = Driver.Instance.FindElement(By.LinkText("Add New"));
                    addPostLink.Click();
                }
            }
        }

        public class Pages
        {
            private static string PagesMenuCode = ".//*[@id='menu-pages']/a";

            public class AllPages
            {
                public static void Select()
                {
                    Driver.Instance.FindElement(By.XPath(PagesMenuCode)).Click();
                    if (!PagesPage.IsOnPage)
                        Driver.IsStepFailed = true;
                }
            }

            public class AddNew
            {
                public static void Select()
                {
                    if (!PagesPage.IsOnPage)
                        PagesPage.GoTo();

                    var addPostLink = Driver.Instance.FindElement(By.XPath(".//div/a[text() = 'Add New']"));
                    addPostLink?.Click();
                }
            }
        }
    }
}