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
    public class MatchStaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchStaffController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("add-matchstaff")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> CreateMatchStaffRelation(MatchStaffPostModel model)
        {
            var mp = new MatchStaff()
            {
                MatchId = model.MatchId,
                StaffMemberId = model.StaffMemberId,
            };

            await _context.MatchStaffs.AddRangeAsync(mp);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-all-matchstaff")]
        public async Task<IActionResult> GetMatchStaffRelation()
        {
            var mp = await _context.MatchStaffs.Select(MatchStaffGetModel.Projection).ToListAsync();

            return Ok(mp);
        }

        [HttpGet("get-by-MatchId")]
        public async Task<IActionResult> GetMatchStaffByMatchId(int id)
        {
            var mp = await _context.MatchStaffs.Select(MatchStaffGetModel.Projection).FirstOrDefaultAsync(mp => mp.MatchId == id);
            return Ok(mp);
        }

        [HttpGet("get-by-StaffMemberId")]
        public async Task<IActionResult> GetMatchStaffByStaffMemberId(int id)
        {
            var mp = await _context.MatchStaffs.Select(MatchStaffGetModel.Projection).FirstOrDefaultAsync(mp => mp.StaffMemberId == id);
            return Ok(mp);
        }

        [HttpDelete("delete-all-matchstaff")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteMatchStaffRelation()
        {
            var mp = await _context.MatchStaffs.ToListAsync();
            foreach (var i in mp)
                _context.MatchStaffs.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-StaffMemberId/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteMatchPlayerByStaffMemberId(int id)
        {
            var md = await _context.MatchStaffs.FirstOrDefaultAsync(md => md.StaffMemberId == id);

            _context.MatchStaffs.Remove(md);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-MatchId/{id}")]
        [Authorize(Roles = "Owner, Manager")]

        public async Task<IActionResult> DeleteMatchStaffByMatchId(int id)
        {
            var md = await _context.MatchStaffs.FirstOrDefaultAsync(md => md.MatchId == id);

            _context.MatchStaffs.Remove(md);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}

