using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using WordPressAutomation.Helpers;
using WordPressAutomation.Pages;

namespace WordPressAutomation
{
    public class ListPostsPage
    {
        private static int postsCountBefore;
        private static int postsCountAfter;
        private static string postSearchBarCode = "post-search-input";
        private static string postSearchButtonCode = "search-submit";

        public static void GoTo(PostType postType)
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.Id("menu-pages"), TimeSpan.FromSeconds(10), 500))
                return;
            switch (postType)
            {
                case PostType.Page:
                    LeftNavigation.Pages.AllPages.Select();
                    break;
            }
        }

        public static void SelectPost(string title)
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.LinkText(title), TimeSpan.FromSeconds(10), 500))
                return;
            var postLink = Driver.Instance.FindElement(By.LinkText(title));
            postLink.Click();
        }

        public static void SavePostsCountBefore()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("displaying-num"), TimeSpan.FromSeconds(10), 500))
                return;
            var postsCountText = Driver.Instance.FindElement(By.ClassName("displaying-num"));
            if (postsCountText == null)
            {
                postsCountBefore = -1;
                return;
            }

            if (!int.TryParse(postsCountText.Text.Split(' ')[0], out postsCountBefore))
            {
                postsCountBefore = -1;
            }

        }

        public static void SavePostsCountAfter()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("displaying-num"), TimeSpan.FromSeconds(10), 500))
                return;
            var postsCountText = Driver.Instance.FindElement(By.ClassName("displaying-num"));
            if (postsCountText == null)
            {
                postsCountAfter = -1;
                return;
            }

            if (!int.TryParse(postsCountText.Text.Split(' ')[0], out postsCountAfter))
            {
                postsCountAfter = -1;
            }
            Console.WriteLine($"postsCountBefore: {postsCountBefore}");
            Console.WriteLine($"postsCountText: {postsCountText.Text}");

        }

        public static bool CompareNumberOfPosts()
        {
            return postsCountBefore == postsCountAfter;
        }

        public static bool IsPostExistWithTitle(string postTitle)
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("wp-heading-inline"), TimeSpan.FromSeconds(10), 500))
                return false;
            return Driver.Instance.FindElement(By.LinkText(postTitle)).Text == postTitle;
        }

        public static void TrashPost(string postTitle)
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("wp-heading-inline"), TimeSpan.FromSeconds(10), 500))
                return;
            var postLink = Driver.Instance.FindElement(By.LinkText(postTitle));
            Actions builder = new Actions(Driver.Instance);
            Actions hoverClick = builder.MoveToElement(postLink);
            hoverClick.Build().Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
            hoverClick = builder.MoveToElement(Driver.Instance.FindElement(By.ClassName("submitdelete"))).Click();
            hoverClick.Build().Perform();
            Driver.Wait(TimeSpan.FromSeconds(5));
        }

        public static void Search(string postTitle)
        {
            if (!PostsPage.IsOnPage)
                PostsPage.GoTo();

            var postSearchBar = Driver.Instance.FindElement(By.Id(postSearchBarCode));
            postSearchBar.Click();
            postSearchBar.SendKeys(postTitle);

            var searchPostsButton = Driver.Instance.FindElement(By.Id(postSearchButtonCode));
            searchPostsButton.Click();
        }
    }
}

public enum PostType
{
    Page
}

