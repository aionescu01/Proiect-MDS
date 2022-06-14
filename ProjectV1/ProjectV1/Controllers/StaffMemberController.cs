using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL;
using ProjectV1.DAL.Models;
using ProjectV1.DAL.Entities;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using Microsoft.AspNetCore.Authorization;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffMemberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-staff-by-link")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> CreateStaffMembers([FromBody] string Link)
        {

            var link = Link;
            var html = GetHtml(link);
            var data = ParseHtmlUsingHtmlAgilityPack(html);

            foreach (var i in data)
            {
                var staffmember = new StaffMember()
                {

                    Name = i.name,
                    Role = i.role

                };

                await _context.Staff.AddRangeAsync(staffmember);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }


        [HttpPost("add-staff-member")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> CreateStaffMember(StaffMemberPostModel model)
        {
            var staffmember = new StaffMember()
            {
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                Name = model.Name,
                Role = model.Role,
                Birth_Date = model.Birth_Date,
                Email = model.Email,
                Phone_Number = model.Phone_Number,
            };

            await _context.Staff.AddRangeAsync(staffmember);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffMembers()
        {
            var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).ToListAsync();

            return Ok(staffmember);
        }
        [HttpGet("get-by-name/{name}")]
        public async Task<IActionResult> GetStaffMemberById(string name)
        {
            var staffs = await _context.Staff.Select(StaffMemberGetModel.Projection).ToListAsync();
            List<StaffMemberGetModel> staff = new List<StaffMemberGetModel>();
            foreach (var j in staffs)
            {
                if (j.Name.ToLower().Contains(name.ToLower()))
                    staff.Add(j);
            }
            //var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).FirstOrDefaultAsync(staffmember => staffmember.Name.ToLower().Contains(name.ToLower()));
            return Ok(staff);
        }

        [HttpDelete("delete-by-id/{id}")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> DeleteStaffMember(int id)
        {
            var staffmember = await _context.Staff.FirstOrDefaultAsync(staffmember => staffmember.Id == id);

            _context.Staff.Remove(staffmember);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-all-staff")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> DeleteStaffMembers()
        {
            var players = await _context.Staff.ToListAsync();
            foreach (var i in players)
                _context.Staff.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("put-by-id/{id}")]
        [Authorize(Roles = "Owner")]

        public async Task<IActionResult> EditStaffMember(int id, StaffMemberPostModel model)
        {
            var staffmember = await _context.Staff.FirstOrDefaultAsync(staffmember => staffmember.Id == id);
            
            {
                return BadRequest($"The staff member with id {id} does not exist");
            }

            if(model.Name != null)
                staffmember.Name = model.Name;
            if (model.Role != null)
                staffmember.Role = model.Role;
            if (model.Birth_Date != null)
                staffmember.Birth_Date = model.Birth_Date;
            if (model.Email != null)
                staffmember.Email = model.Email;
            if (model.Phone_Number != null)
                staffmember.Phone_Number = model.Phone_Number;

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

            link = System.Web.HttpUtility.UrlDecode(link);

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

    }
}
