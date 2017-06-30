using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation
{
    public static class NewPostPage
    {
        public static CreatePostCommand CreatePost(string postTitle)
        {
            return new CreatePostCommand(postTitle);
        }

        public static void GoToNewPost()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.Id("message"), TimeSpan.FromSeconds(10), 500))
                return;
            var message = Driver.Instance.FindElement(By.Id("message"));
            var newPostLink = message.FindElements(By.TagName("a"))[0];
            newPostLink.Click();
        }

        public static bool IsInEditMode()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.ClassName("wp-heading-inline"), TimeSpan.FromSeconds(10), 500))
                return false;
            var element = Driver.Instance.FindElement(By.ClassName("wp-heading-inline"));
            return element.Text == "Edit Page";
        }

        public static string Title
        {
            get
            {
                if (!HtmlElements.IsElementExists(Driver.Instance, By.Id("title"), TimeSpan.FromSeconds(10), 500))
                    return string.Empty;
                var title = Driver.Instance.FindElement(By.Id("title"));
                if (title != null)
                    return title.GetAttribute("value");
                return string.Empty;
            }
        }

        public static void NavigateBack()
        {
            Driver.Instance.Navigate().Back();
        }
    }

    public class CreatePostCommand
    {
        private readonly string _postTitle;
        private string body;

        public CreatePostCommand(string postTitle)
        {
            _postTitle = postTitle;
        }

        public CreatePostCommand WithBody(string body)
        {
            this.body = body;
            return this;
        }

        public void Publish()
        {
            //if (!PostPage.IsOnPage)
            //    return;

            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.FindElement(By.Id("title")).SendKeys(_postTitle);

            Driver.Wait(TimeSpan.FromSeconds(4));
            Driver.Instance.SwitchTo().Frame("content_ifr");
            Driver.Instance.SwitchTo().ActiveElement().SendKeys(body);
            Driver.Instance.SwitchTo().DefaultContent();

            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.FindElement(By.Id("publish")).Click();
        }
    }
}