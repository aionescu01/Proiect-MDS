using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;


namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = GetHtml("https://www.transfermarkt.com/fc-barcelona/mitarbeiter/verein/131");
            var data = ParseHtmlUsingHtmlAgilityPack(html);
            var data2 = ParseHtmlUsingHtmlAgilityPack2(html);
            var x = 1;

            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                foreach (var i in data)
                {
                    foreach (var j in data2)
                    {
                        if (j.name == i.name)
                        {
                            System.Console.WriteLine(DateTime.Parse(j.end));
                            var xaaa=j.end.Replace(".", "/");
                            System.Console.WriteLine(DateTime.Parse(xaaa));
                            //System.Console.WriteLine(i.name + " " + i.role + " " + j.start + " " + j.end);
                            //writetext.WriteLine(i.name + " " + i.role + " " + j.start + " " + j.end);
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

        private static List<(string name, string role)> ParseHtmlUsingHtmlAgilityPack(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//div[@class='box']/table[1]");
            //.SelectNodes("div[@class='row']/div/div");
            //.SelectNodes("div[@class='stickyContent stickySubnavigation']");


            List<(string name, string role)> data = new();

            foreach (var repo in repositories)
            {
                var emp = repo.SelectSingleNode(".//tbody");
                var y = emp.Elements("tr");
                foreach (var x in y)
                {
                    var name = x.SelectSingleNode(".//td[@class='hauptlink']/a").InnerText;
                    name = name.Replace("\r\n", "");
                    name = name.Replace("&nbsp;", "");
                    name = name.TrimStart(' ');
                    name = name.TrimEnd(' ');
                    var role = x.SelectSingleNode(".//tr[2]/td").InnerText;
                    data.Add((name, role));
                }
            }

            return data;
        }

        private static List<(string name, string start, string end)> ParseHtmlUsingHtmlAgilityPack2(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//div[@class='box']/table[1]");


            List<(string name, string start, string end)> data = new();

            foreach (var repo in repositories)
            {
                var emp = repo.SelectSingleNode(".//tbody");
                var y = emp.Elements("tr");
                foreach (var x in y)
                {
                    var name = x.SelectSingleNode(".//td[@class='hauptlink']/a").InnerText;
                    name = name.Replace("\r\n", "");
                    name = name.Replace("&nbsp;", "");
                    name = name.TrimStart(' ');
                    name = name.TrimEnd(' ');
                    var start = x.SelectSingleNode(".//td[4]").InnerText;
                    var end = x.SelectSingleNode(".//td[5]").InnerText;
                    end = end.Replace("\r\n", "");
                    end = end.Replace("\t", "");
                    end = end.TrimStart(' ');
                    end = end.TrimEnd(' ');
                    data.Add((name, start, end));
                }
            }

            return data;
        }
    }
}

