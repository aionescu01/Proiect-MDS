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
using System.Globalization;

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController: ControllerBase
    {
            private readonly AppDbContext _context;

            public ContractController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("add-staff")]
            public async Task<IActionResult> CreateStaffContracts(string Link)
            {
            var link = Link;
            var html = GetHtml(link);
            var data = ParseHtmlUsingHtmlAgilityPack3(html);

            foreach(var i in data)
            {
                var staffmember = await _context.Staff.FirstOrDefaultAsync(staffmember => staffmember.Name == i.name);
                var id = staffmember.Id;
                //CultureInfo invC = CultureInfo.InvariantCulture;
                CultureInfo frFr = new CultureInfo("en-US");
                DateTime d1;
                var startdate = i.start.Replace(".", "/");
                if (startdate is "unknown")
                    d1 = DateTime.MinValue;
                else
                    d1 = DateTime.Parse(startdate, frFr);
                DateTime d2;
                var enddate = i.end.Replace(".", "/");
                if (enddate is "unknown")
                    d2 = DateTime.MaxValue;
                else
                d2 = DateTime.Parse(enddate, frFr);
                var contract = new Contract()
                {
                    Start_date = d1,
                    End_date = d2,
                    StaffMemberId = id
                };
                await _context.Contracts.AddRangeAsync(contract);
                await _context.SaveChangesAsync();

            }
            return Ok();
        }

            [HttpPost("add-players")]
            public async Task<IActionResult> CreatePlayerContracts(string TransfermarktLink, string SalaryLink)
            {
            var link = SalaryLink;
            var html = GetHtml(link);
            var data = ParseHtmlUsingHtmlAgilityPack(html);
            link = TransfermarktLink;
            html = GetHtml(link);
            var data2 = ParseHtmlUsingHtmlAgilityPack2(html);
            foreach (var i in data)
            {
                foreach (var j in data2)
                {
                    if (j.name == i.name)
                    {
                        var player = await _context.Players.FirstOrDefaultAsync(player => player.Name == i.name);
                        var id = player.Id;
                        var sal = int.Parse(i.salary);
                        Contract contract;
                        //de schimbat aici
                        if (j.joined == "unknown" && j.end_date == "unknown")
                        {
                            contract = new Contract()
                            {
                                End_date = DateTime.MaxValue,
                                Salary = sal,
                                Agent = j.agent,
                                PlayerId = id
                            };
                        }
                        else if(j.joined != "unknown" && j.end_date == "unknown")
                        {
                                DateTime d1 = DateTime.Parse(j.joined);
                                contract = new Contract()
                                {
                                    Start_date = d1,
                                    End_date = DateTime.MaxValue,
                                    Salary = sal,
                                    Agent = j.agent,
                                    PlayerId = id

                                };
                        }
                        else if (j.joined == "unknown" && j.end_date != "unknown")
                        {
                            DateTime d2 = DateTime.Parse(j.end_date);
                            contract = new Contract()
                            {
                                Start_date = DateTime.MinValue,
                                End_date = d2,
                                Salary = sal,
                                Agent = j.agent,
                                PlayerId = id


                            };
                        }
                        else
                            //(j.joined != "unknown" && j.end_date != "unknown")
                        {
                            DateTime d1 = DateTime.Parse(j.joined);
                            DateTime d2 = DateTime.Parse(j.end_date);
                            contract = new Contract()
                            {
                                Start_date = d1,
                                End_date = d2,
                                Salary = sal,
                                Agent = j.agent,
                                PlayerId = id

                            };
                        }
                        //DateTime d1 = DateTime.Parse(j.joined);
                        //DateTime d2 = DateTime.Parse(j.end_date);
                        
                        
                        await _context.Contracts.AddRangeAsync(contract);
                        await _context.SaveChangesAsync();
                        //System.Console.WriteLine(i.name + " " + i.salary + " " + j.joined + " " + j.end_date);
                        break;
                    }
                }
                
            }
            return Ok();
        }

            [HttpPost("add-one-contract")]
            public async Task<IActionResult> CreateContract(ContractPostModel model)
            {

            var verif = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(verif => verif.Id == model.PlayerId);
            var verif2 = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(verif => verif.Id == model.StaffMemberId);
            var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.Id == model.PlayerId);
            var staffmember = await _context.Staff.Select(StaffMemberGetModel.Projection).FirstOrDefaultAsync(staffmember => staffmember.Id == model.StaffMemberId);


            if (verif != null)
            {
                if (verif.PlayerId != 0)
                    return BadRequest("The player already has a contract");
            }

            if (verif2 != null)
            {
                if (verif2.StaffMemberId != 0)
                    return BadRequest("The staff member already has a contract");
            }
            if (player == null && staffmember == null)
            {

                if (model.PlayerId != 0)
                    return BadRequest("The player does not exist");
                if (model.StaffMemberId != 0)
                    return BadRequest("The staff member does not exist");
            }

            Contract contract;
            if (model.PlayerId != 0)
                contract = new Contract()
                {
                    Start_date = model.Start_date,
                    End_date = model.End_date,
                    Salary = model.Salary,
                    Agent = model.Agent,
                    PlayerId = model.PlayerId,
                };
            else
            {
                contract = new Contract()
                {
                    Start_date = model.Start_date,
                    End_date = model.End_date,
                    Salary = model.Salary,
                    Agent = model.Agent,
                    StaffMemberId = model.StaffMemberId
                };
            }
                await _context.Contracts.AddRangeAsync(contract);
                await _context.SaveChangesAsync();

                return Ok();
            }

            [HttpGet]
            public async Task<IActionResult> GetContracts()
            {
                var contracts = await _context.Contracts.Select(ContractGetModel.Projection).ToListAsync();

                return Ok(contracts);
            }
            [HttpGet("get-by-name/{name}")]
            public async Task<IActionResult> GetContractById(string name)
            {


            List<PlayerGetModel> player = new List<PlayerGetModel>();
                var players = await _context.Players.Select(PlayerGetModel.Projection).ToListAsync();
                foreach (var i in players)
                {
                    if (i.Name.ToLower().Contains(name.ToLower()))
                        player.Add(i);
                }

            var staffs = await _context.Staff.Select(StaffMemberGetModel.Projection).ToListAsync();
            List<StaffMemberGetModel> staff = new List<StaffMemberGetModel>();
            foreach (var j in staffs)
            {
                if (j.Name.ToLower().Contains(name.ToLower()))
                    staff.Add(j);
            }
            //var player = await _context.Players.Select(PlayerGetModel.Projection).FirstOrDefaultAsync(player => player.Name.ToLower().Contains(name.ToLower()) );
            //var staff = await _context.Staff.Select(StaffMemberGetModel.Projection).FirstOrDefaultAsync(staff => staff.Name.ToLower().Contains(name.ToLower()));


            //ContractGetModel contract;
            /*
            if (player != null)
            {
                var playerid = player.Id;
                contract = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(contract => contract.PlayerId == playerid);           
            }
            else
            {
                var staffid = staff.Id;
                contract = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(contract => contract.StaffMemberId == staffid);
            }*/
            List<ContractGetModel> contract = new List<ContractGetModel>();
            if (player != null)
            {
                foreach(var i in player)
                { 
                    var playerid = i.Id;
                    var c = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(contract => contract.PlayerId == playerid);
                    if(c!=null)
                        contract.Add(c);
                }
            }
            if(staff!=null)
            {
                foreach (var i in staff)
                {
                    var staffid = i.Id;
                    var c = await _context.Contracts.Select(ContractGetModel.Projection).FirstOrDefaultAsync(contract => contract.StaffMemberId == staffid);
                    if (c != null)
                        contract.Add(c);
                }
            }
            return Ok(contract);
            }


        [HttpGet("Players salary statistics")]
        public async Task<IActionResult> GetContractsStats()
        {
            int sum = 0;
            int nrofplayers = 0;
            double avgsalary = 0;
            var contracts = await _context.Contracts.Select(ContractGetModel.Projection).ToListAsync();
            foreach (var i in contracts)
            {
                if (i.PlayerId != 0)
                {
                    nrofplayers++;
                    sum = sum + i.Salary;
                }
            }
            avgsalary = sum / nrofplayers;


            var stats = new SalaryStats()
            {
                sum = sum,
                nrofplayers = nrofplayers,
                avgsalary = avgsalary
            };
                return Ok(stats);
            }

            [HttpDelete]
            public async Task<IActionResult> DeleteContracts()
            {
                var contracts = await _context.Contracts.ToListAsync();
                foreach (var i in contracts)
                    _context.Contracts.Remove(i);
                await _context.SaveChangesAsync();

                return Ok();
            }
            [HttpDelete("delete-by-id/{id}")]
            public async Task<IActionResult> DeleteContract(int id)
            {
                var contract = await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == id);

                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();

                return Ok();
            }

            [HttpPut("put-by-id/{id}")]
            public async Task<IActionResult> EditContract(int id, ContractPostModel model)
            {
                var contract = await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == id);

                if (contract == null)
                {
                    return BadRequest($"The contract with id {id} does not exist");
                }

                
                if(model.Start_date != DateTime.MinValue)
                    contract.Start_date = model.Start_date;
                if (model.End_date != DateTime.MinValue)
                    contract.End_date = model.End_date;
                if (model.Salary != 0)
                    contract.Salary = model.Salary;
                if (model.Agent != null)
                    contract.Agent = model.Agent;

                if(model.PlayerId!=0)
                contract.PlayerId = model.PlayerId;
                if(model.StaffMemberId!=0)
                contract.StaffMemberId = model.StaffMemberId;

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
                var joined = repo.SelectSingleNode(".//td[5]").InnerText;
                var end_date = repo.SelectSingleNode(".//td[6]").InnerText;
                if (joined == "-")
                    joined = "unknown";
                if (end_date == "-")
                    end_date = "unknown";

                var agent = repo.SelectSingleNode(".//td[@class='rechts hauptlink']").InnerText;
                data.Add((name, joined, end_date, agent));
            }

            return data;
        }

        private static List<(string name, string start, string end)> ParseHtmlUsingHtmlAgilityPack3(string html)
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

                    if (start == "-")
                        start = "unknown";
                    if (end == "-")
                        end = "unknown";

                    data.Add((name, start, end));
                }
            }

            return data;
        }

    }


    
}
