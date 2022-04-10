/*
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
            var html = GetHtml("https://www.transfermarkt.com/bayern-munich/kader/verein/27/saison_id/2021/plus/1");
            var data = ParseHtmlUsingHtmlAgilityPack(html);
            decimal aa;
            using (StreamWriter writetext = new StreamWriter("write.txt"))
            {
                foreach (var i in data)
                {
                    DateTime oDate = DateTime.Parse(i.date_of_birth);

                    Single.TryParse(i.value, out float val);

                    aa = (decimal)val;
                    System.Console.WriteLine(aa);
                    //oDate.ToShortDateString(); date without hour
                    writetext.WriteLine(i.name + " " + val + " " + i.position + " born " + oDate + " " + i.nationality + " " + i.height + "m " + i.foot);
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
            //chrome.Navigate().GoToUrl("https://www.transfermarkt.com/fc-barcelona/kader/verein/131/saison_id/2021/plus/1");
            //chrome.Navigate().GoToUrl("https://www.transfermarkt.com/manchester-city/kader/verein/281/saison_id/2021/plus/1");
            chrome.Navigate().GoToUrl(link);

            return chrome.PageSource;
        }

        private static List<(string name, string value, string position, string date_of_birth, string nationality, string height, string foot)> ParseHtmlUsingHtmlAgilityPack(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var repositories =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//div[@class='responsive-table']/div/table/tbody/tr");



            List<(string name, string value, string position, string date_of_birth, string nationality, string height, string foot)> data = new();

            foreach (var repo in repositories)
            {
                var name = repo.SelectSingleNode(".//td[@class='hauptlink']/a").InnerText;
                var value = repo.SelectSingleNode(".//td[@class='rechts hauptlink']").InnerText;
                var position = repo.SelectSingleNode(".//table[@class='inline-table']/tbody/tr[2]").InnerText;
                var date_of_birth = repo.SelectSingleNode(".//td[@class='zentriert']").InnerText;
                var node = repo.SelectNodes(".//td[@class='zentriert']");
                var country = node[1].InnerHtml;
                int first = country.IndexOf("title=") + "title=".Length + 1;
                int end = country.Substring(first).IndexOf('"');
                var nationality = country.Substring(first, end);
                first = date_of_birth.IndexOf("(");
                //end = date_of_birth.Substring(first).IndexOf(')');
                date_of_birth = date_of_birth.Substring(0, first);
                //System.Console.WriteLine(age.Substring(first, end));
                name = name.Replace("\r\n", "");
                name = name.Replace("&nbsp;", "");
                name = name.TrimStart(' ');
                name = name.TrimEnd(' ');
                position = position.Replace("\r\n", "");
                position = position.Replace("&nbsp;", "");
                position = position.TrimStart(' ');
                position = position.TrimEnd(' ');
                var height = node[2].InnerHtml;
                height = height.Replace(" m", "");
                height = height.Replace(",", ".");
                var foot = node[3].InnerHtml;

                if (value.IndexOf('T') == -1)
                {
                    value = value.Replace("€", "");
                    value = value.Replace("m", "");
                }
                else
                {
                    value = value.Replace("€", "0.");
                    value = value.Replace("Th.", "");
                }

                data.Add((name, value, position, date_of_birth, nationality, height, foot));

            }

            return data;
        }
    }
}
*/