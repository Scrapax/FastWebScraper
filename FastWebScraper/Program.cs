using ConsoleTables;
using System;
using System.Diagnostics;
using System.Threading;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var staticUrl = "https://localhost:44392";
            var dynamicUrl = "https://localhost:44392/dynamic";

            Thread.Sleep(5000);
            var httpScraper = new DynamicWebScraper();
            var seleniumScraper = new SeleniumWebScraper();
            var htmlScraper = new FastWebScraper();
            Thread.Sleep(5000);

            var staticHttp = "--";
            var staticHtml = TimeSpentMilliseconds(() => htmlScraper.GetUsers(staticUrl));
            var staticSelenium = TimeSpentMilliseconds(() => seleniumScraper.GetUsers(staticUrl));

            var dynamicHttp = TimeSpentMilliseconds(() => httpScraper.GetUsers(dynamicUrl));
            var dynamicHtml = "--";
            var dynamicSelenium = TimeSpentMilliseconds(() => seleniumScraper.GetUsers(dynamicUrl));

            var table = new ConsoleTable("Website", "HttpClient", "Html Agility Pack", "Selenium");
            table.AddRow("Static", staticHttp, staticHtml, staticSelenium)
                 .AddRow("Dynamic", dynamicHttp, dynamicHtml, dynamicSelenium);

            Console.WriteLine(table.ToString());
        }


        private static long TimeSpentMilliseconds(Action a)
        {
            Stopwatch w = Stopwatch.StartNew();
            a.Invoke();
            return w.ElapsedMilliseconds;
        }
    }
}
