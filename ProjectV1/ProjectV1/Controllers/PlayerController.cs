using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL;
using ProjectV1.DAL.Models;
using ProjectV1.DAL.Entities;
using System.IO;
using System.Web;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-one-player"), Authorize(Roles = "Owner, Manager")]
        public async Task<IActionResult> CreatePlayer(PlayerPostModel model)
        {
            var player = new Player()
            {
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                Name = model.Name,
                Nationality = model.Nationality,
                Birth_Date = model.Birth_Date,
                Height = model.Height,
                Foot = model.Foot,
                Position = model.Position,
                Value = model.Value
            };

            await _context.Players.AddRangeAsync(player);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> CreatePlayers(string Link)
        {

            var link = Link;
            var html = GetHtml(link);
            var data = ParseHtmlUsingHtmlAgilityPack(html);

            foreach (var i in data)
            {

                //writetext.WriteLine(i.name + " " + i.value + " " + i.position + " age " + i.age + " " + i.nationality);
                //float val;
                //System.Console.WriteLine(i);

                Single.TryParse(i.value,  out float val);
                float hei = float.Parse(i.height, CultureInfo.InvariantCulture.NumberFormat);
                Player player;
                if (i.date_of_birth== "unknown")
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
                
                /*
                //float val = float.Parse(i.value, CultureInfo.InvariantCulture.NumberFormat);
                System.Console.WriteLine(val);
                var player = new Player()
                {
                //FirstName = i.FirstName,
                //LastName = i.LastName,
                Name = i.name,
                Nationality = i.nationality,
                Birth_Date = oDate,
                Height = hei,
                Foot = i.foot,
                Position = i.position,
                Value = (decimal)val
                };
                */

                await _context.Players.AddRangeAsync(player);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players.Select(PlayerGetModel.Projection).ToListAsync();

            return Ok(players);
        }
        [HttpGet("get-by-name/{name}")]
        public async Task<IActionResult> GetDriverById(string name)
        {
            //System.Console.WriteLine()
            var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.Name == name);
            //var player = await _context.Players.FirstOrDefaultAsync(player => player.LastName == name);
            return Ok(player);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayers()
        {
            var players = await _context.Players.ToListAsync();
            foreach(var i in players)
            _context.Players.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(player => player.Id == id);

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok();
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
