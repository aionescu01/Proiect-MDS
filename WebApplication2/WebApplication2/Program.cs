using System.Collections.Generic;
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
            var html = GetHtml();
            var data = ParseHtmlUsingHtmlAgilityPack(html);

            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                foreach (var i in data) 
                { 
                    writetext.WriteLine(i.name+" "+i.value+" "+ i.position);
                    //System.Console.WriteLine(i);
                }
            }
        }

        private static string GetHtml()
        {
            var options = new ChromeOptions
            {
                BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            };

            options.AddArguments("headless");

            var chrome = new ChromeDriver(options);
            chrome.Navigate().GoToUrl("https://www.transfermarkt.com/fc-barcelona/kader/verein/131/saison_id/2021/plus/1");

            return chrome.PageSource;
        }

        private static List<(string name, string value, string position)> ParseHtmlUsingHtmlAgilityPack(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//div[@class='responsive-table']/div/table/tbody/tr");



            List<(string name, string value, string position)> data = new();

            foreach (var repo in repositories)
            {
                var name = repo.SelectSingleNode(".//td[@class='hauptlink']/a").InnerText;
                var value = repo.SelectSingleNode(".//td[@class='rechts hauptlink']").InnerText;
                var position = repo.SelectSingleNode(".//table[@class='inline-table']/tbody/tr[2]").InnerText;
                //System.Console.WriteLine(position);
                name = name.Replace("\r\n", "");
                name = name.Replace("&nbsp;", "");
                name = name.TrimStart(' ');
                name = name.TrimEnd(' ');
                position = position.Replace("\r\n", "");
                position = position.Replace("&nbsp;", "");
                position = position.TrimStart(' ');
                position = position.TrimEnd(' ');
                data.Add((name, value, position));

            }

            return data;
        }
    }
}
