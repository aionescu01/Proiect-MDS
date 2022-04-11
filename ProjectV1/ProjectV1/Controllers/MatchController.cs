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
    public class MatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchController(AppDbContext context)
        {
            _context = context;
        }



        [HttpPost("Add one match")]
        public async Task<IActionResult> CreateMatch(MatchPostModel model)
        {
            var match = new Match()
            {

                Opponent = model.Opponent,
                Competition = model.Competition,
                Event_date = model.Event_date,
                Score = model.Score,
                Referee = model.Referee,
            };

            await _context.Matches.AddRangeAsync(match);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var match = await _context.Matches.Select(MatchGetModel.Projection).ToListAsync();

            return Ok(match);
        }

        [HttpGet("get-by-opponent/{opponent}")]
        public async Task<IActionResult> GetMatchesByOpponent(string opponent)
        {
            var match = await _context.Matches.Select(MatchGetModel.Projection).FirstOrDefaultAsync(match => match.opponent == opponent);
            return Ok(match);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            foreach (var i in matches)
                _context.Matches.Remove(i);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeleteMatches(int id)
        {
            var match= await _context.Matches.FirstOrDefaultAsync(match => match.Id == id);

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
