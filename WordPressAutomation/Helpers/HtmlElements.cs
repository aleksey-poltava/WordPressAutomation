using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WordPressAutomation.Helpers
{
    public static class HtmlElements
    {
        public static bool IsElementExists(IWebDriver driver, By by, TimeSpan timeSpan, int iterationSleepTime)
        {
            bool elementFounded = false;
            int elapsed = 0;

            while ((!elementFounded) && (elapsed < timeSpan.TotalMilliseconds))
            {
                Thread.Sleep(iterationSleepTime);
                elapsed += iterationSleepTime;

                var elements = driver.FindElements(by);
                if (elements.Count > 0)
                    elementFounded = true;

            }
            Console.WriteLine("Element founded?: " + elementFounded);
            return elementFounded;
        }

        public static bool IsOnPage(IWebDriver driver, string pageUrl, TimeSpan timeSpan, int iterationSleepTime)
        {
            bool isPageReached = false;
            int elapsed = 0;

            while ((!isPageReached) && (elapsed < timeSpan.TotalMilliseconds))
            {
                Thread.Sleep(iterationSleepTime);
                elapsed += iterationSleepTime;

                if (driver.Url == pageUrl)
                    isPageReached = true;
            }
            Console.WriteLine("Page reached?: " + isPageReached);
            return isPageReached;
        }

        public static IWebElement FindElementByMultipleCriteria(IWebDriver driver, List<By> criteria, IReadOnlyCollection<IWebElement> toFilter = null)
        {
            // If we've reached the end of the criteria list, return the first element:
            if (criteria.Count == 0 && toFilter != null) return toFilter.ElementAt(0);

            // Take the head of the criteria list
            By currentCriteria = criteria[0];
            criteria.RemoveAt(0);

            // If no list of elements found exists, we get all elements from the current criteria:
            if (toFilter == null)
            {
                toFilter = driver.FindElements(currentCriteria);
            }
            // If a list does exist, we must filter out the ones that aren't found by the current criteria:
            else
            {
                List<IWebElement> newFilter = new List<IWebElement>();
                foreach (IWebElement e in driver.FindElements(currentCriteria))
                {
                    if (toFilter.Contains(e)) newFilter.Add(e);
                }
                toFilter = newFilter.AsReadOnly();
            }

            // Pass in the refined criteria and list of elements found.
            return FindElementByMultipleCriteria(driver, criteria, toFilter);
        }


    }
}
