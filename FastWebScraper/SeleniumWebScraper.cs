using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class SeleniumWebScraper
    {
        private IWebDriver driver;
        public SeleniumWebScraper()
        {
            driver = new ChromeDriver();
        }

        public List<User> GetUsers(string url)
        {
            List<User> users = new List<User>();

            driver.Navigate().GoToUrl(url);

            IWebElement nextButton = null;
            do
            {
                if (nextButton != null)
                    nextButton.Click();

                var rows = driver.FindElements(By.XPath("/html/body/div/main/div/table/tbody/tr[position()>1]"));
                foreach (var row in rows)
                {
                    var user = new User
                    {
                        Firstname = row.FindElement(By.XPath("td[1]")).Text,
                        Lastname = row.FindElement(By.XPath("td[2]")).Text,
                        Age = int.Parse(row.FindElement(By.XPath("td[3]")).Text)
                    };

                    users.Add(user);
                }

                nextButton = driver.FindElement(By.XPath("/html/body/div/main/div/a[2]"));
            } while (!nextButton.GetAttribute("class").Contains("disabled"));

            return users;
        }
    }
}