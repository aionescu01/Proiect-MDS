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

namespace ProjectV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryStatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalaryStatsController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("Add one Salary Stats")]
        public async Task<IActionResult> CreateSalaryStats(SalaryStatsPostModel model)
        {
            var salaryStats = new SalaryStats()
            {
                sum = model.sum,
                nrofplayers = model.nrofplayers,
                avgsalary = model.avgsalary,
            };

            await _context.SalaryStats.AddRangeAsync(salaryStats);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetSalaryStats()
        {
            var salaryStats= await _context.SalaryStats.Select(SalaryStatsGetModel.Projection).ToListAsync();

            return Ok(salaryStats);
        }

        [HttpGet("get-by-nrofplayers")]
        public async Task<IActionResult> GetSalaryStatsByNrofplayers(int numar)
        {
            var salaryStats = await _context.SalaryStats.Select(SalaryStatsGetModel.Projection).FirstOrDefaultAsync( salaryStats=> salaryStats.nrofplayers == numar);
            return Ok(salaryStats);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSalaryStats()
        {
            var salaryStats = await _context.SalaryStats.ToListAsync();
            foreach (var i in salaryStats)
                _context.SalaryStats.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeleteSalaryStatsById(int id)
        {
            var salaryStats = await _context.SalaryStats.FirstOrDefaultAsync(salaryStats => salaryStats.id == id);

            _context.SalaryStats.Remove(salaryStats);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
