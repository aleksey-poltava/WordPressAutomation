using OpenQA.Selenium;
using System;
using WordPressAutomation.Helpers;

namespace WordPressAutomation
{
    public static class LoginPage
    {
        public static bool IsLogedIn
        {
            get
            {
                if (!HtmlElements.IsElementExists(Driver.Instance, By.LinkText("Howdy, alex"), TimeSpan.FromSeconds(10), 500))
                    return false;
                return true;
            }
        }

        public static void GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress + "wp-admin");
        }

        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }
    }

    public class LoginCommand
    {
        private static string loginId = "user_login";
        private static string passId = "user_pass";

        private readonly string userName;

        private string password;

        public LoginCommand(string userName)
        {
            this.userName = userName;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;

            return this;
        }

        public void Login()
        {
            if (!IsOn())
                return;

            var loginInput = Driver.Instance.FindElement(By.Id("user_login"));
            loginInput.SendKeys(userName);

            var passwordInput = Driver.Instance.FindElement(By.Id("user_pass"));
            passwordInput.SendKeys(password);

            var loginButton = Driver.Instance.FindElement(By.Id("wp-submit"));
            loginButton.Click();
        }

        private bool IsOn()
        {
            if (HtmlElements.IsElementExists(Driver.Instance,
                By.Id(loginId),
                TimeSpan.FromSeconds(10),
                500) &&
                (HtmlElements.IsElementExists(Driver.Instance,
                By.Id(passId),
                TimeSpan.FromSeconds(10),
                500)))
                return true;
            return false;
        }
    }
}
