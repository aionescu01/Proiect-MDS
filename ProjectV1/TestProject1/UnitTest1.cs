using Microsoft.EntityFrameworkCore;
using ProjectV1.Controllers;
using ProjectV1.DAL;
using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Linq;
using ProjectV1.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProjectV1.DAL.Entities;
using System.Collections.Generic;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Globalization;

namespace TestProject1
{
    public class UnitTest1
    {

        public AppDbContext _context { get; }



        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\ProjectsV13;Initial Catalog = ProjectDatabase; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static UnitTest1()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        
        public UnitTest1()
        {
            _context = new AppDbContext(dbContextOptions);
        }



        [Fact]
        public async void TestGetAll()
        {
            var players = await _context.Players.Select(PlayerGetModel.Projection).ToListAsync();
            Assert.NotNull(players);
        }

        [Fact]
        public async void TestGetOne()
        {
            var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.Id == 1228);
            Assert.NotNull(player);
        }

        [Fact]
        public async void TestDelete()
        {
            var player = new Player()
            {
                Name = "test",
                Nationality = "aa",
                Foot = "aa",
                Position = "aa"
            };
            await _context.Players.AddRangeAsync(player);
            await _context.SaveChangesAsync();
            player = await _context.Players.FirstOrDefaultAsync(player => player.Name == "test");
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            player = await _context.Players.FirstOrDefaultAsync(player => player.Name == "test");
            if (player == null)
                Assert.True(true);
            else Assert.True(false);

        }

        [Fact]
        public async void TestEdit()
        {
            var player = new Player()
            {
                Name = "test",
                Nationality = "aa",
                Foot = "aa",
                Position = "aa"
            };
            await _context.Players.AddRangeAsync(player);
            await _context.SaveChangesAsync();
            player = await _context.Players.FirstOrDefaultAsync(player => player.Name == "test");
            player.Nationality = "bb";
            
            //player = await _context.Players.FirstOrDefaultAsync(player => player.Name == "test");
            if (player.Nationality == "bb")
            { 
                
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                Assert.True(true);
            }

            else {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                Assert.True(false); 
            }

        }

        [Fact]
        public async void TestPost()
        {
            var player = new Player()
            {
                Name = "test",
                Nationality = "aa",
                Foot = "aa",
                Position = "aa"
            };
            await _context.Players.AddRangeAsync(player);
            await _context.SaveChangesAsync();
            player = await _context.Players.FirstOrDefaultAsync(player => player.Name == "test");
            if(player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                Assert.True(true);
            }
            else
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                Assert.True(false);
            }
        }

        [Fact]
        public async void TestPostLink()
        {
            string link = "https://www.transfermarkt.com/fc-barcelona/kader/verein/131/saison_id/2021/plus/1";
            var html = GetHtml(link);
            var data = ParseHtmlUsingHtmlAgilityPack(html);
            List<Player> players = new List<Player>();
            foreach (var i in data)
            {

                //writetext.WriteLine(i.name + " " + i.value + " " + i.position + " age " + i.age + " " + i.nationality);
                //float val;
                //System.Console.WriteLine(i);

                Single.TryParse(i.value, out float val);
                float hei = float.Parse(i.height, CultureInfo.InvariantCulture.NumberFormat);
                Player player;
                if (i.date_of_birth == "unknown")
                {
                    player = new Player()
                    {

                        Name = i.name,
                        Nationality = i.nationality,
                        Height = hei,
                        Foot = i.foot,
                        Position = i.position,
                        Value = (decimal)val
                    };
                }
                else
                {
                    DateTime oDate = DateTime.Parse(i.date_of_birth);
                    player = new Player()
                    {

                        Name = i.name,
                        Nationality = i.nationality,
                        Birth_Date = oDate,
                        Height = hei,
                        Foot = i.foot,
                        Position = i.position,
                        Value = (decimal)val
                    };
                }
                players.Add(player);
            }
                if (players != null)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
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

                if (foot == "&nbsp;" || foot == "-")
                    foot = "unknown";
                if (height == "&nbsp;" || height == "")
                    height = "0";
                if (position == "&nbsp;" || position == "-")
                    position = "unknown";
                if (nationality == "&nbsp;" || nationality == "-")
                    nationality = "unknown";
                if (date_of_birth == "&nbsp;" || date_of_birth == "-")
                    date_of_birth = "unknown";
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
