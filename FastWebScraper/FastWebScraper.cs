using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class FastWebScraper
    {
        public List<User> GetUsers(string url)
        {
            var uri = new Uri(url);
            List<User> users = new List<User>();

            HtmlWeb web = new HtmlWeb();

            HtmlNode nextButton = null;
            do
            {
                var doc = web.Load(url);
                var nodes = doc.DocumentNode.SelectNodes("/html/body/div/main/div/table/tbody/tr[position()>1]");
                foreach (var node in nodes)
                {
                    var user = new User
                    {
                        Firstname = node.SelectSingleNode("td[1]").InnerText,
                        Lastname = node.SelectSingleNode("td[2]").InnerText,
                        Age = int.Parse(node.SelectSingleNode("td[3]").InnerText),
                    };

                    users.Add(user);
                }

                nextButton = doc.DocumentNode.SelectSingleNode("/html/body/div/main/div/a[2]");
                url = uri.Scheme + "://" + uri.Authority + nextButton.GetAttributeValue<string>("href", null);
            } while (!nextButton.GetAttributeValue<string>("class",null).Contains("disabled"));

            return users;
        }
    }
}