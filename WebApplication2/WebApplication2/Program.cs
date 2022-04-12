using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;

namespace WebScraper
{
    public class Class1
{
        static void Main(string[] args)
        {
            var html = GetHtml("https://salarysport.com/football/la-liga/barcelona");
            var data = ParseHtmlUsingHtmlAgilityPack(html);
            html = GetHtml("https://www.transfermarkt.com/fc-barcelona/berateruebersicht/verein/131/plus/1");
            var data2 = ParseHtmlUsingHtmlAgilityPack2(html);
            var x = 1;
            //data.Sort();

            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                foreach (var i in data)
                {

                    foreach (var j in data2)
                    {
                        if (j.name == i.name)
                        {
                            DateTime joindate = DateTime.Parse(j.joined);
                            DateTime enddate = DateTime.Parse(j.end_date);
                            System.Console.WriteLine(i.name + " " + i.salary + " " + joindate + " " + enddate + " agent: " + j.agent);
                            writetext.WriteLine(i.name + " " + i.salary + " " + joindate + " " + enddate + " agent: " + j.agent);
                        }
                    }

                }

            }
        }

        private static string GetHtml(string link)
        {
            var options = new ChromeOptions
            {
                BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            };

            options.AddArguments("headless");

            var chrome = new ChromeDriver(options);
            chrome.Navigate().GoToUrl(link);

            return chrome.PageSource;
        }

        private static List<(string name, string salary)> ParseHtmlUsingHtmlAgilityPack(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//table[@class='Table__TableStyle-sc-373fc0-0 koTFEC']/tbody/tr");



            List<(string name, string salary)> data = new();

            foreach (var repo in repositories)
            {
                if (repo.SelectSingleNode(".//td[1]").InnerText != "")
                {
                    var name = repo.SelectSingleNode(".//td[1]").InnerText;
                    var salary = repo.SelectSingleNode(".//td[3]").InnerText;
                    salary = salary.Replace("£", "");
                    salary = salary.Replace(",", "");
                    data.Add((name, salary));
                }

            }

            return data;
        }
        private static List<(string name, string joined, string end_date, string agent)> ParseHtmlUsingHtmlAgilityPack2(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//div[@class='responsive-table']/div/table/tbody/tr");



            List<(string name, string joined, string end_date, string agent)> data = new();

            foreach (var repo in repositories)
            {
                var name = repo.SelectSingleNode(".//td[@class='hauptlink']/a").InnerText;
                name = name.Replace("\r\n", "");
                name = name.Replace("&nbsp;", "");
                name = name.TrimStart(' ');
                name = name.TrimEnd(' ');

                //var joined = repo.SelectSingleNode(".//td[5]").InnerText;
                var joined = "-";
                var end_date = "-";
                //var end_date = repo.SelectSingleNode(".//td[6]").InnerText;
                if (joined == "-")
                    joined = "Jan 1, 1970";
                if (end_date == "-")
                    end_date = "Jan 1, 1970";
                var agent = repo.SelectSingleNode(".//td[@class='rechts hauptlink']").InnerText;
                data.Add((name, joined, end_date, agent));
            }

            return data;
        }
    }
}
