using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation.Pages
{
    public class NewPagePage
    {
        private static string goToNewPageLink = ".//*[@id='sample-permalink']/a";

        public static CreatePageCommand CreatePage(string pageTitle)
        {
            return new CreatePageCommand(pageTitle);
        }

        public static void GoToNewPage()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.Id("message"), TimeSpan.FromSeconds(10), 500))
                return;
            var message = Driver.Instance.FindElement(By.Id("message"));
            var newPageLink = message.FindElements(By.TagName("a"))[0];
            newPageLink.Click();
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

        public static void GoToNewPost()
        {
            if (!HtmlElements.IsElementExists(Driver.Instance, By.XPath(goToNewPageLink), TimeSpan.FromSeconds(10), 500))
                Driver.IsStepFailed = true;

            var goToPageLink = Driver.Instance.FindElement(By.XPath(goToNewPageLink));
            goToPageLink.Click();


        }
    }

    public class CreatePageCommand
    {
        private readonly string _pageTitle;
        private string body;

        public CreatePageCommand(string pageTitle)
        {
            _pageTitle = pageTitle;
        }

        public CreatePageCommand WithBody(string pageBody)
        {
            this.body = pageBody;
            return this;
        }

        public void Publish()
        {
            Driver.Wait(TimeSpan.FromSeconds(4));
            Driver.Instance.FindElement(By.Id("title")).SendKeys(_pageTitle);

            Driver.Wait(TimeSpan.FromSeconds(4));
            Driver.Instance.SwitchTo().Frame("content_ifr");
            Driver.Instance.SwitchTo().ActiveElement().SendKeys(body);
            Driver.Instance.SwitchTo().DefaultContent();

            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.FindElement(By.Id("publish")).Click();
        }
    }
}
